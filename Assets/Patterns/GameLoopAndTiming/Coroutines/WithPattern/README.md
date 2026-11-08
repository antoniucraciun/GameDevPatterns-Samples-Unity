# Coroutines: with the pattern

The same feature built with Coroutines. The idea, from the page: Write the sequence as **one function that pauses itself**. At each `yield` (or `await`), the function hands control back to the engine and resumes later: next frame, after a delay, or when a condition becomes true.

Compare it with [WithoutPattern](../WithoutPattern/README.md): the behaviour that broke there should hold here, at the cost of the extra types the pattern adds.

Status: not implemented yet.

