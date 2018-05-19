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
    public class CustomUser : BaseObject, IUserWithRoles, IAuthenticationActiveDirectoryUser, IAuthenticationStandardUser {
        private Company _Company;
        [Association("Company-Users")]
        public Company Company {
            get { return _Company; }
            set { SetPropertyValue("Company", ref _Company, value); }
        }
        private string _userName;
        private string _storedPassword;
        private bool _isActive = true;
        private bool _changePasswordAfterLogon = false;
        private List<IPermission> permissions;
        public CustomUser(Session session)
            : base(session) {
            permissions = new List<IPermission>();
        }
        public void ReloadPermissions() {
            Roles.Reload();
            foreach (CustomRole role in Roles) {
                role.PersistentPermissions.Reload();
            }
        }
        public bool ComparePassword(string password) {
            return new PasswordCryptographer().AreEqual(this._storedPassword, password);
        }
        public void SetPassword(string password) {
            this._storedPassword = new PasswordCryptographer().GenerateSaltedPassword(password);
        }
#if MediumTrust
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		[Persistent]
		public string StoredPassword {
			get { return this._storedPassword; }
			set {
				this._storedPassword = value;
				OnChanged("StoredPassword");
			}
		}
#else
        [Persistent]
        private string StoredPassword {
            get { return this._storedPassword; }
            set {
                this._storedPassword = value;
                OnChanged("StoredPassword");
            }
        }
#endif
        [Association("User-Role")]
        public XPCollection<CustomRole> Roles {
            get { return GetCollection<CustomRole>("Roles"); }
        }
        IList<IRole> IUserWithRoles.Roles {
            get {
                return new ListConverter<IRole, CustomRole>(Roles);
            }
        }
        [RuleRequiredField("Fill User Name", "Save", "The user name must not be empty")]
        [RuleUniqueValue("Unique User Name", "Save", "The login with the entered UserName was already registered within the system")]
        public string UserName {
            get { return this._userName; }
            set {
                this._userName = value;
                OnChanged("UserName");
            }
        }
        public bool ChangePasswordOnFirstLogon {
            get { return this._changePasswordAfterLogon; }
            set {
                this._changePasswordAfterLogon = value;
                OnChanged("ChangePasswordOnFirstLogon");
            }
        }
        public bool IsActive {
            get { return this._isActive; }
            set {
                this._isActive = value;
                OnChanged("IsActive");
            }
        }
        public IList<IPermission> Permissions {
            get {
                permissions.Clear();
                foreach (CustomRole role in Roles) {
                    permissions.AddRange(role.Permissions);
                }
                return permissions.AsReadOnly();
            }
        }
    }
}

