# Spatial Partition

**Category:** Performance & Memory · **Site slug:** `spatial-partition`

> Index objects by position so neighbour queries do not check everything.

## What this sample will show

WithoutPattern builds a small feature the straightforward way and runs into the problem the page describes. WithPattern builds the same feature with Spatial Partition, so you can run both and compare them.

The problem, from the page:

> Five hundred units each need to find enemies within 10 metres. Checking every unit against every other is 250,000 distance checks per frame, O(n²).

## Where to begin

1. Read the pattern page: https://antoniucraciun.github.io/GameDevPatterns/patterns/spatial-partition
2. Open [WithoutPattern](WithoutPattern/README.md) to see the problem.
3. Open [WithPattern](WithPattern/README.md) to see the fix.

Status: not implemented yet.

