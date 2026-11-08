# Hierarchical State Machine: with the pattern

The same feature built with Hierarchical State Machine. The idea, from the page: Let states have a **parent**. *Idle*, *Running* and *Crouching* become children of a **Grounded** super-state, and *Jumping* and *Falling* children of **Airborne**.

Compare it with [WithoutPattern](../WithoutPattern/README.md): the behaviour that broke there should hold here, at the cost of the extra types the pattern adds.

Status: not implemented yet.

