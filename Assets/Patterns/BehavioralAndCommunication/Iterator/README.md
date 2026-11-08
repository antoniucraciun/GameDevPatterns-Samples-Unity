# Iterator

**Category:** Behavioral & Communication · **Site slug:** `iterator`

> Walk a collection (inventory, spatial grid, scene tree) without exposing how it is stored.

## What this sample will show

WithoutPattern builds a small feature the straightforward way and runs into the problem the page describes. WithPattern builds the same feature with Iterator, so you can run both and compare them.

The problem, from the page:

> The game keeps the last 32 hits the player took in a fixed-size array that wraps around: a ring buffer, so recording a hit never allocates. Three systems read it: the death recap screen ("killed by: Goblin Archer, 3 hits for 87"), the damage-direction indicators on the HUD, and the lag-compensation history on the server.

## Where to begin

1. Read the pattern page: https://antoniucraciun.github.io/GameDevPatterns/patterns/iterator
2. Open [WithoutPattern](WithoutPattern/README.md) to see the problem.
3. Open [WithPattern](WithPattern/README.md) to see the fix.

Status: not implemented yet.

