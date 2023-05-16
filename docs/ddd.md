# DDD

Domain Driven Design is a methodology of software development, where the model of domain drives development.
To put simply, code itself represents the business rules. We focus on code, rather than documentation, because in practice there is a tendency that the two diverge quite soon.
As new insight is gained on a domain - the code is updated with it.

[Domain Driven Design introduction by MS](https://learn.microsoft.com/en-us/archive/msdn-magazine/2009/february/best-practice-an-introduction-to-domain-driven-design)

Before you try to understand DDD, you should already have a strong understanding of OOP and SOLID principles.

[SOLID in .NET part I](https://youtu.be/mAEscks8cuI)
[SOLID in .NET part II](https://www.youtube.com/watch?v=W0_hJyfD-5c)

## Why should you care?

### Timeless fundamentals

Technologies change rapidly, however principles stay. Even if new principles are created, they are built on top of the old ones.
OOP, SOLID, FP, DDD - every programmer should know about them for that is the fundamental basis for all software development.

20 years ago or no - all of the principles still apply.

### The freedom of choice

You don't have to code using DDD. In fact, in some scenarios it may not be suitable at all. However, you should be aware of the alternatives and pick not just the one you are comfortable with, but the one that is the most fitting for the job.
To be able to pick one, you need to understand it.

### DDD - a popular choice today

For many companies - DDD is still a popular choice today. After all, who doesn't want to simplify complex business logic?

## Basics

When designing software, think about it in terms of real life concepts. The naming in code should match with the naming that domain experts use. Group those concepts into components, components into modules. Modules should be isolated from one another and have a single point of communication.

### Model

#### Entity

#### Value Object

### Repository



## Beyond Basics

The full map of DDD is below:

![DDD full](https://thedomaindrivendesign.io/wp-content/uploads/2019/06/DomainDrivenDesignReference-700x620.png)

When a project is complex, it will have multiple modules which you will have to segregate / group into bounded contexts. There will be pieces of code that all will share and you will have to move that into a shared kernel. The creation of objects may so complex, that you may need factories... And this is just some of the examples of struggles you may have to deal with.

If you want to learn more - refer to [Eric Evans book Domain Driven Design](https://www.amazon.com/Domain-Driven-Design-Tackling-Complexity-Software/dp/0321125215):

![DDD Book](https://m.media-amazon.com/images/I/61aFldsgAmL._AC_UF1000,1000_QL80_.jpg)

## Is Onion Architecture == DDD?

Not quite. The initial idea of DDD was that we evolve the codebase, refactor code to represent business better as we get understanding.
Onion Architecture, on top of that, solves the problem of DB being a centrepiece of all. DB is infrastructure, infra can change, so we should protect ourselves from that change. And the best way to do so is by moving it outside of the domain core - to infrastructure layer. Onion architecture, often called Hexagonal or architecture of Ports and Adapters - puts a one directional connection between infrastructure and domain. Infrastructure can reference domain, but not the other way round.

![Onion Architecture](https://i.stack.imgur.com/gPKrg.jpg)

### Domain Services

The interfaces to operations for persistence of entities. For example Add, Remove, Update, etc.

### Application Services

Interfaces and implementation of logic for multiple services working together. Essentially, the business logic that domain objects couldn't do by themselves in a self-contained manner.

### Infrastructure

Everything else is infrastructure. Domain cannot reference it. So it include: API, UI, tests, Adapters, Domain Services implementation, etc.

## Should you use DDD and how do you know that you have made the right choice?

DDD is difficult to start with and is a learning curve for anyone new to it. If your project is simple (small, short lived) - then you may not bother. There isn't much point to apply DDD, if you won't even get to see how well your system reacts to changes, how easy it is to evolve it. Use DDD when domain is complex and you plan to develop a project for a long time. 

Did you choose the right architecture? It's a question that calls for an allegory of bridges. Is a bridge good? You won't know until it collapses. If it did - it was not a good bridge. But if a bridge can whithstand the test of time - then it was indeed a good bridge.

## Other examples

- https://github.com/kgrzybek/modular-monolith-with-ddd - you don't have to go microservices to have a good architecture
- https://github.com/dotnet-architecture/eShopOnContainers - microservices done right
- https://github.com/Almantask/BDD-Specflow-Demo-SimplCommerce/tree/master/tests/SimplCommerce.AcceptanceTests - self-documenting tests or a wiki covered with automated tests
