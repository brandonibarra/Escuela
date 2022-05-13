using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Escuela_BLL;
using Escuela_DAL;

namespace Escuela.Alumnos
{
    public partial class alumno_i : TemaEscuela,IAcceso
    {

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (sesioniniciada())
                {
                    cargarFacultades();
                    cargarEstados();
                    cargarTabla();
                    cargarMaterias();
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
                
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            agregarAlumno();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Alta", "alert('Alumno agregado exitosamente.')", true);
        }

        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlEstado.SelectedIndex != 0)
            {
                ddlCiudad.Items.Clear();
                cargarCiudades();
            }
            else
            {
                ddlCiudad.Items.Clear();
            }
        }
        #endregion

        #region Metodos
        public void agregarAlumno()
        {
            AlumnoBLL alumBLL = new AlumnoBLL();
            Alumno alumno = new Alumno();

            alumno.matricula = int.Parse(txtMatricula.Text);
            alumno.nombre = txtNombre.Text;
            alumno.fechaNacimiento = Convert.ToDateTime(txtFechaNacimento.Text);
            alumno.semestre = int.Parse(txtSemestre.Text);
            alumno.facultad = int.Parse(ddlFacultad.SelectedValue);
            alumno.ciudad = int.Parse(ddlCiudad.SelectedValue);

            try
            {
                MateriaAlumno materiaAlum;
                List<MateriaAlumno> listMaterias = new List<MateriaAlumno>(); 

                foreach(ListItem item in listBoxMaterias.Items)
                {
                    if (item.Selected)
                    {
                        materiaAlum = new MateriaAlumno();
                        materiaAlum.materia = int.Parse(item.Value);
                        materiaAlum.alumno = alumno.matricula;
                        listMaterias.Add(materiaAlum);
                    }
                }

                



                alumBLL.agregarAlumno(alumno, listMaterias);
                limpiarCampos();

             
            }catch(Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Alta", "alert('"+ ex.Message + "')", true);
            }
            
        }

        public void cargarFacultades()
        {
            FacultadBLL facuBLL = new FacultadBLL();
            List<Facultad> listFacultad = new List<Facultad>();

            listFacultad = facuBLL.cargarFacultades();

            ddlFacultad.DataSource = listFacultad;
            ddlFacultad.DataTextField = "nombre";
            ddlFacultad.DataValueField = "ID_Facultad";
            ddlFacultad.DataBind();

            ddlFacultad.Items.Insert(0, new ListItem("------Seleccione Facultad-----", "0"));
        }

        public void limpiarCampos()
        {
            txtMatricula.Text = "";
            txtNombre.Text = "";
            txtFechaNacimento.Text = "";
            txtSemestre.Text = "";
            ddlFacultad.SelectedIndex = 0;

        }   



        public void cargarTabla()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("matricula");
            dt.Columns.Add("nombre");

            ViewState["tablaAlumnos"] = dt;
        }

        public void cargarEstados()
        {
            EstadoBLL estado = new EstadoBLL();
            DataTable dtEstados = new DataTable();

            dtEstados = estado.cargarEstados();

            ddlEstado.DataSource = dtEstados;
            ddlEstado.DataTextField = "nombre";
            ddlEstado.DataValueField = "ID_Estado";
            ddlEstado.DataBind();

            ddlEstado.Items.Insert(0, new ListItem("------Seleccione Estado-----", "0"));

        }

        public void cargarCiudades()
        {
            CiudadBLL ciudad = new CiudadBLL();
            DataTable dtCiudades = new DataTable();

            dtCiudades = ciudad.cargarCiudadesPorEstado(int.Parse(ddlEstado.SelectedValue));

            ddlCiudad.DataSource = dtCiudades;
            ddlCiudad.DataTextField = "nombre";
            ddlCiudad.DataValueField = "ID_Ciudad";
            ddlCiudad.DataBind();

            ddlCiudad.Items.Insert(0, new ListItem("------Seleccione Ciudad-----", "0"));

        }

        public void cargarMaterias()
        {
            MateriaBLL materia = new MateriaBLL();
            List<Materia> listMaterias = new List<Materia>();

            listMaterias = materia.cargarMaterias();

            listBoxMaterias.DataSource = listMaterias;
            listBoxMaterias.DataTextField = "nombre";
            listBoxMaterias.DataValueField = "ID_Materia";
            listBoxMaterias.DataBind();



        }



        public bool sesioniniciada()
        {
            if (Session["Usuario"] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        #endregion

        
    }
}