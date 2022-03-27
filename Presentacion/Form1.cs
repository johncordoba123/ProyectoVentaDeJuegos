using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using Presentacion;
using Datos;

namespace Presentacion
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dgvVentas.DataSource = Logica.Productos.Obtener();
            MostrarProductos();
        }

        private void MostrarProductos()
        {
            dgvVentas.DataSource = Logica.Productos.Obtener();

        }


        public void ExportToExcel(DataGridView listado)
            {
                Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                app.Application.Workbooks.Add(true);
                int indexColumn = 0;
                foreach (DataGridViewColumn columna in listado.Columns)
                {
                    indexColumn++;
                    app.Cells[1, indexColumn] = columna.Name;
                }
                int indexRow = 0;
                foreach (DataGridViewRow fila in listado.Rows)
                {
                    indexRow++;
                    indexRow = 0;
                    foreach (DataGridViewColumn columna in listado.Columns)
                    {
                        indexColumn++;
                        app.Cells[indexRow + 1, indexColumn] = fila.Cells[columna.Name].Value;
                    }
                }
                app.Visible = true;
            }

        public void ExportToWord(DataGridView listado)
        {
            var app = new Microsoft.Office.Interop.Word.Application();
            var doc = app.Documents.Add();

            object missing = System.Reflection.Missing.Value;

            Table tbl = doc.Tables.Add(doc.Content, listado.RowCount + 1, listado.ColumnCount);

            for (var columna = 0; columna < listado.ColumnCount; columna++)
            {
                tbl.Cell(1, columna).Range.Text = listado.Columns[columna].HeaderText;
            }
            for (var fila = 0; fila < listado.RowCount;fila ++)
            {
                for (var columna = 0; columna < listado.ColumnCount; columna++)
                {
                    tbl.Cell(fila + 2, columna + 1).Range.Text = listado.Rows[fila].Cells[columna].FormattedValue.ToString();
                }
            }
            tbl.set_Style(WdBuiltinStyle.wdStyleTableLightListAccent1);
            app.Visible = true;
        }


        private void btnExportaraExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel(dgvVentas);
        }

        private void btnExportaraWord_Click(object sender, EventArgs e)
        {
            ExportToWord(dgvVentas);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            int? id = null;
            Form2 formCrea = new Form2();
            formCrea.ShowDialog();
            MostrarProductos();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            int? id = int.Parse(dgvVentas.Rows[dgvVentas.CurrentRow.Index].Cells[0].Value.ToString());
            if (id != null)
            {
                Form2 edit = new Form2(id);
                edit.ShowDialog();
                MostrarProductos();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int? id = int.Parse(dgvVentas.Rows[dgvVentas.CurrentRow.Index].Cells[0].Value.ToString());
            if (id != null)
            {
                using (VentasEntities1 db = new VentasEntities1())
                {
                    Productos listado = db.Productos.Find(id);
                    db.Productos.Remove(listado);
                    db.SaveChanges();
                }
                MostrarProductos();
            }
        }
    }
}

