namespace RealEstate.Database.Interceptors;
using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

using RealEstate.Database.Abstraction;
using RealEstate.Shared.Abstraction;
using RealEstate.Shared.Services.AuthorizedContext;

public class AuditInterceptor : SaveChangesInterceptor, IScopedDependency
{
    private readonly IAuthorizedContextService _authorizedContextService;

    public AuditInterceptor(IAuthorizedContextService authorizedContextService)
    {
        _authorizedContextService=authorizedContextService;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        if (eventData.Context is not null)
        {
            var entries = eventData.Context.ChangeTracker.Entries<AuditableEntity>();
            var username = _authorizedContextService.GetUserEmail();

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt=DateTimeOffset.UtcNow;
                        entry.Entity.ModifiedAt=DateTimeOffset.UtcNow;
                        entry.Entity.CreatedBy=username;
                        break;

                    case EntityState.Modified:
                        entry.Entity.ModifiedAt=DateTimeOffset.UtcNow;
                        entry.Entity.ModifiedBy=username;
                        // Ensure the CreatedAt is not modified on update
                        entry.Property(x => x.CreatedAt).IsModified=false;
                        break;
                        //case EntityState.Deleted:

                        //    // Soft delete: Change state to Modified and set IsActive to false
                        //    entry.State = EntityState.Modified;
                        //    entry.Entity.IsActive = false;
                        //    break;
                }
            }
        }

        return base.SavingChanges(eventData, result);
    }
}