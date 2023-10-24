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

## Background and Resources

 - ORMs (Object-Relational Mappers) are designed to translate and mediate working with properly normalized data stored in a Relational Database Management System (RDBMS) from your application code in a more language native, Object-Oriented OOP way
 - Some ORMs already implement desirable patterns such as the Repository pattern and/or Unit of Work pattern.
 - Several of these ORMs specifically designed for c#.Net allow accessing the data by using LINQ, although what happens in the breakground may differ greatly.
   - LINQ is the most desirable c# language native way of requesting a set of data from application code
   - However, Microsoft's ORM product EntityFramework is designed to write queries directly againt Entity classes via the generic DbSet<TEntity> class.
     - This means you either have to use these classes in your application code which are tightly coupled to your persistence layer DDL and may not be in a desirable shape per your domain, or you have to create an extra layer of code to map them to your domain
     - Hitting web APIs, external database sources might causes you to want to create facades, aggregates, or other deviations from source format for use in you application. Hell, it might just be an old COBOL mainframe with no RDBMS and just has data organized on screens. (ask me)

This leads me to the idea that it would be desirable to find separate robust solutions for the following concerns:
- Allowing data requests to be formed on the basis of domain classes, not entities
- Define mapping logic between data/entity classes and domain classse that is reusable and inject into this intermediate layer, which includes mapping lambda expression built in LINQ queries
- Getting data from a persistence layer (delegated to from a Strategy using an existing ORM, presumably)

https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application 

I've seen a lot of examples, including on Microsoft's website attempting to create a generic repository class, but almost all of these either don't include mappping at all or if they, they presume mapping to another object that makes the entity 1 to 1. What is the point? If our application domain needs to be a,b,c and our data source is ab, bc, y, z for whatever reason, if we don't have control over it we cannot fix the discrepancy and our application domain is corrupted because it is so highly coupled. They also either break Entity LINQ access or disallow it used via your domain model, which is my goal. They almost always require you hardcode and ultimtely manage specific 'get' repo methods to get specific sets of data you know your application needs instead of letting your application access what it requires when it is relevant, again based solely on the domain which is what it is supposed to care about.


```csharp
//why is this so bad?
//What if I am accessing a warehousing table that I want to split into my normalized domain?
//The entities have to match the database, so now they can't match my domain
//There is a one->many mapping from db->domain, but this only allows one->one
public class GenericRepository<TEntity> where TEntity : class
{
internal SchoolContext context;
internal DbSet<TEntity> dbSet;

//This is another clue that maybe generics are not the way to go
//The Entity is based on generic typing compilation, but then this repo needs a specific context (which has specific entities)?
//Some generic repos take the parent class DbContext, which would be better.
//Also, where does this leave Unit of Work? Is it passed in by a Unit of Work mirroring/managing the EF Context Unit of Work's life cycle?
public GenericRepository(SchoolContext context)
{
    this.context = context;
    this.dbSet = context.Set<TEntity>();
}

//Doesn't expose IQueryable, so a query cannot be built in normal LINQ, but requires providing a dissasembled set of
//logic it will peice back together. And again, it is against the Entity type, not the domain type
//All said, it doesn't provide everything and isnt' nearly as nice to work with.
//How do I perform all the joining and grouping and stuff and make sure it is translating to SQl to maximize performance?
//Also, passing in a ',' delimited string? Seriously...?
public virtual IEnumerable<TEntity> Get(
    Expression<Func<TEntity, bool>> filter = null,
    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
    string includeProperties = "")
{
    IQueryable<TEntity> query = dbSet;

    if (filter != null)
    {
	query = query.Where(filter);
    }

    foreach (var includeProperty in includeProperties.Split
	(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
    {
	query = query.Include(includeProperty);
    }

    if (orderBy != null)
    {
	return orderBy(query).ToList();
    }
    else
    {
	return query.ToList();
    }
}

public virtual TEntity GetByID(object id)
{
    return dbSet.Find(id);
}

public virtual void Insert(TEntity entity)
{
    dbSet.Add(entity);
}

public virtual void Delete(object id)
{
    TEntity entityToDelete = dbSet.Find(id);
    Delete(entityToDelete);
}

public virtual void Delete(TEntity entityToDelete)
{
    if (context.Entry(entityToDelete).State == EntityState.Detached)
    {
	dbSet.Attach(entityToDelete);
    }
    dbSet.Remove(entityToDelete);
}

public virtual void Update(TEntity entityToUpdate)
{
    dbSet.Attach(entityToUpdate);
    context.Entry(entityToUpdate).State = EntityState.Modified;
}
}
```

## End goal looks like

Only creating domain classes (of course) and data classes (to create the database or generated from the database),
and providing a lightweight mapping configuration from data classes (Entities) to domain and/or data transfer objects. 
Optional features should be clear on how to implement and would require extra configurations associated with their aims.

```csharp
//unfinshed
public class DomainClass1
{
    public string Name { get; set; }
    public int Id { get; set; }
}
public EntityClass1
{
}

public class DomainClass1Mapping
{
}

//maybe can provide generic access to domain class...
//must implement IQueryable to allow LINQ against the domain
//to build the full query expression tree before compiling and translating into SQL to avoid
//returning massive data sets over the wire and filtering application-server-side
public class Repository<DomainClass1>(MappingContainerClass, DbContext maybe...?) : IQueryable<DomainClass1> where DomainClass1 is class
```
