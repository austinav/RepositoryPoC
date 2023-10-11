# RepositoryPoC

## Purpose

The basic purpose of this is find a way to handle all the boiler plate code required to provide data from external sources to an application domain/runtime computing environment.

Data sources are anything an application gets typed data from, including but not limited to databases, apis, and files.

General concerns include:
 - Mapping
	- Entities -> Dtos and vice versa
	- Entities -> Domain objects and vice versa
	- Query Expressions
 - Querying
	- Strongly typed expression trees (LINQ)
	- Joins
	- Groupings
	- Filters
	- Mapping logic
 - Additional optional functions
	- Caching logic
	- Logging 

## End goal looks like

Only creating domain classes (of course) and data classes (to create the database or generated from the database),
and providing a lightweight mapping configuration from data classes (Entities) to domain and/or data transfer objects. 
Optional features should be clear on how to implement and would require extra configurations associated with their aims.
