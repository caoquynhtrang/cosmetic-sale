<%@ Page Title="" Language="C#" MasterPageFile="~/CosmeticSale.Master" AutoEventWireup="true" CodeBehind="Coupon.aspx.cs" Inherits="CSharpAssignment.Coupon" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel CssClass="table-container" ID="Panel2" runat="server">
        <div class="container">
            <div class="table-wrapper">
                <div class="table-title">
                    <div class="row">
                        <div class="col-sm-5">
                            <h2>Discount <b>Management</b>
                            </h2>
                        </div>
                        <%--<div class="col-sm-7">
                            <a href="home" class="btn btn-primary btn-cart">
                                <i class="material-icons"></i>
                                <span>Add New Product</span>
                            </a>
                            <a href="view-booking-history" class="btn btn-primary btn-cart">
                                <span class="material-icons">history_edu
                                </span>
                                <span>Booking History</span>
                            </a>
                            <button class="btn btn-primary book-now btn-cart" data-toggle="modal" data-target="#exampleModal">
                                <i class="material-icons">event_available
                                </i>
                                <span>Check out</span>
                            </button>
                        </div>--%>
                    </div>
                </div>
                <asp:Panel ID="CartPanel" runat="server">
                    <asp:GridView  CssClass="table table-striped table-hover"  ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="SqlDataSource1" ShowFooter="True">
                        <Columns>
                            <asp:CommandField ShowEditButton="True" />
                            <asp:TemplateField HeaderText="ID" SortExpression="ID">
                                <EditItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ValidationGroup="INSERT" ID="lbInsert" runat="server" OnClick="LbInSert_Click">Insert</asp:LinkButton>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Code" SortExpression="Code">
                                <EditItemTemplate>
                                    <asp:TextBox CssClass="width-120" ID="TextBox1" runat="server" Text='<%# Bind("Code") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvEditCode" runat="server" ErrorMessage="Code is a required field"
                                        ControlToValidate="TextBox1" Text="*" ForeColor="Red">
                                    </asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("Code") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox CssClass="width-120" ID="txtCode" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ValidationGroup="INSERT" ID="rfvInsertCode" runat="server" ErrorMessage="Code is a required field"
                                        ControlToValidate="txtCode" Text="*" ForeColor="Red">
                                    </asp:RequiredFieldValidator>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Value" SortExpression="Value">
                                <EditItemTemplate>
                                    <asp:TextBox CssClass="width-120" ID="TextBox2" runat="server" Text='<%# Bind("Value") %>' TextMode="Number"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvEditValue" runat="server" ErrorMessage="Value is a required field"
                                        ControlToValidate="TextBox2" Text="*" ForeColor="Red">
                                    </asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("Value") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox CssClass="width-120" ID="txtValue" runat="server" TextMode="Number"></asp:TextBox>
                                    <asp:RequiredFieldValidator ValidationGroup="INSERT" ID="rfvInsertValue" runat="server" ErrorMessage="Value is a required field"
                                        ControlToValidate="txtValue" Text="*" ForeColor="Red">
                                    </asp:RequiredFieldValidator>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ImportDate" SortExpression="ImportDate">
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("ImportDate") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status" SortExpression="StatusID">
                                <EditItemTemplate>
                                    <asp:DropDownList DataSourceID="SqlDataSource2" DataValueField="ID" DataTextField="Name" ID="DropDownList1" runat="server" SelectedValue='<%# Bind("StatusID") %>'>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("StatusName") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox CssClass="width-120 d-none" ID="txtStatusID" runat="server" Enabled="false">1</asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ExpiredDate" SortExpression="ExpiredDate">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("ExpiredDate") %>' type="date"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvEditExpiredDate" runat="server" ErrorMessage="Expired Date is a required field"
                                        ControlToValidate="TextBox4" Text="*" ForeColor="Red">
                                    </asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("ExpiredDate") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox CssClass="mt-3" ID="txtExpiredDate" runat="server" type="date"></asp:TextBox>
                                    <asp:RequiredFieldValidator ValidationGroup="INSERT" ID="rfvInsertExpiredDate" runat="server" ErrorMessage="Expired Date is a required field"
                                        ControlToValidate="txtExpiredDate" Text="*" ForeColor="Red">
                                    </asp:RequiredFieldValidator>
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:ValidationSummary ValidationGroup="INSERT" ID="ValidationSummary1" ForeColor="Red" runat="server" />
                    <asp:ValidationSummary ID="ValidationSummary2" ForeColor="Red" runat="server" />
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:CSharpAssignmentConnectionString %>"
                        DeleteCommand="DELETE FROM [Discount] WHERE [ID] = @ID"
                        InsertCommand="INSERT INTO [Discount] ([Code], [Value], [ImportDate], [StatusID], [ExpiredDate]) VALUES (@Code, @Value, @ImportDate, @StatusID, @ExpiredDate)"
                        SelectCommand="SELECT [ID], [Code], [Value], [ImportDate], (SELECT Name FROM Status WHERE ID = [StatusID] ) AS StatusName, [StatusID], [ExpiredDate] FROM [Discount] WHERE [ID] &gt; 0"
                        UpdateCommand="UPDATE [Discount] SET [Code] = @Code, [Value] = @Value, [ImportDate] = @ImportDate, [StatusID] = @StatusID, [ExpiredDate] = @ExpiredDate WHERE [ID] = @ID">
                        <DeleteParameters>
                            <asp:Parameter Name="ID" Type="Int32" />
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="Code" Type="String" />
                            <asp:Parameter Name="Value" Type="Double" />
                            <asp:Parameter Name="ImportDate" Type="DateTime" />
                            <asp:Parameter Name="StatusID" Type="Int32" />
                            <asp:Parameter Name="ExpiredDate" Type="DateTime" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="Code" Type="String" />
                            <asp:Parameter Name="Value" Type="Double" />
                            <asp:Parameter Name="ImportDate" Type="DateTime" />
                            <asp:Parameter Name="StatusID" Type="Int32" />
                            <asp:Parameter Name="ExpiredDate" Type="DateTime" />
                            <asp:Parameter Name="ID" Type="Int32" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:CSharpAssignmentConnectionString %>" SelectCommand="SELECT [ID], [Name] FROM [Status] WHERE ID <= 2"></asp:SqlDataSource>
                </asp:Panel>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
