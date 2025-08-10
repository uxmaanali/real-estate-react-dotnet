
# Real Estate 

This project has separate frontend and backend applications.

# Frontend

```sh
cd .\frontend
npm install
npm run dev
```

# Backend
Need to update the database connection string in ```appsettings.Development.json```

Update connection string mentioned below.
```
"ConnectionStrings": {
  "RealEstateConnection": "Server=localhost;Database=RealEstateDB;User Id=sa;Password=P@ssw0rd;MultipleActiveResultSets=true;TrustServerCertificate=True"
}
```

Now run backend project from Visual Studio or if you have CLI installed then you can run through CLI commands.

```sh
  cd .\backend\RealEstate\RealEstate
  dotnet restore
  dotnet build
  dotnet watch run
```

It will automatically seed user and properties.

User will be

- Email is ```buyer@realestate.com```
- Password is ```Abcd!234```
