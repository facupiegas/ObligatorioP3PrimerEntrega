<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Alta_Servicio.aspx.cs" Inherits="InterfazWeb.Alta_Servicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
    Alta de un nuevo servicio</p>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<asp:Panel ID="pnlProveedor" runat="server">
    <p>
        Por favor ingrese un Rut. Tenga en cuenta que para agregar un nuevo servicio al Rut ingresado el mismo debe estar registrado en el sistema</p>
    <p>
        Rut:
        <asp:TextBox ID="txtRut" runat="server" MaxLength="15"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRut" ErrorMessage="(*) Campo Obligatorio" ForeColor="Red"></asp:RequiredFieldValidator>
    </p>
    <p>
        &nbsp;
        <asp:Button ID="btnBuscarProveedor" runat="server" Text="Buscar" OnClick="btnBuscarProveedor_Click" />
    </p>
    <p>
        <asp:Label ID="lblMsjProveedor" runat="server"></asp:Label>
    </p>
    <asp:GridView ID="grdServicios" runat="server" AutoGenerateColumns="False" Caption="Servicios del Proveedor" CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
            <asp:BoundField DataField="TipoServicio" HeaderText="Tipo" />
            <asp:ImageField DataImageUrlField="Imagen" HeaderText="Imagen">
                <ControlStyle Height="65px" Width="65px" />
                <ItemStyle Height="65px" Width="65px" />
            </asp:ImageField>
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>
    <p>
        &nbsp;</p>
</asp:Panel>
<asp:Panel ID="pnlNuevoServicio" runat="server" Visible="False">
    <br />
    Nombre:
    <asp:TextBox ID="txtNombreServicio" runat="server" MaxLength="20"></asp:TextBox>
    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtNombreServicio" ErrorMessage="(*) Campo Obligatorio" ForeColor="Red"></asp:RequiredFieldValidator>
    <br />
    <br />
    Descripcion:
    <br />
    <asp:TextBox ID="txtDescripcionServicio" runat="server" Height="16px" MaxLength="150" Width="241px"></asp:TextBox>
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
    <br />
    <br />
    <br />
</asp:Panel>
<p>
</p>
<p>
    &nbsp;</p>
</asp:Content>
