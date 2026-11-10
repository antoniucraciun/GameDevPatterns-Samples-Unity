# Fixed Timestep

**Category:** Game Loop & Timing · **Site slug:** `fixed-timestep`

> Advance the simulation in constant increments for stable, reproducible physics.

## What this sample shows

Both scenes have the same two objects, one for each Unity example on the page:

- **Rocket:** a Rigidbody cube. Hold Space and a `Thruster` pushes it up.
- **Jumper:** a sphere whose jump is simulated by hand (no Rigidbody), jumping every 1.5 seconds.

A panel shows the frame rate, the jumper's last peak height and the rocket's height, speed and peak, with buttons to switch between 30, 60, 144 FPS and uncapped.

- **WithoutPattern:** the rocket reads the key and calls `AddForce` in `Update`, and the jumper integrates its jump with `Time.deltaTime`. Both change with the frame rate.
- **WithPattern:** the rocket samples the key in `Update` and pushes in `FixedUpdate` (the page's `Thruster`), and the jumper runs in constant 1/50 s steps through the page's `FixedStepRunner`. Both behave the same at every frame rate.

## What to compare

| | WithoutPattern | WithPattern |
| --- | --- | --- |
| Jumper peak at 30 / 60 / 144 FPS | about 1.47 / 1.53 / 1.57 m, and it wobbles from jump to jump | 1.520 m every jump, at every frame rate |
| Rocket climb while Space is held | about 2 m/s² at 30 FPS, 14 at 60, 48 at 144 | about 10 m/s² at every frame rate |

The exact peak for this jump is 1.6 m. The fixed step doesn't get closer to it than a fast frame rate does; it gets the same answer every time, which is what replays and lockstep networking need.

In WithPattern, untick **Interpolate the jumper** at 144 FPS to see the stutter the page warns about: the jumper only moves 50 times a second.

## Where to begin

1. Read the pattern page: https://antoniucraciun.github.io/GameDevPatterns/patterns/fixed-timestep
2. Open [WithoutPattern](WithoutPattern/README.md) and its scene, press Play, and try each frame-rate button.
3. Do the same in [WithPattern](WithPattern/README.md).

Status: implemented and checked in Play Mode in Unity 6000.6.8f1.
