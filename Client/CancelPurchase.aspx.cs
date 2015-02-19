using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Client_CancelPurchase : System.Web.UI.Page
{
    protected string purchaseId, showId, email;
    protected Purchase purchase;
    protected int pId, sId;

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected bool CancelPurchase()
    {
        //DALpurchase dalPur = new DALpurchase();
        
        pId = Convert.ToInt32(purchaseId);
        sId = Convert.ToInt32(showId);

        purchase = BLpurchase.getPurchaseById(pId);

        if (purchase == null)
            return false;
        
        int points = getPointsToCancel(sId) * purchase.Quantity;

     
        if (purchase.PurchasedWith.Equals("Points"))
        {
            int pointsToRefund = (purchase.Price * 80) / 100;

            return BLpurchase.cancelPurchaseByPoints(pId, purchase.ClientMail, pointsToRefund);
        }

        return BLpurchase.cancelPurchase(pId, email, points);
    }

    protected int getPointsToCancel(int showID)
    {
        //DALshow dalShow = new DALshow();
        Show show = BLshow.getShowsById(showID);

        return show.Points;
    }

    protected void ConfirmDeletePurchase_Click(object sender, EventArgs e)
    {
        purchaseId = Request.QueryString["purchaseId"];
        showId = Request.QueryString["showId"];
        email = Session["eMail"] + "";

        if (purchaseId == null || showId == null)
            Response.Redirect("/Default.aspx");

        else if (Session["eMail"] == null)
            Response.Redirect("/Default.aspx");

        CancelPurchase();

        Response.Redirect("MyPurchases.aspx");
    }
}