# Timer / Scheduler

**Category:** Game Loop & Timing · **Site slug:** `timer-scheduler`

> Centralise delayed and repeating callbacks instead of counting down in every object.

## What this sample will show

WithoutPattern builds a small feature the straightforward way and runs into the problem the page describes. WithPattern builds the same feature with Timer / Scheduler, so you can run both and compare them.

The problem, from the page:

> Every object counts down its own timers in its per-frame update: `cooldown -= dt; if (cooldown <= 0) ...`. A trap fires every 3 seconds, a buff expires after 10, a door closes after 5, a respawn happens after 8.

## Where to begin

1. Read the pattern page: https://antoniucraciun.github.io/GameDevPatterns/patterns/timer-scheduler
2. Open [WithoutPattern](WithoutPattern/README.md) to see the problem.
3. Open [WithPattern](WithPattern/README.md) to see the fix.

Status: not implemented yet.

