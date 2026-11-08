# Weighted Random & Loot Tables: with the pattern

The same feature built with Weighted Random & Loot Tables. The idea, from the page: **Weighted selection.** Give each entry a weight. Sum the weights, roll a number in `[0, total)`, and walk the entries subtracting weights until the roll falls inside one.

Compare it with [WithoutPattern](../WithoutPattern/README.md): the behaviour that broke there should hold here, at the cost of the extra types the pattern adds.

Status: not implemented yet.

