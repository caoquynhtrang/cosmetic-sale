<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SideBar.ascx.cs" Inherits="CSharpAssignment.Components.SideBar" %>
<div class="bg-white border px-4" style="height: 100%; min-height: 92.3vh">
    <div class="logo">
        <a href="#" class="navbar-brand">
            <span class="text-secondary">Ăn Chơi Bỏ Code 's</span><br />
            <b class="text-info">Cosmetic</b>
        </a>
    </div>
    <ul class="nav mt-3 px-2">
        <li class="nav-item"><b>Categories ---</b></li>
        <li class="dashboard"></li>
        <asp:DataList ID="DataList1" runat="server" OnSelectedIndexChanged="DataList1_SelectedIndexChanged" DataKeyField="ID" DataSourceID="SqlDataSource1">
            <ItemTemplate>
                <li class="nav-item">
                    <a class="nav-link" href='ShowProduct.aspx?category=<%# Eval("Name") %>'>
                        <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                    </a>
                </li>

            </ItemTemplate>
        </asp:DataList>
    </ul>
</div>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:CSharpAssignmentConnectionString %>" SelectCommand="SELECT [ID], [Name], [Description] FROM [Category] WHERE ([Name] &lt;&gt; @Name)">
    <SelectParameters>
        <asp:Parameter DefaultValue="-- Select --" Name="Name" Type="String" />
    </SelectParameters>
</asp:SqlDataSource>

