using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lesson_5_1_WinForms
{
    public partial class Form1 : Form
    {
        
        SqlDataAdapter da = null;
        DataSet ds = null;
        string fileName = "";
        SqlConnection connection = null;

        public Form1()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            connection = new SqlConnection(connectionString);
            
            InitializeComponent();
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void loadBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter="Графические файлы| *.bmp; *.jpeg; *.png; *.gif";
            ofd.FileName="";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                fileName = ofd.FileName;
                LoadPicture();
            }
        }

        private void LoadPicture()
        {
            try
            {
                byte[] bytes = CreateCopy();
                connection.Open();
                SqlCommand cmd = new SqlCommand("insert into Pictures (Customer_ID, _Name, Picture) " +
                                                "values (@customerID, @name, @picture);", connection);
                int index = -1;
                int.TryParse(textBox1.Text, out index);
                cmd.Parameters.Add("@customerID", SqlDbType.Int).Value = index;
                cmd.Parameters.Add("@name", SqlDbType.NVarChar, 255).Value = fileName;
                cmd.Parameters.Add("@picture", SqlDbType.Image, bytes.Length).Value = bytes;
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
            finally
            {
                if (connection != null) connection.Close();
            }
        }

        private byte[] CreateCopy()
        {
            try
            {
                Image img = Image.FromFile(fileName);
                int maxWidth = 300, maxHeight = 300;
                double ratioX = (double)maxWidth / img.Width;
                double ratioY = (double)maxHeight / img.Height;
                double ratio = Math.Min(ratioX, ratioY);
                int newWidth = (int)(img.Width * ratio);
                int newHeight = (int)(img.Height * ratio);
                Image newImage = new Bitmap(newWidth, newHeight);
                Graphics g = Graphics.FromImage(newImage);
                g.DrawImage(img, 0, 0, newWidth, newHeight);
                MemoryStream ms = new MemoryStream();
                newImage.Save(ms, ImageFormat.Jpeg);
                ms.Flush();
                ms.Seek(0, SeekOrigin.Begin);
                BinaryReader br = new BinaryReader(ms);
                byte[] buf = br.ReadBytes((int)ms.Length);
                return buf;
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
           
        }

        private void showOneBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == null || textBox1.Text.Length == 0)
                {
                    MessageBox.Show("Укажите id клиента");
                    return;
                }
                int index = -1;
                int.TryParse(textBox1.Text, out index);
                if (index == -1)
                {
                    MessageBox.Show("Укажите id клиента в правильном формате");
                    return;
                }
                da = new SqlDataAdapter("select Picture from dbo.Pictures where Customer_ID=@Id", connection);
                SqlCommandBuilder cmb = new SqlCommandBuilder(da);
                da.SelectCommand.Parameters.Add("@Id", SqlDbType.Int).Value = index;
                ds = new DataSet();
                da.Fill(ds);
                byte[] bytes = (byte[])ds.Tables[0].Rows[0]["Picture"];
                MemoryStream ms = new MemoryStream(bytes);
                pictureBox1.Image = Image.FromStream(ms);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void showAllBtn_Click(object sender, EventArgs e)
        {
            try
            {
                da = new SqlDataAdapter("select * from dbo.Pictures;", connection);
                SqlCommandBuilder cmd=new SqlCommandBuilder(da);
                ds=new DataSet();
                da.Fill(ds, "picture");
                dataGridView1.DataSource = ds.Tables["Picture"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}