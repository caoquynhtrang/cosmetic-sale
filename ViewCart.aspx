<%@ Page Title="" Language="C#" MasterPageFile="~/CosmeticSale.Master" AutoEventWireup="true" CodeBehind="ViewCart.aspx.cs" Inherits="CSharpAssignment.ViewCart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel CssClass="table-container" ID="Panel2" runat="server">
        <div class="container">
            <div class="table-wrapper">
                <div class="table-title">
                    <div class="row">
                        <div class="col-sm-5">
                            <h2 class="text-white">View <b>Cart</b>
                            </h2>
                        </div>
                        <div class="col-sm-7">
                            <a href="ShowProduct.aspx" class="btn btn-primary btn-cart">
                                <i class="material-icons"></i>
                                <span>Add New Product</span>
                            </a>
                            <a href="ViewHistory.aspx" class="btn btn-primary btn-cart">
                                <span class="material-icons">history_edu
                                </span>
                                <span>Booking History</span>
                            </a>
                            <asp:LinkButton class="btn btn-primary book-now btn-cart" ID="LinkButton1" runat="server" data-toggle="modal" data-target="#myModal">
                                <i class="material-icons">event_available
                                </i>
                                <span>Check out</span>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <asp:Panel ID="CartPanel" runat="server">
                    <asp:Panel class="mb-3 alert alert-success" ID="PanelMsgg" runat="server">
                        <span><asp:Literal ID="Literal1" runat="server"></asp:Literal></span>
                    </asp:Panel>
                    <asp:Panel class="mb-3 alert alert-danger" ID="PanelErrMsgg" runat="server">
                        <span><asp:Literal ID="LblErr" runat="server"></asp:Literal></span>
                    </asp:Panel>
                    
                    <asp:GridView CssClass="table table-striped table-hover" ID="GridItemCart" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowEditing="GridItemCart_RowEditing" OnRowUpdating="GridItemCart_RowUpdating" OnRowDataBound="GridItemCart_RowDataBound">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <%--<asp:CheckBoxField ID="chkID" runat="server" />--%>
                            <asp:ButtonField ControlStyle-CssClass="btn btn-danger" ButtonType="Button" CommandName="Edit" Text="Delete" HeaderText="Update">
                                <ControlStyle CssClass="btn btn-danger" />
                            </asp:ButtonField>
                            <asp:ButtonField ControlStyle-CssClass="btn btn-warning" ButtonType="Button" CommandName="Update" Text="Update" HeaderText="Update">
                                <ControlStyle CssClass="btn btn-warning" />
                            </asp:ButtonField>
                            <asp:TemplateField HeaderText="New Quanity">
                                <ItemTemplate>
                                    <asp:TextBox CssClass="form-control" ID="txtQuanity" runat="server" Text='<%# Bind("Quantity") %>'>
                                    </asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <table class="table table-striped table-hover">
                        <thead>
                            <tr>
                                <th></th>
                                <th></th>
                                <th></th>
                                <th></th>
                                <th></th>
                                <th></th>
                                <th></th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Panel ID="DiscountPanel" runat="server">
                                <tr>
                                    <td>
                                        <asp:Button CssClass="btn btn-primary" ID="BtnApply" runat="server" Text="Apply" OnClick="BtnApply_Click" />

                                    </td>
                                    <td>
                                        <asp:DropDownList Width="150" ID="DropdownListDiscount" runat="server" CssClass="custom-select">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand='Select ID, Code, Value from Discount D where ExpiredDate &gt;= CURRENT_TIMESTAMP and StatusID = 1 '
                                            ConnectionString="<%$ ConnectionStrings:CSharpAssignmentConnectionString %>"></asp:SqlDataSource>
                                    </td>
                                    <td>
                                        <asp:Literal ID="litDiscount" runat="server"></asp:Literal>
                                    </td>
                                    <td colspan="3">
                                        
                                    </td>
                                    <td><b>Discount:</b></td>
                                    <td>
                                        <b><asp:Label ID="PriceDiscount" runat="server"></asp:Label></b>
                                    </td>
                                </tr>
                            </asp:Panel>
                            <tr>
                                <td colspan="8">
                                    <asp:Panel ID="PleaseLoginPanel" runat="server">
                                        <p class="alert alert-warning">Please login to achieve Discount!!!</p>
                                    </asp:Panel>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="6"></td>
                                <td><b>Total Price:</b></td>
                                <td>
                                    <b>
                                        <asp:Label ID="lblTotalAmount" runat="server"></asp:Label>

                                    </b>
                                </td>
                            </tr>
                        </tbody>
                    </table>

                </asp:Panel>
                <asp:Panel ID="EmptyPanel" runat="server">
                    <div class="text-center d-flex align-items-center justify-content-center text-secondary">

                        <span style="font-size: 2em">Your cart is empty!!
                        </span>
                    </div>
                </asp:Panel>
            </div>

        </div>

    </asp:Panel>
    <div id="myModal" class="modal fade" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Checkout Form</h5>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body p-3">
                    <asp:Panel ID="Panel1" runat="server">
                        
                        
                        <div style="margin-top: 16px">
                            <b>USER INFORMATION</b>
                        </div>
                        <div style="margin-top: 16px">

                            Customer Name:
                                     <asp:TextBox CssClass="form-control" ID="txtCusName" runat="server"></asp:TextBox>
                            <asp:Literal ID="litCusName" runat="server"></asp:Literal>
                        </div>
                        <div style="margin-top: 16px">
                            Phone Number:
                                         <asp:TextBox CssClass="form-control" ID="txtPhoneNumber" runat="server"></asp:TextBox>
                            <asp:Literal ID="litPhone" runat="server"></asp:Literal>
                        </div>
                        <div style="margin-top: 16px">
                            Address:
                                         <asp:TextBox CssClass="form-control" ID="txtAddress" runat="server"></asp:TextBox>
                            <asp:Literal ID="litAddress" runat="server"></asp:Literal>

                        </div>
                        <div style="margin-top: 16px">
                            Contact Person:
                                             <asp:TextBox CssClass="form-control" ID="txtContactPerson" runat="server"></asp:TextBox>
                            <asp:Literal ID="litContactPerson" runat="server"></asp:Literal>
                        </div>
                        <div style="margin-top: 16px">
                            Description:
                                <asp:TextBox CssClass="form-control" ID="txtDescription" runat="server"></asp:TextBox>
                            <asp:Literal ID="litDescription" runat="server"></asp:Literal>
                        </div>
                        <div style="margin-top: 16px">
                            Payment Type:
                                <asp:DropDownList ID="chkPayment" runat="server" Width="157px" CssClass="custom-select">
                                    <asp:ListItem>Cash</asp:ListItem>
                                </asp:DropDownList>
                            <asp:Literal ID="litPayment" runat="server"></asp:Literal>
                        </div>

                    </asp:Panel>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
                    <asp:Button ControlStyle-CssClass="btn btn-success" ID="btnCheckOut" runat="server" Text="Check out" OnClick="btnCheckOut_Click1" />
                    <asp:TextBox CssClass="hide-grid-column" ID="TxtOk" runat="server"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>




</asp:Content>
