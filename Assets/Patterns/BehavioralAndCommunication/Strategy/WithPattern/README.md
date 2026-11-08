# Strategy: with the pattern

The same feature built with Strategy. The idea, from the page: Pull each algorithm out into its own object behind a common interface: the **strategy**. The **context** (the turret) holds a reference to a strategy and calls it, without knowing which one it is.

Compare it with [WithoutPattern](../WithoutPattern/README.md): the behaviour that broke there should hold here, at the cost of the extra types the pattern adds.

Status: not implemented yet.

