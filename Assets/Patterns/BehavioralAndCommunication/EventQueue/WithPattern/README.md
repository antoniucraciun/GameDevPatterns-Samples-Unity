# Event Queue: with the pattern

The same feature built with Event Queue. The idea, from the page: Instead of calling receivers directly, **append the event to a queue** and return. Later, at a well-defined point in the frame, a processor **drains the queue** and handles each event.

Compare it with [WithoutPattern](../WithoutPattern/README.md): the behaviour that broke there should hold here, at the cost of the extra types the pattern adds.

Status: not implemented yet.

