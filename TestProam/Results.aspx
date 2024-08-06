<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Results.aspx.cs" Inherits="TestProam.Results" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Resultados</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btnProyectoProductos" runat="server" Text="Ver proyecto y sus productos (PREMIA)" OnClick="btnProyectoProductos_Click" />
            <asp:Button ID="btnMensaje" runat="server" Text="Ver Mensajes por Proyecto y Producto" OnClick="btnMensaje_Click" />
            <asp:Button ID="btnMensajeTodos" runat="server" Text="Ver Mensajes con Productos 'TODOS'" OnClick="btnMensajeTodos_Click" />
            <asp:Button ID="btnLeerXML" runat="server" Text="Leer Archivo XML" OnClick="btnLeerXML_Click" />
            <asp:GridView ID="gvResultados" runat="server"></asp:GridView>
        </div>
    </form>
</body>
</html>