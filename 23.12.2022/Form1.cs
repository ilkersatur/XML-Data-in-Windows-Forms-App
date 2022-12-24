using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Xml;

namespace _23._12._2022
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            /*
            XML - (Extensible Markup Language)
            Amac� veri payla��m�d�r.
            HTML ile XML tag yap�s�n� kullan�r. Fark� HTML deki tagler �nceler bellidir.
            XML de tag leri biz olu�tururuz. Xml de tag ler element olarak isimlendirilir. �ocu�u olan elementlere Node denir. 
            T�m d�k�man�n en �st Node u, DocumentElement ya da root olarak isimlendirilir.

            Kullan�m Alanlar�:

            1-Veri payla��m�
            2-Ayar dosyalar�
              App.config
              Web.config 
            3-XML Web servisleri
              Servisler
            4-Oyunlar�n save dosyalar�
            5-XAML - XAMARIN - MAUI 
            6-AJAX

            �zellikleri:
            -Well Formed - iyi olu�turulmu� xml dosyas�
            -Valid - Ge�erlilik

            DTD, XSD 
            XSD, XML dosyalar�nda ta��nan verilerin nas�l yorumlanaca��n� tan�mlayan dosya. DTD dosyalar�n�n eksikliklerini gidermek �zere geli�tirilmi�tir.

            -------------------------
            XML veri okuma i�in sa�lanan mekanizma , aray�z
            DOM(Document object model)
            SAX (Simple API for XML)

            XML dosyas�n�n well formed oldu�unu browser �zerinden g�rebiliriz*/

            //Dom'a g�re �al���r

            XmlDocument xdoc = new XmlDocument();
            xdoc.Load("../../../data.xml");
            //treeView1.Nodes.Add(xdoc.InnerXml);
            //treeView1.Nodes.Add(xdoc.DocumentElement.InnerXml);
            //treeView1.Nodes.Add(new TreeNode(xdoc.InnerXml));
            //foreach (XmlNode node in xdoc.DocumentElement.ChildNodes)
            //{
            //    treeView1.Nodes.Add(new TreeNode(node.InnerXml));
            //}

            TreeNode root = new TreeNode(xdoc.DocumentElement.Name);//ROOT
            foreach(XmlNode node in xdoc.DocumentElement.ChildNodes)
            {
                TreeNode childNode = new TreeNode (node.Name + " " + node.Attributes[0].Name + " = " + node.Attributes[0].Value);

                foreach (XmlNode child in node.ChildNodes)
                {
                    childNode.Nodes.Add(childNode.Name + " > " + child.InnerText);
                }
                root.Nodes.Add(childNode);
            }
            treeView1.Nodes.Clear();
            treeView1.Nodes.Add(root);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load("../../../data.xml");

            XmlElement kitap = xdoc.CreateElement("Kitap");
            XmlElement ad = xdoc.CreateElement("Ad");
            XmlElement yazar = xdoc.CreateElement("Yazar");
            XmlElement fiyat = xdoc.CreateElement("Fiyat");
            XmlAttribute id=xdoc.CreateAttribute("Id");
            ad.InnerText = textBox1.Text;
            yazar.InnerText = textBox2.Text;
            fiyat.InnerText = textBox3.Text;
            id.Value = textBox4.Text;

            kitap.Attributes.Append(id);
            kitap.AppendChild(ad);
            kitap.AppendChild(yazar);
            kitap.AppendChild(fiyat);

            xdoc.DocumentElement.AppendChild(kitap);
            xdoc.Save("../../../Data.xml");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("select*from AdvUrunler", "Data Source=DESKTOP\\MSSQLSERVER2014;Initial Catalog=Calisma;User ID=sa;Password=1230");
         
            DataTable dt = new DataTable("Kitaplar");
            da.Fill(dt);
            dt.WriteXml("Urunler.xml");

        }
        //select* from AdvUrunler
        //for xml auto
        //sql den xml e veri aktarma 
    }
}