# _Shared

Code and assets that **two or more samples** use, so each sample folder can stay small. Empty for now.

## What belongs here

- Small helpers more than one sample needs: a simple player mover, a debug overlay that prints a few numbers on screen, a test-scene ground and camera rig, a fake network link for the networking samples.
- Shared materials, a placeholder font, and input action assets that several samples read.
- One assembly definition (`GameDevPatterns.Shared`) once there is code, which sample assemblies may reference.

## What doesn't

- **The pattern itself.** A sample's WithPattern code lives in its own folder, even when another sample uses the same pattern. Readers should find all of a pattern in one place, matching its docs page.
- Code only one sample uses. Keep it in that sample; move it here when a second sample needs it.
- Anything that only exists "in case" a sample needs it later (see [Keep It Simple](https://antoniucraciun.github.io/GameDevPatterns/keep-it-simple)).
- Code that references a sample folder. Dependencies only point *into* `_Shared`, never out of it.
