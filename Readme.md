<!-- default file list -->
*Files to look at*:

* [Company.cs](./CS/DXExample.Module/Company.cs) (VB: [Company.vb](./VB/DXExample.Module/Company.vb))
* [CustomRole.cs](./CS/DXExample.Module/CustomRole.cs) (VB: [CustomRole.vb](./VB/DXExample.Module/CustomRole.vb))
* [CustomUser.cs](./CS/DXExample.Module/CustomUser.cs) (VB: [CustomUser.vb](./VB/DXExample.Module/CustomUser.vb))
* [Updater.cs](./CS/DXExample.Module/Updater.cs) (VB: [Updater.vb](./VB/DXExample.Module/Updater.vb))
<!-- default file list end -->
# OBSOLETE - How to add a property to the standard user class


<p><strong>This example demonstrates a solution for the old security interfaces. You can find an up-to-date solution in our documentation:</strong><strong></strong></p><p><strong></strong><a href="http://documentation.devexpress.com/#Xaf/CustomDocument3384"><strong><u>How to: Implement Custom Security Objects (Users, Roles, Operation Permissions)</u></strong></a><strong><u><br />
</u></strong><a href="http://documentation.devexpress.com/#Xaf/CustomDocument3452"><strong><u>How to: Implement a Custom Security System User Based on an Existing Business Class</u></strong></a><u><br />
</u></p><p>This example demonstrates how to extend the standard user class for the Complex Security (DevExpress.Persistent.BaseImpl.User by default) with the Company property. The simplest way to accomplish this task is to create a descendant from the User class. This class is inherited from the Person class, and if you don't want to use properties from this class in your custom user class, you need to implement the following interfaces yourself: IUserWithRoles, IUser, IAuthenticationActiveDirectoryUser or IAuthenticationStandardUser. As the last solution is more difficult, I've decided to demonstrate how to implement it.<br />
After you created your user class, you need to open the Application Designer (DXExample.Win.WinApplication.cs file), select the SecurityComplex item, and set its UserType property to <SolutionName>.Module.<YourUserClassName>.</p><p><strong>See Also:</strong><br />
<a href="http://documentation.devexpress.com/#Xaf/CustomDocument2768"><u>Apply a Complex Security Strategy</u></a></p>

<br/>


