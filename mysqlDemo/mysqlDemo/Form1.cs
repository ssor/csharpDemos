using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace mysqlDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void QueryCommand(MySqlCommand cmd)
        {
            cmd.CommandText = "SELECT * FROM new_table1";
            cmd.CommandType = CommandType.Text;

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Debug.WriteLine(String.Format("{0}, {1}, {2}",
                    reader.GetInt32(0), reader.GetString(1), reader.GetDouble(2))
                );
            }

            reader.Close();
        }


        public void InsertCommand(MySqlCommand cmd, string id, string name)
        {
            //byte[] unicodeBytes = Encoding.Unicode.GetBytes(name);
            //byte[] utf8Bytes = Encoding.Convert(Encoding.Unicode, Encoding.UTF8, unicodeBytes);
            //string newName = Encoding.UTF8.GetString(utf8Bytes);

            cmd.CommandText = "insert into new_table1(id,name) values("+id+",'"+name+"');";
            //cmd.CommandText = "insert into new_table1(id,name) values(@id,@name)";
            //cmd.CommandType = CommandType.Text;

            //cmd.Parameters.Add(new MySqlParameter("id", id));
            //cmd.Parameters.Add(new MySqlParameter("name", name));

            cmd.ExecuteNonQuery();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnectionStringBuilder connBuilder =
                             new MySqlConnectionStringBuilder();

            connBuilder.Add("Database", "logicbase");
            connBuilder.Add("Data Source", "localhost");
            connBuilder.Add("User Id", "root");
            connBuilder.Add("Password", "078515");
            connBuilder.Add("Charset", "utf8");

            MySqlConnection connection =
                new MySqlConnection(connBuilder.ConnectionString);

            MySqlCommand cmd = connection.CreateCommand();

            connection.Open();

            // Here goes the code needed to perform operations on the

            this.InsertCommand(cmd, "1", "数据1");
            this.InsertCommand(cmd, "2", "数据2");

            // database such as querying or inserting rows into a table


            connection.Close();
        }
    }
}
