using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARCN.Domain.Commons.Authorization
{
    public record AppPermission(string feature, string action, string group, string description, bool isStaff =false, bool isTech=false, bool isSale=false, bool isDataEntry=false)
    {
        public string Name  => NameFor(feature, action);

        public static string NameFor(string feature, string action)
            => $"Permission.{feature}.{action}";

    }

    public class AppPermissions
    {
        private static readonly AppPermission[] _all = new AppPermission[]
        {
            new(AppFeature.Users, AppAction.Create, AppRoleGroup.ManagementHierarchy, "Create Users", isTech:true),
            new(AppFeature.Users, AppAction.Read, AppRoleGroup.SystemAccess, "Read Users", isStaff:true, isTech:true),
            new(AppFeature.Users, AppAction.Update, AppRoleGroup.ManagementHierarchy, "Update Users", isTech:true),
            new(AppFeature.Users, AppAction.Delete, AppRoleGroup.ManagementHierarchy, "Delete Users", isTech:true),
            new(AppFeature.Users, AppAction.Unlock, AppRoleGroup.ManagementHierarchy, "Unlock Users", isTech:true),
            new(AppFeature.Users, AppAction.Reset, AppRoleGroup.ManagementHierarchy, "Reset Password Users", isTech:true),

            new(AppFeature.UserRoles, AppAction.Read, AppRoleGroup.SystemAccess, "Read UserRoles"),
            new(AppFeature.UserRoles, AppAction.Update, AppRoleGroup.SystemAccess, "Update Users"),

            new(AppFeature.Roles, AppAction.Create, AppRoleGroup.SystemAccess, "Create Roles"),
            new(AppFeature.Roles, AppAction.Read, AppRoleGroup.SystemAccess, "Read Roles"),
            new(AppFeature.Roles, AppAction.Update, AppRoleGroup.SystemAccess, "Update Roles"),
            new(AppFeature.Roles, AppAction.Delete, AppRoleGroup.SystemAccess, "Delete Roles"),

            new(AppFeature.RoleClaims, AppAction.Read, AppRoleGroup.SystemAccess, "Read RoleClaims"),
            new(AppFeature.RoleClaims, AppAction.Update, AppRoleGroup.SystemAccess, "Update RoleClaims"),


            new(AppFeature.UserPermission, AppAction.Create, AppRoleGroup.ManagementHierarchy, "Create Permission"),
            new(AppFeature.UserPermission, AppAction.Update, AppRoleGroup.ManagementHierarchy, "Update Permission"),


        };

        public static IReadOnlyList<AppPermission> SuperAdminPermission { get; } =
            new ReadOnlyCollection<AppPermission>(_all.ToArray());


        public static IReadOnlyList<AppPermission> StaffPermission { get; } =
           new ReadOnlyCollection<AppPermission>(_all.Where(x => x.isStaff).ToArray());

        

        public static IReadOnlyList<AppPermission> TechPermission { get; } =
          new ReadOnlyCollection<AppPermission>(_all.Where(x => x.isTech).ToArray());

        public static IReadOnlyList<AppPermission> SalesPermission { get; } =
          new ReadOnlyCollection<AppPermission>(_all.Where(x => x.isSale).ToArray());

        public static IReadOnlyList<AppPermission> DataEntryPermission { get; } =
          new ReadOnlyCollection<AppPermission>(_all.Where(x => x.isDataEntry).ToArray());

        public static IReadOnlyList<AppPermission> AllPermissions { get; } =
          new ReadOnlyCollection<AppPermission>(_all);
    }

}
