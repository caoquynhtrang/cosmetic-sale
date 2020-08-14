<%@ Page Title="" Language="C#" MasterPageFile="~/CosmeticSale.Master" AutoEventWireup="true" CodeBehind="ManageBookings.aspx.cs" Inherits="CSharpAssignment.ManageBookings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel CssClass="table-container" ID="Panel1" runat="server">
        <div class="container">
            <div class="table-wrapper">
                <div class="table-title">
                    <div class="row">
                        <div class="col-sm-5">
                            <h2>Booking <b>Management</b>
                            </h2>
                        </div>
                        <div class="col-sm-7">
                            
                            <!--<a href="home" class="btn btn-primary btn-cart">
                                <i class="material-icons"></i>
                                <span>Add New Product</span>
                            </a>
                            <a href="view-booking-history" class="btn btn-primary btn-cart">
                                <span class="material-icons">history_edu
                                </span>
                                <span>Booking History</span>
                            </a>-->
                            <asp:LinkButton CssClass="btn btn-primary book-now btn-cart" ID="BtnNewProduct" runat="server" OnClick="BtnNewProduct_Click">
                                <i class="material-icons"></i>
                                <span>Add New Product</span>
                            </asp:LinkButton>
                            
                        </div>
                    </div>
                </div>
                <asp:Panel ID="CartPanel" runat="server">
                    <asp:GridView CssClass="table table-striped table-hover" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="ID" DataSourceID="CSharpAssignment" ForeColor="#333333" GridLines="Vertical" ID="GvProducts" PageSize="5" OnSelectedIndexChanged="GvProducts_SelectedIndexChanged">
                        <Columns>
                            <asp:CommandField ControlStyle-CssClass="btn btn-success text-white" ShowSelectButton="True">
                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                            </asp:CommandField>
                            <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" SortExpression="ID">
                                <ItemStyle Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name">
                                <ItemStyle Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity">
                                <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price">
                                <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Usage" HeaderText="Usage" SortExpression="Usage" />
                            <asp:BoundField DataField="CateID" HeaderText="CateID" SortExpression="CateID" HeaderStyle-CssClass="hide-grid-column" ItemStyle-CssClass="hide-grid-column"></asp:BoundField>
                            <asp:BoundField DataField="Category" HeaderText="Category" SortExpression="Category">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="StatusID" HeaderText="StatusID" SortExpression="StatusID" HeaderStyle-CssClass="hide-grid-column" ItemStyle-CssClass="hide-grid-column"></asp:BoundField>
                            <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>

                            <asp:BoundField DataField="ImageLink" HeaderStyle-CssClass="hide-grid-column" ItemStyle-CssClass="hide-grid-column"></asp:BoundField>

                            <asp:ImageField ControlStyle-CssClass="image-table" DataImageUrlField="ImageLink" HeaderText="ImageLink"></asp:ImageField>


                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </div>
            <!--
                            <asp:BoundField DataField="ImageLink" HeaderText="ImageLink" SortExpression="ImageLink">
                                <ItemStyle Width="200px" />
                                 <img src='<%# Eval("ImageLink") %>' alt="image" height="50" />
                            </asp:BoundField>-->
        </div>
    </asp:Panel>

    <a href="#myModal" class="btn btn-lg btn-primary" data-toggle="modal">Launch Demo Modal</a>
    <div id="myModal" class="modal fade" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Create New Product</h5>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body p-3">
                    <div class="row">
                        <div class="col-6">
                            <div class="form-group">
                                <label>ID:</label>
                                <asp:TextBox ID="txtId" runat="server" Enabled="False"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="form-group">
                                <label>Product Name:</label>
                                <asp:Label ID="lblName" runat="server" ForeColor="Red"></asp:Label>
                                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-6">
                            <div class="form-group">
                                <label>Quantity:</label>
                                <asp:Label ID="lblQuantity" runat="server" ForeColor="Red"></asp:Label>
                                <asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="form-group">
                                <label>Price:</label>
                                <asp:Label ID="lblPrice" runat="server" ForeColor="Red"></asp:Label>
                                <asp:TextBox ID="txtPrice" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-6">
                            <div class="form-group">
                                <label>Usage:</label>
                                <asp:Label ID="lblUsage" runat="server" ForeColor="Red"></asp:Label>
                                <asp:TextBox ID="txtUsage" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="form-group">
                                <label>Category:</label>
                                <asp:DropDownList ID="ddlCategory" runat="server" DataSourceID="CShartAssignmentCategory" DataTextField="Name" DataValueField="ID">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-6">
                            <div class="form-group">
                                <label>Status:</label>

                                <asp:DropDownList ID="ddlStatus" runat="server">
                                    <asp:ListItem Selected="True" Value="1">Active</asp:ListItem>
                                    <asp:ListItem Value="2">Inactive</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Image ID="imgPreview" runat="server" Width="150px" />
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="form-group">
                                <label>ImageLink:</label>
                                <asp:TextBox ID="txtImageLink" runat="server"></asp:TextBox>
                                <asp:FileUpload ID="fuImage" runat="server" />
                                <asp:Button ID="BtnPreview" runat="server" OnClick="BtnPreview_Click" Text="Preview" />
                                <asp:Button ID="BtnNew" runat="server" OnClick="BtnNew_Click" Text="New" />
                                <asp:Button ID="BtnInsert" runat="server" Text="Insert" OnClick="BtnInsert_Click" OnClientClick="return confirm('Are you sure?')" />
                                <asp:Button ID="BtnUpdate" runat="server" Text="Update" OnClick="BtnUpdate_Click" OnClientClick="return confirm('Are you sure?')" />
                                <asp:Button ID="BtnDelete" runat="server" Text="Delete" OnClick="BtnDelete_Click" OnClientClick="return confirm('Are you sure?')" />
                                <asp:Label ID="lblMessage" CssClass="d-block" runat="server" ForeColor="Red"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
                    <button type="button" class="btn btn-primary">Submit</button>
                    <asp:TextBox CssClass="hide-grid-column" ID="TxtOk" runat="server"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>


    <asp:SqlDataSource ID="CSharpAssignment" runat="server" ConnectionString="<%$ ConnectionStrings:CSharpAssignmentConnectionString %>" SelectCommand="SELECT Product.ID, Product.Name, Product.Quantity, Product.Price, Product.Usage, Product.CateID, Category.Name AS Category, Product.StatusID, Status.Name AS Status, Product.ImageLink FROM Category INNER JOIN Product ON Category.ID = Product.CateID INNER JOIN Status ON Product.StatusID = Status.ID"></asp:SqlDataSource>
    <asp:SqlDataSource ID="CShartAssignmentCategory" runat="server" ConnectionString="<%$ ConnectionStrings:CSharpAssignmentConnectionString %>" SelectCommand="SELECT [ID], [Name] FROM [Category]"></asp:SqlDataSource>
</asp:Content>
