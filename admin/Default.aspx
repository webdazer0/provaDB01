<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ProvaDB.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:SqlDataSource ID="dbc" runat="server"></asp:SqlDataSource>

            <div>
                <label>Nome</label>
                <asp:TextBox ID="txtNome" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                    ControlToValidate="txtNome"
                    runat="server" ErrorMessage="Nome Obbligatorio"></asp:RequiredFieldValidator>
            </div>
            <div>
                <label>Cognome</label>
                <asp:TextBox ID="txtCognome" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" 
                    ControlToValidate="txtCognome"
                    runat="server" ErrorMessage="Cognome Obbligatorio"></asp:RequiredFieldValidator>
            </div>

            <asp:Button ID="btnSalva" OnClick="btnSalva_Click" runat="server" Text="Salva" />
        </div>
    </form>
</body>
</html>
