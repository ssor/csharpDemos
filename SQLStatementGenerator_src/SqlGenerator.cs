using System;
using System.Configuration;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;

namespace SqlStatementGenerator
{
    /// <summary>
    /// Summary description
    /// </summary>
    public partial class SqlGenerator : System.Windows.Forms.Form
    {
        private string m_sSqlConnectionString = string.Empty;
        private DataTable m_TableInfo = null;
        private string m_sSqlStatementText = string.Empty;
        private enum STATEMENT_TYPES { INSERT, UPDATE, DELETE }
        List<TextBox> columnNameTextBoxList = new List<TextBox>();
        List<TextBox> columnAliasNameTextBoxList = new List<TextBox>();
        List<ComboBox> columnDatatypeComboBox = new List<ComboBox>();
        public SqlGenerator()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            this.columnNameTextBoxList.Add(txtColumnName1);
            this.columnAliasNameTextBoxList.Add(txtAliasName);
            this.columnDatatypeComboBox.Add(cmbDataType1);

        }

        void SqlGenerator_Load(object sender, EventArgs e)
        {

            m_sSqlConnectionString = Convert.ToString(ConfigurationManager.AppSettings["SqlConnectionString"]);
            InitializeDatabaseControls();
        }

        private void btnShowQueryResults_Click(object sender, System.EventArgs e)
        {
            LoadQueryRecords("111");
            //LoadQueryRecords(txtSelectSQL.Text.Trim());
        }

        private void btnGenerate_Click(object sender, System.EventArgs e)
        {
            GenerateSqlStatements();

            // if user cancelled or problem occured, just exit
            if (m_sSqlStatementText.Trim() == string.Empty)
                return;

            string sFilePath = FileUtilities.GetUniqueTempFileName(".txt");
            FileUtilities.WriteFileContents(sFilePath, m_sSqlStatementText, true);
            Process.Start(sFilePath);
        }

        private void cmbDatabases_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void cmbTables_SelectedIndexChanged(object sender, EventArgs e)
        {

            // automatically setup a query based upon the table selected
            string sTableName = string.Empty;
            txtSelectSQL.Text = "SELECT * FROM " + sTableName;
            txtTargetTable.Text = sTableName;

            UpdateControls();
        }

        private void cmbSqlType_SelectedIndexChanged(object sender, EventArgs e)
        {

            // user changed the type of statement to generate, 
            //  so flip the checkboxes for user convenience
            for (int i = 0; i < chklstIncludeFields.Items.Count; i++)
            {
                //if(cmbSqlType.SelectedIndex == (int)STATEMENT_TYPES.DELETE)
                //{
                //    // assume that the primary key is in column 0, 
                //    //  and that deletes trigger off that
                //    chklstIncludeFields.SetItemChecked(i, (i == 0));
                //}
                //else
                //{
                //    // assume that the primary key is in column 0, 
                //    //  and that it should NOT be included in inserts and updates
                //    chklstIncludeFields.SetItemChecked(i, (i != 0));
                //}                
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Initializes the database combobox control
        /// </summary>
        private void InitializeDatabaseControls()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                //cmbDatabases.DataSource = null;
                //cmbDatabases.Items.Clear();
                //cmbTables.DataSource = null;
                //cmbTables.Items.Clear();     // clear the tables also since they'll be invalid       

                //cmbDatabases.DisplayMember = "DATABASE_NAME";
                //cmbDatabases.ValueMember = "DATABASE_NAME";
                //DataTable dt = DatabaseUtilities.GetDatabases(m_sSqlConnectionString);
                //DataTable dt = new DataTable();
                //dt.Columns.Add("c1", typeof(string));
                //dt.Columns.Add("c2", typeof(string));
                //dt.Columns.Add("c3", typeof(string));
                //cmbDatabases.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading database-tables into list: " + ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                UpdateControls();
            }
        }

        /// <summary>
        /// Updates the enable states of various form controls
        /// </summary>
        private void UpdateControls()
        {
            //btnShowQueryResults.Enabled = (cmbDatabases.SelectedIndex >= 0);
        }

