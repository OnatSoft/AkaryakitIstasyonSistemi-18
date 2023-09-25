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
                lbl_Kursunsuz95.Text = DR[3].ToString();
                progress_Arac1.Value = int.Parse(DR[4].ToString());
                label14.Text = progress_Arac1.Value + " L";
            }
            connect.Close();

            connect.Open();
            SqlCommand cmd2 = new SqlCommand("select * from TBL_AKARYAKIT where petrolturu='UltraForce'", connect);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                lbl_UltraforceMotorin.Text = dr2[3].ToString();
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
            SqlCommand cmd4 = new SqlCommand("select * from TBL_AKARYAKIT where petrolturu='LPG'", connect);
            SqlDataReader dr4 = cmd4.ExecuteReader();
            while (dr4.Read())
            {
                lbl_Gaz.Text = dr4[3].ToString();
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
                lbl_Kasa.Text = dr[0] + " ₺";
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
            if (txt_Ultraforce95Litre.Text == "")
            {
                txt_Ultraforce95Litre.Text = "0";
            }
            double kursunsuz95, litre, tutar;
            kursunsuz95 = Convert.ToDouble(lbl_Kursunsuz95.Text);
            litre = int.Parse(txt_Ultraforce95Litre.Text);
            tutar = kursunsuz95 * litre;
            txt_Ultraforce95Tutar.Text = tutar.ToString();
        }

        private void txt_MotorinLitre_TextChanged(object sender, EventArgs e)
        {
            if (txt_MotorinLitre.Text == "")
            {
                txt_MotorinLitre.Text = "0";
            }
            double ultraforceMtr, litre, tutar;
            ultraforceMtr = Convert.ToDouble(lbl_UltraforceMotorin.Text);
            litre = Convert.ToDouble(txt_MotorinLitre.Text);
            tutar = ultraforceMtr * litre;
            txt_MotorinTutar.Text = tutar.ToString();
        }

        private void txt_EcoforceLitre_TextChanged(object sender, EventArgs e)
        {
            if (txt_EcoforceLitre.Text == "")
            {
                txt_EcoforceLitre.Text = "0";
            }
            double ecoforceMtr, litre, tutar;
            ecoforceMtr = Convert.ToDouble(lbl_ecoforceMotorin.Text);
            litre = Convert.ToDouble(txt_EcoforceLitre.Text);
            tutar = ecoforceMtr * litre;
            txt_EcoforceTutar.Text = tutar.ToString();
        }

        private void txt_GazLitre_TextChanged(object sender, EventArgs e)
        {
            if (txt_GazLitre.Text == "")
            {
                txt_GazLitre.Text = "0";
            }
            double gaz, litre, tutar;
            gaz = Convert.ToDouble(lbl_Gaz.Text);
            litre = Convert.ToDouble(txt_GazLitre.Text);
            tutar = gaz * litre;
            txt_GazTutar.Text = tutar.ToString();
        }

        private void btn_Ultraforce95Doldur_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdbtn_Kursunsuz95.Checked == true)
                {

                    if (txt_Ultraforce95Litre.Text != String.Empty && txt_Ultraforce95Plaka.Text != String.Empty)
                    {
                        connect.Open();
                        SqlCommand cmd1 = new SqlCommand("insert into TBL_HAREKET (PLAKA,BENZINTUR,LITRE,FIYAT) values (@pr1, @pr2, @pr3, @pr4)", connect);
                        cmd1.Parameters.AddWithValue("@pr1", txt_Ultraforce95Plaka.Text);
                        cmd1.Parameters.AddWithValue("@pr2", label3.Text);
                        cmd1.Parameters.AddWithValue("@pr3", txt_Ultraforce95Litre.Text);
                        cmd1.Parameters.AddWithValue("@pr4", decimal.Parse(txt_Ultraforce95Tutar.Text));
                        cmd1.ExecuteNonQuery();
                        connect.Close();

                        connect.Open();
                        SqlCommand cmd2 = new SqlCommand("update tbl_akaryakıt set stok=stok-@p1 where petrolturu='UltraForce95'", connect);
                        cmd2.Parameters.AddWithValue("@p1", txt_Ultraforce95Litre.Text);
                        cmd2.ExecuteNonQuery();
                        connect.Close();

                        connect.Open();
                        SqlCommand cmd3 = new SqlCommand("update TBLKASA set mıktar=mıktar+@p1", connect);
                        cmd3.Parameters.AddWithValue("@p1", Convert.ToDecimal(txt_Ultraforce95Tutar.Text));
                        cmd3.ExecuteNonQuery();
                        connect.Close();
                        MessageBox.Show("UltraForce 95 yakıtı 'Araç 1'e " + txt_Ultraforce95Litre.Text + " Litre dolum işlemi tamamlandı ve satışı yapıldı.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        txt_Ultraforce95Litre.BackColor = Color.White;
                        txt_Ultraforce95Plaka.BackColor = Color.White;
                        fiyatListesi();
                        listele();
                    }
                    else
                    {
                        txt_Ultraforce95Litre.BackColor = Color.OrangeRed;
                        txt_Ultraforce95Plaka.BackColor = Color.OrangeRed;
                        MessageBox.Show("Yakıt dolumu için litre ve plaka bilgisi zorunludur!\nLütfen kırmızı renkli alanları doldurunuz.", "UYARI");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Beklenmeyen bir sorun oluştu.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btn_MotorinDoldur_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdbtn_UltraforceMotorin.Checked == true)
                {
                    if (txt_MotorinLitre.Text != "" && txt_MotorinPlaka.Text != "")
                    {
                        connect.Open();
                        SqlCommand cmd1 = new SqlCommand("insert into TBL_HAREKET (PLAKA,BENZINTUR,LITRE,FIYAT) values (@pr1, @pr2, @pr3, @pr4)", connect);
                        cmd1.Parameters.AddWithValue("@pr1", txt_MotorinPlaka.Text);
                        cmd1.Parameters.AddWithValue("@pr2", label4.Text);
                        cmd1.Parameters.AddWithValue("@pr3", txt_MotorinLitre.Text);
                        cmd1.Parameters.AddWithValue("@pr4", decimal.Parse(txt_MotorinTutar.Text));
                        cmd1.ExecuteNonQuery();
                        connect.Close();

                        connect.Open();
                        SqlCommand cmd2 = new SqlCommand("update TBL_AKARYAKIT set STOK=STOK-@s1 where PETROLTURU='UltraForce'", connect);
                        cmd2.Parameters.AddWithValue("@s1", txt_MotorinLitre.Text);
                        cmd2.ExecuteNonQuery();
                        connect.Close();

                        connect.Open();
                        SqlCommand cmd3 = new SqlCommand("update TBLKASA set MIKTAR=MIKTAR+@k1", connect);
                        cmd3.Parameters.AddWithValue("@k1", decimal.Parse(txt_MotorinTutar.Text));
                        cmd3.ExecuteNonQuery();
                        connect.Close();
                        MessageBox.Show("UltraForce Motorin yakıtı 'Araç 2'e " + txt_MotorinLitre.Text + " Litre dolum işlemi tamamlandı ve satışı yapıldı.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        txt_MotorinLitre.BackColor = Color.White;
                        txt_MotorinPlaka.BackColor = Color.White;
                        listele();
                        fiyatListesi();
                    }
                    else
                    {
                        txt_MotorinLitre.BackColor = Color.OrangeRed;
                        txt_MotorinPlaka.BackColor = Color.OrangeRed;
                        MessageBox.Show("Yakıt dolumu için litre ve plaka bilgisi zorunludur!\nLütfen kırmızı renkli alanları doldurunuz.", "UYARI");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Beklenmeyen bir sorun oluştu!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btn_EcoforceDoldur_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdbtn_EcoforceMotorin.Checked == true)
                {
                    if (txt_EcoforceLitre.Text != "" && txt_EcoforcePlaka.Text != "")
                    {
                        connect.Open();
                        SqlCommand cmd1 = new SqlCommand("insert into TBL_HAREKET (plaka,benzıntur,lıtre,fıyat) values (@par1, @par2, @par3, @par4)", connect);
                        cmd1.Parameters.AddWithValue("@par1", txt_EcoforcePlaka.Text);
                        cmd1.Parameters.AddWithValue("@par2", label5.Text);
                        cmd1.Parameters.AddWithValue("@par3", txt_EcoforceLitre.Text);
                        cmd1.Parameters.AddWithValue("@par4", decimal.Parse(txt_EcoforceTutar.Text));
                        cmd1.ExecuteNonQuery();
                        connect.Close();

                        connect.Open();
                        SqlCommand cmd2 = new SqlCommand("update TBL_AKARYAKIT set stok=stok-@stok where petrolturu='EcoForce'", connect);
                        cmd2.Parameters.AddWithValue("@stok", txt_EcoforceLitre.Text);
                        cmd2.ExecuteNonQuery();
                        connect.Close();

                        connect.Open();
                        SqlCommand cmd3 = new SqlCommand("update TBLKASA set mıktar=mıktar+@kasa", connect);
                        cmd3.Parameters.AddWithValue("@kasa", decimal.Parse(txt_EcoforceTutar.Text));
                        cmd3.ExecuteNonQuery();
                        connect.Close();
                        MessageBox.Show("EcoForce Motorin yakıtı 'Araç 3'e " + txt_EcoforceLitre.Text + " Litre dolum işlemi tamamlandı ve satışı yapıldı.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        txt_EcoforceLitre.BackColor = Color.White;
                        txt_EcoforcePlaka.BackColor = Color.White;
                        listele();
                        fiyatListesi();
                    }
                    else
                    {
                        txt_EcoforceLitre.BackColor = Color.OrangeRed;
                        txt_EcoforcePlaka.BackColor = Color.OrangeRed;
                        MessageBox.Show("Yakıt dolumu için litre ve plaka bilgisi zorunludur!\nLütfen kırmızı renkli alanları doldurunuz.", "UYARI");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Beklenmeyen bir sorun oluştu!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btn_GazDoldur_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdbtn_Gaz.Checked == true)
                {
                    if (txt_GazLitre.Text != "" && txt_GazPlaka.Text != "")
                    {
                        connect.Open();
                        SqlCommand komut1 = new SqlCommand("insert into TBL_HAREKET (plaka,benzıntur,lıtre,fıyat) values (@pr1, @pr2, @pr3, @pr4)", connect);
                        komut1.Parameters.AddWithValue("@pr1", txt_GazPlaka.Text);
                        komut1.Parameters.AddWithValue("@pr2", label6.Text);
                        komut1.Parameters.AddWithValue("@pr3", txt_GazLitre.Text);
                        komut1.Parameters.AddWithValue("@pr4", decimal.Parse(txt_GazTutar.Text));
                        komut1.ExecuteNonQuery();
                        connect.Close();

                        connect.Open();
                        SqlCommand komut2 = new SqlCommand("update TBL_AKARYAKIT set stok=stok-@stok where petrolturu='LPG'", connect);
                        komut2.Parameters.AddWithValue("@stok", txt_GazLitre.Text);
                        komut2.ExecuteNonQuery();
                        connect.Close();

                        connect.Open();
                        SqlCommand komut3 = new SqlCommand("update TBLKASA set mıktar=mıktar+@kasa", connect);
                        komut3.Parameters.AddWithValue("@kasa", decimal.Parse(txt_GazTutar.Text));
                        komut3.ExecuteNonQuery();
                        connect.Close();
                        MessageBox.Show("LPG yakıtı 'Araç 4'e " + txt_GazLitre.Text + " Litre dolum işlemi tamamlandı ve satışı yapıldı.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        txt_GazLitre.BackColor = Color.White;
                        txt_GazPlaka.BackColor = Color.White;
                        fiyatListesi();
                        listele();
                    }
                    else
                    {
                        txt_EcoforceLitre.BackColor = Color.OrangeRed;
                        txt_EcoforcePlaka.BackColor = Color.OrangeRed;
                        MessageBox.Show("Yakıt dolumu için litre ve plaka bilgisi zorunludur!\nLütfen kırmızı renkli alanları doldurunuz.", "UYARI");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Beklenmeyen bir sorun oluştu!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}