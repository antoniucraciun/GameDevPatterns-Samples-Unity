# Iterator: with the pattern

The same feature built with Iterator. The idea, from the page: Give the collection an **iterator**: a small object that knows how to step through it and hands out one element at a time. Callers only see "is there a next element, and what is it", and the collection keeps its layout to itself.

Compare it with [WithoutPattern](../WithoutPattern/README.md): the behaviour that broke there should hold here, at the cost of the extra types the pattern adds.

Status: not implemented yet.

