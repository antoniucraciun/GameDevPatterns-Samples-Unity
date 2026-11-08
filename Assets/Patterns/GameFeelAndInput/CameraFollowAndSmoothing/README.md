# Camera Follow & Smoothing

**Category:** Game Feel & Input · **Site slug:** `camera-smoothing`

> Dead zones, look-ahead and damping to keep the camera readable.

## What this sample will show

WithoutPattern builds a small feature the straightforward way and runs into the problem the page describes. WithPattern builds the same feature with Camera Follow & Smoothing, so you can run both and compare them.

The problem, from the page:

> `camera.position = player.position + offset` produces a camera glued to the player: every tiny movement and jump bob jerks the whole screen. Smoothing it with `Lerp(camera, target, 0.1f)` behaves differently at 30 and 144 FPS, and lags so far behind a sprinting player that they run off-screen.

## Where to begin

1. Read the pattern page: https://antoniucraciun.github.io/GameDevPatterns/patterns/camera-smoothing
2. Open [WithoutPattern](WithoutPattern/README.md) to see the problem.
3. Open [WithPattern](WithPattern/README.md) to see the fix.

Status: not implemented yet.

