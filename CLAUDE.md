# CLAUDE.md

Runnable Unity samples for the Game Dev Patterns docs site (https://antoniucraciun.github.io/GameDevPatterns/). The site's source is the separate repo at `../GameDevPatterns`: read it, but don't edit it from a chat in this repo. If a sample shows that a docs page is wrong, say so in your summary so it can be fixed there.

Godot and Unreal sample repos will follow with the same layout, so keep names, folder structure and READMEs engine-neutral where that costs nothing.

## How we work

**The user writes the sample code. Claude guides.** Don't write or edit C# in sample folders (`Assets/Patterns`, `Assets/Recipes`, `Assets/_Shared`), scenes or prefabs unless the user explicitly asks for it in that chat. Repo tooling (`Tools~`, `Assets/_Tools`) is fine to change when asked.

When the user asks how to set up a pattern or recipe, answer with:

1. **The goal in one or two sentences**, from the docs page (read the page in `../GameDevPatterns/docs/`, don't paraphrase from memory).
2. **The scene for each version**: the smallest setup that shows the problem (objects, components, what is on screen), and what the player or reader does in Play Mode.
3. **The scripts for each version**: file names, class names, and what each one is responsible for. For WithPattern, name which types come straight from the page's `Unity (C#)` tab, and what glue the page leaves out. For WithoutPattern, describe the plain version someone would write first and where it should break.
4. **Folder setup**: the assembly definitions (names below), scene names, and any `_Shared` code it needs or should start.
5. **Packages and settings** it needs beyond what's installed, if any.
6. **What to compare** between the two versions, and **how to check it works**: what to look for in Play Mode, the Console or the Profiler.

Keep the guidance short and concrete, and don't paste the page's code back: point to it. When the user shares their code, review it against the docs page and these conventions: correctness first, then whether it still matches the page, then simplicity.

**Docs corrections.** While a pattern or recipe is being built, the user proofreads its docs page and tells you what needs to change. Collect those notes plus anything the sample revealed (Unity tab code that doesn't work as written, a missing pitfall). When the sample is done, send them to the docs session, currently **"Unity samples page modifications"** (find it with `ListAgents`, send with `SendMessage`): one message per page, each item marked *verified in the Editor* or *suggestion*, quoting the user's own words for their notes. Show the user the list before sending, and never edit `../GameDevPatterns` yourself.

Standing requests for every page, applied one page at a time as its sample is built (Game Loop first):

- **Description.** Each pattern and recipe page needs a short `## Overview` section (between the Category line and `## Problem`) so readers know what to expect. Include it in every page's corrections until all pages have one. Game Loop has one.
- **Sample link.** Once a sample is pushed to the samples repo's `main`, tell the docs session so it adds the `samples` entry (the page's "See it in action" link).
- **Sample differences.** When the sample's code differs from the page's Unity tab (for example installed from a scene component instead of at startup), ask for a short note on the page saying the sample differs and why, and say the same in the sample's README.
- **Unexplained code.** Flag types or lines in the Unity tab whose purpose isn't obvious (like an empty marker struct) and ask for a comment.
- **Engine docs links.** Suggest links to the official docs for the engine APIs the example uses, in that engine's Engine notes bullet. Use unversioned Unity URLs (`https://docs.unity3d.com/ScriptReference/...`, `.../Manual/...`) and check each one returns 200 before suggesting it. Godot and Unreal links are for the docs session to add.
- **Visualizations and diagrams.** Some pages need an interactive visualizer (`<…Viz />`, see the docs repo's contributing.md) or a diagram to make the idea clear. While working on a page, say whether it would benefit from one and what it should show, and include it in that page's corrections if the user agrees. Game Loop already has one.
- **Related concepts.** Suggest key terms the page should name, where they fit (for example "frame rate independence" on Game Loop).

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
  _Tools/Editor/                   repo tooling for the Editor (README Viewer), not sample code
Tools~/                            generator script (ignored by Unity)
```

- **README Viewer** (`Window > GameDevPatterns > README Viewer`): renders the README of the folder selected in the Project window, with working links. It handles the Markdown the generated READMEs use (headings, lists, quotes, tables, code blocks, bold, italic, inline code, links); keep READMEs within that subset.

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
- **A sample only runs in its own scene.** Every sample shares one project, so nothing may start globally: no `[RuntimeInitializeOnLoadMethod]` that installs systems, subscribes to events or creates objects, no PlayerLoop changes left behind, no `DontDestroyOnLoad` objects that outlive the sample. When a docs page installs something at startup, keep its install code but make it callable (`Install()` / `Uninstall()`), call it from a small component in the sample's scene (`OnEnable` / `OnDisable`), and add a comment saying a real game would install it at startup as the page shows. A `SubsystemRegistration` method that only resets statics is fine.
- On-screen debug text must be readable on large monitors: scale `OnGUI` panels with the screen height (see the Game Loop sample), or use UI Toolkit with a Panel Settings scale mode.
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
