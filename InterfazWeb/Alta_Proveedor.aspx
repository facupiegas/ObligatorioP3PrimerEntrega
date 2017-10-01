<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Alta_Proveedor.aspx.cs" Inherits="InterfazWeb.Alta_Proveedor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        ALTA DE NUEVOS PROVEEDORES:</p>
    <p>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:HyperLink ID="lnkMenuPrincipal" runat="server" NavigateUrl="~/Login.aspx">Volver al menu principal</asp:HyperLink>
        &nbsp;</p>
    <asp:Panel ID="pnlProveedor" runat="server">
        <p>
            Tras completar la informacion requerida se le solicitara que agregue un servicio</p>
        <p>
        </p>
        <p>
            Nombre de Usuario:
            <asp:TextBox ID="txtNomUsuario" runat="server" MaxLength="20"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNomUsuario" ErrorMessage="(*) Campo Obligatorio" ForeColor="Red"></asp:RequiredFieldValidator>
        </p>
        <p>
            Contraseña:
            <asp:TextBox ID="txtPass" runat="server" MaxLength="20"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPass" ErrorMessage="(*) Campo Obligatorio" ForeColor="Red"></asp:RequiredFieldValidator>
        </p>
        <p>
            RUT:
            <asp:TextBox ID="txtRut" runat="server" MaxLength="15"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRut" ErrorMessage="(*) Campo Obligatorio" ForeColor="Red"></asp:RequiredFieldValidator>
        </p>
        <p>
            Nombre Fantasía:
            <asp:TextBox ID="txtNomFantasia" runat="server" MaxLength="30"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtNomFantasia" ErrorMessage="(*) Campo Obligatorio" ForeColor="Red"></asp:RequiredFieldValidator>
        </p>
        <p>
            Email:
            <asp:TextBox ID="txtEmail" runat="server" MaxLength="30"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEmail" ErrorMessage="(*) Campo Obligatorio" ForeColor="Red"></asp:RequiredFieldValidator>
        </p>
        <p>
            Teléfono:
            <asp:TextBox ID="txtTel" runat="server" MaxLength="10"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtTel" ErrorMessage="(*) Campo Obligatorio" ForeColor="Red"></asp:RequiredFieldValidator>
        </p>
        <p>
            Vip
            <asp:RadioButton ID="radVipN" runat="server" Text="NO" />
            <asp:RadioButton ID="radVipS" runat="server" GroupName="vip" Text="SI" />
            &nbsp;
            <asp:Label ID="lbErrorlVip" runat="server" Visible="False" ForeColor="Red"></asp:Label>
        </p>
        <p>
            <asp:Button ID="btnGuardar" runat="server" OnClick="btnGuardar_Click" Text="Agregar Proveedor" />
        </p>
        <p>
            <asp:Label ID="lblMensaje" runat="server" Visible="False"></asp:Label>
        </p>
    </asp:Panel>
    <br />
    <asp:Panel ID="pnlServicio" runat="server" Visible="False">
        Los datos ingresados fueron guardados correctamente.<br /> Para finalizar agregue un servicio por favor<br />
        <br />
        <br />
        Nombre:
        <asp:TextBox ID="txtNombreServicio" runat="server" MaxLength="20"></asp:TextBox>
        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtNombreServicio" ErrorMessage="(*) Campo Obligatorio" ForeColor="Red"></asp:RequiredFieldValidator>
        <br />
        <br />
        Descripcion:
        <br />
        <asp:TextBox ID="txtDescripcionServicio" runat="server" Height="16px" Width="241px" MaxLength="150"></asp:TextBox>
        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtDescripcionServicio" ErrorMessage="(*) Campo Obligatorio" ForeColor="Red"></asp:RequiredFieldValidator>
        <br />
        <br />
        Imagen:
        <asp:FileUpload ID="fupImagenServicio" runat="server" />
        &nbsp;&nbsp;
        <asp:ImageButton ID="btnUpload" runat="server" Height="31px" ImageUrl="~/img/upload.jpg" OnClick="btnUpload_Click" Width="28px" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <br />
        <asp:Label ID="lblErrorFoto" runat="server" ForeColor="Red"></asp:Label>
        <br />
        <br />
        <asp:Image ID="imgImagenServicio" runat="server" Height="100px" Width="100px" />
        <br />
        <br />
        Tipo Servicio:
        <asp:DropDownList ID="ddlTipoServicios" runat="server">
        </asp:DropDownList>
        <br />
        <br />
        <asp:Button ID="btnServicio" runat="server" OnClick="btnServicio_Click" Text="Agregar Servicio" />
        <br />
        <br />
        <asp:Label ID="lblMsjServicio" runat="server"></asp:Label>
    </asp:Panel>
    <br />
    <br />
</asp:Content>
