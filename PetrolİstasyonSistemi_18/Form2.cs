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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        SqlConnection connect = new SqlConnection(@"");

        private void Form2_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void listele()
        {
            string query = "select PETROLTURU, ALISFIYAT, SATISFIYAT, STOK from TBL_AKARYAKIT";
            SqlDataAdapter dap = new SqlDataAdapter(query, connect);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            listView1.Items.Clear();

            foreach (DataRow row in dt.Rows)
            {
                ListViewItem item = new ListViewItem(row["PETROLTURU"].ToString());
                item.SubItems.Add(row["ALISFIYAT"].ToString());
                item.SubItems.Add(row["SATISFIYAT"].ToString());
                item.SubItems.Add(row["STOK"].ToString());
                listView1.Items.Add(item);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        { // HAREKET TABLOSUNU TEMİZLEME
            DialogResult result = MessageBox.Show("Bayii'nin kayıtlı tüm satışları sıfırlanacak, emin misiniz?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                connect.Open();
                SqlCommand komut = new SqlCommand("truncate table TBL_HAREKET", connect);
                komut.ExecuteNonQuery();
                connect.Close();
                MessageBox.Show("Tüm satışlar başarıyla sıfırlanmıştır.", "BİLGİ");
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1 frm1 = new Form1();
            frm1.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        { // KASA TABLOSUNU TEMİZLEME
            DialogResult result = MessageBox.Show("Bayii'nin tüm geliri sıfırlanacak, bu işlemden emin misiniz?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                connect.Open();
                SqlCommand komut = new SqlCommand("truncate table TBLKASA", connect);
                komut.ExecuteNonQuery();
                connect.Close();
                MessageBox.Show("Bayii'nin kasası başarıyla sıfırlanmıştır.", "BİLGİ");
            }
        }

        private void btn_FiyatGuncelleme_Click(object sender, EventArgs e)
        {
            try
            {
                if (check_Ultraforce95.Checked == true)
                {
                    connect.Open();
                    SqlCommand cmd = new SqlCommand("update TBL_AKARYAKIT set alısfıyat=@p1, satısfıyat=@p2 where petrolturu='UltraForce95'", connect);
                    cmd.Parameters.AddWithValue("@p1", decimal.Parse(txt_AlisUltraforce95.Text));
                    cmd.Parameters.AddWithValue("@p2", decimal.Parse(txt_SatisUltraforce95.Text));
                    cmd.ExecuteNonQuery();
                    connect.Close();
                }

                if (check_Ultraforce.Checked == true)
                {
                    connect.Open();
                    SqlCommand cmd = new SqlCommand("update TBL_AKARYAKIT set alısfıyat=@p1, satısfıyat=@p2 where petrolturu='UltraForce'", connect);
                    cmd.Parameters.AddWithValue("@p1", decimal.Parse(txt_AlisUltraforce.Text));
                    cmd.Parameters.AddWithValue("@p2", decimal.Parse(txt_SatisUltraforce.Text));
                    cmd.ExecuteNonQuery();
                    connect.Close();
                }

                if (check_Ecoforce.Checked == true)
                {
                    connect.Open();
                    SqlCommand cmd = new SqlCommand("update TBL_AKARYAKIT set alısfıyat=@p1, satısfıyat=@p2 where petrolturu='EcoForce'", connect);
                    cmd.Parameters.AddWithValue("@p1", decimal.Parse(txt_AlisEcoforce.Text));
                    cmd.Parameters.AddWithValue("@p2", decimal.Parse(txt_SatisEcoforce.Text));
                    cmd.ExecuteNonQuery();
                    connect.Close();
                }

                if (check_LPG.Checked == true)
                {
                    connect.Open();
                    SqlCommand cmd = new SqlCommand("update TBL_AKARYAKIT set alısfıyat=@p1, satısfıyat=@p2 where petrolturu='LPG'", connect);
                    cmd.Parameters.AddWithValue("@p1", decimal.Parse(txt_AlisLPG.Text));
                    cmd.Parameters.AddWithValue("@p2", decimal.Parse(txt_SatisLPG.Text));
                    cmd.ExecuteNonQuery();
                    connect.Close();
                }
                
                listele();
                MessageBox.Show("Seçili akaryakıtların fiyatları başarıyla güncellenmiştir.", "BİLGİ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Fiyat güncellenirken beklenmedik hata oluştu.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UltraForce95()
        {
            try
            {
                if (txt_TankUltraforce95.Text != "")
                {
                    decimal dolarKur = Convert.ToDecimal(label16.Text);
                    decimal OTV = Convert.ToDecimal(label22.Text);
                    decimal ilktoplam = dolarKur + OTV;
                    decimal ikincitoplam = ilktoplam + Convert.ToDecimal(txt_TankUltraforce95.Text);

                    if (check_TankUltraforce95.Checked == true)
                    {
                        DialogResult odemeMesaj = MessageBox.Show("UltraForce95 yakıtını " + txt_TankUltraforce95.Text + " Litre kadar doldurarak aşağıdaki hesaplamalarla kasadan ödeme yapılacak, onaylıyor musunuz?\n\n" +
                            "Kurşunsuz Benzin ÖTV Fiyatı: " + OTV + "\n" +
                            "Birinci Hesaplama: " + dolarKur + " + " + OTV + " = " + ilktoplam + "\n" +
                            "İkinci Hesaplama: " + ilktoplam + " + " + txt_TankUltraforce95.Text + " = " + ikincitoplam + "\n" +
                            "Hesaplanan Bayii Alış Fiyatı: " + ikincitoplam + " ₺",
                            "ÖDEME ONAYI", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                        if (odemeMesaj == DialogResult.Yes)
                        {
                            connect.Open();
                            SqlCommand cmd1 = new SqlCommand("update TBLKASA set mıktar=mıktar-@pr1", connect);
                            cmd1.Parameters.AddWithValue("@pr1", ikincitoplam);
                            cmd1.ExecuteNonQuery();
                            connect.Close();

                            connect.Open();
                            SqlCommand cmd2 = new SqlCommand("update TBL_AKARYAKIT set stok=stok+@pr1 where petrolturu='UltraForce95'", connect);
                            cmd2.Parameters.AddWithValue("@pr1", txt_TankUltraforce95.Text);
                            cmd2.ExecuteNonQuery();
                            connect.Close();
                            MessageBox.Show("UltraForce95 yakıtın tankı aşağıdaki bilgilere göre ödeme yapılarak doldurulmuştur.\n\n" +
                                "Doldurulan litre: " + txt_TankUltraforce95.Text + " L \n" +
                                "Ödenen toplam tutar: " + ikincitoplam + " ₺", "BİLGİ");
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Program çalışırken sıra dışı bir hata oluştu.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void UltraForceMotorin()
        {
            try
            {
                if (txt_TankUltraforce.Text != "")
                {
                    decimal dolarKur = Convert.ToDecimal(label16.Text);
                    decimal OTV = Convert.ToDecimal(label23.Text);
                    decimal ilktoplam = dolarKur + OTV;
                    decimal ikincitoplam = ilktoplam + Convert.ToDecimal(txt_TankUltraforce.Text);

                    if (check_TankUltraforce.Checked == true)
                    {
                        DialogResult odemeMesaj = MessageBox.Show("'UltraForce Motorin' yakıtını " + txt_TankUltraforce.Text + " Litre kadar doldurarak aşağıdaki hesaplamalarla kasadan ödeme yapılacak, onaylıyor musunuz?\n\n" +
                            "Motorin ÖTV Fiyatı: " + OTV + "\n" +
                            "Birinci Hesaplama: " + dolarKur + " + " + OTV + " = " + ilktoplam + "\n" +
                            "İkinci Hesaplama: " + ilktoplam + " + " + txt_TankUltraforce.Text + " = " + ikincitoplam + "\n" +
                            "Hesaplanan Bayii Alış Fiyatı: " + ikincitoplam + " ₺",
                            "ÖDEME ONAYI", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                        if (odemeMesaj == DialogResult.Yes)
                        {
                            connect.Open();
                            SqlCommand cmd1 = new SqlCommand("update TBLKASA set mıktar=mıktar-@pr1", connect);
                            cmd1.Parameters.AddWithValue("@pr1", ikincitoplam);
                            cmd1.ExecuteNonQuery();
                            connect.Close();

                            connect.Open();
                            SqlCommand cmd2 = new SqlCommand("update TBL_AKARYAKIT set stok=stok+@pr1 where petrolturu='UltraForce'", connect);
                            cmd2.Parameters.AddWithValue("@pr1", txt_TankUltraforce.Text);
                            cmd2.ExecuteNonQuery();
                            connect.Close();
                            MessageBox.Show("'UltraForce Motorin' yakıtın tankı aşağıdaki bilgilere göre ödeme yapılarak doldurulmuştur.\n\n" +
                                "Doldurulan litre: " + txt_TankUltraforce.Text + " L \n" +
                                "Ödenen toplam tutar: " + ikincitoplam + " ₺", "BİLGİ");
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Program çalışırken sıra dışı bir hata oluştu.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void EcoforceMotorin()
        {
            try
            {
                if (txt_TankEcoforce.Text != "")
                {
                    decimal dolarKur = Convert.ToDecimal(label16.Text);
                    decimal OTV = Convert.ToDecimal(label24.Text);
                    decimal ilktoplam = dolarKur + OTV;
                    decimal ikincitoplam = ilktoplam + Convert.ToDecimal(txt_TankEcoforce.Text);

                    if (check_TankEcoforce.Checked == true)
                    {
                        DialogResult odemeMesaj = MessageBox.Show("'EcoForce Motorin' yakıtını " + txt_TankEcoforce.Text + " Litre kadar doldurarak aşağıdaki hesaplamalarla kasadan ödeme yapılacak, onaylıyor musunuz?\n\n" +
                            "Motorin ÖTV Fiyatı: " + OTV + "\n" +
                            "Birinci Hesaplama: " + dolarKur + " + " + OTV + " = " + ilktoplam + "\n" +
                            "İkinci Hesaplama: " + ilktoplam + " + " + txt_TankEcoforce.Text + " = " + ikincitoplam + "\n" +
                            "Hesaplanan Bayii Alış Fiyatı: " + ikincitoplam + " ₺",
                            "ÖDEME ONAYI", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                        if (odemeMesaj == DialogResult.Yes)
                        {
                            connect.Open();
                            SqlCommand cmd1 = new SqlCommand("update TBLKASA set mıktar=mıktar-@pr1", connect);
                            cmd1.Parameters.AddWithValue("@pr1", ikincitoplam);
                            cmd1.ExecuteNonQuery();
                            connect.Close();

                            connect.Open();
                            SqlCommand cmd2 = new SqlCommand("update TBL_AKARYAKIT set stok=stok+@pr1 where petrolturu='EcoForce'", connect);
                            cmd2.Parameters.AddWithValue("@pr1", txt_TankEcoforce.Text);
                            cmd2.ExecuteNonQuery();
                            connect.Close();
                            MessageBox.Show("'EcoForce Motorin' yakıtın tankı aşağıdaki bilgilere göre ödeme yapılarak doldurulmuştur.\n\n" +
                                "Doldurulan litre: " + txt_TankEcoforce.Text + " L \n" +
                                "Ödenen toplam tutar: " + ikincitoplam + " ₺", "BİLGİ");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Program çalışırken sıra dışı bir hata oluştu.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Gaz()
        {
            try
            {
                if (txt_TankLPG.Text != "")
                {
                    decimal dolarKur = Convert.ToDecimal(label16.Text);
                    decimal OTV = Convert.ToDecimal(label25.Text);
                    decimal ilktoplam = dolarKur + OTV;
                    decimal ikincitoplam = ilktoplam + Convert.ToDecimal(txt_TankLPG.Text);

                    if (check_TankLPG.Checked == true)
                    {
                        DialogResult odemeMesaj = MessageBox.Show("'LPG' yakıtını " + txt_TankLPG.Text + " Litre kadar doldurarak aşağıdaki hesaplamalarla kasadan ödeme yapılacak, onaylıyor musunuz?\n\n" +
                            "LPG ÖTV Fiyatı: " + OTV + "\n" +
                            "Birinci Hesaplama: " + dolarKur + " + " + OTV + " = " + ilktoplam + "\n" +
                            "İkinci Hesaplama: " + ilktoplam + " + " + txt_TankLPG.Text + " = " + ikincitoplam + "\n" +
                            "Hesaplanan Bayii Alış Fiyatı: " + ikincitoplam + " ₺",
                            "ÖDEME ONAYI", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                        if (odemeMesaj == DialogResult.Yes)
                        {
                            connect.Open();
                            SqlCommand cmd1 = new SqlCommand("update TBLKASA set mıktar=mıktar-@pr1", connect);
                            cmd1.Parameters.AddWithValue("@pr1", ikincitoplam);
                            cmd1.ExecuteNonQuery();
                            connect.Close();

                            connect.Open();
                            SqlCommand cmd2 = new SqlCommand("update TBL_AKARYAKIT set stok=stok+@pr1 where petrolturu='LPG'", connect);
                            cmd2.Parameters.AddWithValue("@pr1", txt_TankLPG.Text);
                            cmd2.ExecuteNonQuery();
                            connect.Close();
                            MessageBox.Show("'LPG' yakıtın tankı aşağıdaki bilgilere göre ödeme yapılarak doldurulmuştur.\n\n" +
                                "Doldurulan litre: " + txt_TankLPG.Text + " L \n" +
                                "Ödenen toplam tutar: " + ikincitoplam + " ₺", "BİLGİ");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Program çalışırken sıra dışı bir hata oluştu.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void btn_TankDoldurma_Click(object sender, EventArgs e)
        {
            UltraForce95();
            UltraForceMotorin();
            EcoforceMotorin();
            Gaz();
        }
    }
}
