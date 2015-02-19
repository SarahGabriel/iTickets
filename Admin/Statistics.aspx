<%@ Page Title="Statistics" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Statistics.aspx.cs" Inherits="Admin_Statistics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <div class="page-header body-center white-text text-shadow">
            <div class="row">
                <div class="col-md-12">
                    <h2><span class='glyphicon glyphicon-stats white-text'></span>&nbsp Site Statistics <asp:Label class="label label-success pull-right" ID="totalSellingsLabel" runat="server" Text=""></asp:Label></h2>
                </div>
            </div>
        </div>
    <div class="container top-padding">
        <asp:Panel ID="AllShows" runat="server"></asp:Panel>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="beforeContainer" runat="Server">
</asp:Content>

