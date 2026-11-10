# Update Method: with the pattern

Scene: `UpdateMethodWith.unity`

Each object has its own component that keeps its own state and advances itself by one frame in `Update`. Unity's `MonoBehaviour.Update` is this pattern built into the engine: Unity keeps the list of active components and calls each one once per frame.

| Component | On | What it does |
| --- | --- | --- |
| `Patrol` | Guard | Walks between the waypoints and remembers which one is next. Copied unchanged from the page's first Unity example. |
| `Spinner` | Pickup | Spins in place. |
| `SlidingDoor` | Door | Slides down into the floor and back up, and remembers how far it has moved. |

To add another kind of object, write one new component with its own `Update` and put it on that object. No other script changes, because nothing knows about the whole world.

## Differences from the page

- The page only shows `Patrol`. `Spinner` and `SlidingDoor` are added here so the scene matches the page's Problem section (a guard, a pickup and a door).
- The page's second example, a `TickManager` that calls `Tick` on a plain list for thousands of objects, isn't in this sample. It is an optimisation, and only worth it when the Profiler shows the per-object `Update` calls are a real cost.

Compare it with [WithoutPattern](../WithoutPattern/README.md): the scenes behave the same, but there one script moves everything.

Status: implemented in Unity 6000.6.8f1.
