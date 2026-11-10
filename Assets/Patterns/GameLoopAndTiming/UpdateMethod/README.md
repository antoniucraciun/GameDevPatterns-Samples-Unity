# Update Method

**Category:** Game Loop & Timing · **Site slug:** `update-method`

> Let every object simulate one frame of its own behaviour when asked.

## What this sample shows

Both scenes show the same small world, taken from the page's Problem section: a guard patrolling four waypoints, a pickup spinning in place, and a door that slides down into the floor and back up. In Play Mode they look identical. The difference is in the code:

- **WithoutPattern:** one `WorldUpdater` script knows every object, holds all their state, and moves all of them in one `Update`.
- **WithPattern:** each object has its own small component (`Patrol`, `Spinner`, `SlidingDoor`) that keeps its own state and advances itself every frame. Nothing knows about the whole world.

## Adding another kind of object

This is where the two versions differ. Say you want a lamp that pulses:

| | WithoutPattern | WithPattern |
| --- | --- | --- |
| Code | Edit `WorldUpdater`: add fields for the lamp, fields for its state, and a new block in `Update` | Write one new component, e.g. `PulsingLight`, with its own `Update` |
| Scene | Add the lamp, then drag it into `WorldUpdater` | Add the lamp and put the component on it |
| Other files touched | `WorldUpdater` grows with every new kind of object | None |

## Where to begin

1. Read the pattern page: https://antoniucraciun.github.io/GameDevPatterns/patterns/update-method
2. Open [WithoutPattern](WithoutPattern/README.md) and its scene, and read `WorldUpdater`.
3. Open [WithPattern](WithPattern/README.md) and its scene, and read the three components.

Status: implemented in Unity 6000.6.8f1.
