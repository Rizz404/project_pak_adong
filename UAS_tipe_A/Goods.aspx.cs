using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UAS_tipe_A
{
    public partial class Goods : Page
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
            TxtGoodsName.Text = "";
            TxtUnit.Text = "";
            TxtStock.Text = "";
            TxtPrice.Text = "";

            HiddenGoodsID.Value = "";
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

        private void PopulateTable()
        {
            // Buka koneksi
            conn.Open();

            // Buat query SQL untuk memilih data
            SqlCommand commandSelect = new SqlCommand("SELECT * FROM TblItemGoods", conn);

            // Jalankan query dan ambil hasilnya
            SqlDataAdapter dataAdapter = new SqlDataAdapter(commandSelect);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            ViewState["TblItemGoods"] = dataTable;

            // Ikat hasil query ke GridView
            GrvTblItemGoods.DataSource = dataTable;
            GrvTblItemGoods.DataBind();

            // Tutup koneksi
            conn.Close();
        }


        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "sp_goods";

                SqlCommand command = new SqlCommand(sql, conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                //command.Parameters.AddWithValue("@GoodsID", TxtGoodsId.Text);
                command.Parameters.AddWithValue("@GoodsName", TxtGoodsName.Text);
                command.Parameters.AddWithValue("@Unit", TxtUnit.Text);
                command.Parameters.AddWithValue("@Price", TxtPrice.Text);
                command.Parameters.AddWithValue("@Stock", TxtStock.Text);

                if(string.IsNullOrEmpty(HiddenGoodsID.Value))
                {
                    command.Parameters.AddWithValue("@param", "SAVE");
                    ShowAlertAndRedirect("Create goods success", "Goods.aspx");  
                }
                else
                {
                    command.Parameters.AddWithValue("@GoodsID", HiddenGoodsID.Value);
                    command.Parameters.AddWithValue("@param", "UPDATE");
                    ShowAlertAndRedirect("Update goods success", "Goods.aspx"); 
                }

                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();

                PopulateTable();
                HiddenGoodsID.Value = "";
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
            HiddenGoodsID.Value = GrvTblItemGoods.DataKeys[index].Value.ToString();

            // Ambil data dari ViewState
            DataTable dataTable = ViewState["TblItemGoods"] as DataTable;

            // Ambil baris data dari DataTable
            DataRow dataRow = dataTable.Rows[index];

            // Ambil data dari DataRow
            TxtGoodsName.Text = dataRow["GoodsName"].ToString();
            TxtUnit.Text = dataRow["Unit"].ToString();
            TxtStock.Text = dataRow["Stock"].ToString();
            TxtPrice.Text = dataRow["Price"].ToString();

            OpenModal("openGoodsModal");
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int index = row.RowIndex;
            HiddenGoodsID.Value = GrvTblItemGoods.DataKeys[index].Value.ToString();

            OpenModal("openDeleteModal");
        }

        protected void BtnConfirmDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Implementasi untuk menghapus user berdasarkan userId
                string sql = "sp_goods";
                SqlCommand commandDelete = new SqlCommand(sql, conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                commandDelete.Parameters.AddWithValue("@GoodsID", HiddenGoodsID.Value);
                commandDelete.Parameters.AddWithValue("@param", "DELETE");

                conn.Open();
                commandDelete.ExecuteNonQuery();
                conn.Close();
                HiddenGoodsID.Value = "";
                PopulateTable();
                ShowAlertAndRedirect("Delete user success", "Goods.aspx");   
            }
            catch(Exception ex)
            {
                ErrorMessage.Text = "Error: " + ex.Message;
            }
        }

        protected void OpenModalGoods_Click(object sender, EventArgs e)
        {
            InitialState();
            OpenModal("openGoodsModal");
        }
    }
}