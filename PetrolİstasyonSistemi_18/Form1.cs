using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PetrolİstasyonSistemi_18
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection connect = new SqlConnection(@"Data Source=LAPTOP-ONATSOFT\ONATSOFT;Initial Catalog=PetrolTestDB;Integrated Security=True");

        private void fiyatListesi()
        {
            connect.Open();
            SqlCommand cmd = new SqlCommand("select * from TBL_AKARYAKIT where petrolturu='UltraForce95'", connect);
            SqlDataReader DR = cmd.ExecuteReader();
            while (DR.Read())
            {
                lbl_kursunsuzBenzin.Text = DR[3].ToString();
                progress_Arac1.Value = int.Parse(DR[4].ToString());
                label14.Text = progress_Arac1.Value + " L";
            }
            connect.Close();

            connect.Open();
            SqlCommand cmd2 = new SqlCommand("select * from TBL_AKARYAKIT where petrolturu='UltraForce'", connect);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                lbl_ultraforceMotorin.Text = dr2[3].ToString();
                progress_Arac2.Value = int.Parse(dr2[4].ToString());
                label15.Text = progress_Arac2.Value + " L";
            }
            connect.Close();

            connect.Open();
            SqlCommand cmd3 = new SqlCommand("select * from TBL_AKARYAKIT where petrolturu='EcoForce'", connect);
            SqlDataReader dr3 = cmd3.ExecuteReader();
            while (dr3.Read())
            {
                lbl_ecoforceMotorin.Text = dr3[3].ToString();
                progress_Arac3.Value = int.Parse(dr3[4].ToString());
                label16.Text = progress_Arac3.Value + " L";
            }
            connect.Close();

            connect.Open();
            SqlCommand cmd4 = new SqlCommand("select * from TBL_AKARYAKIT where petrolturu='Gaz'", connect);
            SqlDataReader dr4 = cmd4.ExecuteReader();
            while (dr4.Read())
            {
                lbl_gaz.Text = dr4[3].ToString();
                progress_Arac4.Value = int.Parse(dr4[4].ToString());
                label17.Text = progress_Arac4.Value + " L";
            }
            connect.Close();
        }

        private void listele()
        {
            connect.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_hareket", connect);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGrid1.DataSource = dt;
            connect.Close();

            connect.Open();
            SqlCommand cmd = new SqlCommand("select * from tblkasa", connect);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                lbl_Kasa.Text = dr[0].ToString();
            }
            connect.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            fiyatListesi();
            listele();
        }

        private void txt_Ultraforce95Litre_TextChanged(object sender, EventArgs e)
        {
            decimal kursunsuz95, litre, tutar;
            kursunsuz95 = Convert.ToDecimal(lbl_kursunsuzBenzin.Text);
            litre = Convert.ToDecimal(txt_Ultraforce95Litre.Text);
            tutar = kursunsuz95 * litre;
            txt_Ultraforce95Tutar.Text = tutar.ToString();
        }

        private void txt_MotorinLitre_TextChanged(object sender, EventArgs e)
        {
            decimal ultraforceMtr, litre, tutar;
            ultraforceMtr = Convert.ToDecimal(lbl_ultraforceMotorin.Text);
            litre = Convert.ToDecimal(txt_MotorinLitre.Text);
            tutar = ultraforceMtr * litre;
            txt_MotorinTutar.Text = tutar.ToString();
        }

        private void txt_EcoforceLitre_TextChanged(object sender, EventArgs e)
        {
            double ecoforceMtr, litre, tutar;
            ecoforceMtr = Convert.ToDouble(lbl_ecoforceMotorin.Text);
            litre = Convert.ToDouble(txt_EcoforceLitre.Text);
            tutar = ecoforceMtr * litre;
            txt_EcoforceTutar.Text = tutar.ToString();
        }

        private void txt_GazLitre_TextChanged(object sender, EventArgs e)
        {
            double gaz, litre, tutar;
            gaz = Convert.ToDouble(lbl_gaz.Text);
            litre = Convert.ToDouble(txt_GazLitre.Text);
            tutar = gaz * litre;
            txt_GazTutar.Text = tutar.ToString();
        }

        private void btn_Ultraforce95Doldur_Click(object sender, EventArgs e)
        {
            //try
            //{
                if (txt_Ultraforce95Litre.Text != "" || txt_Ultraforce95Plaka.Text != "")
                {
                    connect.Open();
                    SqlCommand komut1 = new SqlCommand("insert into tbl_hareket (plaka,benzıntur,lıtre,fıyat) values (@PR1, @PR2, @PR3, @PR4)", connect);
                    komut1.Parameters.AddWithValue("@PR1", txt_Ultraforce95Plaka.Text);
                    komut1.Parameters.AddWithValue("@PR2", "UltraForce95");
                    komut1.Parameters.AddWithValue("@PR3", txt_Ultraforce95Litre.Text);
                    komut1.Parameters.AddWithValue("@PR4", decimal.Parse(txt_Ultraforce95Tutar.Text));
                    komut1.ExecuteNonQuery();
                    connect.Close();

                    connect.Open();
                    SqlCommand cmd = new SqlCommand("update tblkasa set mıktar=mıktar+@p1", connect);
                    cmd.Parameters.AddWithValue("@p1", decimal.Parse(txt_Ultraforce95Tutar.Text));
                    cmd.ExecuteNonQuery();
                    connect.Close();
                    MessageBox.Show("UltraForce 95 yakıtı 'Araç 1'e dolum işlemi tamamlandı ve satışı yapıldı.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    connect.Open();
                    SqlCommand komut2 = new SqlCommand("update tbl_akaryakıt set stok=stok-@p1 where petrolturu='UltraForce95'", connect);
                    komut2.Parameters.AddWithValue("@p1", txt_Ultraforce95Litre.Text);
                    komut2.ExecuteNonQuery();
                    connect.Close();
                    fiyatListesi();
                    listele();

                    txt_Ultraforce95Litre.Clear();
                    txt_Ultraforce95Plaka.Clear();
                    txt_Ultraforce95Litre.BackColor = Color.White;
                    txt_Ultraforce95Plaka.BackColor = Color.White;
                }
                else
                {
                    txt_Ultraforce95Litre.BackColor = Color.OrangeRed;
                    txt_Ultraforce95Plaka.BackColor = Color.OrangeRed;
                    MessageBox.Show("Yakıt dolumu için litre ve plaka bilgisi zorunludur!\nLütfen kırmızı renkli alanları doldurunuz.", "UYARI");
                }
                
            //}
            //catch (Exception ex)
            //{
            //    //MessageBox.Show(ex.Message.ToString(), "Beklenmeyen bir sorun oluştu.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            
        }
    }
}
