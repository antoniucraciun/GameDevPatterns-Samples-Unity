# Factory

**Category:** Architecture & Structure · **Site slug:** `factory`

> Centralise object creation so callers ask for "an enemy" rather than constructing one.

## What this sample will show

WithoutPattern builds a small feature the straightforward way and runs into the problem the page describes. WithPattern builds the same feature with Factory, so you can run both and compare them.

The problem, from the page:

> Creating an enemy properly takes more than instancing a template (`Instantiate`, `instantiate()`, `SpawnActor`). The template must be looked up, an instance taken from a pool, stats applied from its type, services injected, it must be registered with the AI director and the minimap, and it must be scaled for difficulty.

## Where to begin

1. Read the pattern page: https://antoniucraciun.github.io/GameDevPatterns/patterns/factory
2. Open [WithoutPattern](WithoutPattern/README.md) to see the problem.
3. Open [WithPattern](WithPattern/README.md) to see the fix.

Status: not implemented yet.

