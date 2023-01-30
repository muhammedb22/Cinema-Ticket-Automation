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
    public partial class frmSalonEkle : Form
    {
        public frmSalonEkle()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void frmSalonEkle_Load(object sender, EventArgs e)
        {

        }

        private void frmSalonEkle_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmAnaSayfa anaSayfa= new frmAnaSayfa();
            anaSayfa.Show();
        }
        sinemaTableAdapters.Salon_BilgileriTableAdapter salon = new sinemaTableAdapters.Salon_BilgileriTableAdapter();
        private void BtnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                salon.SalonEkleme(txtSalonAdi.Text);
                MessageBox.Show("Salon Eklendi", "Kayıt");
            }
            catch (Exception)
            {

                MessageBox.Show("Aynı salonu daha önce eklediniz!!!","Uyarı");
            }
            txtSalonAdi.Text = "" ;
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void txtSalonAdi_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
