# Observer

**Category:** Behavioral & Communication · **Site slug:** `observer`

> Let objects announce that something happened without knowing who is listening.

## What this sample will show

WithoutPattern builds a small feature the straightforward way and runs into the problem the page describes. WithPattern builds the same feature with Observer, so you can run both and compare them.

The problem, from the page:

> When the player dies, the HUD must update, a sound plays, the achievement system checks "die 100 times", analytics logs it and the music changes. If `Player.Die()` calls all of these directly, the player class depends on audio, UI, achievements and analytics.

## Where to begin

1. Read the pattern page: https://antoniucraciun.github.io/GameDevPatterns/patterns/observer
2. Open [WithoutPattern](WithoutPattern/README.md) to see the problem.
3. Open [WithPattern](WithPattern/README.md) to see the fix.

Status: not implemented yet.

