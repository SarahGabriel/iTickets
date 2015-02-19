<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MyAccount.aspx.cs" Inherits="Client_MyAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="page-header body-center white-text top-padding">
        <%string name = "";
          //DALclient dalClient = new DALclient();
          if (Session["name"] != null)
          {
              name = Session["name"].ToString() + " " + BLclient.getClientLastName(Session["eMail"].ToString());
          } %>
        <h1 class="text-shadow">Welcome <%=name %>!</h1>


    </div>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div class="row body-center">
        <div class="col-md-4">
            <div class="panel panel-default panel-carousel">
                <div class="panel-heading text-center">
                    <h3><span class="glyphicon glyphicon-user"></span>&nbsp My Details</h3>
                </div>
                <asp:Panel ID="detailsPanel" runat="server" DefaultButton="SaveDetailsButton">
                    <div class="panel-body font-panel">
                        <ul class="list-unstyled">
                            <li><span class="glyphicon glyphicon-user"></span>&nbsp <b>First Name: </b>
                                <asp:Label ID="LabelFirst" runat="server"></asp:Label>
                                <asp:TextBox class="form-control" ID="TextBoxFirst" runat="server" Visible="False"></asp:TextBox>
                            </li>
                            <li><span class="glyphicon glyphicon-user"></span>&nbsp <b>Last Name: </b>
                                <asp:Label ID="LabelLast" runat="server"></asp:Label>
                                <asp:TextBox class="form-control" ID="TextBoxLast" runat="server" Visible="False"></asp:TextBox>
                            </li>
                            <li><span class="glyphicon glyphicon-envelope"></span>&nbsp <b>Mail: </b>
                                <asp:Label ID="LabelMail" runat="server"></asp:Label>
                            </li>
                            <li><span class="glyphicon glyphicon-phone-alt"></span>&nbsp <b>Phone Number: </b>
                                <asp:Label ID="LabelPhone" runat="server"></asp:Label>
                                <asp:TextBox class="form-control" ID="TextBoxPhone" runat="server" Visible="False"></asp:TextBox>
                            </li>
                            <li><span class="glyphicon glyphicon-map-marker"></span>&nbsp <b>Address: </b>
                                <asp:Label ID="LabelAddress" runat="server"></asp:Label>
                                <asp:TextBox class="form-control" ID="TextBoxAddress" runat="server" Visible="False"></asp:TextBox>
                            </li>
                            <!--  <li><b>Points: </b>
                            <asp:Label ID="LabelPoint" runat="server"></asp:Label>
                        </li> -->


                        </ul>
                        <asp:LinkButton class="btn btn-primary" ID="EditDetailsButton" runat="server" OnClick="EditDetailsButton_Click"><span class="glyphicon glyphicon-pencil"></span> &nbsp Edit</asp:LinkButton>
                        <asp:LinkButton class="btn btn-success" ID="SaveDetailsButton" runat="server" OnClick="SaveDetailsButton_Click" Visible="False"><span class="glyphicon glyphicon-ok"></span> &nbsp Save changes</asp:LinkButton>
                        <asp:LinkButton class="btn btn-danger" ID="CancelDetailButton" runat="server" OnClick="CancelDetailsButton_Click" Visible="False"><span class="glyphicon glyphicon-remove"></span> &nbsp Cancel</asp:LinkButton>
                        <asp:Label ID="LabelMsg" runat="server" Visible="False"></asp:Label>
                    </div>
                </asp:Panel>
            </div>
        </div>
        <div class="col-md-4">
            <div class="panel panel-default panel-carousel">
                <div class="panel-heading text-center">
                    <h3><span class="glyphicon glyphicon-asterisk"></span>&nbsp My Password</h3>
                </div>
                <asp:Panel ID="passwordPanel" runat="server" DefaultButton="SavePwdButton">
                    <div class="panel-body font-panel">
                        <ul style="list-style-type: none; padding-left: 10px; font-size: medium">
                            <li>
                                <asp:Label ID="LabelOldPwd1" runat="server"><b>Password:</b></asp:Label>
                                <asp:Label ID="LabelOldPwd2" runat="server" Visible="false"><b>Enter old password:</b></asp:Label>
                                <asp:Label ID="LabelPwd" Visible="false" Text="*********" runat="server"></asp:Label>
                                <asp:TextBox class="form-control" TextMode="password" ID="TextBoxOldPwd" runat="server" Visible="False"></asp:TextBox>
                            </li>
                            <li>
                                <asp:Label ID="LabelNewPwd1" runat="server" Visible="false"><b>Enter new password:</b></asp:Label>
                                <asp:TextBox class="form-control" TextMode="password" ID="TextBoxNewPwd1" runat="server" Visible="False"></asp:TextBox>
                            </li>
                            <li>
                                <asp:Label ID="LabelNewPwd2" runat="server" Visible="false"><b>Re-enter new password:</b></asp:Label>
                                <asp:TextBox class="form-control" TextMode="password" ID="TextBoxNewPwd2" runat="server" Visible="False"></asp:TextBox>
                            </li>
                        </ul>
                        <asp:LinkButton class="btn btn-primary" ID="EditPwdButton" runat="server" OnClick="EditPasswordButton_Click"><span class="glyphicon glyphicon-pencil"></span> &nbsp Change password</asp:LinkButton>
                        <asp:LinkButton class="btn btn-success" ID="SavePwdButton" runat="server" OnClick="SavePasswordButton_Click" Visible="False"><span class="glyphicon glyphicon-ok"></span> &nbsp Save changes</asp:LinkButton>
                        <asp:LinkButton class="btn btn-danger" ID="CancelButton" Visible="false" runat="server" OnClick="CancelButton_Click"><span class="glyphicon glyphicon-remove"></span> &nbsp Cancel</asp:LinkButton>
                        <asp:Label ID="LabelMsgPwd" runat="server" Visible="False"></asp:Label>
                    </div>
                </asp:Panel>
            </div>
        </div>
        <div class="col-md-4">
            <div class="panel panel-default panel-carousel">
                <div class="panel-heading text-center">
                    <h3><span class="glyphicon glyphicon-star"></span>&nbsp My Points</h3>
                </div>
                <div class="panel-body font-panel">
                    <h4><strong>What can I reach with my points?</strong></h4>
                    The points you earn with each purchase can be used to purchase more tickets here at iTickets!

                    <br />
                    <h3>
                        <p class="label label-success">
                            <span class="glyphicon glyphicon-star"></span>&nbsp You have
                     <asp:Label ID="LabelPoints" runat="server">0</asp:Label>
                            points.
                        </p>
                    </h3>
                    <br />
                    <asp:LinkButton class="btn btn-primary pull-right" ID="LearnButton" runat="server" OnClick="LearnMoreButton_Click"><span class="glyphicon glyphicon-info-sign"></span> &nbsp Learn more...</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>

</asp:Content>


<asp:Content ID="Content4" ContentPlaceHolderID="beforeContainer" runat="Server">
    <!-- pop up login -->
    <div class="modal fade deleteModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title">Are you sure?</h4>
                </div>
                <div class="modal-body text-center">
                    <div class="form-group">
                        Are you sure to want to delete your account? 
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                        <asp:Button ID="YesButtonPopup" class="btn btn-danger" runat="server" OnClick="YesPopupButton_Click" Text="Delete my account" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
