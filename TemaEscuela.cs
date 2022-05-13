using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Escuela
{
    public class TemaEscuela: System.Web.UI.Page
    {

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["Usuario"] != null)
            {
                DataTable dtUsuario = new DataTable();
                dtUsuario = (DataTable)Session["Usuario"];


                string tipo = dtUsuario.Rows[0]["tipo"].ToString();

                if(tipo == "Administrador")
                {
                    Page.Theme = "Tema1";

                }
                else
                {
                    Page.Theme = "Tema2";
                }

            }
        }
    }
}