using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate that StudentID is provided
                if (string.IsNullOrWhiteSpace(textBox7.Text))
                {
                    MessageBox.Show("Please enter StudentID for deletion.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Stop execution if validation fails
                }

                using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-7J1QUL9\\SQLEXPRESS;Initial Catalog=LMS;Integrated Security=True"))
                {
                    con.Open();

                    // Provide the SqlConnection to the SqlCommand constructor
                    SqlCommand cmd = new SqlCommand("DELETE FROM REG_FORM WHERE StudentID=@StudentID", con);

                    // Assuming your parameters are of type string. Adjust SqlDbType accordingly.
                    cmd.Parameters.AddWithValue("@StudentID", textBox7.Text);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Successfully Deleted");
                    }
                    else
                    {
                        MessageBox.Show("No record found with the provided StudentID.", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-7J1QUL9\\SQLEXPRESS;Initial Catalog=LMS;Integrated Security=True"))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM REG_FORM", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
