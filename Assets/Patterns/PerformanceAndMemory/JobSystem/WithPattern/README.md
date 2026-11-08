# Job System: with the pattern

The same feature built with Job System. The idea, from the page: Describe work as **jobs**: small structs that process plain data (no references to managed objects), with declared inputs and outputs. A **scheduler** runs them on worker threads, splits large jobs into batches across cores, and orders dependent jobs.

Compare it with [WithoutPattern](../WithoutPattern/README.md): the behaviour that broke there should hold here, at the cost of the extra types the pattern adds.

Status: not implemented yet.

