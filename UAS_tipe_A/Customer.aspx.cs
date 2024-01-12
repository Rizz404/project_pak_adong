using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UAS_tipe_A
{
    public partial class Customer : System.Web.UI.Page
    {
        private SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                GetUser();
                PopulateTable();
            }
        }

        private void InitialState()
        {
            TxtCustName.Text = "";
            TxtEmail.Text = "";
            TxtAddress.Text = "";
            TxtNpwp.Text = "";
            TxtPhone.Text = "";
            TxtFax.Text = "";

            HiddenCustID.Value = "";
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

        protected void GetUser()
        {
            string sql = "SELECT CAST(ROW_NUMBER() OVER (ORDER BY Username) AS varchar) + '. ' + " +
                "Username AS NoAndUsername, UserID " +
                "FROM TblUserLogin";

            conn.Open();

            SqlCommand commandSelect = new SqlCommand(sql, conn);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(commandSelect);
            DataTable dataTable = new DataTable();

            dataAdapter.Fill(dataTable);
            conn.Close();

            if(dataTable.Rows.Count > 0)
            {
                DDLUserID.DataSource = dataTable;
                DDLUserID.DataTextField = "NoAndUsername";
                DDLUserID.DataValueField = "UserID";
                DDLUserID.DataBind();
            }
        }

        private void PopulateTable()
        {
            // Buat query SQL untuk memilih data
            string sql = "SELECT tc.*, tu.Username FROM TblCustomer tc " +
                "INNER JOIN TblUserLogin tu ON tc.UserID = tu.UserID";
            SqlCommand commandSelect = new SqlCommand(sql, conn);

            conn.Open();

            // Jalankan query dan ambil hasilnya
            SqlDataAdapter dataAdapter = new SqlDataAdapter(commandSelect);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            ViewState["TblCustomer"] = dataTable;

            // Ikat hasil query ke GridView
            GrvTblCustomer.DataSource = dataTable;
            GrvTblCustomer.DataBind();

            // Tutup koneksi
            conn.Close();
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "sp_customer";

                SqlCommand command = new SqlCommand(sql, conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@CustName", TxtCustName.Text);
                command.Parameters.AddWithValue("@Address", TxtAddress.Text);
                command.Parameters.AddWithValue("@Phone", TxtPhone.Text);
                command.Parameters.AddWithValue("@Fax", TxtFax.Text);
                command.Parameters.AddWithValue("@Email", TxtEmail.Text);
                command.Parameters.AddWithValue("@NPWP", TxtNpwp.Text);
                command.Parameters.AddWithValue("@UserID", DDLUserID.SelectedValue);

                if(string.IsNullOrEmpty(HiddenCustID.Value))
                {
                    command.Parameters.AddWithValue("@param", "SAVE");
                    ShowAlertAndRedirect("Add customer success", "Customer.aspx");
                }
                else
                {
                    command.Parameters.AddWithValue("@CustID", HiddenCustID.Value);
                    command.Parameters.AddWithValue("@param", "UPDATE");
                    ShowAlertAndRedirect("Update customer success", "Customer.aspx");
                }

                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();

                PopulateTable();
                HiddenCustID.Value = "";
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

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int index = row.RowIndex;

            // Ambil id dari tabelnya
            HiddenCustID.Value = GrvTblCustomer.DataKeys[index].Value.ToString();

            // Ambil data dari ViewState
            DataTable dataTable = ViewState["TblCustomer"] as DataTable;

            // Ambil baris data dari DataTable
            DataRow dataRow = dataTable.Rows[index];

            // Ambil data dari DataRow
            TxtCustName.Text = dataRow["CustName"].ToString();
            TxtAddress.Text = dataRow["Address"].ToString();
            TxtPhone.Text = dataRow["Phone"].ToString();
            TxtEmail.Text = dataRow["Email"].ToString();
            TxtFax.Text = dataRow["Fax"].ToString();
            TxtNpwp.Text = dataRow["Npwp"].ToString();

            OpenModal("openCustomerModal");
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int index = row.RowIndex;
            HiddenCustID.Value = GrvTblCustomer.DataKeys[index].Value.ToString();

            OpenModal("openDeleteModal");
        }

        protected void BtnConfirmDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Implementasi untuk menghapus customer berdasarkan customerId
                string sql = "sp_customer";
                SqlCommand commandDelete = new SqlCommand(sql, conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                commandDelete.Parameters.AddWithValue("@CustID", HiddenCustID.Value);
                commandDelete.Parameters.AddWithValue("@param", "DELETE");

                conn.Open();
                commandDelete.ExecuteNonQuery();
                conn.Close();
                HiddenCustID.Value = "";
                PopulateTable();
                ShowAlertAndRedirect("Delete customer success", "Customer.aspx");
            }
            catch(Exception ex)
            {
                ErrorMessage.Text = "Error: " + ex.Message;
            }
        }

        protected void OpenModalCustomer_Click(object sender, EventArgs e)
        {
            InitialState();
            OpenModal("openCustomerModal");
        }
    }
}