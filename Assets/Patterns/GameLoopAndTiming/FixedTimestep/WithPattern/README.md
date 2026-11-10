# Fixed Timestep: with the pattern

Scene: `FixedTimestepWith.unity`

| Script | On | What it does |
| --- | --- | --- |
| `Thruster` | Rocket | Samples Space in `Update`, pushes in `FixedUpdate`. Copied unchanged from the page's first Unity example. |
| `FixedStepRunner` | (plain C#) | The accumulator: runs whole fixed steps for the time that has passed and returns the interpolation alpha. The page's second Unity example, with its fields renamed to this repo's style. |
| `Jumper` | Jumper | The same jump as WithoutPattern, advanced by a `FixedStepRunner` at 1/50 s and drawn between the last two states. |
| `FixedTimestepHud` | Hud | Shows the numbers, switches the frame rate, and toggles the jumper's interpolation. |

Unity already runs physics at a fixed step (`FixedUpdate`, Project Settings > Time > Fixed Timestep, 0.02 s here), so the rocket only needs its push moved there. The jumper isn't physics, so it brings its own accumulator, as the page shows for deterministic gameplay code.

## Scene setup

The same as [WithoutPattern](../WithoutPattern/README.md), with this version's scripts, and the Rocket's Rigidbody set to **Interpolate**, as the page's Engine notes say.

## Differences from the page

- The page doesn't show code that uses `FixedStepRunner`. `Jumper` is that glue: it stores the previous and current height and blends them by the returned alpha.
- `FixedTimestepHud` is scene glue for comparing the two versions.

Status: implemented and checked in Play Mode in Unity 6000.6.8f1.
