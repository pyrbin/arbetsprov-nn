# Nordic Nest: Arbetsprov 2020

A simple ASP .NET Core 3.1 WebApp built using the "Clean Architecture" principles.

Uses React for the frontend & EntityFrameworkCore for the backend.

## Assignment

> Uppgiften är att läsa in ett dataset i en databas och skapa en enkel webapplikation i C# med .NET Core 3.  
> I webapplikationen skall det per produkt (även kallat SKU) visas en tabell med aktuellt ”optimerat” försäljningspris för
> produkten och hur det förändrats med tiden. Den optimerade tabellen ska enbart visa ett giltigt pris för varje aktuell tidpunkt.

_pdf: [Full Assignment](docs/arbetsprov_nn_2020v2.pdf)_

## Technologies

- .NET Core 3.1
- ASP .NET Core 3.1
- Entity Framework Core 3.1
- ReactJS
- CSVHelper

## How To Run

> TODO

## Overview

Reference: [jasontaylordev/CleanArchitecture](https://github.com/jasontaylordev/CleanArchitecture#overview)

- ### Application

  This layer contains all application logic & is dependent on the Core layer.

- ### Core

  This will contain all entities, enums, exceptions, interfaces, types for the application.

- ### Infrastructure

  This layer contains classes for accessing external resources such as file systems, web services, smtp, and so on. These classes should be based on interfaces defined within the application layer.

- ### Web
  The web application (Presentation layer). Depends on Application and Infrastructure (only for DependencyInjection) layers.

## Resources

### CLEAN Architecture Templates/References

- https://github.com/ardalis/CleanArchitecture
- https://github.com/jasontaylordev/CleanArchitecture
- https://github.com/rafaelfgx/Architecture
- https://github.com/dotnet-architecture/eShopOnWeb

### Tutorials / Guides

#### Videos

- [Clean Architecture with ASP.NET Core 3.0 - Jason Taylor - NDC Sydney 2019](https://www.youtube.com/watch?v=5OtUm1BLmG0)
- [Clean Architecture with ASP.NET Core with Steve "Ardalis" Smith](https://www.youtube.com/watch?v=joNTQy-KXiU)
- [Clean Testing: Clean Architecture with .NET Core](https://www.youtube.com/watch?v=2UJ7mAtFuio)

#### Text

- [Seeding Entity Framework Database from CSV](https://www.davepaquette.com/archive/2014/03/18/seeding-entity-framework-database-from-csv.aspx)

## Developer

<table>
  <tbody>
    <tr>
      <td align="center" valign="top">
        <img width="150" height="150" src="https://github.com/pyrbin.png?s=150">
        <br>
        <a href="https://github.com/pyrbin">pyrbin</a>
      </td>
     </tr>
  </tbody>
</table>
