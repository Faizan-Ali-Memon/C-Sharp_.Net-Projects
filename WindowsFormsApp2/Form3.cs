using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-7J1QUL9\\SQLEXPRESS;Initial Catalog=LMS;Integrated Security=True"))
                {
                    con.Open();

                    // Check if the record exists before updating
                    if (!RecordExists(con, textBox1.Text))
                    {
                        MessageBox.Show("Record with StudentID " + textBox1.Text + " does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Construct the dynamic SQL UPDATE statement
                    string updateSql = "UPDATE REG_FORM SET ";
                    if (!string.IsNullOrWhiteSpace(textBox8.Text))
                        updateSql += "FullName=@FullName, ";
                    if (!string.IsNullOrWhiteSpace(GetSelectedGender()))
                        updateSql += "GENDER=@GENDER, ";
                    if (!string.IsNullOrWhiteSpace(comboBox2.Text))
                        updateSql += "CITY=@CITY, ";
                    if (!string.IsNullOrWhiteSpace(textBox11.Text))
                        updateSql += "PHONENUMBER=@PHONENUMBER, ";
                    if (!string.IsNullOrWhiteSpace(comboBox1.Text))
                        updateSql += "Enrolled_Courses=@Enrolled_Courses, ";

                    // Remove the trailing comma and add the WHERE clause
                    updateSql = updateSql.TrimEnd(',', ' ') + " WHERE StudentID=@StudentID";

                    using (SqlCommand cmd = new SqlCommand(updateSql, con))
                    {
                        cmd.Parameters.AddWithValue("@StudentID", textBox1.Text);
                        if (!string.IsNullOrWhiteSpace(textBox8.Text))
                            cmd.Parameters.AddWithValue("@FullName", textBox8.Text);
                        if (!string.IsNullOrWhiteSpace(GetSelectedGender()))
                            cmd.Parameters.AddWithValue("@GENDER", GetSelectedGender());
                        if (!string.IsNullOrWhiteSpace(comboBox2.Text))
                            cmd.Parameters.AddWithValue("@CITY", comboBox2.Text);
                        if (!string.IsNullOrWhiteSpace(textBox11.Text))
                            cmd.Parameters.AddWithValue("@PHONENUMBER", textBox11.Text);
                        if (!string.IsNullOrWhiteSpace(comboBox1.Text))
                            cmd.Parameters.AddWithValue("@Enrolled_Courses", comboBox1.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Successfully Updated");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private bool RecordExists(SqlConnection connection, string studentID)
        {
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM REG_FORM WHERE StudentID=@StudentID", connection);
            cmd.Parameters.AddWithValue("@StudentID", studentID);
            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-7J1QUL9\\SQLEXPRESS;Initial Catalog=LMS;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM REG_FORM", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private string GetSelectedGender()
        {
            if (radioButtonMale.Checked)
                return "Male";
            else if (radioButtonFemale.Checked)
                return "Female";
            else
                return string.Empty;
        }
    }
}
