# Event Queue

**Category:** Behavioral & Communication · **Site slug:** `event-queue`

> Decouple when a message is sent from when it is processed.

## What this sample will show

WithoutPattern builds a small feature the straightforward way and runs into the problem the page describes. WithPattern builds the same feature with Event Queue, so you can run both and compare them.

The problem, from the page:

> An explosion damages twelve enemies in one frame, and each one requests its death sound, so twelve identical sounds play at once, clipping the mix. A damage handler raises an event whose handler deals damage, which raises the event again, and the call stack recurses.

## Where to begin

1. Read the pattern page: https://antoniucraciun.github.io/GameDevPatterns/patterns/event-queue
2. Open [WithoutPattern](WithoutPattern/README.md) to see the problem.
3. Open [WithPattern](WithPattern/README.md) to see the fix.

Status: not implemented yet.

