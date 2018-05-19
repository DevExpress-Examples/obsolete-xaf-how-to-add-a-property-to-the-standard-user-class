Imports Microsoft.VisualBasic
Imports System

Imports DevExpress.Xpo

Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation
Imports DevExpress.Persistent.Base.Security
Imports System.Collections.Generic
Imports System.Security

Namespace DXExample.Module
	<DefaultClassOptions> _
	Public Class CustomUser
		Inherits BaseObject
		Implements IUserWithRoles, IAuthenticationActiveDirectoryUser, IAuthenticationStandardUser
		Private _Company As Company
		<Association("Company-Users")> _
		Public Property Company() As Company
			Get
				Return _Company
			End Get
			Set(ByVal value As Company)
				SetPropertyValue("Company", _Company, value)
			End Set
		End Property
		Private _userName As String
		Private _storedPassword As String
		Private _isActive As Boolean = True
		Private _changePasswordAfterLogon As Boolean = False
		Private permissions_Renamed As List(Of IPermission)
		Public Sub New(ByVal session As Session)
			MyBase.New(session)
			permissions_Renamed = New List(Of IPermission)()
		End Sub
        Public Sub ReloadPermissions() Implements IUserWithRoles.ReloadPermissions
            Roles.Reload()
            For Each role As CustomRole In Roles
                role.PersistentPermissions.Reload()
            Next role
        End Sub
        Public Function ComparePassword(ByVal password As String) As Boolean Implements IAuthenticationStandardUser.ComparePassword
            Return New PasswordCryptographer().AreEqual(Me._storedPassword, password)
        End Function
        Public Sub SetPassword(ByVal password As String) Implements IAuthenticationStandardUser.SetPassword
            Me._storedPassword = New PasswordCryptographer().GenerateSaltedPassword(password)
        End Sub
#If MediumTrust Then
		<Browsable(False), EditorBrowsable(EditorBrowsableState.Never), Persistent> _
		Public Property StoredPassword() As String
			Get
				Return Me._storedPassword
			End Get
			Set(ByVal value As String)
				Me._storedPassword = value
				OnChanged("StoredPassword")
			End Set
		End Property
#Else
		<Persistent> _
		Private Property StoredPassword() As String
			Get
				Return Me._storedPassword
			End Get
			Set(ByVal value As String)
				Me._storedPassword = value
				OnChanged("StoredPassword")
			End Set
		End Property
#End If
		<Association("User-Role")> _
		Public ReadOnly Property Roles() As XPCollection(Of CustomRole)
			Get
				Return GetCollection(Of CustomRole)("Roles")
			End Get
		End Property
		Private ReadOnly Property IUserWithRoles_Roles() As IList(Of IRole) Implements IUserWithRoles.Roles
			Get
				Return New ListConverter(Of IRole, CustomRole)(Roles)
			End Get
		End Property
        <RuleRequiredField("Fill User Name", "Save", "The user name must not be empty"), RuleUniqueValue("Unique User Name", "Save", "The login with the entered UserName was already registered within the system")> _
        Public Property UserName() As String Implements IAuthenticationActiveDirectoryUser.UserName
            Get
                Return Me._userName
            End Get
            Set(ByVal value As String)
                Me._userName = value
                OnChanged("UserName")
            End Set
        End Property
        Public ReadOnly Property IUser_UserName() As String Implements IUser.UserName
            Get
                Return Me._userName
            End Get
        End Property
        Public ReadOnly Property IAuthentication_UserName() As String Implements IAuthenticationStandardUser.UserName
            Get
                Return Me._userName
            End Get
        End Property
        Public Property ChangePasswordOnFirstLogon() As Boolean Implements IAuthenticationStandardUser.ChangePasswordOnFirstLogon
            Get
                Return Me._changePasswordAfterLogon
            End Get
            Set(ByVal value As Boolean)
                Me._changePasswordAfterLogon = value
                OnChanged("ChangePasswordOnFirstLogon")
            End Set
        End Property
        Public Property IsActive() As Boolean Implements IUser.IsActive
            Get
                Return Me._isActive
            End Get
            Set(ByVal value As Boolean)
                Me._isActive = value
                OnChanged("IsActive")
            End Set
        End Property
        Public ReadOnly Property Permissions() As IList(Of IPermission) Implements IUser.Permissions
            Get
                permissions_Renamed.Clear()
                For Each role As CustomRole In Roles
                    permissions_Renamed.AddRange(role.Permissions)
                Next role
                Return permissions_Renamed.AsReadOnly()
            End Get
        End Property
    End Class
End Namespace

