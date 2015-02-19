<%@ Page Title="Search For Shows..." Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SearchBar.aspx.cs" Inherits="SearchBar" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="page-header body-center white-text top-padding">
        <h2>
            <asp:Label ID="LabelTitle" runat="server" Text="Shows"></asp:Label></h2>
    </div>

    <div class="container">
        <div class="row">
            <%int i;
                for (i = index; i<showSize && i < index+ITEMS_PER_PAGE ; i++)
              { %>
            <%String imgPath = "/Images/" + shows[i].Id + ".jpg"; %>
            <div class="col-sm-6 col-md-4">
                <div class="panel panel-carousel">
                    <img class="img-rounded" data-src="holder.js/100%x200" alt="100%x200" src="<%=imgPath %>" style="height: 200px; width: 100%; display: block;">
                    <!-- <img src="<%=imgPath %>" alt="..."> -->
                    <div class="caption panel-body text-center">
                        <h3><%=trimString(shows[i].Name , TITLE_LENGTH) %></h3>
                        <p> <%=trimString(shows[i].Description , DESCRIPTION_LENGTH) %></p>
                        <!-- <p>show size <%=showSize %> , index <%=index %> , index+ITEMS_PER_PAGE <%=index+ITEMS_PER_PAGE %> , path <%=imgPath %> </p> -->
                        <p><a href="/ShowDetails.aspx?show=<%= shows[i].Id %>" id="DetailsButton<%= shows[i].Id %>" class="btn btn-warning"><span class='glyphicon glyphicon-tags'></span>&nbsp Get Your Ticket!</a></p>
                    </div>
                </div>
            </div>
            <%} %>
        </div>
        <ul class="pager">
            <li class="previous"><asp:LinkButton runat="server" ID="PrevButton" OnClick="PrevButton_Click" Visible="False" ><span class="glyphicon glyphicon-chevron-left"></span>&nbsp Older</asp:LinkButton></li>
            <li class="next"><asp:LinkButton runat="server" ID="NextButton" OnClick="NextButton_Click" Visible="False" >Newer &nbsp<span class="glyphicon glyphicon-chevron-right"></span></asp:LinkButton></li>
            
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="beforeContainer" runat="Server">
</asp:Content>

