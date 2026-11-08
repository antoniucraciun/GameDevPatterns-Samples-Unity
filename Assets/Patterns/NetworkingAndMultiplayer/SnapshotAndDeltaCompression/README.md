# Snapshot & Delta Compression

**Category:** Networking & Multiplayer · **Site slug:** `snapshot-delta-compression`

> Send only what changed relative to a state the receiver already acknowledged.

## What this sample will show

WithoutPattern builds a small feature the straightforward way and runs into the problem the page describes. WithPattern builds the same feature with Snapshot & Delta Compression, so you can run both and compare them.

The problem, from the page:

> The server sends 64 players' state, 20 times per second, to 64 clients. Each player is a position (3 floats), rotation (4 floats), velocity (3 floats), health, ammo and animation state: about 60 bytes.

## Where to begin

1. Read the pattern page: https://antoniucraciun.github.io/GameDevPatterns/patterns/snapshot-delta-compression
2. Open [WithoutPattern](WithoutPattern/README.md) to see the problem.
3. Open [WithPattern](WithPattern/README.md) to see the fix.

Status: not implemented yet.

