using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form5 : Form
    {
        // Declare SqlConnection as a class-level variable
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-7J1QUL9\\SQLEXPRESS;Initial Catalog=LMS;Integrated Security=True");

        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            // You can perform any initialization here if needed
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Open the connection
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM REG_FORM", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate that StudentID is provided
                string studentIDToFilter = textBox1.Text.Trim();

                if (string.IsNullOrWhiteSpace(studentIDToFilter))
                {
                    MessageBox.Show("Please enter StudentID for Searching Record.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Stop execution if validation fails
                }

                // Open the connection
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM REG_FORM WHERE StudentID = @StudentID", con);
                cmd.Parameters.AddWithValue("@StudentID", studentIDToFilter);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    // Records found, display them
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    // No records found, show an error message
                    MessageBox.Show($"No records found with StudentID {studentIDToFilter}.", "Retrieve Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
    }
}
