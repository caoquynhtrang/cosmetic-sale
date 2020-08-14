<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadFile.aspx.cs" Inherits="CSharpAssignment.UploadFile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Upload File</title>
    <style type="text/css">
        .auto-style2 {
            width: 400px;
        }
        .auto-style3 {
            width: 350px;
        }
        .auto-style4 {
            width: 400px;
            font-size: large;
            text-align: center;
        }
        .auto-style5 {
            width: 400px;
            text-align: center;
        }
        .auto-style6 {
            font-size: large;
        }
        .auto-style7 {
            width: 371px;
        }
    </style>
</head>
<body>
    <form id="frmUploadFile" runat="server">
        <table border="1" style="width:100%;">
            <tr>
                <td class="auto-style3">
                    <asp:FileUpload ID="fuFile" runat="server" Height="30px" Width="300px" />
                </td>
                <td class="auto-style7">&nbsp;</td>
                <td class="auto-style4"><strong>PREVIEW</strong></td>
            </tr>
            <tr>
                <td class="auto-style3">
                    <asp:Button ID="BtnUpload" runat="server" Font-Bold="True" ForeColor="Blue" Height="30px" OnClick="BtnUpload_Click" Text="Upload File" Width="150px" />
                </td>
                <td class="auto-style7">
                    <asp:Image ID="imgUploadMsg" runat="server" Height="30px" Width="30px" ImageUrl="~/images/icons/none.png" />
                    <asp:Label ID="lblUploadMsg" runat="server" Font-Size="20px"></asp:Label>
                </td>
                <td class="auto-style2">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style3">
                    <asp:ListBox ID="LstFiles" runat="server" AutoPostBack="True" Height="400px" OnSelectedIndexChanged="LstFiles_SelectedIndexChanged" Width="350px"></asp:ListBox>
                </td>
                <td class="auto-style7">&nbsp;</td>
                <td class="auto-style5">
                    <asp:Image ID="imgPreview" runat="server" Height="400px" Width="300px" />
                </td>
            </tr>
            <tr>
                <td class="auto-style3">
                    <asp:Button ID="BtnDelete" runat="server" Font-Bold="True" ForeColor="Red" Height="30px" OnClick="BtnDelete_Click" Text="Delete File" Width="150px" />
                </td>
                <td class="auto-style7">
                    <asp:Image ID="imgDeleteMsg" runat="server" Height="30px" Width="30px" ImageUrl="~/images/icons/none.png" />
                    <asp:Label ID="lblDeleteMsg" runat="server" Font-Size="20px"></asp:Label>
                </td>
                <td class="auto-style5"><span class="auto-style6">Map path: </span> <asp:Label ID="lblMapPath" runat="server" ForeColor="Red" CssClass="auto-style6"></asp:Label>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
