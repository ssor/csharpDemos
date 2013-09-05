using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Community.CsharpSqlite.SQLiteClient;
using System.Diagnostics;

namespace Csharp_sqlite_demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dbFilename = @"db.db3";
            string cs = string.Format("Version=3,uri=file:{0}", dbFilename);
            Console.WriteLine("Set connection String: {0}", cs);
            SqliteConnection con = new SqliteConnection();

            con.ConnectionString = cs;

            Console.WriteLine("Open database...");
            con.Open();

            //Console.WriteLine("create command...");
            //IDbCommand cmd = con.CreateCommand();

            //SqliteCommand command = new SqliteCommand("PRAGMA table_info('T_CLASS_CHECK_INFO')", con);
            //DataTable dataTable = new DataTable();
            //SqliteDataAdapter dataAdapter = new SqliteDataAdapter();
            //dataAdapter.SelectCommand = command;
            //dataAdapter.Fill(dataTable);
            //DisplayDataTable(dataTable, "Columns");


            IDbCommand cmd = con.CreateCommand();
            cmd.CommandText = "insert into T_QUESTION(question_id, caption) values('112211111', '研一');";

            if (con.State == ConnectionState.Closed)
                con.Open();
            int result = cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

        public void DisplayDataTable(DataTable table, string name)
        {
            Debug.WriteLine("Display DataTable: {0}", name);
            int r = 0;
            foreach (DataRow row in table.Rows)
            {
                Debug.WriteLine(string.Format("Row {0}", r));
                int c = 0;
                foreach (DataColumn col in table.Columns)
                {

                    Debug.WriteLine(string.Format("   Col {0}: {1} {2}", c, col.ColumnName, col.DataType));
                    Debug.WriteLine(string.Format("       Value: {0}", row[col]));
                    c++;
                }
                r++;
            }
            Debug.WriteLine(string.Format("Rows in data table: {0}", r));

        }
    }
}
