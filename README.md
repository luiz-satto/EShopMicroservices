# EShopMicroservices

See the overall picture of **implementations on microservices with .net tools** on real-world **e-commerce microservices** project

![microservices](https://github.com/aspnetrun/run-aspnetcore-microservices/assets/1147445/efe5e688-67f2-4ddd-af37-d9d3658aede4)

There is a couple of microservices which implemented **e-commerce** modules over **Catalog, Basket, Discount** and **Ordering** microservices with **NoSQL (DocumentDb, Redis)** and **Relational databases (PostgreSQL, Sql Server)** with communicating over **RabbitMQ Event Driven Communication** and using **Yarp API Gateway**.

### To run migrations:
```
Add-Migration InitialCreate -OutputDir Data/Migrations -Project Ordering.Infrastructure -StartupProject Ordering.API
```

### Check Explanation of this Repository on Medium
* [.NET 8 Microservices: DDD, CQRS, Vertical/Clean Architecture and Event-Driven Communication](https://medium.com/@mehmetozkaya/net-8-microservices-ddd-cqrs-vertical-clean-architecture-2dd7ebaaf4bd)
