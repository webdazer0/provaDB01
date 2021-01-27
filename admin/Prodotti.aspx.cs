using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using System.Data.SqlClient;
namespace ProvaDB
{
    public partial class Prodotti : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //Connetto il Database all'applicazione
            dbc.ConnectionString = ConfigurationManager.ConnectionStrings["dbPrincipale"].ConnectionString;
            //Creo una DataSource per la GridView
            dbc.SelectCommand = "SELECT * FROM Prodotti";

            //Associo la DataSource al GridView
            grdProdotti.DataSource = dbc;

            //Solo se l'utente esegue la pagina popolo la DataGrid
            if (!Page.IsPostBack)
            {
                //Popolo il GridView con i Dati presenti nel DataSource
                grdProdotti.DataBind();
            }
        }

        protected void grdProdotti_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ID Selezionato per la modifica
            string idSelezionato = grdProdotti.SelectedValue.ToString();
            //I:Creoo una datasource per il FormView  
            dbc.SelectCommand = "SELECT * FROM Prodotti WHERE Id=@ExtID";
            dbc.SelectParameters.Clear();
            dbc.SelectParameters.Add("ExtID", System.Data.DbType.String, idSelezionato);
            //F:Creoo una datasource per il FormView  

            //Associo la DataSource al FormView
            frmProdotto.DataSource = dbc;
            //Popolo il FormView con i Dati presenti nel DataSource
            frmProdotto.DataBind();
            //Mostro la FormView
            frmProdotto.Visible = true;
        }

        protected void btnAggiorna_Click(object sender, EventArgs e)
        {
            //Prendo l'id dell'elemento dalla griglia
            string idSelezionato = grdProdotti.SelectedValue.ToString();

            //Controllo l'esistenza della cartella Upload su server 
            string realPath = Server.MapPath("~/");
            if(!Directory.Exists(realPath + "Upload"))
            {
                Directory.CreateDirectory(realPath + "Upload");
            }
            //Recupero i WebControl situati dentro la FormView
            FileUpload uplImmagine = (FileUpload)frmProdotto.FindControl("uplImmagine");
            TextBox txtNome = (TextBox)frmProdotto.FindControl("txtNome");
            TextBox txtPrezzo = (TextBox)frmProdotto.FindControl("txtPrezzo");
            TextBox txtDescrizione = (TextBox)frmProdotto.FindControl("txtDescrizione");
            TextBox txtDescrizione_HTML = (TextBox)frmProdotto.FindControl("txtDescrizione_HTML");
            //Recupero il WebControl DropDownList
            DropDownList drpAttivo = (DropDownList)frmProdotto.FindControl("drpAttivo");

            
            //VErifico se l'utente ha inserto l'immagine
            if (uplImmagine.HasFile )
            {
                string NomeFile = "";
                realPath = Server.MapPath("~/Upload/");
                NomeFile = uplImmagine.FileName;

                //Controllo che sia un'immagine
                if (Path.GetExtension(NomeFile)==".jpg" 
                    || Path.GetExtension(NomeFile) == ".png")
                {
                    //Salvo l'immagine nella cartella di Upload
                    uplImmagine.SaveAs(realPath + uplImmagine.FileName);

                    //I:Aggiorno la tabella Prodotti riportando il nome dell'immagine
                    dbc.UpdateCommand = @"
                                    UPDATE Prodotti
                                        SET                                           
                                           Immagine = @UpdImmagine                                          
                                        WHERE
                                            Id=@ExtID
        
                                 ";
                    dbc.UpdateParameters.Clear();
                    dbc.UpdateParameters.Add("UpdImmagine", System.Data.DbType.String, NomeFile);
                    dbc.UpdateParameters.Add("ExtID", System.Data.DbType.String, idSelezionato);
                    dbc.Update();
                    //F:Aggiorno la tabella Prodotti riportando il nome dell'immagine
                }

            }

            // I:Aggiorno la tabella Prodotti riportando il contenuto dei campi popolati dall'utente
            dbc.UpdateCommand = @"
                                    UPDATE Prodotti
                                        SET 
                                           Nome = @UpdNome 
                                           ,Prezzo = @UpdPrezzo
                                           ,Descrizione = @UpdDescrizione
                                           ,Descrizione_HTML = @UpdDescrizioneHtml
                                           ,Attivo = @UpdAttivo
                                        WHERE
                                            Id=@ExtID
        
                                 ";
            

            dbc.UpdateParameters.Clear();
            //Associo i parametri delle TextBox nel FormView con i parametri della query di Update 
            dbc.UpdateParameters.Add("UpdNome", System.Data.DbType.String,txtNome.Text);
            dbc.UpdateParameters.Add("UpdPrezzo", System.Data.DbType.Decimal,txtPrezzo.Text);
            dbc.UpdateParameters.Add("UpdDescrizione", System.Data.DbType.String,txtDescrizione.Text);
            dbc.UpdateParameters.Add("UpdDescrizioneHtml", System.Data.DbType.String,txtDescrizione_HTML.Text);
            //Associo i parametri della DropDownList nel FormView con i parametri della query di Update 
            dbc.UpdateParameters.Add("UpdAttivo", System.Data.DbType.String, drpAttivo.SelectedValue);
            //Associo l'id Selezionato dalla GridView per la Query di Update (@ExtID)
            dbc.UpdateParameters.Add("ExtID", System.Data.DbType.String, idSelezionato);
            //eseguo il comando di Update
            dbc.Update();
            // F:Aggiorno la tabella Prodotti riportando il contenuto dei campi popolati dall'utente

            // Aggiorno la GridView
            grdProdotti.DataBind();
            //Nascondo il FormView
            frmProdotto.Visible = false;

        }

        protected void grdProdotti_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //Recupera id Selezionato dall'utente per l'eliminazione
            string idSelezionato = e.Keys["Id"].ToString();
            
            //Connetto al DB
            SqlConnection oConn = new SqlConnection(dbc.ConnectionString);
            oConn.Open();
            //Leggiamo da Db il nome del File relativo alla riga che stiamo per eliminare
            string sSQL = @" SELECT * FROM Prodotti WHERE Id=@ExtId";
            SqlCommand oCmd = new SqlCommand(sSQL, oConn);
            oCmd.Parameters.AddWithValue("ExtId", idSelezionato);
            SqlDataReader oReader = oCmd.ExecuteReader();

            //Se presente qualcosa
            if(oReader.Read())
            {
                string NomeFile = oReader["Immagine"].ToString();
                string realPath = Server.MapPath("~/Upload/");
                //Controllo se il file esiste su server nella cartella Upload
                if(File.Exists(realPath + NomeFile))
                {
                    //Elimino il File
                    File.Delete(realPath + NomeFile);
                }
            }


            //I: Eliminazione riga selezionata dall'utente
            dbc.DeleteCommand = "DELETE FROM Prodotti WHERE Id=@ExtID";
            dbc.DeleteParameters.Add("ExtID", System.Data.DbType.String, idSelezionato);
            dbc.Delete();
            //F: Eliminazione riga selezionata dall'utente

            //Aggiorna la GridView
            grdProdotti.DataBind();
            
        }

        protected void btnAnnulla_Click(object sender, EventArgs e)
        {
            //Alla pressione del Bottone annulla nascondo il modulo del Form View
            frmProdotto.Visible = false;
        }
    }
}