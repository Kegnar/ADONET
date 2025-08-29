using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lesson_6_DataGridView
{
    public partial class Form1 : Form
    {
        SqlDataAdapter da = null;
        DataSet ds = null;
        string fileName = "";
        SqlConnection connection = null;
        SqlCommandBuilder cmdBuilder = null;
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        
        public Form1()
        {
            InitializeComponent();
            
            

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            da.Update(ds, "Table_1");
        }

        private void btnFill_Click(object sender, EventArgs e)
        {
            connection = new SqlConnection(connectionString);;
            try
            {
                string sqld = inputBox.Text;
                ds = new DataSet();
                da = new SqlDataAdapter(sqld, connection);
                dataGridView1.DataSource = null;
                cmdBuilder = new SqlCommandBuilder(da);
                da.Fill(ds, "Table_1");
                dataGridView1.DataSource = ds.Tables["Table_1"];
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
           
            
        }
    }
}