# Database

**Adding Migratuions**

```
dotnet ef migrations add InitialCreate \
--context DatabaseContext \
--output-dir Migrations/folder \
--project YourApp.Infrastructure \
--startup-project YourApp.Web
```

## Description

**Flags Explained**
```
--context           The name of the specific DbContext (e.g., RmsDbContext)
```
```
--output-dir	    Directory where the migration files will be placed
```
```
--project	        The project that contains the DbContext and migrations
```
```
--startup-project	The startup project (usually the API or web project) — contains Program.cs
```

## Usage

```
dotnet ef migrations add InitialCreate --context RealEstateContext --output-dir Entities/Migrations --project RealEstate.Database --startup-project RealEstate
```

**Update Command**
```
dotnet ef database update --context RealEstateContext --project RealEstate.Database --startup-project RealEstate
```

**Remove Command**
```
dotnet ef migrations remove --context RealEstateContext --project RealEstate.Database --startup-project RealEstate
```

**Migration List Command**
```
dotnet ef migrations list --context RealEstateContext --project RealEstate.Database --startup-project RealEstate
```

**Migration Assembly is Already Added**
```
options.UseSqlServer(connectionString, sql =>
{
    sql.MigrationsAssembly(typeof(RealEstateContext).Assembly.FullName);
});
```

## Revert & Update Migration

Step 1. Revert to previous migration.
```
dotnet ef database update InitialCreate
```

Step 2. Remove last migration
```
dotnet ef migrations remove
```

Step 3. Add new migration
```
dotnet ef migrations add AddNewMigration
```

Step 4. Update to latest data base
```
dotnet ef database update
```

## Option can be used in Update or Remove

**Common EF CLI Commands and Options** \

Command	Supports --context	--project	--startup-project	--output-dir \

dotnet ef migrations add	✅ Yes	✅ Yes	✅ Yes	✅ Yes \

dotnet ef migrations remove	✅ Yes	✅ Yes	✅ Yes	❌ No \

dotnet ef database update	✅ Yes	✅ Yes	✅ Yes	❌ No \

dotnet ef migrations list	✅ Yes	✅ Yes	✅ Yes	❌ No \

dotnet ef database drop	✅ Yes	✅ Yes	✅ Yes	❌ No \

--output-dir is only relevant for migrations add.