using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;


namespace ARCN.Repository.EfCoreInterceptors
{
    public class AuditLogInterceptor : SaveChangesInterceptor
    {
        private readonly IServiceProvider serviceDescriptors;
        private readonly ILogger<AuditLogInterceptor> logger;

        public AuditLogInterceptor(IServiceProvider serviceDescriptors, ILogger<AuditLogInterceptor> logger)
        {
            this.serviceDescriptors = serviceDescriptors;
            this.logger = logger;
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            try
            {
                var currentDate = DateTime.Now;

                using (var scope = serviceDescriptors.CreateAsyncScope())
                {
                    IUserIdentityService userIdentityService = scope.ServiceProvider.GetRequiredService<IUserIdentityService>();
                    IHttpContextAccessor httpContextAccessor = scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>();
                    var ipAddress = httpContextAccessor.HttpContext?.Request?.HttpContext?.Connection?.RemoteIpAddress?.ToString();
                    var userName = userIdentityService.UserName;
                    var userProfileId = userIdentityService.UserProfileId;

                    var auditLogDbSet = eventData.Context.Set<AuditLog>();

                    var entities = eventData.Context.ChangeTracker.Entries<AuditableEntity>().ToList();
                    foreach (EntityEntry<AuditableEntity> entity in entities)
                    {
                        var tableName = entity.Entity.GetType().Name;
                        if (entity.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                        {
                            var auditLog = new AuditLog
                            {
                                ActionType = Microsoft.EntityFrameworkCore.EntityState.Added.ToString(),
                                LoggedAt = currentDate,
                                IPAddress = ipAddress,
                                TableObject = tableName,
                                UserName = userName,
                                UserProfileId = userProfileId
                            };
                            var entityProperties = entity.CurrentValues.Properties;
                            for (int i = 0; i < entityProperties.Count(); i++)
                            {
                                var property = entityProperties[i];
                                var entityProperty = entity.Property(property);
                                if (entityProperty != null && !entityProperty.IsTemporary)
                                {
                                    auditLog.AuditLogEntries.Add(new AuditLogEntry
                                    {
                                        NewValue = entityProperty?.CurrentValue?.ToString() ?? "",
                                        PropertyName = property.Name
                                    });
                                }

                            }
                            await auditLogDbSet.AddAsync(auditLog);
                        }
                        if (entity.State == Microsoft.EntityFrameworkCore.EntityState.Modified)
                        {
                            var auditLog = new AuditLog
                            {
                                ActionType = Microsoft.EntityFrameworkCore.EntityState.Modified.ToString(),
                                LoggedAt = currentDate,
                                IPAddress = ipAddress,
                                TableObject = tableName,
                                UserName = userName,
                                UserProfileId = userProfileId
                            };

                            var entityProperties = entity.CurrentValues.Properties;
                            for (int i = 0; i < entityProperties.Count(); i++)
                            {
                                var property = entityProperties[i];
                                var entityProperty = entity.Property(property);
                                if (entityProperty != null && !entityProperty.IsTemporary)
                                {
                                    auditLog.AuditLogEntries.Add(new AuditLogEntry
                                    {
                                        NewValue = entityProperty?.CurrentValue?.ToString() ?? "",
                                        OldValue = entityProperty?.OriginalValue?.ToString() ?? "",
                                        PropertyName = property.Name
                                    });
                                }

                            }
                            await auditLogDbSet.AddAsync(auditLog);
                        }
                        if (entity.State == Microsoft.EntityFrameworkCore.EntityState.Deleted)
                        {
                            var auditLog = new AuditLog
                            {
                                ActionType = Microsoft.EntityFrameworkCore.EntityState.Deleted.ToString(),
                                LoggedAt = currentDate,
                                IPAddress = ipAddress,
                                TableObject = tableName,
                                UserName = userName,
                                UserProfileId = userProfileId
                            };

                            var entityProperties = entity.CurrentValues.Properties;
                            for (int i = 0; i < entityProperties.Count(); i++)
                            {
                                var property = entityProperties[i];
                                var entityProperty = entity.Property(property);
                                if (entityProperty != null && !entityProperty.IsTemporary)
                                {
                                    auditLog.AuditLogEntries.Add(new AuditLogEntry
                                    {
                                        NewValue = entityProperty?.CurrentValue?.ToString() ?? "",
                                        OldValue = entityProperty?.OriginalValue?.ToString() ?? "",
                                        PropertyName = property.Name
                                    });
                                }

                            }
                            await auditLogDbSet.AddAsync(auditLog);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "");
            }

            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
