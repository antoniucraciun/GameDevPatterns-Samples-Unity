# Bootstrapper

**Category:** Architecture & Structure · **Site slug:** `bootstrapper`

> One entry point initialises core services in a known order before gameplay starts.

## What this sample will show

WithoutPattern builds a small feature the straightforward way and runs into the problem the page describes. WithPattern builds the same feature with Bootstrapper, so you can run both and compare them.

The problem, from the page:

> Services initialise themselves in their own startup callbacks (`Awake`, `_ready`, `BeginPlay`): the audio manager, the save system, analytics, input, the platform SDK. Their order depends on the order in which the engine creates objects, so it differs between scenes and between editor and build.

## Where to begin

1. Read the pattern page: https://antoniucraciun.github.io/GameDevPatterns/patterns/bootstrapper
2. Open [WithoutPattern](WithoutPattern/README.md) to see the problem.
3. Open [WithPattern](WithPattern/README.md) to see the fix.

Status: not implemented yet.

