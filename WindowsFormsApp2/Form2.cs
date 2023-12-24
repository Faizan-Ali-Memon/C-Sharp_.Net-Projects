using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-7J1QUL9\\SQLEXPRESS;Initial Catalog=LMS;Integrated Security=True");
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if FullName and Enrolled_Courses are provided
                if (string.IsNullOrWhiteSpace(textBox8.Text))
                {
                    MessageBox.Show("Please enter all * mandatory fields", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Stop execution if validation fails
                }

                if (string.IsNullOrWhiteSpace(textBox7.Text))
                {
                    MessageBox.Show("Please enter your StudentID", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Stop execution if validation fails
                }

                // Convert StudentID to an integer
                if (!int.TryParse(textBox7.Text, out int studentID))
                {
                    MessageBox.Show("Please enter a valid StudentID (numeric value).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Stop execution if validation fails
                }

                // Determine the selected gender based on the radio buttons
                string gender = radioButtonMale.Checked ? "Male" : (radioButtonFemale.Checked ? "Female" : "");

                if (string.IsNullOrWhiteSpace(gender))
                {
                    MessageBox.Show("Please select a gender.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Stop execution if validation fails
                }

                if (string.IsNullOrWhiteSpace(comboBox3.Text))
                {
                    MessageBox.Show("Please SELECT ANY COURSE.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Stop execution if validation fails
                }

                using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-7J1QUL9\\SQLEXPRESS;Initial Catalog=LMS;Integrated Security=True"))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO REG_FORM VALUES (@FullName, @StudentID, @GENDER, @CITY, @PHONENUMBER, @Enrolled_Courses )", con);

                    // Assuming your parameters are of type string. Adjust the SqlDbType accordingly.
                    cmd.Parameters.AddWithValue("@FullName", textBox8.Text);
                    cmd.Parameters.AddWithValue("@StudentID", textBox7.Text);
                    cmd.Parameters.AddWithValue("@GENDER", gender);
                    cmd.Parameters.AddWithValue("@CITY", comboBox2.Text);
                    cmd.Parameters.AddWithValue("@PHONENUMBER", textBox11.Text); // Assuming PHONENUMBER is VARCHAR(20)
                    cmd.Parameters.AddWithValue("@Enrolled_Courses", comboBox3.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully Saved");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}


