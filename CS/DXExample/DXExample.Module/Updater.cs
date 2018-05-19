using System;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Security;

namespace DXExample.Module {
    public class Updater : ModuleUpdater {
        public Updater(ObjectSpace objectSpace, Version currentDBVersion) : base(objectSpace, currentDBVersion) { }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();
            // If a user named 'Sam' doesn't exist in the database, create this user
            CustomUser user1 = ObjectSpace.FindObject<CustomUser>(new BinaryOperator("UserName", "Sam"));
            if (user1 == null) {
                user1 = ObjectSpace.CreateObject<CustomUser>();
                user1.UserName = "Sam";
                // Set a password if the standard authentication type is used
                user1.SetPassword("");
            }
            // If a user named 'John' doesn't exist in the database, create this user
            CustomUser user2 = ObjectSpace.FindObject<CustomUser>(new BinaryOperator("UserName", "John"));
            if (user2 == null) {
                user2 = ObjectSpace.CreateObject<CustomUser>();
                user2.UserName = "John";
                // Set a password if the standard authentication type is used
                user2.SetPassword("");
            }
            // If a role with the Administrators name doesn't exist in the database, create this role
            CustomRole adminRole = ObjectSpace.FindObject<CustomRole>(new BinaryOperator("Name", "Administrators"));
            if (adminRole == null) {
                adminRole = ObjectSpace.CreateObject<CustomRole>();
                adminRole.Name = "Administrators";
            }
            // If a role with the Users name doesn't exist in the database, create this role
            CustomRole userRole = ObjectSpace.FindObject<CustomRole>(new BinaryOperator("Name", "Users"));
            if (userRole == null) {
                userRole = ObjectSpace.CreateObject<CustomRole>();
                userRole.Name = "Users";
            }
            // Delete all permissions assigned to the Administrators and Users roles
            while (adminRole.PersistentPermissions.Count > 0) {
                ObjectSpace.Delete(adminRole.PersistentPermissions[0]);
            }
            while (userRole.PersistentPermissions.Count > 0) {
                ObjectSpace.Delete(userRole.PersistentPermissions[0]);
            }
            // Allow full access to all objects to the Administrators role
            adminRole.AddPermission(new ObjectAccessPermission(typeof(object), ObjectAccess.AllAccess));
            // Allow editing the Application Model to the Administrators role
            adminRole.AddPermission(new EditModelPermission(ModelAccessModifier.Allow));
            // Save the Administrators role to the database
            adminRole.Save();
            // Allow full access to all objects to the Users role
            userRole.AddPermission(new ObjectAccessPermission(typeof(object), ObjectAccess.AllAccess));
            // Deny change access to the User type objects to the Users role
            userRole.AddPermission(new ObjectAccessPermission(typeof(CustomUser),
               ObjectAccess.ChangeAccess, ObjectAccessModifier.Deny));
            userRole.AddPermission(new ObjectAccessPermission(typeof(Company),
               ObjectAccess.ChangeAccess, ObjectAccessModifier.Deny));
            // Deny full access to the Role type objects to the Users role
            userRole.AddPermission(new ObjectAccessPermission(typeof(Role),
               ObjectAccess.AllAccess, ObjectAccessModifier.Deny));
            // Deny editing the Application Model to the Users role
            userRole.AddPermission(new EditModelPermission(ModelAccessModifier.Deny));
            // Save the Users role to the database
            userRole.Save();
            // Add the Administrators role to the user1
            user1.Roles.Add(adminRole);
            // Add the Users role to the user2
            user2.Roles.Add(userRole);
            // Save the users to the database
            user1.Save();
            user2.Save();
        }
    }
}
