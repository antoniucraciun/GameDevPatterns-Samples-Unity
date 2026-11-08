# Object Pool: with the pattern

The same feature built with Object Pool. The idea, from the page: Create objects ahead of time, or on first demand, and keep inactive ones in a pool. **Get** takes an inactive object from the pool and activates it, creating a new one only if the pool is empty.

Compare it with [WithoutPattern](../WithoutPattern/README.md): the behaviour that broke there should hold here, at the cost of the extra types the pattern adds.

Status: not implemented yet.

