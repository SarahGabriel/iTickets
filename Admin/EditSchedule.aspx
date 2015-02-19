<%@ Page Title="Edit Schedule" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EditSchedule.aspx.cs" Inherits="Admin_EditSchedule" %>




<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container top-padding">
        <div class="panel panel-default panel-carousel">
            <div class="panel-body">
                <div class="col-md-6">
                    <div class="form-group">
                        <ul class="list-unstyled form-group">
                            <li class="form-group">
                                <span class="glyphicon glyphicon-filter"></span>&nbsp
                                <asp:Label ID="ShowTitleLabel" runat="server" Text="Show Title"></asp:Label>
                                <div class="input-group">
                                    <asp:TextBox class="form-control" ID="ShowNameTextBoxSc" runat="server" AutoPostBack="true" OnTextChanged="FilterShowList"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <asp:LinkButton class="btn btn-primary" ID="nameSearchSc" runat="server" OnClick="FilterShowList">&nbsp<span class="glyphicon glyphicon-search"></span></asp:LinkButton>

                                    </span>
                                </div>
                                <asp:Label ID="TitleLabelSc" runat="server" ForeColor="Red"></asp:Label>
                            </li>
                            <li class="form-group">
                                <asp:ListBox class="form-control" ID="ShowsListSc" runat="server" OnSelectedIndexChanged="SelectShowSc_Click" AutoPostBack="true"></asp:ListBox>
                            </li>
                            <li class="form-group">
                                <asp:Button class="btn btn-primary" ID="SelectShowSc" runat="server" Text="Select Show" OnClick="SelectShowSc_Click" />
                            </li>
                            <li class="form-group">
                                <span class="glyphicon glyphicon-calendar"></span>&nbsp
                                <asp:Label ID="SchedulesLabel" runat="server" Text="Schedules"></asp:Label>
                                <asp:Label ID="SchedualsFromLabel" runat="server" ForeColor="#FF9900" Text="*Showing Schedules from today onwards "></asp:Label>
                            </li>
                            <li class="form-group">
                                <asp:ListBox class="form-control" ID="SchedulesListSc" runat="server" OnSelectedIndexChanged="SelectSCheduleSc_Click" AutoPostBack="true"></asp:ListBox>
                                <br />
                                <asp:Button class="btn btn-primary" ID="SelectSCheduleSc" runat="server" Text="Select Schedule" OnClick="SelectSCheduleSc_Click" />
                                <asp:Button class="btn btn-warning" ID="SelectFromBeginingButton" runat="server" Text="Show Schedules from begining of time" OnClick="SelectFromBegining_Click" />
                            </li>
                            <li class="form-group edit-show-img">

                                <asp:Image ID="Imagetest" class="img-responsive img-rounded" runat="server" />
                            </li>
                            <li class="form-group">
                                <asp:TextBox ID="TestTextBox" Visible="false" runat="server" Height="181px" TextMode="MultiLine" Width="271px"></asp:TextBox>
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <ul class="list-unstyled form-group">

                            <li class="form-group">
                                <span class="glyphicon glyphicon-check"></span>&nbsp
                                <asp:Label ID="SelectedShowLabel" runat="server" Text="Selected Show"></asp:Label>
                                <br />
                                <asp:TextBox class="form-control" ID="SelectedShowTextBox" runat="server" ReadOnly="True"></asp:TextBox>

                                <asp:Label ID="SelectedShowErrLabel" runat="server" ForeColor="Red"></asp:Label>

                            </li>

                            <li class="form-group row">
                                <div class="col-md-6">
                                    <span class="glyphicon glyphicon-calendar"></span>&nbsp
                                    <asp:Label ID="DateLabel" runat="server" Text="Date"></asp:Label>
                                    <br />
                                    <asp:TextBox class="form-control" ID="DateTextBox" runat="server" TextMode="Date" ReadOnly="True"> </asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <br />
                                    <asp:Label ID="DateErrLabel" runat="server" ForeColor="Red"></asp:Label>
                                </div>
                            </li>

                            <li class="form-group row">
                                <div class="col-md-5">
                                    <span class="glyphicon glyphicon-time"></span>&nbsp
                                    <asp:Label ID="TimeLabel" runat="server" Text="Time" Enabled="False"></asp:Label>
                                    <br />
                                    <asp:TextBox class="form-control" ID="TimeTextBox" runat="server" TextMode="Time" ReadOnly="True"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <br />
                                    <asp:Label ID="TimeErrLabel" runat="server" ForeColor="Red"></asp:Label>
                                </div>
                            </li>

                            <li class="form-group row">
                                <div class="col-md-6">
                                    <span class="glyphicon glyphicon-map-marker"></span>&nbsp
                                    <asp:Label ID="LocationLabel" runat="server" Text="Location"></asp:Label>
                                    <br />
                                    <asp:TextBox class="form-control" ID="LocationTextBox" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <br />
                                    <asp:Label ID="LocationErrLabel" runat="server" ForeColor="Red"></asp:Label>
                                </div>
                            </li>

                            <li class="form-group">
                                <div class="row">
                                    <div class="col-md-5">
                                        <asp:Label ID="RegularPriceLabel" runat="server" Text="Regular Price"></asp:Label>
                                        <br />
                                        <div class="input-group">
                                            <asp:TextBox class="form-control" ID="RegularPriceTextBox" runat="server" TextMode="Number" Width="97px"></asp:TextBox>
                                            <span class="input-group-addon">₪</span>
                                        </div>
                                    </div>
                                    <div>
                                        <div class="col-md-8">
                                            <asp:Label ID="RegularPriceErrLabel" runat="server" ForeColor="Red"></asp:Label>
                                        </div>
                                    </div>
                                </div>


                                <div class="row">
                                    <div class="col-md-5">
                                        <asp:Label ID="ChildPriceLabel" runat="server" Text="Child Price*" ForeColor="#339966"></asp:Label>
                                        <br />
                                        <div class="input-group">
                                            <asp:TextBox class="form-control" ID="ChildPriceTextBox" runat="server" TextMode="Number" Width="97px">0</asp:TextBox>
                                            <span class="input-group-addon">₪</span>
                                        </div>
                                    </div>

                                    <div class="col-md-5">
                                        <asp:Label ID="SoldierPriceLabel" runat="server" Text="Soldier Price*" ForeColor="#339966"></asp:Label>
                                        <br />
                                        <div class="input-group">
                                            <asp:TextBox class="form-control" ID="SoldierPriceTextBox" runat="server" TextMode="Number" Width="97px">0</asp:TextBox>
                                            <span class="input-group-addon">₪</span>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-5">
                                        <asp:Label ID="StudentPriceLabel" runat="server" Text="Student Price*" ForeColor="#339966"></asp:Label>
                                        <br />
                                        <div class="input-group">
                                            <asp:TextBox class="form-control" ID="StudentPriceTextBox" runat="server" TextMode="Number" Width="97px">0</asp:TextBox>
                                            <span class="input-group-addon">₪</span>
                                        </div>
                                    </div>

                                    <div class="col-md-5">
                                        <asp:Label ID="VipPriceLabel" runat="server" Text="Vip Price*" ForeColor="#339966"></asp:Label>
                                        <br />
                                        <div class="input-group">
                                            <asp:TextBox class="form-control" ID="VipPriceTextBox" runat="server" TextMode="Number" Width="97px">0</asp:TextBox>
                                            <span class="input-group-addon">₪</span>
                                        </div>
                                    </div>
                                </div>

                                <div>

                                    <asp:Label ID="Optional" runat="server" Text="*(Optional)" ForeColor="#339966"></asp:Label>
                                    &nbsp
                        <asp:Label ID="PriceErrLabel" runat="server" ForeColor="Red"></asp:Label>
                                </div>
                            </li>

                            <li class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <span class="glyphicon glyphicon-tags"></span>&nbsp
                                        <asp:Label ID="AvailableTickets" runat="server" Text="Available Tickets"></asp:Label>
                                        <br />
                                        <asp:TextBox class="form-control" ID="AvailableTicketsTextBox" runat="server" TextMode="Number"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <br />
                                        <asp:Label ID="AvailibleTicketsErrLabel" runat="server" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>
                            </li>

                            <li class="form-group">
                                <div class="row">
                                    <div class="col-md-3">
                                        <asp:Button class="btn btn-success" ID="UpdateScheduleButton" runat="server" Text="Update" OnClick="AddScheduleButton_Click" />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:LinkButton class="btn btn-danger" ID="DeletePopupButton" runat="server" data-toggle="modal" data-target=".delete-popup"><span class="glyphicon glyphicon-remove"></span> Delete Schedule</asp:LinkButton>
                                    </div>
                                    <br />
                                </div>

                                <div>

                                    <asp:Label ID="ScheduleExistsErrLabel" runat="server" ForeColor="Red"></asp:Label>

                                    <asp:Label ID="AddLabel" runat="server" ForeColor="#66FF33"></asp:Label>
                                    <asp:Label ID="CannotDeleteLabel" runat="server" Text="Cannot delete schedule : there are purchases in this schedule" ForeColor="Red" Visible="False"></asp:Label>
                                </div>
                            </li>

                        </ul>
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
                    <p>Are you sure you want to delete the schedule?</p>
                    <div class="text-center">
                        <button type="button" class="btn btn-primary" data-dismiss="modal">Cancel</button>
                        <asp:Button ID="DeleteShowButton" class="btn btn-danger" runat="server" OnClick="DeleteButton_Click" Text="Delete Schedule" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
