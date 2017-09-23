<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Modificacion_Arancel_Proveedores_Porcentaje_Vip.aspx.cs" Inherits="InterfazWeb.Modificacion_Arancel_Proveedores_Porcentaje_Vip" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ARANCEL PROVEEDORES</p>
    <p>
        Valor Actual
        <asp:TextBox ID="txtArancelActual" runat="server" Width="60px" style="text-align: center"></asp:TextBox>
&nbsp;&nbsp; Nuevo Valor&nbsp;
        <asp:TextBox ID="txtArancelNuevo" runat="server" Width="60px"></asp:TextBox>
&nbsp;
        &nbsp;<asp:Label ID="lblErrorArancel" runat="server" Visible="False"></asp:Label>
    </p>
    <p>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnActualizarArancel" runat="server" Text="Actualizar Arancel" OnClick="btnActualizarArancel_Click" />
    </p>
    <p>
        <asp:Label ID="lblMensajeArancel" runat="server" Text="Label" Visible="False"></asp:Label>
    </p>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; PORCENTAJE VIP<br />
    <br />
    Valor Actual <asp:TextBox ID="txtPorcentajeVipActual" runat="server" Width="60px" style="text-align: center"></asp:TextBox>
&nbsp; Nuevo Valor
    <asp:TextBox ID="txtPorcentajeVipNuevo" runat="server" Width="60px"></asp:TextBox>
&nbsp;
    &nbsp;<asp:Label ID="lblErrorVip" runat="server" Visible="False"></asp:Label>
    <br />
    <br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnPorcentajeVip" runat="server" Text="Actualizar Porcentaje VIP" OnClick="btnPorcentajeVip_Click" />
    <br />
    <br />
    <asp:Label ID="lblMensajeVip" runat="server" Text="Label" Visible="False"></asp:Label>
</asp:Content>
