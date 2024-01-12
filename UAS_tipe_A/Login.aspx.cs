using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace UAS_tipe_A
{
    public partial class Login : System.Web.UI.Page
    {
        private SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            string sql = "sp_login";

            SqlCommand loginCommand = new SqlCommand(sql, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            loginCommand.Parameters.AddWithValue("@Username", TxtUsername.Text);
            loginCommand.Parameters.AddWithValue("@Password", TxtPassword.Text);
            loginCommand.Parameters.AddWithValue("@param", "LOGIN");

            conn.Open();

            SqlDataAdapter dataAdapter = new SqlDataAdapter(loginCommand);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            conn.Close();

            if (dataTable.Rows.Count == 1)
            {
                Session["UserId"] = dataTable.Rows[0]["UserId"];
                Session["UserName"] = dataTable.Rows[0]["UserName"];
                Response.Redirect("~/Home.aspx");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "alert", "window.alert('" + "Username atau Password anda salah " + "');window.location.href='Login.aspx';", true);
            }

        }
    }
}
