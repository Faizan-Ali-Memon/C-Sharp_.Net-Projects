using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        // Declare SqlConnection as a class-level variable
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-7J1QUL9\\SQLEXPRESS;Initial Catalog=LMS;Integrated Security=True");

        public Form1()
        {
            InitializeComponent();
            this.Load += Form5_Load;
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            // Load all records when the form is opened
            LoadAllRecords();
        }

        private void LoadAllRecords()
        {
            try
            {
                // Open the connection
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM REG_FORM", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView2.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close the connection in a 'finally' block to ensure it's closed even if an exception occurs
                con.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var newform = new Form2();
            newform.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var newform = new Form3();
            newform.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var newform = new Form4();
            newform.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var newform = new Form5();
            newform.Show();
        }
    }
}
