# GameDevPatterns Samples (Unity)

Runnable Unity samples for **[Game Dev Patterns](https://antoniucraciun.github.io/GameDevPatterns/)**, a catalog of programming patterns for game development. Each sample shows a feature written the plain way first, then the same feature with the pattern, so you can run both and see what the pattern actually changes.

> **Status:** the folders and READMEs are in place; no sample is implemented yet. Each sample's README says whether it is.

Godot and Unreal versions of this repo will follow, with the same folder layout.

## How the folders map to the site

| On the site | In this repo |
| --- | --- |
| Pattern category, e.g. *Game Loop & Timing* | `Assets/Patterns/GameLoopAndTiming/` |
| Pattern page `<site>/patterns/fixed-timestep` | `Assets/Patterns/GameLoopAndTiming/FixedTimestep/` |
| Recipe group, e.g. *Combat & Game Feel* | `Assets/Recipes/CombatAndGameFeel/` |
| Recipe page `<site>/recipes/boss-fight` | `Assets/Recipes/CombatAndGameFeel/BossFight/` |

Folder names are the site's titles in PascalCase ("A* Pathfinding" → `AStarPathfinding`, "1,000 Enemies at 60 FPS" → `1000EnemiesAt60Fps`). Each sample's README gives its site slug and a link to its page.

Inside every sample:

```
FixedTimestep/
  README.md          what the sample shows, link to the docs page, status
  WithoutPattern/    the straightforward version, and the problem it runs into
  WithPattern/       the same feature, solved with the pattern
```

Recipes use `WithoutPatterns/` and `WithPatterns/`, because a recipe combines several patterns. A few patterns have no meaningful "without" version (Game Loop, Lazy Initialization, Debug Draw, Automated Playtesting); their `WithoutPattern` README says why.

`Assets/_Shared/` holds code that two or more samples use. `Tools~/` holds the script that generates the folders from the docs site's catalog (Unity doesn't import folders ending in `~`).

## Opening a sample

1. Install **Unity 6000.6.8f1** (Unity 6.6) with Unity Hub, and add this folder as a project. If you clone with Git, install [Git LFS](https://git-lfs.com) first: textures, audio, models and fonts are stored with LFS.
2. Read the sample's page on the site (linked from its README).
3. In the Project window, open the sample's `WithoutPattern` folder and its scene, press Play, and look for the problem the page describes.
4. Do the same in `WithPattern`, and compare the two scripts side by side.

## Contributing

Corrections are welcome, especially when you have run a sample and found that it disagrees with its docs page. Conventions for this repo are in [CLAUDE.md](CLAUDE.md); the docs site has its own [contributing guide](https://antoniucraciun.github.io/GameDevPatterns/contributing).
