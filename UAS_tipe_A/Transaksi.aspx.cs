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
    public partial class Transaksi : Page
    {
        private SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString);
        private double totalPrice = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["UserID"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                if(!IsPostBack)
                {
                    DataTable dataTable = new DataTable();

                    TxtInvDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    GetCustomer();

                    dataTable.Columns.AddRange(new DataColumn[6] { new DataColumn("GoodsID"), new DataColumn("GoodsName"), new DataColumn("Unit"), new DataColumn("Price"), new DataColumn("Qty"), new DataColumn("TotalPrice") });
                    ViewState["Goods"] = dataTable;
                    BindGrid();
                }
            }
        }

        private void Alert(string message)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "alert", $"window.alert('{message}');", true);
        }

        protected void GetCustomer()
        {
            string sql = "SELECT CustID, CustName, CustID + ' ' + '|' + ' '+ CustName AS Comdesc " +
                "FROM TblCustomer with(nolock) ";
            conn.Open();

            SqlCommand commandSelect = new SqlCommand(sql, conn);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(commandSelect);
            DataTable dataTable = new DataTable();

            dataAdapter.Fill(dataTable);
            conn.Close();

            if(dataTable.Rows.Count > 0)
            {
                DDLCustID.DataSource = dataTable;
                DDLCustID.DataTextField = "Comdesc";
                DDLCustID.DataValueField = "CustID";
                DDLCustID.DataBind();
            }
        }

        protected void BindGrid()
        {
            GrvDetail.DataSource = (DataTable)ViewState["Goods"];
            GrvDetail.DataBind();
        }

        protected void GetGoods()
        {
            string sql;

            sql = "SELECT GoodsName, CONVERT(varchar(36), GoodsID) AS GoodsID, CONVERT(varchar (36), GoodsID) +' ' +'|'+ ' '+ GoodsName as Comdesc " +
                  "From TblItemGoods with(nolock)";
            conn.Open();

            SqlCommand commandSelect = new SqlCommand(sql, conn);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(commandSelect);
            DataTable dataTable = new DataTable();

            dataAdapter.Fill(dataTable);
            conn.Close();

            if(dataTable.Rows.Count > 0)
            {
                // Hapus semua item yang ada
                DDLGoods.Items.Clear();

                // Tambahkan item "Select Goods" kembali dan buat menjadi non-pilih
                ListItem selectGoodsItem = new ListItem("Select Goods", "0")
                {
                    Enabled = false
                };
                DDLGoods.Items.Add(selectGoodsItem);

                // Mengikat data ke DropDownList
                DDLGoods.DataSource = dataTable;
                DDLGoods.DataTextField = "Comdesc";
                DDLGoods.DataValueField = "GoodsID";
                DDLGoods.DataBind();
            }
        }

        protected void BtnAddDetail_Click(object sender, EventArgs e)
        {
            GetGoods();
            ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "openModal();", true);
            ClearInput();
        }

        protected void DDLGoods_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string sql;

                sql = "SELECT * FROM TblItemGoods with(nolock) " +
                        "WHERE GoodsID = @GoodsID";

                conn.Open();

                SqlCommand commandSelect = new SqlCommand(sql, conn);
                Response.Write("Nilai GoodsID: " + TxtGoodsID.Text); // Contoh pesan debug
                commandSelect.Parameters.AddWithValue("@GoodsID", DDLGoods.SelectedValue);

                SqlDataAdapter dataAdapter = new SqlDataAdapter(commandSelect);
                DataTable dataTable = new DataTable();

                dataAdapter.Fill(dataTable);
                conn.Close();

                if(dataTable.Rows.Count > 0)
                {
                    TxtGoodsID.Text = dataTable.Rows[0]["GoodsID"].ToString();
                    TxtGoodsName.Text = dataTable.Rows[0]["GoodsName"].ToString();
                    TxtStock.Text = dataTable.Rows[0]["Stock"].ToString();
                    TxtUnit.Text = dataTable.Rows[0]["Unit"].ToString();
                    TxtPrice.Text = dataTable.Rows[0]["Price"].ToString();
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "openModal();", true);
            }
            catch(Exception ex)
            {
                ErrorMessage.Text = ex.Message.ToString();
                Response.Write(ex.Message.ToString());
            }
        }

        protected void TxtQty_TextChanged(object sender, EventArgs e)
        {
            double qty = Convert.ToDouble(TxtQty.Text);
            double price = Convert.ToDouble(TxtPrice.Text);
            double PPN = 0.11;
            double subtotal = qty * price;
            double calculatedPPN = subtotal * PPN;
            double grandTotal = subtotal + calculatedPPN;

            TxtPPN.Text = calculatedPPN.ToString();
            TxtSubTotal.Text = subtotal.ToString();
            TxtTotalPrice.Text = grandTotal.ToString();

            ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "openModal();", true);
        }

        protected void BtnSaveDetail_Click(object sender, EventArgs e)
        {
            if(TxtPrice.Text == "0" || TxtPrice.Text == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "openModal();", true);
            }
            else
            {
                DataTable dataTable = (DataTable)ViewState["Goods"];

                dataTable.Rows.Add(TxtGoodsID.Text, TxtGoodsName.Text, TxtUnit.Text, TxtPrice.Text, TxtQty.Text, TxtTotalPrice.Text);
                ViewState["Goods"] = dataTable;
                BindGrid();
                ClearInput();
            }
        }

        protected void ClearInput()
        {
            // Mereset DropDownList
            DDLGoods.SelectedIndex = 0;
            // Mereset TextBox
            TxtGoodsID.Text = "";
            TxtGoodsName.Text = "";
            TxtStock.Text = "";
            TxtUnit.Text = "";
            TxtPrice.Text = "";
            TxtQty.Text = "";
            TxtTotalPrice.Text = "";
        }

        private string GetInvoiceNumber()
        {
            const string sql = "sp_InvNo";
            string invNo = null;

            conn.Open();

            SqlCommand commandGet = new SqlCommand(sql, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            commandGet.Parameters.AddWithValue("@CustID", DDLCustID.SelectedValue);

            SqlDataAdapter dataAdapter = new SqlDataAdapter(commandGet);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            conn.Close();

            if(dataTable.Rows.Count > 0)
            {
                invNo = dataTable.Rows[0]["InvNo"].ToString();
            }

            return invNo;
        }

        private void SaveInvoiceHeader(string invNo)
        {
            const string sql = "sp_invoiceH";
            SqlCommand commandSave = new SqlCommand(sql, conn)
            {
                CommandType = CommandType.StoredProcedure
            };


            commandSave.Parameters.AddWithValue("@InvNo", invNo);
            //commandSave.Parameters.AddWithValue("@InvDate", TxtInvDate.Text);
            commandSave.Parameters.AddWithValue("@CustID", DDLCustID.SelectedValue);
            commandSave.Parameters.AddWithValue("@SubTotal", TxtSubTotal.Text);
            commandSave.Parameters.AddWithValue("@PPN", TxtPPN.Text);
            commandSave.Parameters.AddWithValue("@GrandTotal", TxtGrandTotal.Text);

            commandSave.Parameters.AddWithValue("@param", "SAVE");

            conn.Open();
            commandSave.ExecuteNonQuery();
            conn.Close();
        }

        private void SaveInvoiceDetails(string invNo)
        {
            const string sql = "sp_invoiceD";

            foreach(GridViewRow row in GrvDetail.Rows)
            {
                SqlCommand commandSave = new SqlCommand(sql, conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                commandSave.Parameters.AddWithValue("@InvNo", invNo);

                string goodsID = row.Cells[0].Text;
                commandSave.Parameters.AddWithValue("@GoodsID", string.IsNullOrEmpty(goodsID) ? DBNull.Value : (object)goodsID);

                string unit = row.Cells[2].Text;
                commandSave.Parameters.AddWithValue("@Unit", string.IsNullOrEmpty(unit) ? DBNull.Value : (object)unit);

                double price = double.TryParse(row.Cells[3].Text, out price) ? price : 0;
                commandSave.Parameters.AddWithValue("@Price", price == 0 ? DBNull.Value : (object)price);

                int qty = int.TryParse(row.Cells[4].Text, out qty) ? qty : 0;
                commandSave.Parameters.AddWithValue("@Qty", qty == 0 ? DBNull.Value : (object)qty);

                double totalPrice = double.TryParse(row.Cells[5].Text, out totalPrice) ? totalPrice : 0;
                commandSave.Parameters.AddWithValue("@TotalPrice", totalPrice == 0 ? DBNull.Value : (object)totalPrice);

                commandSave.Parameters.AddWithValue("@param", "SAVE");

                conn.Open();
                commandSave.ExecuteNonQuery();
                conn.Close();
            }

        }

        protected void BtnSaveInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                string invNo = GetInvoiceNumber();

                if(string.IsNullOrEmpty(invNo))
                {
                    Alert("Invoice Number tidak ditemukan");
                    return;
                }

                if(GrvDetail.Rows.Count <= 0)
                {
                    Alert("Data tidak boleh kosong");
                    return;
                }

                SaveInvoiceHeader(invNo);
                SaveInvoiceDetails(invNo);

                Page.ClientScript.RegisterStartupScript(GetType(), "alert", "window.alert('" + "Berhasil simpan invoice" + "');window.location.href='InvoiceDetails.aspx';", true);
            }
            catch(Exception ex)
            {
                ErrorMessage.Text = ex.ToString();
                throw;
            }
        }

        protected void GrvDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.RowIndex);
                DataTable dataTable = ViewState["Goods"] as DataTable;

                dataTable.Rows[index].Delete();
                ViewState["Goods"] = dataTable;
                BindGrid();
            }
            catch(Exception ex)
            {
                Response.Write(ex.Message.ToString());
                conn.Close();
            }
        }

        protected void GrvDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.DataRow)
            {
                string item = e.Row.Cells[0].Text;
                foreach(Button button in e.Row.Cells[2].Controls.OfType<Button>())
                {
                    if(button.CommandName == "Delete")
                    {
                        button.Attributes["onclick"] = "if(!confirm('Do you want to delete " + item + "?')){ return false; };";
                    }
                }

                totalPrice = totalPrice + Convert.ToInt32(e.Row.Cells[5].Text);
                TxtGrandTotal.Text = totalPrice.ToString();
            }
        }


    }
}