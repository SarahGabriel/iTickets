<%@ Page Title="iTickets - Sign Up" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Registration.aspx.cs" Inherits="Registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="RegPanel" runat="server" DefaultButton="RegButton">
        <div class="container top-padding">
            <div class="col-md-6">
                <div class="panel panel-size panel-carousel">
                    <h2><span class="glyphicon glyphicon-edit"></span>&nbsp Sign Up</h2>
                    <ul class="list-unstyled form-group">

                        <li class="row">
                            <div class="col-md-8">
                                <span class="glyphicon glyphicon-user"></span>&nbsp First Name                
                                <asp:TextBox class="form-control" ID="FirstNameTextBox" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <br />
                                <asp:Label class="errorMessage" ID="Label1" runat="server" Text=""></asp:Label>
                            </div>
                        </li>

                        <li class="row">
                            <div class="col-md-8">
                                <span class="glyphicon glyphicon-user"></span>&nbsp Last Name
                            <asp:TextBox Class="form-control" ID="LastNameTextBox" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <br />
                                <asp:Label class="errorMessage" ID="Label2" runat="server" Text=""></asp:Label>
                            </div>
                        </li>

                        <li class="row">
                            <div class="col-md-8">
                                <span class="glyphicon glyphicon-envelope"></span>&nbsp Mail
                            <asp:TextBox Class="form-control" ID="MailTextBox" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <br />
                                <asp:Label class="errorMessage" ID="Label3" runat="server" Text=""></asp:Label>
                                <asp:Label class="errorMessage" ID="ExistLabel" runat="server" Text=""></asp:Label>
                            </div>
                        </li>

                        <li class="row">
                            <div class="col-md-8">
                                <span class="glyphicon glyphicon-phone-alt"></span>&nbsp Phone Number
                            <asp:TextBox Class="form-control" ID="PhoneNumberTextBox" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <br />
                                <asp:Label class="errorMessage" ID="Label4" runat="server" Text=""></asp:Label>
                            </div>
                        </li>

                        <li class="row">
                            <div class="col-md-8">
                                <span class="glyphicon glyphicon-map-marker"></span>&nbsp Address
                            <asp:TextBox Class="form-control" ID="AddressTextBox" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <br />
                                <asp:Label class="errorMessage" ID="Label5" runat="server" Text=""></asp:Label>
                            </div>
                        </li>

                        <li class="row">
                            <div class="col-md-8">
                                <span class="glyphicon glyphicon-asterisk"></span>&nbsp Password
                            <asp:TextBox Class="form-control" ID="PasswordTextBox" runat="server" TextMode="password"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <br />
                                <asp:Label class="errorMessage" ID="Label6" runat="server" Text=""></asp:Label>
                            </div>
                        </li>

                        <li class="row">
                            <div class="col-md-8">
                                <span class="glyphicon glyphicon-asterisk"></span>&nbsp Repeat Password
                            <asp:TextBox Class="form-control" ID="RePasswordTextBox" runat="server" TextMode="password"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <br />
                                <asp:Label class="errorMessage" ID="Label7" runat="server" Text=""></asp:Label>
                            </div>
                        </li>
                    </ul>
                    <p>
                        <asp:Button class="btn btn-success" ID="RegButton" runat="server" OnClick="Button1_Click" Text="Confirm" />
                    </p>
                </div>
            </div>
        </div>
    </asp:Panel>

</asp:Content>

