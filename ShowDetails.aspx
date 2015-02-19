<%@ Page Title="iTickets.com - Show Details" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ShowDetails.aspx.cs" Inherits="SiteDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="row">
            <div class="col-md-10 col-md-push-1">
                <div id="show-details" class="panel panel-default panel-carousel">

                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-6" id="ShowImageDiv">
                                <asp:Image class="img-responsive img-rounded" ID="ShowImage" runat="server" />
                            </div>
                            <div class="col-md-6" id="show-details-right">
                                <div class="row" id="title-div">
                                    <h3>
                                        <asp:Label ID="ShowTitle" runat="server" Text="..."></asp:Label>
                                    </h3>
                                </div>

                                <div class="row" id="description-div">
                                    <asp:Label ID="ShowDescription" runat="server" Text="Label"></asp:Label>
                                </div>
                                <br />

                                <div class="row" id="dates-div">
                                    <div class="col-md-10">
                                        <div class="input-group">
                                            <span class="input-group-addon glyphicon glyphicon-calendar"></span>
                                            <asp:DropDownList ID="ShowDates" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ShowDates_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                        <div class="row">


                                            <%int availableTickets = BLshow.getAvailableTicketsAmount(show, selectedSchedule); %>
                                            <%int precentToShow = 0;%>
                                            <%if (availableTickets > 0)
                                                  precentToShow = (availableTickets * 100) / selectedSchedule.AvailableTickets; %>

                                            <h4>
                                                <%if (availableTickets > 0)
                                                  { %>
                                                Tickets Left: <%= availableTickets %>
                                                <%}
                                                  else
                                                  {%>
                                                No Tickets Left!
                                                <%} %>
                                            </h4>
                                            <div class="progress">
                                                <%if (availableTickets > 0)
                                                  {%>
                                                <div class="progress-bar progress-bar-success progress-bar-striped active" role="progressbar" aria-valuenow="<%= availableTickets %>" aria-valuemin="0" aria-valuemax="<%= selectedSchedule.AvailableTickets %>" style="width: <%=  precentToShow %>%"></div>
                                                <%} %>
                                                <div class="progress-bar progress-bar-danger progress-bar-striped active" style="width: <%= (100 - precentToShow)%>%"></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row" id="tickets-div">
                                    <div class="col-md-6">
                                        <div id="tickets-type">
                                            <h4>Tickets Type:</h4>
                                            <asp:Panel ID="TicketsPanel" runat="server">
                                                <asp:Panel ID="RegularPanel" runat="server">
                                                    <asp:Label ID="RegularLabel" runat="server" Text="Regular: " BorderStyle="None" Font-Bold="True"></asp:Label>
                                                    <asp:Label ID="RegularPrice" runat="server" Text="Label"></asp:Label>

                                                    <asp:DropDownList ID="RegularAmount" runat="server" AutoPostBack="true" OnSelectedIndexChanged="AmountDropDown_Changed"></asp:DropDownList>

                                                </asp:Panel>

                                                <asp:Panel ID="VipPanel" runat="server">
                                                    <asp:Label ID="VipLabel" runat="server" Text="VIP: " BorderStyle="None" Font-Bold="True"></asp:Label>
                                                    <asp:Label ID="VIPPrice" runat="server" Text="Label"></asp:Label>
                                                    <asp:DropDownList ID="VipAmount" runat="server" AutoPostBack="true" OnSelectedIndexChanged="AmountDropDown_Changed"></asp:DropDownList>

                                                </asp:Panel>

                                                <asp:Panel ID="ChildPanel" runat="server">
                                                    <asp:Label ID="ChildLabel" runat="server" Text="Child: " BorderStyle="None" Font-Bold="True"></asp:Label>
                                                    <asp:Label ID="ChildPrice" runat="server" Text="Label"></asp:Label>
                                                    <asp:DropDownList ID="ChildAmount" runat="server" AutoPostBack="true" OnSelectedIndexChanged="AmountDropDown_Changed"></asp:DropDownList>

                                                </asp:Panel>
                                                <asp:Panel ID="SoldierPanel" runat="server">
                                                    <asp:Label ID="SoldierLabel" runat="server" Text="Soldier: " BorderStyle="None" Font-Bold="True"></asp:Label>
                                                    <asp:Label ID="SoldierPrice" runat="server" Text="Label"></asp:Label>
                                                    <asp:DropDownList ID="SoldierAmount" runat="server" AutoPostBack="true" OnSelectedIndexChanged="AmountDropDown_Changed"></asp:DropDownList>

                                                </asp:Panel>

                                                <asp:Panel ID="StudentPanel" runat="server">
                                                    <asp:Label ID="StudentLabel" runat="server" Text="Student: " BorderStyle="None" Font-Bold="True"></asp:Label>
                                                    <asp:Label ID="StudentPrice" runat="server" Text="Label"></asp:Label>
                                                    <asp:DropDownList ID="StudentAmount" runat="server" AutoPostBack="true" OnSelectedIndexChanged="AmountDropDown_Changed"></asp:DropDownList>
                                                </asp:Panel>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                    <div class="col-md-6 text-center">
                                        <asp:Panel ID="PricePanel" runat="server">
                                            <h4>
                                                <asp:Label ID="PriceTitle" runat="server" Text="Total Price: " BorderStyle="None" Font-Bold="True" ForeColor="Red"></asp:Label>
                                                <asp:Label ID="Price" runat="server" BorderStyle="None" Font-Bold="True" ForeColor="Red"></asp:Label>
                                            </h4>
                                            <h4>
                                                <asp:Label ID="PointsTitle" runat="server" Text="Points earned: " BorderStyle="None" Font-Bold="True" ForeColor="Blue"></asp:Label>
                                                <asp:Label ID="PointsLabel" runat="server" BorderStyle="None" Font-Bold="True" ForeColor="Blue"></asp:Label>
                                            </h4>

                                            <div class="col-md-6">
                                                <asp:DropDownList class="form-control input-sm" ID="PayWithDropBox" runat="server" OnSelectedIndexChanged="PayWithDropBox_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>

                                            </div>
                                            <div class="col-md-6">
                                                <asp:LinkButton ID="PurchaseButton" runat="server" Text="" class="btn btn-sm btn-success" OnClick="PurchaseButton_Click"><span class='glyphicon glyphicon-shopping-cart'></span> &nbsp Buy Now!</asp:LinkButton>
                                            </div>

                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>

                    <asp:Image src="Images/Site/sold_out.png" class="" ID="SoldOutPic" runat="server" Visible="False" />
                </div>
            </div>
        </div>
    </div>

    <!-- Popup-->
    <div class="modal fade errorModal" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title text-center" id="myModalLabel">Error</h4>
                </div>
                <div class="modal-body text-center">
                    <h4>There are only <strong><%= availableTickets %></strong> tickets left & you try to purchase <strong><%= ticketsQuantity %>!</strong>
                    </h4>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
