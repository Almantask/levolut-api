# SOLID

## Idea

Currency exchange:

Api (LevolutApi) where you can exchange money of dfferent currencies:
Different banks have different rules for currency exchange fee calculation.

Api can:

- Add money to bank
- Redeem money from bank
- Exchange money from one currency to another

- Different currencies have a rate
- Exchange currency
- Exchange fee

The API has just enough complexity to showcase what problems could come from a poorly managed code base.
From a codebase that violates SOLID principles.

## S - Single Responsibility

### [Bootstrap through extension methods](./Levolut.Api.V2/Program.cs)

- Why do we need bootstrapping through extension methods?

### [Central Error Handling](./Levolut.Api.V2/Bootstrap/ErrorHandling.cs)

- Why is try-catch everywhere a bad thing?
- Why custom exceptions?

### [Light controller](./Levolut.Api.V2/Controllers/TransferController.cs)

- Why should controller only delegate and not do anything by itself?

### [Choosing the right place to error](./Levolut.Api.V2/Services/Balance/BalanceService.cs)

- Which component should throw?

### [How many repetitions is too much](./Levolut.Api.V2/Services/Balance/BalanceService.cs)

- How many times are you okay with repeating yourself? Is it a constant?

---

- When is a method too big? Best loc?
- Is SRP solved always by splitting things?
- What is the ultimate goal of a developer? (scope)

## O - Open-Closed

- Starts as early as hardcoding instead of parameterising

### [Contracts](./Levolut.Api.V2/Contracts/Requests/AddBankFeeRuleRequest.cs)

- How do contracts help avoiding breaking changes?
- How does API versioning help with breaking changes? What breaking changes do we have (MoneyExchange model)
- Enties, models, value objects. What's the difference?

### [Custom Exceptions and testing](./Levolut.Api.V2/Exceptions/EntityNotFoundException.cs)

- When creating custom exception, consider using properties for key details. Helps with testing

___

- How do you know that you wrote good software / chose good architecture?
- What is the ultimate goal of a developer? (effort)

## L - Liskov Substitution

- Layout by semantics vs layout by components

### [Good inheritance](./Levolut.Api.V2/Services/Balance/BalanceService.cs)

- Composition vs inheritance and why there was no inheritance here?
- is-a and has-a

## I - Interface Segregation

### [Atomic interfaces in CQRS](./Levolut.Api.V2/Database/Query/Handlers/IQueryHandler.cs)

- Why is a small interface a good thing?
- A common problem with repositories and how to solve it?
- Mediator

## Dependency Inversion

- Is Dependency Injection the only way to do dependency inversino?
- Naming based on implementation

### [Services](./Levolut.Api.V2/Services/Balance/BalanceService.cs)

- Why do we need layers? What good is a service that does trivial stuff now?
- What alternatives are there to services?
- How much abstraction is too much?

### Choosing the right implementation

[Record for contracts]()

---

In short:
S - Split it
O - don't hardcode it
L - don't fake it
I - keep it small
D - inject it

In the all the principles are related to one another.




