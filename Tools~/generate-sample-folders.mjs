// Creates the sample folders and their READMEs from the docs site's catalog, so names and links
// can't drift from GameDevPatterns.
//
//   node "Tools~/generate-sample-folders.mjs"                      # docs repo at ../GameDevPatterns
//   node "Tools~/generate-sample-folders.mjs" --docs <path>        # or set GAMEDEVPATTERNS_DOCS
//   node "Tools~/generate-sample-folders.mjs" --dry-run            # report only, write nothing
//
// Reads docs/.vitepress/catalog.ts, recipes.ts and config.mts (for `base`), plus the first
// sentences of each page's Problem / Solution sections. Safe to rerun: it only writes files that
// don't exist yet, lists the ones it skipped, and never deletes anything. Folders that no longer
// match the catalog (a renamed or removed pattern) are reported so you can move them by hand.
//
// No dependencies; needs Node 18+. Engine-neutral: only `engine` below is Unity-specific, so the
// Godot and Unreal sample repos can reuse this script with their own root folder.
import { existsSync, mkdirSync, readFileSync, readdirSync, writeFileSync } from 'node:fs'
import { dirname, join, relative, resolve } from 'node:path'
import { fileURLToPath } from 'node:url'

const engine = { name: 'Unity', root: 'Assets' }
const siteHost = 'https://antoniucraciun.github.io'

// Patterns where a "without" version would be invented rather than shown. Their WithoutPattern
// README says why instead. Keep this list short; most patterns have a real "before".
const noWithout = {
  'game-loop': 'Unity owns the game loop (the PlayerLoop), so there is no plain Unity version without it. The sample only shows how to add your own step to the loop Unity already runs.',
  'lazy-initialization': 'the "without" version is the same code run eagerly in Awake, and the difference only shows up as startup time in the Profiler. WithPattern logs when each resource is created instead.',
  'debug-draw': 'without it you simply can\'t see the state; there is no feature to build the "plain" way. WithPattern draws a component\'s invisible settings (ranges, cones, paths) as Gizmos.',
  'automated-playtesting': 'the alternative is a person replaying the game by hand, which a sample can\'t show. WithPattern is a Play Mode smoke test that loads each sample scene and fails on errors.',
}

// ── Arguments ──────────────────────────────────────────────────────────
const args = process.argv.slice(2)
const dryRun = args.includes('--dry-run')
const repoRoot = resolve(dirname(fileURLToPath(import.meta.url)), '..')
const docsArg = args.includes('--docs') ? args[args.indexOf('--docs') + 1] : process.env.GAMEDEVPATTERNS_DOCS
const docsRepo = resolve(repoRoot, docsArg ?? '../GameDevPatterns')
const vp = join(docsRepo, 'docs/.vitepress')
if (!existsSync(join(vp, 'catalog.ts'))) {
  console.error(`Can't find ${join(vp, 'catalog.ts')}. Pass --docs <path to the GameDevPatterns repo>.`)
  process.exit(1)
}

// ── Load the TypeScript data files ─────────────────────────────────────
// They only hold interfaces and object literals, so dropping the interfaces and the type
// annotations on `export const` is enough to import them as plain JavaScript.
async function loadTs(file) {
  const js = readFileSync(file, 'utf8')
    .replace(/export interface \w+ \{[\s\S]*?\n\}/g, '')
    .replace(/(export const \w+)\s*:\s*[\w[\]<>, ]+\s*=/g, '$1 =')
  return import('data:text/javascript;base64,' + Buffer.from(js).toString('base64'))
}
const { categories, patterns } = await loadTs(join(vp, 'catalog.ts'))
const { recipeGroups, recipes } = await loadTs(join(vp, 'recipes.ts'))
const base = readFileSync(join(vp, 'config.mts'), 'utf8').match(/base:\s*'([^']+)'/)?.[1] ?? '/'
const site = siteHost + base
const pageUrl = (section, slug) => `${site}${section}/${slug}`

// ── Names ──────────────────────────────────────────────────────────────
// "Game Loop & Timing" → GameLoopAndTiming, "A* Pathfinding" → AStarPathfinding.
// Acronyms follow .NET naming: two letters stay upper case (AI, UI), longer ones are Pascal
// case (FPS → Fps, NPCs → Npcs, MVC → Mvc).
function pascal(title) {
  return title
    .replace(/&/g, ' And ')
    .replace(/\*/g, ' Star ')
    .replace(/(\d),(\d)/g, '$1$2')
    .split(/[^A-Za-z0-9]+/)
    .filter(Boolean)
    .map((w) => {
      if (/^[A-Z]{3,}s?$/.test(w)) w = w[0] + w.slice(1).toLowerCase()
      return w[0].toUpperCase() + w.slice(1)
    })
    .join('')
}

