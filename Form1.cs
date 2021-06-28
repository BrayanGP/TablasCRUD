using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TablasCRUD
{
    public partial class panelGeneral : Form
    {
        modelo mod = new modelo();
        bool en;
      
        public panelGeneral()
        {
            InitializeComponent();
            panelHiden.Visible = false;
       
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {

            panelHiden.Visible = true;
            en = true;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            int id =Convert.ToInt32(listNombre.SelectedValue);
            List<estadoAlumno> lista = mod.ConsultarLst();
            estadoAlumno art = lista.Find(x => x.id == id);
            txtClave.Text = art.clave;
            txtNombre.Text = art.nombre;

            panelHiden.Visible = true;
            en = false;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            mod.EliminarEstatus(Convert.ToInt32(listNombre.SelectedValue));
            refrescar();
            panelHiden.Visible = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            panelHiden.Visible = false;
        }

        public void listNombre_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void panelGeneral_Load(object sender, EventArgs e)
        {


            List<estadoAlumno> lista = mod.ConsultarLst();
            listNombre.DataSource = lista;
            listNombre.DisplayMember = "nombre";
             listNombre.ValueMember = "id";
           
            


            dataGridEstatus.DataSource = lista;

          

        }

        private void dataGridEstatus_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if(en==true)
            {
                mod.AgregarEstatus(txtNombre.Text, txtClave.Text);
                refrescar();
            }

            else
            {
                int id = Convert.ToInt32(listNombre.SelectedValue);
                mod.ModificarEstatus(id, txtNombre.Text, txtClave.Text);
                refrescar();

            }


      

        }



        private void refrescar()
        {
            BindingSource bind = new BindingSource();
            List<estadoAlumno> lista = mod.ConsultarLst();
            bind.DataSource = lista;
            listNombre.DataSource = bind;
            dataGridEstatus.DataSource = bind;
        }
    }
    
}
