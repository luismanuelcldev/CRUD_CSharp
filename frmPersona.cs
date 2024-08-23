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
    public partial class frmPersona : Form
    {
        PERSONA oPersonaEditar = null;
        public frmPersona(PERSONA oPersonaRecibida = null)
        {
            InitializeComponent();

            if (oPersonaRecibida != null)
            {
                oPersonaEditar = oPersonaRecibida;

                txtNombres.Text = oPersonaRecibida.Nombres;
                txtTelefono.Text = oPersonaRecibida.Telefono;
            }

         }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            if(oPersonaEditar != null)
            {
                using (CNX_DBPRUEBAS db = new CNX_DBPRUEBAS())
                {
                    PERSONA oPersonaParaEditar = (from seleccionado in db.PERSONA
                                                   where seleccionado.IdPersona == oPersonaEditar.IdPersona
                                                   select seleccionado
                                                   ).FirstOrDefault();

                    oPersonaEditar.Nombres = txtNombres.Text;
                    oPersonaEditar.Telefono = txtTelefono.Text;

                    db.SaveChanges();

                }
                
            }
            else
            {
                using (CNX_DBPRUEBAS db = new CNX_DBPRUEBAS())
                {
                    db.PERSONA.Add(new PERSONA()
                    {
                        Nombres = txtNombres.Text,
                        Telefono = txtTelefono.Text

                    });

                    db.SaveChanges();
                }
            }
            

            this.Close();

        }
    }
}
