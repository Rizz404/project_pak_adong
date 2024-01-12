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
    public partial class InvoiceDetails : System.Web.UI.Page
    {
        private SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                PopulateTableInvoiceH();
            }
        }

        private void PopulateTableInvoiceH()
        {
            string sql = "SELECT * FROM TblInvoiceH";
            SqlCommand commandSelect = new SqlCommand(sql, conn);

            conn.Open();

            SqlDataAdapter dataAdapter = new SqlDataAdapter(commandSelect);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            GrvTblInvoiceH.DataSource = dataTable;
            GrvTblInvoiceH.DataBind();

            // Tutup koneksi
            conn.Close();
        }

        protected void GrvTblInvoiceH_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "ShowDetail")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string InvNo = GrvTblInvoiceH.DataKeys[index].Value.ToString();

                PopulateTableInvoiceD(InvNo);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
        }

        private void PopulateTableInvoiceD(string InvNo)
        {
            string sql = "SELECT * FROM TblInvoiceD WHERE InvNo = @InvNo";
            SqlCommand commandSelect = new SqlCommand(sql, conn);

            commandSelect.Parameters.AddWithValue("@InvNo", InvNo);

            conn.Open();

            SqlDataAdapter dataAdapter = new SqlDataAdapter(commandSelect);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            GrvTblInvoiceD.DataSource = dataTable;
            GrvTblInvoiceD.DataBind();

            // Tutup koneksi
            conn.Close();
        }
    }
}