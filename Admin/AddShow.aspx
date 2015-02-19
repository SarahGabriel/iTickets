<%@ Page Title="Add A Show" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddShow.aspx.cs" Inherits="Admin_AddShow" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container top-padding">
        <div class="col-md-4">
            <div id="show-details" class="panel panel-default panel-carousel">
                <div class="panel-body">
                    <ul class="list-unstyled">
                        <li class="form-group">
                            <span class="glyphicon glyphicon-font"></span>&nbsp
                            <asp:Label ID="ShowTitle" runat="server" Text="Show Title"></asp:Label>

                            <asp:TextBox class="form-control" ID="ShowNameTextBox" runat="server"></asp:TextBox>
                            <asp:Label ID="TitleLabel" runat="server" ForeColor="Red"></asp:Label>
                        </li>
                        <li class="form-group">
                            <span class="glyphicon glyphicon-time"></span>&nbsp
                            <asp:Label ID="ShowLength" runat="server" Text="Show Duration (min)"></asp:Label>

                            <asp:TextBox class="form-control" ID="ShowLengthTextBox" runat="server" TextMode="Number"></asp:TextBox>
                            <asp:Label ID="DurationLabel" runat="server" ForeColor="Red"></asp:Label>
                        </li>
                        <li class="form-group">
                            <span class="glyphicon glyphicon-eye-open"></span>&nbsp
                            <asp:Label ID="Label3" runat="server" Text="Show Genre"></asp:Label>
                            <div class="input-group">
                                <div class="input-group-btn">
                                    <asp:LinkButton class="btn btn-primary" ID="AddGenreButton" runat="server" data-toggle="modal" data-target=".AddGenreModal"><span class="glyphicon glyphicon-plus"></span></asp:LinkButton>
                                    <asp:LinkButton class="btn btn-danger" ID="DeleteGenreButton" runat="server" OnClick="DeleteGenreButton_Click"><span class="glyphicon glyphicon-remove"></span></asp:LinkButton>
                                </div>
                                <asp:DropDownList class="form-control" ID="DropDownGenre" runat="server" DataSourceID="SqlDataSource1" DataTextField="Genre" DataValueField="Genre">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [Genre] FROM [Genre]"></asp:SqlDataSource>
                            </div>
                                <asp:Label ID="GenreLabelErr" runat="server" ForeColor="Red"></asp:Label>
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
                    <asp:TextBox class="form-control" ID="DescriptionTextBox" runat="server" Height="70px" TextMode="MultiLine"></asp:TextBox>
                        </li>
                        <li class="form-group">
                            <span class="glyphicon glyphicon-picture"></span>&nbsp
                            Upload Picture &nbsp&nbsp<asp:Label ID="UploadLabel" runat="server" ForeColor="Red"></asp:Label>
                            &nbsp;<asp:FileUpload ID="FileUpload1" runat="server" Height="25px" />
                            <asp:Button ID="Button1" class="btn btn-success pull-right" runat="server" OnClick="Button1_Click" Text="Save" />
                        </li>
                    </ul>

                    <asp:Label ID="SavedLabel" runat="server" ForeColor="Lime"></asp:Label>

                    <asp:Image ID="Imagetest" runat="server" />
                </div>
            </div>
        </div>
    </div>

    <!-- START OF ADD GENRE MODAL-->
    <div class="modal fade addGenreModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <span class="glyphicon glyphicon-eye-open pull-left"></span>
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title text-center" id="myModalLabel">Add A New Genre</h4>
                </div>
                <div class="modal-body text-center">
                    <asp:Panel ID="GenrePanel" runat="server" DefaultButton="AddNewGenreButton">
                        <div class="form-group">
                            <asp:TextBox ID="GenreTextBox" class="form-control" runat="server" placeholder="Genre..."></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:LinkButton class="btn btn-success" ID="AddNewGenreButton" runat="server" OnClick="AddNewGenreButton_Click"><span class="glyphicon glyphicon-plus"></span> Add</asp:LinkButton>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>

    <!-- END OF POPUP-->
</asp:Content>

