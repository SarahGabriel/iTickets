<%@ Page Title="Edit A Show" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EditShows.aspx.cs" Inherits="Admin_EditShow" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container top-padding">
        <div class="panel panel-default panel-carousel">
            <div class="panel-body">
                <div class="col-md-6">
                    <div class="form-group">
                        <ul class="list-unstyled form-group">
                            <li class="form-group">
                                <span class="glyphicon glyphicon-filter"></span>&nbsp
                                <asp:Label ID="Label1" runat="server" Text="Show Title"></asp:Label>
                                <div class="input-group">
                                    <asp:TextBox class="form-control" ID="ShowNameTextBox" AutoPostBack="true" OnTextChanged="FilterShowList" runat="server"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <asp:LinkButton class="btn btn-primary" ID="nameSearch" runat="server" OnClick="FilterShowList">&nbsp<span class="glyphicon glyphicon-search"></span></asp:LinkButton>

                                    </span>
                                </div>
                                <asp:Label ID="TitleLabel" runat="server" ForeColor="Red"></asp:Label>
                            </li>
                            <li class="form-group">
                                <asp:ListBox class="form-control" ID="ShowsList" runat="server" OnSelectedIndexChanged="LoadButton_Click" AutoPostBack="true"></asp:ListBox>
                            </li>
                            <li class="form-group">
                                <asp:Button class="btn btn-primary" ID="LoadButton" runat="server" OnClick="LoadButton_Click" Text="Load Show" />
                                <asp:LinkButton class="btn btn-danger" ID="DeletePopupButton" runat="server" data-toggle="modal" data-target=".delete-popup"><span class="glyphicon glyphicon-remove"></span> Delete Show</asp:LinkButton>

                            </li>
                            <li class="form-group">

                                <asp:Label ID="CannotDeleteLabel" runat="server" Text="Cannot delete show : there are purchases from this show" ForeColor="Red" Visible =" false"></asp:Label>
                            </li>
                            <li class="form-group edit-show-img">

                                <asp:Image ID="Imagetest" class="img-responsive img-rounded" runat="server" />
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <ul class="list-unstyled form-group">
                            <li class="form-group">
                                <span class="glyphicon glyphicon-time"></span>&nbsp
                                <asp:Label ID="ShowLength" runat="server" Text="Show Duration (min)"></asp:Label>

                                <asp:TextBox class="form-control" ID="ShowLengthTextBox" runat="server" TextMode="Number"></asp:TextBox>
                                <asp:Label ID="DurationLabel" runat="server" ForeColor="Red"></asp:Label>
                            </li>
                            <li class="form-group">
                                <span class="glyphicon glyphicon-eye-open"></span>&nbsp
                                <asp:Label ID="Label3" runat="server" Text="Show Genre"></asp:Label>

                                <asp:DropDownList class="form-control" ID="DropDownGenre" runat="server" DataSourceID="SqlDataSource1" DataTextField="Genre" DataValueField="Genre">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [Genre] FROM [Genre]"></asp:SqlDataSource>

                            </li>
                            <li class="form-group">
                                <span class="glyphicon glyphicon-star"></span>&nbsp
                                <asp:Label ID="Label4" runat="server" Text="Points"></asp:Label>
                                <br />
                                <asp:TextBox class="form-control" ID="ShowPointsTextBox" runat="server" TextMode="Number"></asp:TextBox>
                                <asp:Label ID="PointsLabel" runat="server" ForeColor="Red"></asp:Label>
                            </li>
                            <li>
                                <span class="glyphicon glyphicon-list-alt"></span>&nbsp Description
                    <asp:TextBox class="form-control" ID="DescriptionTextBox" runat="server" Height="163px" TextMode="MultiLine"></asp:TextBox>
                            </li>
                            <li class="form-group">
                                <span class="glyphicon glyphicon-picture"></span>&nbsp
                                Upload Picture &nbsp&nbsp
                                <asp:Label ID="UploadLabel" runat="server" ForeColor="Red"></asp:Label>
                                &nbsp;<asp:FileUpload ID="FileUpload1" runat="server" Height="25px" />
                            </li>
                        </ul>
                        <asp:Button ID="UpdateButton" class="btn btn-success" runat="server" OnClick="UpdateButton_Click" Text="Save" />

                        <asp:Label ID="SavedLabel" runat="server" ForeColor="Lime"></asp:Label>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="beforeContainer" runat="Server">
    <!-- Delete popup-->
    <div class="modal fade delete-popup" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">

                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only"></span></button>
                    <h4 class="modal-title">Delete show</h4>

                </div>
                <div class="modal-body">
                    <p>Are you sure you want to delete the show?</p>
                    <div class="text-center">
                        <button type="button" class="btn btn-primary" data-dismiss="modal">Cancel</button>
                        <asp:Button ID="DeleteShowButton" class="btn btn-danger" runat="server" OnClick="DeleteButton_Click" Text="Delete Show" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

