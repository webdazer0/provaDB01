using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace ProvaDB
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptResourceDefinition jQuery = new ScriptResourceDefinition();
            jQuery.Path = "~/Scripts/jquery-1.8.0.min.js";
            jQuery.DebugPath = "~/Scripts/jquery-1.8.0.js";
            jQuery.CdnPath = "http://ajax.microsoft.com/ajax/jQuery/jquery-1.8.0.min.js";
            jQuery.CdnDebugPath = "http://ajax.microsoft.com/ajax/jQuery/jquery-1.8.0.js";
            ScriptManager.ScriptResourceMapping.AddDefinition("jquery", jQuery);
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.WebForms;
        }


        private bool ValidazioneUtente(string username, string password)
        {
            bool tmp = false;
            //Connetto al Database
            string cnn = System.Configuration.ConfigurationManager.ConnectionStrings["dbPrincipale"].ConnectionString;
            SqlConnection oConn = new SqlConnection(cnn);
            oConn.Open();

            //I:Controllo se nel Db sono presenti le credenziali immesse dall'utente in fadse di Login
            string sSQL = @" SELECT * FROM Persone 
                               WHERE
                                    Username=@ExtUsername
                                    AND
                                    Password=@ExtPassword
                                ";

            SqlCommand oCmd = new SqlCommand(sSQL, oConn);
            oCmd.Parameters.AddWithValue("ExtUsername", username);
            oCmd.Parameters.AddWithValue("ExtPassword", password);
            //F:Controllo se nel Db sono presenti le credenziali immesse dall'utente in fadse di Login

            SqlDataReader oReader = oCmd.ExecuteReader();
            if(oReader.Read())
            {
                //Se presenti lascio accedere l'utente
                tmp = true;
            }
            return tmp;
        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            //Controllo credenziali nel Db
            if(ValidazioneUtente(Login1.UserName, Login1.Password))
            {
                e.Authenticated = true;
            }
        }
    }
}