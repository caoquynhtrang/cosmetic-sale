<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="CSharpAssignment.Header" %>
<%@ Reference Control="~/Components/SearchBar.ascx" %>
<nav class="navbar navbar-expand-lg navbar-light bg-light">

    <button type="button" class="navbar-toggler" data-toggle="collapse" data-target="#navbarCollapse">
        <span class="navbar-toggler-icon"></span>
    </button>
    <!-- Collection of nav links, forms, and other content for toggling -->
    <div id="navbarCollapse" class="collapse navbar-collapse justify-content-between px-4">
        <div class="navbar-nav">
            <a href="HomePage.aspx" class="nav-item nav-link">Home</a>
            <div class="nav-item dropdown">
                <a href="#" data-toggle="dropdown" class="nav-item nav-link dropdown-toggle">Categories</a>
                <div class="dropdown-menu">
                    <asp:DataList ID="DataList1" runat="server" DataKeyField="ID" DataSourceID="SqlDataSource1">
                        <ItemTemplate>
                            <a href='ShowProduct.aspx?category=<%# Eval("Name") %>' class="dropdown-item"><%# Eval("Name") %></a>
                        </ItemTemplate>
                    </asp:DataList>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:CSharpAssignmentConnectionString %>" SelectCommand="SELECT [ID], [Name] FROM [Category] WHERE Name <> '-- Select --'"></asp:SqlDataSource>
                </div>
            </div>
            <asp:PlaceHolder ID="AdminPlaceHolder1" runat="server">
                <a href="ManageOrderDetail.aspx" class="nav-item nav-link">Manage Orders</a>
                <a href="Coupon.aspx" class="nav-item nav-link">Manage Coupon</a>
                <a href="ManageProducts.aspx" class="nav-item nav-link">Manage Products</a>
            </asp:PlaceHolder>

        </div>
        <asp:Panel CssClass="ml-auto mr-5" ID="SearchBarPanel" runat="server">

            <!--
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
                        <asp:DropDownList CssClass="custom-select mb-1" ID="CbCategory" runat="server" DataSourceID="SqlDataSource1" DataTextField="Name" DataValueField="Name"></asp:DropDownList>
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
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:CSharpAssignmentConnectionString %>" SelectCommand="SELECT [ID], [Name], [Description] FROM [Category]"></asp:SqlDataSource>
                    </div>
                </div>
            </div>-->
        </asp:Panel>
        <asp:LinkButton CssClass="btn btn-warning mx-2" ID="BtnViewCartt" OnClick="BtnViewCart_Click" runat="server">
            CART
                                        <span class="ml-2">
                                            <span class="material-icons">shopping_cart</span>
                                            <asp:Panel ID="PanelQuantity" runat="server"><span class="badge"><%= Session["CountItem"] %></span></asp:Panel>

                                        </span>

        </asp:LinkButton>

        <%-- <asp:Button CssClass="btn btn-warning mx-2" ID="BtnViewCart" runat="server" Text="View Cart" OnClick="BtnViewCart_Click" />--%>
        <asp:Panel CssClass="navbar-nav action-buttons" ID="ContainerAction" runat="server">
            <div class="nav-item dropdown">
                <a href="#" data-toggle="dropdown" class="nav-link dropdown-toggle mr-4" aria-expanded="false">Login</a>
                <div class="dropdown-menu action-form">
                    <p class="hint-text">Sign in with your social media account</p>
                    <div class="form-group social-btn clearfix">
                        <a href="<%=RedirectLink%>"
                            style="display: block; cursor: pointer; margin: 0 auto"
                            class="google-btn">
                            <div class="google-icon-wrapper">
                                <img class="google-icon" src="https://upload.wikimedia.org/wikipedia/commons/5/53/Google_%22G%22_Logo.svg" />
                            </div>
                            <p class="btn-text"><b>Sign in with Google</b></p>
                        </a>
                    </div>
                    <div class="or-seperator"><b>or</b></div>
                    <asp:Label CssClass="d-block text-danger" ID="LabelLoginFailed" runat="server" Text="Invalid username or password!" Visible="False"></asp:Label>
                    <div class="form-group">
                        <asp:TextBox CssClass="form-control" ID="TxtUsernameLogin" runat="server" placeholder="Username"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:TextBox TextMode="Password" CssClass="form-control" ID="TxtPasswordLogin" runat="server" placeholder="Password"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary btn-block" Text="Login" OnClick="BtnLogin_Click" />
                    </div>
                </div>

            </div>
            <div class="nav-item dropdown">
                <a href="#" data-toggle="dropdown" class="btn btn-primary dropdown-toggle sign-up-btn" aria-expanded="false">Sign up</a>
                <div class="dropdown-menu action-form">
                    <asp:Panel ID="RegisterPanel" runat="server">
                        <p class="hint-text">Fill in this form to create your account!</p>
                        <div class="form-group">
                            <asp:Label CssClass="d-block text-danger" ID="LblRequiredUsername" runat="server" Text="This field is required!" Visible="False"></asp:Label>

                            <asp:Label CssClass="d-block text-danger" ID="LblNotValidUsername" runat="server" Text="Must include at least 5 characters!" Visible="False"></asp:Label>

                            <asp:TextBox CssClass="form-control" ID="TxtUsername" runat="server" placeholder="Username"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label CssClass="d-block text-danger" ID="LblRequiredEmail" runat="server" Text="This field is required!" Visible="False"></asp:Label>

                            <asp:Label CssClass="d-block text-danger" ID="LblNotValidEmail" runat="server" Text="Email is invalid!" Visible="False"></asp:Label>

                            <asp:TextBox CssClass="form-control" ID="TxtEmail" runat="server" placeholder="Email"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label CssClass="d-block text-danger" ID="LblRequiredFullName" runat="server" Text="This field is required!" Visible="False"></asp:Label>

                            <asp:Label CssClass="d-block text-danger" ID="LblNotValidFullname" runat="server" Text="Must include at least 5 characters!" Visible="False"></asp:Label>

                            <asp:TextBox CssClass="form-control" ID="TxtFullname" runat="server" placeholder="Fullname"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label CssClass="d-block text-danger" ID="LblRequiredPassword" runat="server" Text="This field is required!" Visible="False"></asp:Label>

                            <asp:Label CssClass="d-block text-danger" ID="LblNotValidPassword" runat="server" Text="Must include 8 characters!" Visible="False"></asp:Label>

                            <asp:TextBox TextMode="Password" CssClass="form-control" ID="TxtPassword" runat="server" placeholder="Password"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label CssClass="d-block text-danger" ID="LblMatchingPassword" runat="server" Text="Confirm password is not matched!" Visible="False"></asp:Label>

                            <asp:TextBox TextMode="Password" CssClass="form-control" ID="TxtConfirm" runat="server" placeholder="Confirm Password"></asp:TextBox>
                        </div>
                        <div class="form-group mb-1">
                            <asp:Label CssClass="d-block text-danger" ID="LblAccept" Visible="false" runat="server" Text="Please accept terms!"></asp:Label>
                            <label class="form-check-label">
                                &nbsp;
                                <asp:CheckBox ID="ChbAccept" runat="server" />
                                I accept the <a href="#">Terms &amp; Conditions</a>
                            </label>
                            <asp:Button ID="BtnSignUp" runat="server" CssClass="btn btn-primary btn-block mb-0" OnClick="BtnSignUp_Click" Text="Sign Up" />
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="LogMeInPanel" runat="server" Visible="False">
                        <p class="hint-text text-success">Successfully Registered!</p>
                        <asp:Button ID="BtnLogMeIn" runat="server" CssClass="btn btn-primary btn-block mb-0" OnClick="BtnLogMeIn_Click" Text="Log Me In" />
                    </asp:Panel>

                </div>
            </div>
        </asp:Panel>

        <asp:Panel ID="ContainerUserInfo" runat="server" CssClass="nav-item dropdown">

            <a aria-expanded="false" class="nav-link dropdown-toggle user-action" data-toggle="dropdown" href="#">
                <div class="d-flex align-items-center">
                    <asp:Image CssClass="avatar" ID="PictureBox" runat="server" />
                    <asp:Button CssClass="btn border rounded-circle px-2 pb-3 bg-white text-danger" Height="30" Width="30" ID="BtnUser" runat="server" Text="" />
                    <asp:Label CssClass="ml-3" ID="Welcome" runat="server"></asp:Label>
                </div>

            </a>
            <div class="dropdown-menu">
                <a class="dropdown-item" href="ViewHistory.aspx"><i class="fa fa-user-o"></i>Booking History</a>
                <div class="dropdown-divider">
                </div>
                <asp:LinkButton CssClass="dropdown-item" ID="BtnLogout" runat="server" OnClick="BtnLogout_Click"><i class="material-icons"></i> Logout</asp:LinkButton>

            </div>

        </asp:Panel>

    </div>

</nav>


