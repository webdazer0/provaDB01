using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace ProvaDB
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            dbc.ConnectionString = ConfigurationManager.ConnectionStrings["dbPrincipale"].ConnectionString;
        }

        protected void btnSalva_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                /*
                int o;
                if (int.TryParse(txtCognome.Text,out o))
                {
                    return;
                }
                */

                dbc.InsertCommand = @"INSERT INTO Persone 
	                                    (Nome,Cognome)
                                        VALUES 
                                        (
                                         @InsNome
                                        ,@InsCognome
                                       )
                                ";
                dbc.InsertParameters.Add("InsNome", System.Data.DbType.String, txtNome.Text);
                dbc.InsertParameters.Add("InsCognome", System.Data.DbType.String, txtCognome.Text);
                dbc.Insert();
            }
        }
    }
}