# Coroutines

**Category:** Game Loop & Timing · **Site slug:** `coroutines`

> Write multi-frame sequences as straight-line code that yields between frames.

## What this sample will show

WithoutPattern builds a small feature the straightforward way and runs into the problem the page describes. WithPattern builds the same feature with Coroutines, so you can run both and compare them.

The problem, from the page:

> A door opens over two seconds, waits, plays a sound, then closes. Written in `Update`, it becomes a state machine of flags and timers: `if (opening) { t += dt; if (t > 2) { opening = false; waiting = true; t = 0; } } else if (waiting) ...`.

## Where to begin

1. Read the pattern page: https://antoniucraciun.github.io/GameDevPatterns/patterns/coroutines
2. Open [WithoutPattern](WithoutPattern/README.md) to see the problem.
3. Open [WithPattern](WithPattern/README.md) to see the fix.

Status: not implemented yet.

