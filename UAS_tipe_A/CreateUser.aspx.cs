using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UAS_tipe_A
{
    public partial class CreateUser : System.Web.UI.Page
    {
        private SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                PopulateTable();
            }
        }

        private void InitialState()
        {
            TxtUsername.Text = "";
            TxtEmail.Text = "";
            TxtPassword.Text = "";
            TxtPhone.Text = "";
            
            TxtPassword.TextMode = TextBoxMode.Password;
            HiddenUserID.Value = "";
        }

        private void PopulateTable()
        {
            try
            {
                string sql = "SELECT * FROM TblUserLogin";
                SqlCommand commandSelect = new SqlCommand(sql, conn);

                conn.Open();

                // Jalankan query dan ambil hasilnya
                SqlDataAdapter dataAdapter = new SqlDataAdapter(commandSelect);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                // Simpan DataTable ke ViewState
                ViewState["TblUserLogin"] = dataTable;

                // Ikat hasil query ke GridView
                GrvTblUserLogin.DataSource = dataTable;
                GrvTblUserLogin.DataBind();

                // Tutup koneksi
                conn.Close();
            }
            catch(Exception ex)
            {
                ErrorMessage.Text = "Error: " + ex.Message;
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "sp_user";

                SqlCommand command = new SqlCommand(sql, conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Username", TxtUsername.Text);
                command.Parameters.AddWithValue("@Email", TxtEmail.Text);
                command.Parameters.AddWithValue("@Password", TxtPassword.Text);
                command.Parameters.AddWithValue("@Phone", TxtPhone.Text);

                if(string.IsNullOrEmpty(HiddenUserID.Value))
                {
                    command.Parameters.AddWithValue("@param", "SAVE");
                    ShowAlertAndRedirect("Add user success", "CreateUser.aspx");
                }
                else
                {
                    command.Parameters.AddWithValue("@UserId", HiddenUserID.Value);
                    command.Parameters.AddWithValue("@param", "UPDATE");
                    ShowAlertAndRedirect("Update user success", "CreateUser.aspx");
                }

                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();

                HiddenUserID.Value = "";
                PopulateTable(); // Refresh grid after save/update
            }
            catch(SqlException sqlEx)
            {
                // Handle SqlException
                ErrorMessage.Text = "SQL Error: " + sqlEx.Message.ToString();
            }
            catch(Exception ex)
            {
                // Handle other types of exceptions
                ErrorMessage.Text = "General Error: " + ex.Message.ToString();
            }
        }

        private void OpenModal(string modal)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Pop", $"{modal}();", true);
        }

        private void ShowAlertAndRedirect(string message, string url)
        {
            // Intinya biar javascript tidak error
            message = message.Replace("'", "\\'");
            url = url.Replace("'", "\\'");

            // messagenya
            string script = $"alert('{message}'); window.location.href = '{url}';";

            ScriptManager.RegisterStartupScript(this, GetType(), "AlertAndRedirect", script, true);
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int index = row.RowIndex;
            HiddenUserID.Value = GrvTblUserLogin.DataKeys[index].Value.ToString();

            TxtPassword.TextMode = TextBoxMode.SingleLine;

            // Ambil data dari ViewState
            DataTable dataTable = ViewState["TblUserLogin"] as DataTable;

            // Ambil baris data dari DataTable
            DataRow dataRow = dataTable.Rows[index];

            // Ambil data dari DataRow
            TxtUsername.Text = dataRow["Username"].ToString();
            TxtEmail.Text = dataRow["Email"].ToString();
            TxtPassword.Text = dataRow["Password"].ToString();
            TxtPhone.Text = dataRow["Phone"].ToString();

            OpenModal("openUserModal");
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int index = row.RowIndex;
            HiddenUserID.Value = GrvTblUserLogin.DataKeys[index].Value.ToString();

            OpenModal("openDeleteModal");
        }

        protected void BtnConfirmDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Implementasi untuk menghapus user berdasarkan userId
                string sql = "sp_user";
                SqlCommand commandDelete = new SqlCommand(sql, conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                commandDelete.Parameters.AddWithValue("@UserID", HiddenUserID.Value);
                commandDelete.Parameters.AddWithValue("@param", "DELETE");

                conn.Open();
                commandDelete.ExecuteNonQuery();
                conn.Close();       
                HiddenUserID.Value = "";
                PopulateTable();
                ShowAlertAndRedirect("Delete user success", "CreateUser.aspx");
            }
            catch(Exception ex)
            {
                ErrorMessage.Text = "Error: " + ex.Message;
            }
        }

        protected void OpenModalUser_Click(object sender, EventArgs e)
        {
            InitialState();
            OpenModal("openUserModal");
        }
    }
}