        /// <summary>
        /// Loads the grid and column list controls according to the specified query
        /// </summary>
        /// <param name="sSQL"></param>
        private void LoadQueryRecords(string sSQL)
        {
            if (sSQL.Trim() == string.Empty)
            {
                MessageBox.Show("The SQL query was empty!  Please enter a valid SQL query!");
                return;
            }

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                //m_TableInfo = DatabaseUtilities.LoadDataTable(m_sSqlConnectionString, cmbDatabases.Text, sSQL);
                DataTable m_TableInfo = new DataTable();
                m_TableInfo.Columns.Add("c1", typeof(string));
                m_TableInfo.Columns.Add("c2", typeof(string));
                m_TableInfo.Columns.Add("c3", typeof(string));

                // load the list of columns to include in the generator
                chklstIncludeFields.Items.Clear();
                foreach (DataColumn col in m_TableInfo.Columns)
                {
                    // exclude the primary/auto-increment key by default, but select/check all the others
                    chklstIncludeFields.Items.Add(col.ColumnName, (chklstIncludeFields.Items.Count > 0));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("LoadQueryRecords error: " + ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                UpdateControls();
            }
        }

        /// <summary>
        /// Generates sql statements based upon the generator type selected and the tableinfo provided
        /// </summary>
        private void GenerateSqlStatements()
        {
            string tableName = this.txtTargetTable.Text;
            //create table
            string sqlCreate = "create table " + tableName + "(";
            for (int i = 0; i < this.columnNameTextBoxList.Count; i++)
            {
                if (i == this.columnNameTextBoxList.Count - 1)
                {
                    sqlCreate += this.columnNameTextBoxList[i].Text + " " + this.columnDatatypeComboBox[i].Text;
                }
                else
                {
                    sqlCreate += this.columnNameTextBoxList[i].Text + " " + this.columnDatatypeComboBox[i].Text + ",";

                }
            }
            sqlCreate += ");\r\n";

            // select Table
            string sqlSelectWithAlias = "select ";
            for (int i = 0; i < this.columnNameTextBoxList.Count; i++)
            {
                if (i == this.columnNameTextBoxList.Count - 1)
                {
                    sqlSelectWithAlias += this.columnNameTextBoxList[i].Text + " as " + this.columnAliasNameTextBoxList[i].Text;
                }
                else
                {
                    sqlSelectWithAlias += this.columnNameTextBoxList[i].Text + " as " + this.columnAliasNameTextBoxList[i].Text + ",";
                }
            }
            sqlSelectWithAlias += " from " + tableName + "\r\n";
            string sqlSelectWithoutAlias = "select ";
            for (int i = 0; i < this.columnNameTextBoxList.Count; i++)
            {
                if (i == this.columnNameTextBoxList.Count - 1)
                {
                    sqlSelectWithoutAlias += this.columnNameTextBoxList[i].Text;
                }
                else
                {
                    sqlSelectWithoutAlias += this.columnNameTextBoxList[i].Text + ",";
                }
            }
            sqlSelectWithoutAlias += " from " + tableName + "\r\n";

            string sqlInsert = "insert into " + tableName + "(";
            for (int i = 0; i < this.columnNameTextBoxList.Count; i++)
            {
                if (i == this.columnNameTextBoxList.Count - 1)
                {
                    sqlInsert += this.columnNameTextBoxList[i].Text;
                }
                else
                {
                    sqlInsert += this.columnNameTextBoxList[i].Text + ",";
                }
            }
            sqlInsert += ") values();\r\n";

            string sqlDelete = "delete from " + tableName + "\r\n";
            string sqlUpdate = "update " + tableName + " set ";
            for (int i = 0; i < this.columnNameTextBoxList.Count; i++)
            {
                if (i == this.columnNameTextBoxList.Count - 1)
                {
                    sqlUpdate += this.columnNameTextBoxList[i].Text + " = ";
                }
                else
                {
                    sqlUpdate += this.columnNameTextBoxList[i].Text + " = " + ",";
                }
            }
            sqlUpdate += "\r\n";

            string strTotal = sqlCreate + sqlSelectWithAlias + sqlSelectWithoutAlias + sqlInsert + sqlDelete + sqlUpdate;
            this.txtSelectSQL.Text = strTotal;
            // clear the string member
            //m_sSqlStatementText = string.Empty;

            //// create an array of all the columns that are to be included
            //ArrayList aryColumns = new ArrayList();
            //for (int i = 0; i < chklstIncludeFields.CheckedItems.Count; i++)
            //{
            //    aryColumns.Add(chklstIncludeFields.CheckedItems[i].ToString());
            //}

            //// if no columns included, return with a msg
            //if (aryColumns.Count <= 0)
            //{
            //    MessageBox.Show("No columns selected!  Please check/select some columns to include!");
            //    return;
            //}

            //string sTargetTableName = txtTargetTable.Text.Trim();
            //if (sTargetTableName == string.Empty)
            //{
            //    MessageBox.Show("No valid target table name!  Please enter a table name to be used in the SQL statements!");
            //    return;
            //}

            // generate the sql by type
            //if (cmbSqlType.SelectedIndex == (int)STATEMENT_TYPES.INSERT)
            //{
            //    m_sSqlStatementText = SqlScriptGenerator.GenerateSqlInserts(aryColumns, m_TableInfo, sTargetTableName);
            //}
            //else if (cmbSqlType.SelectedIndex == (int)STATEMENT_TYPES.UPDATE)
            //{
            //    // get an array of all the active table columns         
            //    ArrayList aryWhereColumns = new ArrayList();
            //    for (int i = 0; i < chklstIncludeFields.Items.Count; i++)
            //    {
            //        aryWhereColumns.Add(chklstIncludeFields.GetItemText(chklstIncludeFields.Items[i]));
            //    }

            //    // create dlg and pass in array of columns
            //    SelectMultipleItems dlg = new SelectMultipleItems();
            //    dlg.Text = "Select WHERE Columns";
            //    dlg.Description = "Select WHERE-Clause Columns for UPDATE SQLs:";
            //    dlg.Initialize(aryWhereColumns, string.Empty, false);

            //    // user cancelled, so exit
            //    if (dlg.ShowDialog() != DialogResult.OK)
            //    {
            //        return;
            //    }

            //    aryWhereColumns.Clear();
            //    aryWhereColumns = dlg.UserSelectedItems;

            //    m_sSqlStatementText = SqlScriptGenerator.GenerateSqlUpdates(aryColumns, aryWhereColumns, m_TableInfo, sTargetTableName);
            //}
            //else if (cmbSqlType.SelectedIndex == (int)STATEMENT_TYPES.DELETE)
            //{
            //    m_sSqlStatementText = SqlScriptGenerator.GenerateSqlDeletes(aryColumns, m_TableInfo, sTargetTableName);
            //}            
        }

        private void txtSelectSQL_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTargetTable_TextChanged(object sender, EventArgs e)
        {

        }
        int txtColumnNameIndex = 2;
        int txtColumnNameTop = 90 + 45;
        //增加新列，增加控件
        private void button1_Click(object sender, EventArgs e)
        {
            TextBox tb = new TextBox();
            tb.Location = new System.Drawing.Point(13, this.txtColumnNameTop);
            tb.Name = "txtColumnName" + this.txtColumnNameIndex.ToString();
            tb.Size = new System.Drawing.Size(148, 21);
            this.groupBox1.Controls.Add(tb);
            this.columnNameTextBoxList.Add(tb);


            TextBox tbAlias = new TextBox();

            tbAlias.Location = new System.Drawing.Point(177, this.txtColumnNameTop);
            tbAlias.Name = "txtAliasName" + this.txtColumnNameIndex.ToString();
            tbAlias.Size = new System.Drawing.Size(124, 21);
            this.groupBox1.Controls.Add(tbAlias);
            this.columnAliasNameTextBoxList.Add(tbAlias);

            ComboBox cmb = new ComboBox();
            //cmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmb.FormattingEnabled = true;
            cmb.Location = new System.Drawing.Point(319, this.txtColumnNameTop);
            cmb.Name = "cmbDataType" + this.txtColumnNameIndex.ToString();
            cmb.Size = new System.Drawing.Size(112, 20);
            cmb.Items.Add("varchar(32)");
            cmb.Items.Add("int");
            this.groupBox1.Controls.Add(cmb);
            this.columnDatatypeComboBox.Add(cmb);

            this.txtColumnNameIndex++;
            this.txtColumnNameTop += 45;
        }

    }
}