<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchBar.ascx.cs" Inherits="CSharpAssignment.Components.SearchBar" %>

<div class="ml-5 navbar-form d-flex align-items-center">
    <div class="input-group search-box">
        <asp:TextBox placeholder="Search by name" CssClass="form-control" ID="TxtSearchName" runat="server" OnTextChanged="TxtSearchName_TextChanged"></asp:TextBox>
        <span class="input-group-addon"><i class="material-icons"></i></span>
    </div>
    <div class="dropdown">
        <a href="#" data-toggle="dropdown" class="px-1 ml-1 btn" aria-expanded="false">
            <span class="material-icons text-warning">filter_alt
            </span>
        </a>
        <div class="dropdown-menu action-form">
            <asp:Label ID="Label4" runat="server" Text="Label">
                    <span class="material-icons">
                        phonelink
                    </span>
                    Product name:
            </asp:Label>
            <asp:TextBox CssClass="form-control mb-1" ID="TxtName" runat="server"></asp:TextBox>
            <asp:Label ID="Label1" runat="server" Text="Label">
                <span class="material-icons">
                    category
                </span>
                Categories:
            </asp:Label>
            <asp:DropDownList CssClass="custom-select mb-1" ID="CbCategory" runat="server" DataSourceID="SqlDataSourceSearch" DataTextField="Name" DataValueField="Name">
                <asp:ListItem Value="">-- Select --</asp:ListItem>
            </asp:DropDownList>
            <div>
                <asp:Label ID="Label2" runat="server" Text="Label">
                    <span class="material-icons">
                        payments
                    </span>
                    From (price):
                </asp:Label>
                <asp:TextBox CssClass="form-control mb-1" ID="TxtFromPrice" runat="server"></asp:TextBox>
                <asp:Label ID="Label3" runat="server" Text="Label">
                    <span class="material-icons">
                        payments
                    </span>
                    To (price):
                </asp:Label>
                <asp:TextBox CssClass="form-control mb-1" ID="TxtToPrice" runat="server"></asp:TextBox>
            </div>
            <asp:Button CssClass="btn btn-info mt-2" ID="BtnSearch" runat="server" Text="Search" OnClick="BtnSearch_Click" />

            <br />
            <asp:SqlDataSource ID="SqlDataSourceSearch" runat="server" ConnectionString="<%$ ConnectionStrings:CSharpAssignmentConnectionString %>" SelectCommand="SELECT [ID], [Name], [Description] FROM [Category]"></asp:SqlDataSource>
        </div>
    </div>
</div>

