# Factory: with the pattern

The same feature built with Factory. The idea, from the page: Put creation in **one object whose job is creating things**: a factory. Callers say *what* they want ("a goblin at this position"), and the factory decides *how*: which template (prefab, scene or Blueprint), pooled or new, which dependencies, which registrations.

Compare it with [WithoutPattern](../WithoutPattern/README.md): the behaviour that broke there should hold here, at the cost of the extra types the pattern adds.

Status: not implemented yet.

