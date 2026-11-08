# Tools~

Unity ignores folders whose names end in `~`, so nothing here is imported into the project.

## generate-sample-folders.mjs

Creates `Assets/Patterns/<Category>/<Pattern>/` and `Assets/Recipes/<Group>/<Recipe>/`, each with a README and the `WithoutPattern(s)` / `WithPattern(s)` folders, from the docs repo's `catalog.ts` and `recipes.ts`.

Rerun it whenever a pattern or recipe is added to the docs site (needs Node 18+, no `npm install`):

```bash
node "Tools~/generate-sample-folders.mjs" --dry-run
node "Tools~/generate-sample-folders.mjs"
```

- The docs repo is expected at `../GameDevPatterns`. Otherwise pass `--docs <path>` or set `GAMEDEVPATTERNS_DOCS`.
- It only creates files that don't exist yet, so READMEs you have edited are never overwritten. It lists the files it skipped.
- It never deletes or moves anything. If a pattern was renamed on the site, it reports the old folder as "not in the catalog any more"; move its contents by hand (in the Unity Editor, so the `.meta` files move with them).
- To regenerate one README from scratch, delete it and rerun.
- Switch to Unity afterwards (or open the project) so it creates the `.meta` files for the new folders.
