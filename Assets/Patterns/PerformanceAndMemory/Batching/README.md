# Batching

**Category:** Performance & Memory · **Site slug:** `batching`

> Group many small operations (draw calls, raycasts, writes) into one larger one.

## What this sample will show

WithoutPattern builds a small feature the straightforward way and runs into the problem the page describes. WithPattern builds the same feature with Batching, so you can run both and compare them.

The problem, from the page:

> Each small operation has a fixed overhead that has nothing to do with the work itself. Drawing one rock costs a draw call: the CPU sets up state, validates and submits to the GPU, and 3,000 rocks cost 3,000 of those even though each is tiny. 500 agents each casting a ray pay the per-call cost 500 times, on the main thread, one after another.

## Where to begin

1. Read the pattern page: https://antoniucraciun.github.io/GameDevPatterns/patterns/batching
2. Open [WithoutPattern](WithoutPattern/README.md) to see the problem.
3. Open [WithPattern](WithPattern/README.md) to see the fix.

Status: not implemented yet.

