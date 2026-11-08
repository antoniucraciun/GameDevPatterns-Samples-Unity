# Dependency Injection

**Category:** Architecture & Structure · **Site slug:** `dependency-injection`

> Hand objects the services they need instead of letting them look them up.

## What this sample will show

WithoutPattern builds a small feature the straightforward way and runs into the problem the page describes. WithPattern builds the same feature with Dependency Injection, so you can run both and compare them.

The problem, from the page:

> A `ShopController` calls `SaveService.Instance`, `Analytics.Instance` and `PlayerWallet.Instance`. You cannot tell what it needs without reading every line.

## Where to begin

1. Read the pattern page: https://antoniucraciun.github.io/GameDevPatterns/patterns/dependency-injection
2. Open [WithoutPattern](WithoutPattern/README.md) to see the problem.
3. Open [WithPattern](WithPattern/README.md) to see the fix.

Status: not implemented yet.

