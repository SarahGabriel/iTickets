<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CancelPurchase.aspx.cs" Inherits="Client_CancelPurchase" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
                <div class="modal-header text-center">
                    <span class="glyphicon glyphicon-question-sign pull-left"></span>
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title">Are you sure?</h4>
                </div>
                <div class="modal-body text-center">
                    <div class="form-group">
                        Are you sure to want to cancel your purchase?
                        <br />
                        <strong>You will be charged with 20%</strong> of the deal's value and your tickets will be available to others. 
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" data-dismiss="modal">Cancel</button>
                        <asp:Button ID="ConfirmDeletePurchase" class="btn btn-danger" runat="server" OnClick="ConfirmDeletePurchase_Click" Text="Cancel this purchase" />
                    </div>
                </div>
            
    </form>
</body>
</html>
