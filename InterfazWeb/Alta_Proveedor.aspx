<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Alta_Proveedor.aspx.cs" Inherits="InterfazWeb.Alta_Proveedor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        ALTA DE NUEVOS PROVEEDORES:</p>
    <p>
        Nombre de Usuario:
        <asp:TextBox ID="txtNomUsuario" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNomUsuario" ErrorMessage="(*) Campo Obligatorio" ForeColor="Red"></asp:RequiredFieldValidator>
    </p>
    <p>
        Contraseña:
        <asp:TextBox ID="txtPass" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPass" ErrorMessage="(*) Campo Obligatorio" ForeColor="Red"></asp:RequiredFieldValidator>
    </p>
    <p>
        RUT:
        <asp:TextBox ID="txtRut" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRut" ErrorMessage="(*) Campo Obligatorio" ForeColor="Red"></asp:RequiredFieldValidator>
    </p>
    <p>
        Nombre Fantasía:
        <asp:TextBox ID="txtNomFantasia" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtNomFantasia" ErrorMessage="(*) Campo Obligatorio" ForeColor="Red"></asp:RequiredFieldValidator>
    </p>
    <p>
        Email:
        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEmail" ErrorMessage="(*) Campo Obligatorio" ForeColor="Red"></asp:RequiredFieldValidator>
    </p>
    <p>
        Teléfono: <asp:TextBox ID="txtTel" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtTel" ErrorMessage="(*) Campo Obligatorio" ForeColor="Red"></asp:RequiredFieldValidator>
    </p>
    <p>
        Vip <asp:RadioButton ID="radVipN" runat="server" Text="NO" />
        <asp:RadioButton ID="radVipS" runat="server" GroupName="vip" Text="SI" />
&nbsp;
        <asp:Label ID="lbErrorlVip" runat="server" Visible="False" ForeColor="Red"></asp:Label>
    </p>
    <p>
        <asp:Button ID="btnGuardar" runat="server" Text="Crear" OnClick="btnGuardar_Click" />
    </p>
    <p>
        <asp:Label ID="lblMensaje" runat="server" Visible="False"></asp:Label>
    </p>
</asp:Content>
