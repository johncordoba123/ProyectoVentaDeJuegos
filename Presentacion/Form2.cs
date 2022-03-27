using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Datos;
using Logica;


namespace Presentacion
{
    public partial class Form2 : Form
    {
        Datos.Productos lista = null;
        public int? id;
        public Form2(int? id= null)
        {
            InitializeComponent();
            this.id = id;
            if (id != null)
            {
                cargarDatosFormularios();
            }
        }

        private void cargarDatosFormularios()
        {
            using (VentasEntities1 db = new VentasEntities1())
            {
                lista = db.Productos.Find(id);
                txtProducto.Text = lista.Nombre_Producto;
                txtStock.Text = lista.Stock.ToString();
                txtValor.Text = lista.Valor.ToString();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            using (VentasEntities1 db = new VentasEntities1())
            {
                if (id == null)
                {
                 lista = new Datos.Productos();
                }
                lista.Nombre_Producto = txtProducto.Text;
                lista.Stock = int.Parse(txtStock.Text);
                lista.Valor = int.Parse(txtValor.Text);
                if (id == null)
                {
                    db.Productos.Add(lista);
                }
                else
                {
                    db.Entry(lista).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();
                this.Close();
            }             
        }
    }
}
