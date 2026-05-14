# Entity Framework Core

## What is Entity Framework Core?
- Entity Framework Core (EF Core) is a Modern & Lightweight, open-source ORM (Object Relational Mapper) for .NET applications.

- It allows developers to work with databases using C# objects instead of writing raw SQL queries most of the time.

- EF Core handles:
    - Database connections
    - CRUD operations
    - Change tracking
    - Migrations
    - Relationships
    - Query generation


## Advantages of EF Core:-
- Reduces boilerplate database code
- Improves developer productivity
- Supports LINQ queries
- Database provider independent
- Supports migrations
- Supports async operations
- Easy integration with ASP.NET Core

## Why EF Core is Used in Industry:-
1. Faster development
1. Less boilerplate SQL
1. Strongly typed (compile time safety)
1. Works perfectly with ASP.NET Core.
1. Supports SQL Server, PostgreSQL, MySQL, SQLite.

## Main Components of EF Core:-
1. DbContext
1. DbSet
1. ModelBuilder & OnModelCreating() method (Fluent API)
1. Change Tracker
1. LINQ Provider
1. Migrations
1. Database Provider

- Additionally, EF Core works with Entity / Domain Models and Data Annotations, but they are not part of EF Core itself. They are commonly used for mapping, validation, and database schema configuration.

## Hands-on Entity Framework Core:-

1. Install Packages:-
    1. Microsoft.EntityFrameworkCore ✅
    1. Microsoft.EntityFrameworkCore.Tools ✅
    1. Microsoft.EntityFrameworkCore.SqlServer ✅
    1. Microsoft.EntityFrameworkCore.InMemory(optional) 
    1. Microsoft.EntityFrameworkCore.Design

1. Configure Connection String in appsettings.json file:-
    ```cs
    {
    "ConnectionStrings": {
        "DefaultConnectionString": "Server=<server>; Database=<dbName>; Trusted_Connection=True; TrustServerCertificate=True"
    }
    }
    ```

1. Make Data Folder & in that folder create AppDbContext.cs file:-

1. AppDbContext.cs file:-
    ```cs
    using Microsoft.EntityFrameworkCore;
    using MyProject.Models.Domain;

    namespace MyProject.Data
    {
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
            {
            }

            public DbSet<Employee> Employees {get; set;}
            public DbSet<Product> Products {get; set;}
        }
    }
    ```

1. Register DbContext:-
    ```cs
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
    
    builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString) );
    // builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("EmployeesDb") );
    ```


1. Injecting object into class/Repository using Constructor Injection:-
    ```cs
    private readonly AppDbContext _context;

    Constructor(AppDbContext context)
    {
        _context = context;
    }
    ```

## CRUD Operations using EF Core:-
### CREATE
```cs
await _context.Employees.AddAsync(employee);
await _context.SaveChangesAsync();
```

### READ (get all)
```cs
await _context.Employees.AsNoTracking().ToListAsync();
```

### READ (get by id)
```cs
await _context.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
```

### UPDATE 
```cs
var existingEmployee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);

if (existingEmployee is null)
    return null;

existingEmployee.Name = "Updated Name";

await _context.SaveChangesAsync();
```

### DELETE
```cs
var existingEmployee = await _context.Employees
    .FirstOrDefaultAsync(x => x.Id == id);

if(existingEmployee is null)
    return null;

_context.Employees.Remove(existingEmployee);

await _context.SaveChangesAsync();
```