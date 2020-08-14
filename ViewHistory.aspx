<%@ Page Title="" Language="C#" MasterPageFile="~/CosmeticSale.Master" AutoEventWireup="true" CodeBehind="ViewHistory.aspx.cs" Inherits="CSharpAssignment.ViewHistory" %>

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
                            <asp:LinkButton CssClass="btn btn-primary book-now btn-cart" ID="BtnNewProduct" runat="server">
                                <i class="material-icons"></i>
                                <span>Add New Product</span>
                            </asp:LinkButton>

                        </div>
                    </div>
                </div>
                <asp:Panel ID="CartPanel" runat="server">
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True"
                        CssClass="table table-striped table-hover"
                        AllowSorting="True"
                        AutoGenerateColumns="False"
                        DataSourceID="SqlDataSource1" DataKeyNames="ID"
                        OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                        OnSelectedIndexChanging="GridView1_SelectedIndexChanging" CellPadding="4" GridLines="None">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" />
                            <asp:BoundField ControlStyle-CssClass="hide-grid-column d-none" DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
                            <asp:BoundField DataField="CustomerName" HeaderText="CustomerName" SortExpression="CustomerName" />
                            <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />
                            <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
                            <asp:BoundField DataField="StatusName" HeaderText="Status" SortExpression="StatusName" />
                            <asp:BoundField DataField="ImportDate" HeaderText="ImportDate" SortExpression="ImportDate" />
                            <asp:BoundField DataField="PaymentType" HeaderText="PaymentType" SortExpression="PaymentType" />
                        </Columns>
                    </asp:GridView>

                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:CSharpAssignmentConnectionString %>" SelectCommand="SELECT DISTINCT [ID], [CustomerName], [Address], [Phone], (SELECT Name FROM Status WHERE Booking.StatusID = Status.ID) AS StatusName, [ImportDate], [PaymentType] FROM [Booking] WHERE ([AccountID] = @AccountID)">
                        <SelectParameters>
                            <asp:SessionParameter DefaultValue="" Name="AccountID" SessionField="AccountID" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:DetailsView ID="DetailsView1" runat="server" Height="50px" Width="498px" AllowPaging="True" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" OnPageIndexChanging="DetailsView1_PageIndexChanging">
                            </asp:DetailsView>
                        </div>
                        <div>
                           <%-- Discount Code:&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblDiscountCode" runat="server"></asp:Label>
                            <br />
                            Discount Value:&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblDiscountValue" runat="server"></asp:Label>
                            <br />
                            Total price:
                        <asp:Label ID="lblTotalPrice" runat="server"></asp:Label>
                            <br />--%>
                        </div>
                    </div>

                </asp:Panel>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
