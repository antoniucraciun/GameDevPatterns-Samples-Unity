# Arena Allocator

**Category:** Performance & Memory · **Site slug:** `arena-allocator`

> Allocate from a block that is freed all at once, e.g. per frame or per level.

## What this sample will show

WithoutPattern builds a small feature the straightforward way and runs into the problem the page describes. WithPattern builds the same feature with Arena Allocator, so you can run both and compare them.

The problem, from the page:

> Every frame, gameplay code creates short-lived data: lists of nearby enemies, raycast hit buffers, path candidates, temporary arrays for sorting, strings for the debug overlay. In a garbage-collected language such as C# (Unity, Godot .NET), each `new List<T>()` or array is a heap allocation that the garbage collector must later find and free.

## Where to begin

1. Read the pattern page: https://antoniucraciun.github.io/GameDevPatterns/patterns/arena-allocator
2. Open [WithoutPattern](WithoutPattern/README.md) to see the problem.
3. Open [WithPattern](WithPattern/README.md) to see the fix.

Status: not implemented yet.

