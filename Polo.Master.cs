using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace ProvaDB
{
    public partial class Polo : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Debug.WriteLine(MainContentID.Page.ToString());
            switch(MainContentID.Page.ToString())
            {
                case "ASP.default_aspx":
                    lnkHome.CssClass = "active";
                    break;
                case "ASP.contatti_aspx":
                    lnkContatti.CssClass = "active";
                    break;
                default:
                break;
            }
        }
    }
}