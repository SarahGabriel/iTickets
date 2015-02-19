<%@ Page Title="My Points" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MyPoints.aspx.cs" Inherits="Client_MyPoints" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-header body-center white-text">
        <div class="row">
            <div class="col-md-12">
                <h2 class="text-shadow">Tickets I Can Buy With My Points 
                    <label class="label label-success pull-right">
                    <span class="glyphicon glyphicon-star"></span>&nbsp Your points:
                
                    <asp:Label ID="LabelPoint" runat="server" Text=""></asp:Label></label></h2>
            </div>
        </div>
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
                //client = BLclient.getClientDetails(clientMail);
                //if (client != null)
                //{

                    //List<Schedule> schedules = 

                    if (schedules.Count == 0)
                    { %>
        <p class="white-text">
            There is no show you can buy, you don't have enough points!!<br />
        </p>

        <%}
                    else
                    { %>

        <div>
            <div class="panel-body">
                <ul class="list-unstyled">
                    <%
                        foreach (Schedule s in schedules)
                        {

                            Show show = BLshow.getShowsById(s.ShowId);

                            if (show != null)
                            {

                                string name = show.Name;
                                string imgSrc = "/Images/" + show.Id + ".jpg";
                                string date = s.Date.ToString("dd/MM/yyyy");
                                string time = s.Time.ToString("H:mm");

                                int price = s.RegularPrice;
                                int points = client.Points;
                      
                    %>

                    <li>
                        <div class="panel panel-default panel-carousel">
                            <div class="panel-heading">
                                <span class="glyphicon glyphicon-tags"></span>&nbsp <%=name %>
                            </div>

                            <div class="panel-body">
                                <div class="col-md-2">
                                    <p>
                                        <img style="width: 160px;" src="<%=imgSrc %>" alt="..." class="img-rounded">
                                    </p>
                                </div>
                                <!-- Table -->
                                <div class="col-md-10">
                                    <table class="table white-text">
                                        <tr>
                                            <th><span class="glyphicon glyphicon-calendar"></span>&nbsp Date</th>
                                            <th><span class="glyphicon glyphicon-time"></span>&nbsp Time</th>

                                            <th>₪ Price</th>
                                            <th><span class="glyphicon glyphicon-star"></span>&nbsp Your points</th>
                                            <th><span class="glyphicon glyphicon-shopping-cart"></span>&nbsp Buy With Points</th>

                                        </tr>
                                        <tr>
                                            <td><%=date %></td>
                                            <td><%=time %></td>

                                            <td><%=price %>₪</td>
                                            <td><%=points%></td>
                                            <td><a href="/ShowDetails.aspx?show=<%= show.Id %>" id="DetailsButton<%= show.Id %>" class="btn btn-primary"><span class="glyphicon glyphicon-tags"></span>&nbsp Get Your Ticket!</a></td>

                                        </tr>
                                    </table>
                                </div>
                            </div>

                        </div>
                    </li>


                    <% }
                            else
                            {%>
                    <asp:Label ID="LabelError" runat="server">An Error Occured :/</asp:Label>
                    <%}
                        }%>
                </ul>
            </div>
        </div>
        <%}
             //   }
           // }%>
    </div>
</asp:Content>
