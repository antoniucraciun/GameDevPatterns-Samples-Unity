# Scene / Level Management

**Category:** Architecture & Structure · **Site slug:** `scene-management`

> Load, unload and transition between levels, including additive and streamed scenes.

## What this sample will show

WithoutPattern builds a small feature the straightforward way and runs into the problem the page describes. WithPattern builds the same feature with Scene / Level Management, so you can run both and compare them.

The problem, from the page:

> Each level is one scene that contains everything: the player, the HUD, the camera rig, the audio manager and the level geometry. Loading level 2 by replacing the whole scene (`SceneManager.LoadScene`, `change_scene_to_file()`, `OpenLevel`) destroys all of it and recreates it, so music restarts, the HUD flashes, managers are duplicated or lost, and the game freezes for two seconds with no loading screen.

## Where to begin

1. Read the pattern page: https://antoniucraciun.github.io/GameDevPatterns/patterns/scene-management
2. Open [WithoutPattern](WithoutPattern/README.md) to see the problem.
3. Open [WithPattern](WithPattern/README.md) to see the fix.

Status: not implemented yet.

