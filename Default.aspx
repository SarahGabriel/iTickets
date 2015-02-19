<%@ Page Title="Welcome to iTickets.com - All your tickets needs" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="shows-carousel" class="carousel slide" data-ride="carousel">
        
        <% int counter = 0;%>
        <!-- Indicators -->
        <ol class="carousel-indicators">
            <% foreach (Show show in shows)
               {%>
            <%if (counter == 0)
              {%>
            <li data-target="#shows-carousel" data-slide-to="0" class="active"></li>
            <%}
              else
              { %>
            <li data-target="#shows-carousel" data-slide-to="<%= counter %>"></li>
            <% } counter++; %>
            <%} counter = 0;%>
        </ol>

        <!-- Wrapper for slides -->
        <div class="carousel-inner">
            <% foreach (Show show in shows)
               {%>
            <%String title = show.Name, imgSrc = "/Images/" + show.Id + ".jpg"; %>
            <%if (counter == 0)
              {%>
            <div class="item active">
                <%}
              else
              { %>
                <div class="item">
                    <%} %>
                    <img class="img-responsive center-block carousel-img" src="<%= imgSrc %>" alt="...">
                    <div class="carousel-caption">
                        <div class="panel panel-default panel-carousel">
                            <div class="panel-body">
                                <%= title %>

                                <a href="/ShowDetails.aspx?show=<%= show.Id %>" id="DetailsButton<%= show.Id %>" class="btn btn-warning"><span class='glyphicon glyphicon-tags'></span>&nbsp Get Your Ticket!</a>
                            </div>
                        </div>
                    </div>
                </div>
                <% counter++;
               } %>
            </div>
            <!-- Controls -->
            <a class="left carousel-control" href="#shows-carousel" role="button" data-slide="prev">
                <span class="glyphicon glyphicon-chevron-left"></span>
            </a>
            <a class="right carousel-control" href="#shows-carousel" role="button" data-slide="next">
                <span class="glyphicon glyphicon-chevron-right"></span>
            </a>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <div class="welcome-footer">

        <div class="container">

            <div class="row">
                <div class="col-md-5">
                    <div class="panel panel-default panel-carousel">
                        <div class="panel-body font-panel" id="dateSearchPanel">

                            <div class="form-group">
                                <div class="row">

                                    <div class="col-md-3">
                                        <span class="glyphicon glyphicon-indent-left"></span>&nbsp 
                                        <asp:Label ID="FromLabel" runat="server" Text="From:"></asp:Label>
                                    </div>

                                    <div class="col-md-6 ">
                                        <div class="form-inline" role="form">
                                            <div class="form-group has-feedback">
                                                <i class="glyphicon glyphicon-calendar form-control-feedback"></i>
                                                <asp:TextBox class="form-control" ID="FromDateTextBox" runat="server" TextMode="Date"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-3">
                                        <span class="glyphicon glyphicon-indent-right"></span>&nbsp 
                                        <asp:Label ID="ToLabel" runat="server" Text="To: "></asp:Label>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-inline" role="form">
                                            <div class="form-group has-feedback">
                                                <i class="glyphicon glyphicon-calendar form-control-feedback"></i>
                                                <asp:TextBox class="form-control" ID="ToDateTextBox" runat="server" TextMode="Date"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <div class="row">
                                    <div class="col-md-3">
                                        <span class="glyphicon glyphicon-eye-open"></span>&nbsp 
                                        <label for="DropDownGenre">Genre:</label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:DropDownList class="form-control" ID="DropDownGenre" runat="server" DataSourceID="SqlDataSource1" DataTextField="Genre" DataValueField="Genre">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [Genre] FROM [Genre]"></asp:SqlDataSource>
                                    </div>

                                    <div class="col-md-3">
                                        <asp:LinkButton class="btn btn-sm btn-success" ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"><span class="glyphicon glyphicon-search"></span>&nbsp Go Find Those Shows!</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-md-7">
                    <h1 id="welcome-size" class="welcome-title text-center">Welcome to 
                        <asp:Image ID="Image1" runat="server" src="/Images/Site/iTicketsLogo.png" Width="200px" Height="90px" /></h1>
                    <h3 class="welcome-description text-center">Here you can find the best deals for Shows / Movies</h3>
                </div>
            </div>




        </div>
    </div>
</asp:Content>

