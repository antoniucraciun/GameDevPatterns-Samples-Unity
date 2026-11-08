# Timer / Scheduler: with the pattern

The same feature built with Timer / Scheduler. The idea, from the page: A **scheduler** owns all timers. Code asks it to *"call this in 3 seconds"* or *"call this every 0.5 seconds"* and receives a **handle** that can cancel the timer.

Compare it with [WithoutPattern](../WithoutPattern/README.md): the behaviour that broke there should hold here, at the cost of the extra types the pattern adds.

Status: not implemented yet.

