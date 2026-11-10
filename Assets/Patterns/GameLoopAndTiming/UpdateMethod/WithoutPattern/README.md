# Update Method: without the pattern

Scene: `UpdateMethodWithout.unity`

One script, `WorldUpdater` on the **WorldUpdater** object, runs the whole world. It has a serialized reference to every object (guard, waypoints, pickup, door), keeps all their state (the guard's next waypoint, the door's progress), and moves all three in a single `Update`, one block per object.

It works, and for three objects it is easy to read. This is the code you would reasonably write first, as the page's [Keep It Simple](https://antoniucraciun.github.io/GameDevPatterns/keep-it-simple) advice suggests. The cost shows when the world grows: every new kind of object means new fields, new state and a new block in this one script, which ends up knowing about everything.

Compare it with [WithPattern](../WithPattern/README.md): the scenes behave the same, but there each object moves itself.

Status: implemented in Unity 6000.6.8f1.
