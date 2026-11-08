# Culling

**Category:** Performance & Memory · **Site slug:** `culling`

> Skip rendering or simulating objects that cannot be seen or do not matter.

## What this sample will show

WithoutPattern builds a small feature the straightforward way and runs into the problem the page describes. WithPattern builds the same feature with Culling, so you can run both and compare them.

The problem, from the page:

> An open level has 5,000 objects, and the camera sees perhaps 300 of them. Without culling, the engine still processes the rest: draw calls for objects behind the camera, animation for characters inside closed buildings, particle systems in rooms nobody is in, AI for enemies a kilometre away.

## Where to begin

1. Read the pattern page: https://antoniucraciun.github.io/GameDevPatterns/patterns/culling
2. Open [WithoutPattern](WithoutPattern/README.md) to see the problem.
3. Open [WithPattern](WithPattern/README.md) to see the fix.

Status: not implemented yet.

