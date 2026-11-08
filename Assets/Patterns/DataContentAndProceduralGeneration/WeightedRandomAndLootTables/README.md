# Weighted Random & Loot Tables

**Category:** Data, Content & Procedural Generation · **Site slug:** `weighted-random`

> Pick outcomes by weight, with pity timers and shuffle bags to control streaks.

## What this sample will show

WithoutPattern builds a small feature the straightforward way and runs into the problem the page describes. WithPattern builds the same feature with Weighted Random & Loot Tables, so you can run both and compare them.

The problem, from the page:

> A chest should drop common items often, rare items sometimes and legendaries almost never. Hard-coding this as `if (rand() < 0.01) ... else if (rand() < 0.1) ...` is wrong (each branch rolls again) and impossible for designers to tune.

## Where to begin

1. Read the pattern page: https://antoniucraciun.github.io/GameDevPatterns/patterns/weighted-random
2. Open [WithoutPattern](WithoutPattern/README.md) to see the problem.
3. Open [WithPattern](WithPattern/README.md) to see the fix.

Status: not implemented yet.

