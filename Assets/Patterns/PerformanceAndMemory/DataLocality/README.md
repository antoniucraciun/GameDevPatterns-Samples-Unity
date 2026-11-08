# Data Locality

**Category:** Performance & Memory · **Site slug:** `data-locality`

> Lay data out contiguously in the order it is processed to keep the CPU cache warm.

## What this sample will show

WithoutPattern builds a small feature the straightforward way and runs into the problem the page describes. WithPattern builds the same feature with Data Locality, so you can run both and compare them.

The problem, from the page:

> You profile a loop that updates 10,000 particles. The math per particle is trivial, yet the loop is slow.

## Where to begin

1. Read the pattern page: https://antoniucraciun.github.io/GameDevPatterns/patterns/data-locality
2. Open [WithoutPattern](WithoutPattern/README.md) to see the problem.
3. Open [WithPattern](WithPattern/README.md) to see the fix.

Status: not implemented yet.

