using CRUD_Entity.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_Entity
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DiseñoInicial();
            ObtenerPersonas();

        }
         
        private void DiseñoInicial()
        {
            BotonCrear.Cursor = Cursors.Hand;
            BotonCrear.BackColor = Color.Blue;
            BotonCrear.ForeColor = Color.White;
            BotonCrear.FlatStyle = FlatStyle.Flat;

            DataGridViewPersona.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridViewPersona.MultiSelect = false;
            DataGridViewPersona.ReadOnly = true;
            DataGridViewPersona.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewPersona.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


        }

        private void ObtenerPersonas()
        {

            //LIMPIAMOS EL DATAGRIDVIEW
            DataGridViewPersona.DataSource = null;
            DataGridViewPersona.Rows.Clear();
            DataGridViewPersona.Columns.Clear();

            List<PERSONA> oListaPersona;
            using (CNX_DBPRUEBAS db = new CNX_DBPRUEBAS())
            {
                oListaPersona = (from filas in db.PERSONA
                                 select filas).ToList();
            }

            DataGridViewPersona.DataSource = oListaPersona;

            //Agregar Boton Editar
            DataGridViewButtonColumn BotonEditar = new DataGridViewButtonColumn();

            BotonEditar.HeaderText = "Editar";
            BotonEditar.Text = "Editar";
            BotonEditar.Name = "btnEditar";
            BotonEditar.FlatStyle |= FlatStyle.Flat;
            BotonEditar.UseColumnTextForButtonValue = true;
            BotonEditar.CellTemplate.Style.BackColor = Color.Green;
            BotonEditar.CellTemplate.Style.ForeColor = Color.White;

            //Agregar Boton Eliminar
            DataGridViewButtonColumn BotonEliminar = new DataGridViewButtonColumn();

            BotonEliminar.HeaderText = "Eliminar";
            BotonEliminar.Text = "Eliminar";
            BotonEliminar.Name = "btnEliminar";
            BotonEliminar.FlatStyle = FlatStyle.Flat;
            BotonEliminar.UseColumnTextForButtonValue = true;
            BotonEliminar.CellTemplate.Style.BackColor = Color.Red;
            BotonEliminar.CellTemplate.Style.ForeColor = Color.White;

            //Agregamos Los Botones
            DataGridViewPersona.Columns.Add(BotonEditar);
            DataGridViewPersona.Columns.Add(BotonEliminar);

        } 

        private void BotonCrear_Click_1(object sender, EventArgs e)
        {
            frmPersona oFrmPersona = new frmPersona();
            oFrmPersona.ShowDialog();
            ObtenerPersonas();
        }

        private void DataGridViewPersona_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DataGridViewPersona.Columns[e.ColumnIndex].Name == "btnEditar")
            {
                PERSONA personaSeleccionada = (PERSONA)DataGridViewPersona.SelectedRows[0].DataBoundItem;
                frmPersona oFrmPersona = new frmPersona(personaSeleccionada);
                oFrmPersona.ShowDialog();
                ObtenerPersonas();
            }


            if (DataGridViewPersona.Columns[e.ColumnIndex].Name == "btnEliminar")
            {
                PERSONA personaSeleccionada = (PERSONA)DataGridViewPersona.SelectedRows[0].DataBoundItem;

                using (CNX_DBPRUEBAS db = new CNX_DBPRUEBAS())
                {
                    PERSONA personaEliminar = (from persona in db.PERSONA
                                               where persona.IdPersona == personaSeleccionada.IdPersona
                                               select persona).FirstOrDefault();

                    db.PERSONA.Remove(personaEliminar);
                    db.SaveChanges();
                }

                ObtenerPersonas();
            }
        }
    } 
}
