using System;

using DevExpress.Xpo;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;

namespace DXExample.Module {
    [DefaultClassOptions]
    public class Company : BaseObject {
        public Company(Session session) : base(session) { }
        private string _CompanyName;
        public string CompanyName {
            get { return _CompanyName; }
            set { SetPropertyValue("CompanyName", ref _CompanyName, value); }
        }
        private Address _CompanyAddress;
        public Address CompanyAddress {
            get { return _CompanyAddress; }
            set { SetPropertyValue("CompanyAddress", ref _CompanyAddress, value); }
        }
        private PhoneNumber _PhoneNumber;
        public PhoneNumber PhoneNumber {
            get { return _PhoneNumber; }
            set { SetPropertyValue("PhoneNumber", ref _PhoneNumber, value); }
        }
        [Association("Company-Users")]
        public XPCollection<CustomUser> Users {
            get { return GetCollection<CustomUser>("Users"); }
        }
        
    }

}
