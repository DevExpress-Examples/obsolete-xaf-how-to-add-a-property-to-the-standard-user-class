using System;

using DevExpress.Xpo;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base.Security;
using System.Collections.Generic;
using System.Security;

namespace DXExample.Module {
    [DefaultClassOptions]
    public class CustomRole : RoleBase, IRole, ICustomizableRole {
        public CustomRole(Session session) : base(session) { }
        [Association("User-Role")]
        public XPCollection<CustomUser> Users {
            get { return GetCollection<CustomUser>("Users"); }
        }
		IList<IUser> IRole.Users {
			get {
                return new ListConverter<IUser, CustomUser>(Users);
			}
		}
		void ICustomizableRole.AddPermission(IPermission permission) {
			base.AddPermission(permission);
		}
    }

}
