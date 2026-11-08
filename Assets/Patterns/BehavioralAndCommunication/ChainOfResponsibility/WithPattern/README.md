# Chain of Responsibility: with the pattern

The same feature built with Chain of Responsibility. The idea, from the page: Give every potential receiver the same interface, and arrange them in a **chain**, ordered by priority. The request (the click) travels down the chain: each handler either **handles it and stops** the chain, or **passes it on**.

Compare it with [WithoutPattern](../WithoutPattern/README.md): the behaviour that broke there should hold here, at the cost of the extra types the pattern adds.

Status: not implemented yet.

