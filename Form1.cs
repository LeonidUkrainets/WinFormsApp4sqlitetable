using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace WinFormsApp4sqlitetable
{
    public partial class Form1 : Form
    {
        private const string ConnectionString = "Data Source = students.db; Version=3;";
        public Form1()
        {
            InitializeComponent();
            LoadData();
        }
        private void Form1_Load()
        {

        }

        private void LoadData()
        {
            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "SELECT StudentId AS Id, StudentName AS 'Ім’я та прізвище студента', DateOfBirth FROM students;";
                    using (var adapter = new SQLiteDataAdapter(query, connection))
                    {
                        var table = new DataTable();
                        adapter.Fill(table);
                        dataGridView1.DataSource = table;
                        //dataGridView1.Columns["StudentId"].Width = 10;
                        //dataGridView1.Columns["StudentName"].Width = 200;
                        //dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.ColumnHeader);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Повідомлення про виняток", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    string columnName = dataGridView1.Columns[e.ColumnIndex].Name;
                    object Newvalue   = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    int     studentId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value);
                    string query = $"UPDATE students SET '{columnName}'=@Newvalue WHERE StudentId==@studentId";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Newvalue", Newvalue);
                        command.Parameters.AddWithValue("@studentId", studentId);
                        command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Повідомлення про виняток", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}