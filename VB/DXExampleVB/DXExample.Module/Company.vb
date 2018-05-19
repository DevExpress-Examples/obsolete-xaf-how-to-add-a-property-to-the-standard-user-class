Imports Microsoft.VisualBasic
Imports System

Imports DevExpress.Xpo

Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation

Namespace DXExample.Module
	<DefaultClassOptions> _
	Public Class Company
		Inherits BaseObject
		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub
		Private _CompanyName As String
		Public Property CompanyName() As String
			Get
				Return _CompanyName
			End Get
			Set(ByVal value As String)
				SetPropertyValue("CompanyName", _CompanyName, value)
			End Set
		End Property
		Private _CompanyAddress As Address
		Public Property CompanyAddress() As Address
			Get
				Return _CompanyAddress
			End Get
			Set(ByVal value As Address)
				SetPropertyValue("CompanyAddress", _CompanyAddress, value)
			End Set
		End Property
		Private _PhoneNumber As PhoneNumber
		Public Property PhoneNumber() As PhoneNumber
			Get
				Return _PhoneNumber
			End Get
			Set(ByVal value As PhoneNumber)
				SetPropertyValue("PhoneNumber", _PhoneNumber, value)
			End Set
		End Property
		<Association("Company-Users")> _
		Public ReadOnly Property Users() As XPCollection(Of CustomUser)
			Get
				Return GetCollection(Of CustomUser)("Users")
			End Get
		End Property

	End Class

End Namespace
