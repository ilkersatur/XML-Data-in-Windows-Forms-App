using Microsoft.VisualBasic.ApplicationServices;
using System.Data;
using System.Data.SqlClient;

namespace XLM_SQL
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_PerEkle_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=SUNUM1\\MSSQLSERVER2014;Initial Catalog =Calisma;User ID = sa; Password = 1230" );
            conn.Open();
            SqlCommand cmd = new SqlCommand("gp_UrunEkle", conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@urunAdi","Telefon");
            cmd.Parameters.AddWithValue("@renk","Blue");
            cmd.Parameters.AddWithValue("@UrunKodu","123A");
            cmd.Parameters.AddWithValue("@Fiyat",1124);

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //geriye deðer döndüren saklý prosedür
            SqlConnection conn = new SqlConnection("Data Source=SUNUM1\\MSSQLSERVER2014;Initial Catalog =Calisma;User ID = sa; Password = 1230");
            conn.Open();
            SqlCommand cmd = new SqlCommand("gp_UrunEkleReturnID", conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@urunAdi", "Telefon");
            cmd.Parameters.AddWithValue("@renk", "Blue");
            cmd.Parameters.AddWithValue("@UrunKodu", "123A");
            cmd.Parameters.AddWithValue("@Fiyat", 1124);
            cmd.Parameters.Add("@ID", SqlDbType.Int);
            cmd.Parameters["@ID"].Direction=ParameterDirection.ReturnValue;
            cmd.ExecuteNonQuery();
            MessageBox.Show(cmd.Parameters["@ID"].Value.ToString());
            conn.Close();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Out parametre 
            SqlConnection conn = new SqlConnection("Data Source=SUNUM1\\MSSQLSERVER2014;Initial Catalog =Calisma;User ID = sa; Password = 1230");
            conn.Open();
            SqlCommand cmd = new SqlCommand("gp_AdvUrunBul", conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@urunID", 316);

            cmd.Parameters.Add("@urunAdi", SqlDbType.VarChar,50);
            cmd.Parameters.Add("@fiyat", SqlDbType.Money);

            cmd.Parameters["@urunAdi"].Direction = ParameterDirection.Output;
            cmd.Parameters["@fiyat"].Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();
            MessageBox.Show(cmd.Parameters["@fiyat"].Value.ToString() + " " + cmd.Parameters["@urunAdi"].Value.ToString());
            conn.Close();
            /*  saklý prosedür çalýþmasý
             *  declare @ID int = 316
                declare @Ad varchar(20)
                declare @fiyat money
                exec gp_AdvUrunBul @ID,@Ad output, @fiyat output
                select @Ad,@fiyat
             */
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //view kullanýmý
            SqlConnection conn = new SqlConnection("Data Source=SUNUM1\\MSSQLSERVER2014;Initial Catalog =Calisma;User ID = sa; Password = 1230");
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from vw_Personel", conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                listBox1.Items.Add(dr[0].ToString() + " " + dr[1].ToString());
            }
            conn.Close();
        }
    }
}