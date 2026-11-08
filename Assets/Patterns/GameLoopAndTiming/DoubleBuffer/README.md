# Double Buffer

**Category:** Game Loop & Timing · **Site slug:** `double-buffer`

> Keep a current and a next state so readers never see a half-written update.

## What this sample will show

WithoutPattern builds a small feature the straightforward way and runs into the problem the page describes. WithPattern builds the same feature with Double Buffer, so you can run both and compare them.

The problem, from the page:

> A cellular simulation (Game of Life, fire spreading, fluid on a grid) updates each cell from its neighbours. If you update the grid in place, cell 5 reads cell 4 *after* cell 4 has already changed this tick, but cell 3 reads cell 4 *before*.

## Where to begin

1. Read the pattern page: https://antoniucraciun.github.io/GameDevPatterns/patterns/double-buffer
2. Open [WithoutPattern](WithoutPattern/README.md) to see the problem.
3. Open [WithPattern](WithPattern/README.md) to see the fix.

Status: not implemented yet.

