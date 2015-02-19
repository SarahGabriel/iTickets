<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MyPurchases.aspx.cs" Inherits="Client_ViewPurchases" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-header body-center white-text top-padding text-shadow">
        <h2>My Purchases
            <asp:Button class="btn btn-primary" ID="NextButton" Visible="false" runat="server" Text="Show purchases from today" OnClick="NextButton_Click" />
            <asp:Button class="btn btn-primary" ID="AllButton" runat="server" Text="Show all my purchases" OnClick="AllButton_Click" />
            <asp:Button class="btn btn-primary" ID="PreviousButton" runat="server" Text="Show purchases up today" OnClick="PreviousButton_Click" />
            <asp:Label ID="LabelIsDeleted" runat="server" Visible="false" Text="Your purchase has been deleted"></asp:Label>
        </h2>
    </div>
    <div class="row body-center">

        <%  //Client client = null;
            //DALclient dalClient;
            //DALshow dalShow;

            //string clientMail;

            //dalClient = new DALclient();
            //dalShow = new DALshow();

            //if (Session["name"] != null && Session["eMail"] != null)
            //{
                //clientMail = Session["eMail"].ToString();
                //client = dalClient.getClientDetails(clientMail);
                //if (client != null)
                //{

                    //List<Purchase> purchases = null;
                    if (Label2.Text.Equals("prev"))
                    {
                        purchase = BLclient.getClientPurchaseUp(client.Mail, DateTime.Today);
        %><h3 class="white-text text-shadow">Your purchases till today - <%=DateTime.Today.ToString("dd-MM-yyyy") %></h3>
        <% 
                    }
                    else if (Label2.Text.Equals("next"))
                    {
                        purchase = BLclient.getClientPurchaseFrom(client.Mail, DateTime.Today);
        %>
        <h3 class="white-text text-shadow">Your purchases from today - <%=DateTime.Today.ToString("dd-MM-yyyy")%></h3>
        <% 
                    }
                    else if (Label2.Text.Equals("all"))
                    {
                        purchase = BLclient.getClientsPurchases(client.Mail);
        %><h3 class="white-text text-shadow">All your purchases</h3>
        <% 
                    }

                    if (purchase.Count == 0)
                    { %>
        <p class="white-text">
            You have no purchase!!<br />

        </p>
        <%}
                    else
                    { %>
        <asp:Label ID="Label2" runat="server" Visible="false" Text="Label"></asp:Label>
        <div>
            <div class="panel-body">
                <ul class="list-unstyled">
                    <%
                        foreach (Purchase p in purchase)
                        {

                            Show show = BLshow.getShowsById(p.ShowId);

                            if (show != null)
                            {

                                string name = show.Name;
                                string imgSrc = "/Images/" + show.Id + ".jpg";
                                string date = p.Date.ToString("dd/MM/yyyy");
                                string time = p.Time.ToString("H:mm");
                                int quantity = p.Quantity;
                                int price = p.Price;
                                int points = show.Points * quantity; //points client got for the purchase
                                string purchasedWith = p.PurchasedWith;
                    %>

                    <li>
                        <div class="panel panel-default panel-carousel">
                            <div class="panel-heading">
                                <span class="glyphicon glyphicon-tags"></span>&nbsp <%=name %>
                                <asp:Label ID="LabelName" runat="server" Visible="true"></asp:Label>
                                <%//LabelName.Text = "IDDDDDD" + p.Id + ""; %>
                            </div>

                            <div class="panel-body">
                                <div class="col-md-2">
                                    <a href="/ShowDetails.aspx?show=<%= show.Id %>" id="DetailsButton<%= show.Id %>">
                                        <img style="width: 140px;" src="<%=imgSrc %>" alt="<%= show.Name %>" class="img-rounded">
                                        <p class="btn btn-primary">Click to view show</p>
                                    </a>
                                </div>
                                <!-- Table -->
                                <div class="col-md-10">
                                    <table class="table white-text">
                                        <tr>
                                            <th><span class="glyphicon glyphicon-calendar"></span>&nbsp Date</th>
                                            <th><span class="glyphicon glyphicon-time"></span>&nbsp Time</th>
                                            <th><span class="glyphicon glyphicon-tags"></span>&nbsp Quanity</th>
                                            <th>₪ Price</th>
                                            <th><span class="glyphicon glyphicon-star"></span>&nbsp Points</th>
                                            <th><span class="glyphicon glyphicon-shopping-cart"></span>&nbsp Payment Method</th>
                                        </tr>
                                        <tr>
                                            <td><%=date %></td>
                                            <td><%=time %></td>
                                            <td><%=quantity %></td>
                                            <td><%=price %>₪</td>
                                            <td><%=points %></td>
                                            <td><%=purchasedWith %></td>

                                        </tr>
                                    </table>

                                    <%if (DateTime.Compare(p.Date, DateTime.Today) < 0) // if date is earlier than today
                                      { %>
                                    <asp:LinkButton class="btn btn-danger pull-right" ID="deleteOldButton" runat="server" data-toggle="modal" data-target=".deleteOldModal">Remove from the list</asp:LinkButton>
                                    <%
                                      }
                                      else
                                      { %>
                                    <a href="CancelPurchase.aspx?purchaseId=<%= p.Id %>&showId=<%= p.ShowId %>" class="btn btn-danger pull-right" data-toggle="modal" data-target=".deleteModal"><span class="glyphicon glyphicon-remove"></span>&nbsp Cancel this purchase</a>
                                    <%} %>

                                    <% }
                            
                        }%>
                                </div>
                            </div>
                        </div>
                    </li>
                </ul>
            </div>

        </div>
        <%}
                //}
            //}%>
    </div>


    <!-- pop up login -->
    <div class="modal fade deleteOldModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title">Are you sure?</h4>
                </div>
                <div class="modal-body text-center">
                    <div class="form-group">
                        Are you sure to want to delete your purchase? 
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                        <asp:Button ID="YesButtonPopup" class="btn btn-danger" runat="server" OnClick="YesOldPopupButton_Click" Text="Remove this purchase from the list" />
                    </div>

                </div>



            </div>
        </div>
    </div>

    <!-- pop up login -->
    <div class="modal fade deleteModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="beforeContainer" runat="Server">
</asp:Content>