// ── Page text ──────────────────────────────────────────────────────────
function section(section, slug, heading) {
  const file = join(docsRepo, 'docs', section, `${slug}.md`)
  if (!existsSync(file)) return ''
  const md = readFileSync(file, 'utf8').replace(/\r\n/g, '\n')
  const m = md.match(new RegExp(`^## ${heading}\\s*\\n([\\s\\S]*?)(?=^## |(?![\\s\\S]))`, 'm'))
  if (!m) return ''
  const para = m[1].split(/\n\s*\n/).map((p) => p.trim()).find((p) => p && !/^([<|>!-]|```)/.test(p)) // skip HTML, tables, quotes, lists, code fences ?? ''
  return para
    .replace(/\[([^\]]+)\]\([^)]+\)/g, '$1') // links → their text (site-relative links break here)
    .replace(/\s+/g, ' ')
    .trim()
}
function sentences(text, n) {
  const parts = text.split(/(?<=[.!?])\s+(?=[A-Z`*"(])/)
  return parts.slice(0, n).join(' ')
}

// ── Output ─────────────────────────────────────────────────────────────
const created = []
const skipped = []
function write(path, content) {
  const rel = relative(repoRoot, path).replace(/\\/g, '/')
  if (existsSync(path)) return skipped.push(rel)
  created.push(rel)
  if (dryRun) return
  mkdirSync(dirname(path), { recursive: true })
  writeFileSync(path, content.replace(/\n{3,}/g, '\n\n').trimStart() + '\n')
}
const status = 'Status: not implemented yet.'

// Folder names must be unique, or two pages would share a folder.
function checkUnique(items, what) {
  const seen = new Map()
  for (const [name, id] of items) {
    if (seen.has(name)) throw new Error(`${what} "${id}" and "${seen.get(name)}" both map to the folder ${name}`)
    seen.set(name, id)
  }
}
checkUnique(categories.map((c) => [pascal(c.title), c.id]), 'Category')
checkUnique(recipeGroups.map((g) => [pascal(g.title), g.id]), 'Recipe group')
checkUnique(patterns.map((p) => [pascal(p.name), p.slug]), 'Pattern')
checkUnique(recipes.map((r) => [pascal(r.name), r.slug]), 'Recipe')

const patternsRoot = join(repoRoot, engine.root, 'Patterns')
const recipesRoot = join(repoRoot, engine.root, 'Recipes')
const categoryOf = new Map(categories.map((c) => [c.id, c]))
const groupOf = new Map(recipeGroups.map((g) => [g.id, g]))
const patternBySlug = new Map(patterns.map((p) => [p.slug, p]))
const patternDir = (p) => join(patternsRoot, pascal(categoryOf.get(p.category).title), pascal(p.name))

// ── Patterns ───────────────────────────────────────────────────────────
for (const p of patterns) {
  const cat = categoryOf.get(p.category)
  const dir = patternDir(p)
  const problem = sentences(section('patterns', p.slug, 'Problem'), 2)
  let solution = sentences(section('patterns', p.slug, 'Solution'), 2)
  // Leave it out when it only introduces a list ("Two small pieces:") that isn't quoted here.
  if (/:$/.test(solution) || solution.length < 40) solution = ''
  const skip = noWithout[p.slug]

  const shows = skip
    ? `There is no "without" version of this one: ${skip}`
    : `WithoutPattern builds a small feature the straightforward way and runs into the problem the page describes. WithPattern builds the same feature with ${p.name}, so you can run both and compare them.${problem ? `

The problem, from the page:

> ${problem}` : ''}`

  write(join(dir, 'README.md'), `
# ${p.name}

**Category:** ${cat.title} · **Site slug:** \`${p.slug}\`

> ${p.summary}

## What this sample will show

${shows}

## Where to begin

1. Read the pattern page: ${pageUrl('patterns', p.slug)}
2. ${skip ? 'Read [WithoutPattern](WithoutPattern/README.md) for why there is no "without" version.' : 'Open [WithoutPattern](WithoutPattern/README.md) to see the problem.'}
3. Open [WithPattern](WithPattern/README.md) to see ${skip ? 'the pattern' : 'the fix'}.

${status}
`)

  write(join(dir, 'WithoutPattern/README.md'), skip ? `
# ${p.name}: without the pattern

No "without" version for this pattern: ${skip}

This folder will hold no code. It exists so every pattern has the same layout.
` : `
# ${p.name}: without the pattern

The feature written the way you would reasonably write it first, with no ${p.name}. It is not a strawman: it works, until it hits the problem on the [pattern page](${pageUrl('patterns', p.slug)}).

Compare it with [WithPattern](../WithPattern/README.md): run both the same way, note where the behaviour differs, then look at which code had to change.

${status}
`)

  write(join(dir, 'WithPattern/README.md'), `
# ${p.name}: with the pattern

The same feature built with ${p.name}.${solution ? ` The idea, from the page: ${solution}` : ''}

${skip ? `There is nothing to compare it with here; see [WithoutPattern](../WithoutPattern/README.md) for why.` : `Compare it with [WithoutPattern](../WithoutPattern/README.md): the behaviour that broke there should hold here, at the cost of the extra types the pattern adds.`}

${status}
`)
}

const count = (n) => ['zero', 'one', 'two', 'three', 'four', 'five', 'six', 'seven', 'eight', 'nine'][n] ?? String(n)

// ── Recipes ────────────────────────────────────────────────────────────
for (const r of recipes) {
  const group = groupOf.get(r.group)
  const dir = join(recipesRoot, pascal(group.title), pascal(r.name))
  const rows = r.patterns.map((slug) => {
    const p = patternBySlug.get(slug)
    const link = relative(dir, join(patternDir(p), 'README.md')).replace(/\\/g, '/')
    return `| ${p.name} | [site page](${pageUrl('patterns', slug)}) | [folder](${link}) |`
  })

  write(join(dir, 'README.md'), `
# ${r.name}

**Group:** ${group.title} · **Site slug:** \`${r.slug}\`

> ${r.summary}

## What this sample will show

WithoutPatterns builds this feature directly, with plain scripts and no patterns, the way you would build it first. WithPatterns builds the same feature with the ${count(r.patterns.length)} patterns below working together, as the recipe page describes, so you can see where the plain version strains and what each pattern takes over.

## Patterns used

| Pattern | Docs | Sample in this repo |
| --- | --- | --- |
${rows.join('\n')}

## Where to begin

1. Read the recipe page: ${pageUrl('recipes', r.slug)}
2. Open [WithoutPatterns](WithoutPatterns/README.md) to see the plain version and where it strains.
3. Open [WithPatterns](WithPatterns/README.md) to see the same feature built from the patterns.

${status}
`)

  write(join(dir, 'WithoutPatterns/README.md'), `
# ${r.name}: without patterns

The feature built directly, with plain MonoBehaviours, direct references and no patterns. This is the version the [Keep It Simple](${site}keep-it-simple) page says to build first, so it should work and stay readable at small scale.

Compare it with [WithPatterns](../WithPatterns/README.md): count the places you would have to edit to add one more case, and look for the bugs the recipe page warns about.

${status}
`)

  write(join(dir, 'WithPatterns/README.md'), `
# ${r.name}: with patterns

The same feature built from ${r.patterns.map((s) => patternBySlug.get(s).name).join(', ')}, wired together as the [recipe page](${pageUrl('recipes', r.slug)}) describes.

Compare it with [WithoutPatterns](../WithoutPatterns/README.md): the behaviour should match, and each pattern should replace a specific piece of the plain version.

${status}
`)
}

// ── Report ─────────────────────────────────────────────────────────────
function orphans(root, expected) {
  if (!existsSync(root)) return []
  const out = []
  for (const top of readdirSync(root, { withFileTypes: true }).filter((d) => d.isDirectory())) {
    const tops = expected.get(top.name)
    if (!tops) { out.push(`${top.name}/`); continue }
    for (const sub of readdirSync(join(root, top.name), { withFileTypes: true }).filter((d) => d.isDirectory()))
      if (!tops.has(sub.name)) out.push(`${top.name}/${sub.name}/`)
  }
  return out
}
function expectedTree(groups, items, groupKey, titleOf) {
  const map = new Map(groups.map((g) => [pascal(g.title), new Set()]))
  for (const it of items) map.get(pascal(titleOf(it[groupKey]).title)).add(pascal(it.name))
  return map
}
const stray = [
  ...orphans(patternsRoot, expectedTree(categories, patterns, 'category', (id) => categoryOf.get(id))).map((s) => `${engine.root}/Patterns/${s}`),
  ...orphans(recipesRoot, expectedTree(recipeGroups, recipes, 'group', (id) => groupOf.get(id))).map((s) => `${engine.root}/Recipes/${s}`),
]

console.log(`${dryRun ? '[dry run] ' : ''}${patterns.length} patterns in ${categories.length} categories, ${recipes.length} recipes in ${recipeGroups.length} groups.`)
console.log(`${dryRun ? 'Would create' : 'Created'} ${created.length} file(s).`)
if (skipped.length) {
  console.log(`Skipped ${skipped.length} file(s) that already exist (never overwritten):`)
  for (const s of skipped) console.log(`  ${s}`)
}
if (stray.length) {
  console.log(`Folders not in the catalog any more (renamed or removed? move them by hand):`)
  for (const s of stray) console.log(`  ${s}`)
}
