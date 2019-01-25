using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Form_DataTableToPDF
{
    public partial class Form1 : Form
    {
        private int i;
        private int j;

        public Form1()
        {
            InitializeComponent();
            this.BindDataGridView();
        }

        private void BindDataGridView()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[4] { new DataColumn("Id", typeof(int)),
            new DataColumn("İsim", typeof(string)),
            new DataColumn("Şehir", typeof(string)),
            new DataColumn("E-Mail",typeof(string)) });
            dt.Rows.Add(1, "Ali UYSAL", "tokat", "test@ali.com");
            dt.Rows.Add(2, "Ahmet", "istanbul", "test@ahmet.com");
            dt.Rows.Add(3, "Mehmet", "ankara", "test@mehmet.com");
            dt.Rows.Add(4, "Arif", "izmir", "test@arif.com");
            this.dataGridView1.DataSource = dt;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            iTextSharp.text.pdf.BaseFont STF_Helvetica_Turkish = iTextSharp.text.pdf.BaseFont.CreateFont("Helvetica", "CP1254", iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font fontNormal = new iTextSharp.text.Font(STF_Helvetica_Turkish, 12, iTextSharp.text.Font.NORMAL);
            PdfPTable pdfTable = new PdfPTable(dataGridView1.ColumnCount);
            pdfTable.DefaultCell.Padding = 3;
            pdfTable.WidthPercentage = 30;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.DefaultCell.BorderWidth = 1;

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, fontNormal));
                pdfTable.AddCell(cell);
            }

            int row = dataGridView1.Rows.Count;
            int cell2 = dataGridView1.Rows[1].Cells.Count;
            for (int i = 0; i < row - 1; i++)
            {
                for (int j = 0; j < cell2; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value == null)
                    {

                        dataGridView1.Rows[i].Cells[j].Value = "null";
                    }
                    pdfTable.AddCell(dataGridView1.Rows[i].Cells[j].Value.ToString());
                    

                }
            }


            //PDF'e Aktar
            string folderPath = @"C:\pdf\";

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            using (FileStream stream = new FileStream(folderPath + "DataGrid.pdf", FileMode.Create))
            {
                Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                pdfDoc.Add(pdfTable);
                pdfDoc.Close();
                stream.Close();
            }
            MessageBox.Show("PDF Oluşturuldu " + folderPath);
        }
       
    }
}
