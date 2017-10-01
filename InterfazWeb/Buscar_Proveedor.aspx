<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Buscar_Proveedor.aspx.cs" Inherits="InterfazWeb.Buscar_Proveedor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
    <br />
    RUT
    <asp:TextBox ID="txtRut" runat="server"></asp:TextBox>
&nbsp;
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRut" ErrorMessage="(*) Ingrese un RUT" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:Label ID="lblError" runat="server" ForeColor="Red" Text="Ingrese un RUT Valido" Visible="False"></asp:Label>
</p>
<p>
    <asp:Button ID="btnBuscar" runat="server" Text="Buscar Proveedor" OnClick="btnBuscar_Click" />
</p>
<asp:GridView ID="grdProveedor" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" style="text-align: center">
    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
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
    <br />
        <asp:GridView ID="grdServicios" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False">
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
    <br />
<asp:Label ID="lblMensaje" runat="server" Visible="False"></asp:Label>
<br />
</asp:Content>
