using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace kelimebulmaca
{
    public partial class frmbul : Form
    {
        public frmbul()
        {
            InitializeComponent();
        }
        string dosyayol, satir, kelime = "", butonname;
        List<string> lstkelimler = new List<string>();
        List<int> lstindexno = new List<int>();
        List<Control> butonlar = new List<Control>();
        List<Control> pasifbutonlar = new List<Control>();
        public List<int> sayiuret(int kelimezunluk)
        {
            List<int> rastgelesayilar = new List<int>();
            Random rnd = new Random();
            int sayi, sayac = 0;
            do
            {
                sayi = rnd.Next(0, kelimezunluk);
                if (rastgelesayilar.IndexOf(sayi) == -1)
                {
                    rastgelesayilar.Add(sayi);
                    sayac++;
                }
            } while (sayac != kelimezunluk);

            return rastgelesayilar;
        }

        private void frmbul_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            label2.Text = string.Empty;
            groupBox1.Controls.Clear();
        }

        private void lstbxkelimeler_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public List<string> kelimeyaz()
        {
            List<string> yedekkelimler = new List<string>();
            dosyayol = Directory.GetCurrentDirectory() + "\\kelimeler.txt";
            FileStream dosyaerisim = new FileStream(dosyayol, FileMode.Open, FileAccess.Read);
            StreamReader okuyucu = new StreamReader(dosyaerisim);
            satir = okuyucu.ReadLine();
            do
            {
                yedekkelimler.Add(satir.ToUpper());
                satir = okuyucu.ReadLine();
                if (satir == null)
                {
                    break;
                }

            } while (satir != null);

            foreach (string item in yedekkelimler)
            {
                lstbxkelimeler.Items.Add(item);
            }
            return yedekkelimler;
        }

        private void btnbasla_Click(object sender, EventArgs e)
        {
            lstbxkelimeler.Items.Clear();
            groupBox1.Controls.Clear();
            label2.Text = "";
            int harfno = 0;
            List<char> lstharfler = new List<char>();
            List<int> lstharfindexno = new List<int>();
            lstkelimler = kelimeyaz();


            for (int i = 0; i < lstkelimler.Count; i++)
            {
                lstindexno.Clear();
                lstindexno = sayiuret(lstkelimler[i].Length);

                foreach (int item in lstindexno)
                {
                    lstharfler.Add(lstkelimler[i][item]);
                    //listBox1.Items.Add(lstkelimler [i][ item].ToString ());
                }
            }
            lstharfindexno = sayiuret(90);
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Button btnharf = new Button();
                    btnharf.Height = 25;
                    btnharf.Width = 25;
                    btnharf.Text = lstharfler[lstharfindexno[harfno]].ToString();
                    harfno++;
                    btnharf.BackColor = Color.White;
                    btnharf.Location = new Point(10 + (j * 35), 10 + (i * 35));
                    btnharf.Name = (10 + (j * 35)).ToString() + (10 + (i * 35)).ToString();
                    btnharf.Click += new EventHandler(btnharf_click);
                    groupBox1.Controls.Add(btnharf);
                }
            }

        }

        public void btnharf_click(object sender, System.EventArgs e)
        {
            Button btn = sender as Button;

            btn.Enabled = false;
            kelime += btn.Text;
            if (kelime.Length > 7)
            {
                kelime = "";
                kelime += btn.Text;
            }

            label2.Text = kelime;
            butonname = btn.Location.X.ToString() + btn.Location.Y.ToString();
            if (butonname == btn.Name)
            {
                butonlar.Add(btn);
                pasifbutonlar.Add(btn);
            }

            if (lstkelimler.IndexOf(kelime) > -1)
            {
                lstkelimler.Remove(kelime);
                kelime = "";

                lstbxkelimeler.Items.Clear();
                for (int i = 0; i < butonlar.Count; i++)
                {
                    butonlar[i].Enabled = false;
                    butonlar[i].BackColor = Color.Red;
                }
                butonlar.Clear();
                pasifbutonlar.Clear();
                foreach (string item in lstkelimler)
                {
                    lstbxkelimeler.Items.Add(item);
                }
            }
            else if (kelime.Length > 6)
            { for (int i = 0; i < pasifbutonlar.Count; i++)
                {
                    pasifbutonlar[i].Enabled = true;
                }
                pasifbutonlar.Clear();
                butonlar.Clear();
            }


        }
    }
}
