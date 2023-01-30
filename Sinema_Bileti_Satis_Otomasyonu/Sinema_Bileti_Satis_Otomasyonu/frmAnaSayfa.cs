using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sinema_Bileti_Satis_Otomasyonu
{
    public partial class frmAnaSayfa : Form
    {
        public frmAnaSayfa()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-44H9RT4;Initial Catalog=Sinema_Bileti;Integrated Security=True");


        int sayac = 0;
        private void FilmveSalonGetir(ComboBox combo, string sql1, string sql2)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand(sql1, baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                combo.Items.Add(read[sql2].ToString());
            }
            baglanti.Close();
        }
           // SqlDataReader read = komut.ExecuteReader(); 

        private void FilmAfişiGoster()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from film_bilgileri where filmadi='"+comboFilmAdi.SelectedItem+"'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                pictureBox1.ImageLocation = read["resim"].ToString();
            }
            baglanti.Close();
        }
        private void Combo_Dolu_Koltuklar()
        {
            comboKoltukİptal.Items.Clear();
            comboKoltukİptal.Text = "";
            foreach (Control item in panel2.Controls)
            {
                if (item is Button)
                {
                    if (item.BackColor==Color.Red)
                    {
                        comboKoltukİptal.Items.Add(item.Text);
                    }
                }
            }
        }
        private void YenidenRenklendir()
        {
            foreach (Control item in panel2.Controls)
            {
                if (item is Button)
                {
                    item.BackColor = Color.White;
                }
            }
        }
        private void Veritabanı_Dolu_Koltuklar()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from satis_bilgileri where filmadi='"+comboFilmAdi.SelectedItem+"' and salonadi='"+comboSalonAdi.Text+"' and tarih='"+comboFilmTarihi.SelectedItem+"' and saat='"+comboFilmSeansi.SelectedItem+"'",baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                foreach (Control item in panel2.Controls)
                {
                    if (item is Button)
                    {
                        if (read["koltukno"].ToString()==item.Text)
                        {
                            item.BackColor = Color.Red;

                        }


                    }
                }
            }
            baglanti.Close();
        }
        private void frmAnaSayfa_Load(object sender, EventArgs e)
        {
            Boş_Koltuklar();
            FilmveSalonGetir(comboFilmAdi, "select *from film_bilgileri", "filmadi");
            FilmveSalonGetir(comboSalonAdi, "select *from salon_bilgileri", "salonadi");
        }

        private void FilmveSalonGoster(ComboBox comboFilmAdi, string v1, string v2)
        {

        }

        private void Boş_Koltuklar()
        {
            sayac = 1;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Button btn = new Button();
                    btn.Size = new Size(30, 30);
                    btn.BackColor = Color.White;
                    btn.Location = new Point(j * 30 + 20, i * 30 + 30);
                    btn.Name = sayac.ToString();
                    btn.Text = sayac.ToString();
                    if (j == 4)
                    {
                        continue;
                    }
                    sayac++;
                    this.panel2.Controls.Add(btn);
                    btn.Click += Btn_Click;
                }
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.BackColor == Color.White)
            {
                txtKoltukNo.Text = b.Text;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnSalonEkle_Click(object sender, EventArgs e)
        {
            frmSalonEkle ekle = new frmSalonEkle();
            ekle.Show();
            this.Hide();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnFilmEkle_Click(object sender, EventArgs e)
        {
            frmFilmEkle ekle2 = new frmFilmEkle();
            ekle2.Show();
            this.Hide();

        }

        private void BtnSeansEkle_Click(object sender, EventArgs e)
        {
            frmSeansEkle ekle3 = new frmSeansEkle();
            ekle3.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            frmSatisListeleme satis = new frmSatisListeleme();
            satis.Show();
            this.Hide();
        }

        private void btnSeansListele_Click(object sender, EventArgs e)
        {
            frmSeansListele seans = new frmSeansListele();
            seans.Show();
            this.Hide();
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void comboFilmAdi_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboFilmSeansi.Items.Clear();
            comboFilmTarihi.Items.Clear();
            comboFilmSeansi.Text = "";
            comboFilmTarihi.Text = "";
            comboSalonAdi.Text = "";
            foreach (Control item in groupBox1.Controls) if (item is TextBox) item.Text = "";
            {

            }
            FilmAfişiGoster();
            YenidenRenklendir();
            Combo_Dolu_Koltuklar();
        }
        sinemaTableAdapters.Satis_BilgileriTableAdapter satis = new sinemaTableAdapters.Satis_BilgileriTableAdapter();

        private void btnBiletSat_Click(object sender, EventArgs e)
        {
            if (txtKoltukNo.Text != "")
            {
                try
                {

                    satis.Satış_Yap(txtKoltukNo.Text, comboSalonAdi.Text, comboFilmAdi.Text, comboFilmTarihi.Text, comboFilmSeansi.Text, txtAd.Text, txtSoyad.Text, comboUcret.Text, DateTime.Now.ToString());
                    foreach (Control item in groupBox1.Controls) if (item is TextBox) item.Text = "";
                    YenidenRenklendir();
                    Veritabanı_Dolu_Koltuklar();
                    Combo_Dolu_Koltuklar();

                }
                catch (Exception hata)
                {

                    MessageBox.Show("Hata Oluştu!!!"+hata.Message, "Uyarı");
                }
            }
            else
            {
                MessageBox.Show("Koltuk Seçimi yapmadınız!!!", "Uyarı");

            }
            }
        private void Film_Tarihi_Getir()
        {
            comboFilmTarihi.Text = "";
            comboFilmSeansi.Text = "";
            comboFilmTarihi.Items.Clear();
            comboFilmSeansi.Items.Clear();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from seans_bilgileri where filmadi='"+comboFilmAdi.SelectedItem+"' and salonadi='"+comboSalonAdi.SelectedItem+"' ",baglanti);
            SqlDataReader read= komut.ExecuteReader();
            while (read.Read())
            {
                if (DateTime.Parse(read["tarih"].ToString()) >= DateTime.Parse(DateTime.Now.ToShortDateString()))
                {
                    if (!comboFilmTarihi.Items.Contains(read["tarih"].ToString()))
                    {

                    }
                    comboFilmTarihi.Items.Add(read["tarih"].ToString());

                }

            }
            baglanti.Close();

        }
        private void comboSalonAdi_SelectedIndexChanged(object sender, EventArgs e)
        {
            Film_Tarihi_Getir();
        }
        private void Film_Seansi_Getir()
        {
            comboFilmSeansi.Text = "";
            comboFilmSeansi.Items.Clear();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from seans_bilgileri where filmadi='" + comboFilmAdi.SelectedItem + "' and salonadi='" + comboSalonAdi.SelectedItem + "' and tarih='"+comboFilmTarihi.SelectedItem+"' ", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (DateTime.Parse(read["tarih"].ToString()) == DateTime.Parse(DateTime.Now.ToShortDateString()))
                {
                 
                    if (DateTime.Parse(read["seans"].ToString()) > DateTime.Parse(DateTime.Now.ToShortTimeString()))
                    {
                        comboFilmSeansi.Items.Add(read["seans"].ToString());

                    }



                }
               else if (DateTime.Parse(read["tarih"].ToString()) > DateTime.Parse(DateTime.Now.ToShortDateString()))
                {
                    

                    
                    comboFilmSeansi.Items.Add(read["seans"].ToString());

                }

            }
            baglanti.Close();
        }
        private void comboFilmTarihi_SelectedIndexChanged(object sender, EventArgs e)
        {
            Film_Seansi_Getir();
        }

        private void comboFilmSeansi_SelectedIndexChanged(object sender, EventArgs e)
        {
            YenidenRenklendir();
            Veritabanı_Dolu_Koltuklar();
            Combo_Dolu_Koltuklar();
        }

        private void btnBiletİptal_Click(object sender, EventArgs e)
        {
            if (comboKoltukİptal.Text!="")
            {
                try
                {
                    satis.Satış_İptal(comboFilmAdi.Text, comboSalonAdi.Text, comboFilmTarihi.Text, comboFilmSeansi.Text, comboKoltukİptal.Text);
                    YenidenRenklendir();
                    Veritabanı_Dolu_Koltuklar();
                    Combo_Dolu_Koltuklar();
                }
                catch (Exception hata)
                {
                    MessageBox.Show("Hata oluştu!!!"+hata.Message, "Uyarı");

                }

            }
            else
            {
            }
        }

        private void comboKoltukİptal_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }
    }
    }
