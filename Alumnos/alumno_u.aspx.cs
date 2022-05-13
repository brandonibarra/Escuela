using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Escuela_BLL;
using Escuela_DAL;
using System.Web.UI.WebControls;

namespace Escuela.Alumnos
{
    public partial class alumno_u : TemaEscuela,IAcceso
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (sesioniniciada())
                {
                    int matricula = int.Parse(Request.QueryString["pMatricula"]);
                    cargarFacultades();
                    cargarEstados();
                    cargarAlumno(matricula);
                  
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
                
            }

        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            modificarAlumno();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Alta", "alert('Alumno modificado exitosamente.')", true);
        }

        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEstado.SelectedIndex != 0)
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
        public void cargarAlumno(int matricula)
        {
            AlumnoBLL alumBLL = new AlumnoBLL();
            Alumno alumno = new Alumno();

            alumno = alumBLL.cargarAlumno(matricula);

            lblMatricula.Text = alumno.matricula.ToString();
            txtNombre.Text = alumno.nombre;
            txtFechaNacimento.Text = alumno.fechaNacimiento.ToString().Substring(0,10);
            txtSemestre.Text = alumno.semestre.ToString();
            ddlFacultad.SelectedValue = alumno.facultad.ToString();

            cargarEstados();
            ddlEstado.SelectedValue = alumno.Ciudad1.estado.ToString();

            cargarCiudades();
            ddlCiudad.SelectedValue = alumno.ciudad.ToString();

            cargarMaterias();
            List<MateriaAlumno> listMaterias = new List<MateriaAlumno>();
            listMaterias = alumno.MateriaAlumno.ToList();

            foreach(MateriaAlumno materiaAlum in listMaterias)
            {
                listBoxMaterias.Items.FindByValue(materiaAlum.materia.ToString()).Selected = true;
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

        public void modificarAlumno()
        {

            AlumnoBLL alumBLL = new AlumnoBLL();
            Alumno alumno = new Alumno();

            alumno.matricula = int.Parse(lblMatricula.Text);
            alumno.nombre = txtNombre.Text;
            alumno.fechaNacimiento = Convert.ToDateTime(txtFechaNacimento.Text);
            alumno.semestre = int.Parse(txtSemestre.Text);
            alumno.facultad = int.Parse(ddlFacultad.SelectedValue);
            alumno.ciudad = int.Parse(ddlCiudad.SelectedValue);

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

            alumBLL.modificarAlumno(alumno, listMaterias);
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