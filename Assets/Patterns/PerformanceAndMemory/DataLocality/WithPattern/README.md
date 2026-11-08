# Data Locality: with the pattern

The same feature built with Data Locality. The idea, from the page: Arrange data so that **what you process together sits together in memory, in the order you process it**. The CPU fetches memory in cache lines (typically 64 bytes) and prefetches ahead when access is sequential.

Compare it with [WithoutPattern](../WithoutPattern/README.md): the behaviour that broke there should hold here, at the cost of the extra types the pattern adds.

Status: not implemented yet.

