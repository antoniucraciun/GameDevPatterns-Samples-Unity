# CLAUDE.md

Runnable Unity samples for the Game Dev Patterns docs site (https://antoniucraciun.github.io/GameDevPatterns/). The site's source is the separate repo at `../GameDevPatterns`: read it, but don't edit it from a chat in this repo. If a sample shows that a docs page is wrong, say so in your summary so it can be fixed there.

Godot and Unreal sample repos will follow with the same layout, so keep names, folder structure and READMEs engine-neutral where that costs nothing.

## Project

- **Unity 6000.6.8f1**, URP, C# 9 (Unity's language version). Don't upgrade the editor in passing.
- Visible Meta Files and Force Text serialization (Unity 6 defaults). Never write `.meta` files by hand; let the Editor create them, and move or rename assets inside the Editor.
- Active Input Handling is **Both** (`activeInputHandler: 2`), because about 19 docs pages' Unity tabs read `UnityEngine.Input` (the old Input Manager) and the samples copy those tabs as written. Keep the input code the page uses; don't switch a sample to the other input API unless the page does.
- Binary assets (textures, audio, models, fonts) go through Git LFS; see `.gitattributes`.
- Don't install packages without asking (current ones are in `Packages/manifest.json`). When a sample needs a new package, name it (`com.unity.…`), say which samples need it, and ask first.

## Layout and naming

```
Assets/
  _Shared/                         code used by two or more samples (see its README)
  Patterns/<Category>/<Pattern>/   README.md, WithoutPattern/, WithPattern/
  Recipes/<Group>/<Recipe>/        README.md, WithoutPatterns/, WithPatterns/
Tools~/                            generator script (ignored by Unity)
```

- Folder names are the site's titles in PascalCase: `&` → `And`, `*` → `Star`, punctuation dropped, acronyms of three or more letters in Pascal case (`Fps`, `Npcs`, `Mvc`), two-letter ones upper case (`AI`, `UI`). Each README keeps the site slug, which is the real key.
- Don't create or rename sample folders by hand. They come from the docs repo's `catalog.ts` and `recipes.ts`:

  ```bash
  node "Tools~/generate-sample-folders.mjs"
  ```

  It only creates missing files, never overwrites an edited README, and reports folders that no longer match the catalog. See `Tools~/README.md`.
- Game Loop, Lazy Initialization, Debug Draw and Automated Playtesting have no "without" version; their `WithoutPattern` folder holds only a README saying why. Don't invent one.

## Once a sample has code

- **One assembly definition per `WithPattern` / `WithoutPattern` (or `WithPatterns` / `WithoutPatterns`) folder**, named and namespaced `GameDevPatterns.<Patterns|Recipes>.<Folder>.<WithPattern|WithoutPattern>`, e.g. `GameDevPatterns.Patterns.FixedTimestep.WithPattern`. If a folder name starts with a digit, prefix that segment with `_` (`_1000EnemiesAt60Fps`).
- The two versions never reference each other. Both may reference `GameDevPatterns.Shared`. A recipe's WithPatterns version doesn't reference the pattern samples either: it contains its own copy of what it uses, as the recipe page does.
- One scene per version, inside its folder, named after the folder: `FixedTimestepWithout.unity`, `FixedTimestepWith.unity`.
- **Match the docs.** Where the page has a `Unity (C#)` tab, the WithPattern code starts from that code: same type names, same members, same comments where they still apply. Add only the scene glue the page leaves out. If the tab's code doesn't work as written, keep the fix small, mark it with a comment, and list it in your summary as a docs correction. Where the page has no Unity tab, follow its Pseudocode section.
- Both versions build the same feature, so the only difference a reader sees is the pattern. The "without" version is the code someone would reasonably write first, not a strawman.
- When a sample is done, change its README's `Status:` line to say what it contains and which Unity version it was verified in.

## Keep it simple

Follow the docs site's [Keep It Simple](https://antoniucraciun.github.io/GameDevPatterns/keep-it-simple) page in the samples themselves:

- The smallest scene that shows the problem: primitives, a few lines of on-screen text, no art pipeline.
- No frameworks, base classes or shared abstractions "for later". Move code to `_Shared` only when a second sample needs it.
- No interfaces with one implementation unless the docs page shows one.
- Plain `MonoBehaviour`s and serialized fields unless the pattern is about something else.

## Reporting what was verified

End each piece of work with an explicit list, separated into what was checked and how:

- **Compiled in the Editor**: the Console showed no errors or warnings after a domain reload (say which assemblies).
- **Ran in Play Mode**: which scene, what you did, what you observed (numbers, logs, behaviour).
- **Tests**: which Test Runner tests ran and passed (Edit Mode or Play Mode).
- **Not verified**: everything else, stated plainly, e.g. "didn't open the scene", "only compiled with `dotnet build`, not in the Editor", "didn't check the Profiler".

Never call a sample verified on the strength of an outside compile alone. If you couldn't drive the Editor, say so and list the steps for the user to check.
