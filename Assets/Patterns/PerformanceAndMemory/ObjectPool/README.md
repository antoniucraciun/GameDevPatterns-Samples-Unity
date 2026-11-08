# Object Pool

**Category:** Performance & Memory · **Site slug:** `object-pool`

> Reuse objects from a fixed pool instead of allocating and destroying them.

## What this sample will show

WithoutPattern builds a small feature the straightforward way and runs into the problem the page describes. WithPattern builds the same feature with Object Pool, so you can run both and compare them.

The problem, from the page:

> A bullet-hell game fires hundreds of projectiles per second. Each spawn allocates memory and runs initialisation, and each destroy frees it again or leaves garbage for the collector.

## Where to begin

1. Read the pattern page: https://antoniucraciun.github.io/GameDevPatterns/patterns/object-pool
2. Open [WithoutPattern](WithoutPattern/README.md) to see the problem.
3. Open [WithPattern](WithPattern/README.md) to see the fix.

Status: not implemented yet.

