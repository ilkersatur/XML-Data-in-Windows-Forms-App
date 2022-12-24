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
            Amacý veri paylaþýmýdýr.
            HTML ile XML tag yapýsýný kullanýr. Farký HTML deki tagler önceler bellidir.
            XML de tag leri biz oluþtururuz. Xml de tag ler element olarak isimlendirilir. Çocuðu olan elementlere Node denir. 
            Tüm dökümanýn en üst Node u, DocumentElement ya da root olarak isimlendirilir.

            Kullaným Alanlarý:

            1-Veri paylaþýmý
            2-Ayar dosyalarý
              App.config
              Web.config 
            3-XML Web servisleri
              Servisler
            4-Oyunlarýn save dosyalarý
            5-XAML - XAMARIN - MAUI 
            6-AJAX

            Özellikleri:
            -Well Formed - iyi oluþturulmuþ xml dosyasý
            -Valid - Geçerlilik

            DTD, XSD 
            XSD, XML dosyalarýnda taþýnan verilerin nasýl yorumlanacaðýný tanýmlayan dosya. DTD dosyalarýnýn eksikliklerini gidermek üzere geliþtirilmiþtir.

            -------------------------
            XML veri okuma için saðlanan mekanizma , arayüz
            DOM(Document object model)
            SAX (Simple API for XML)

            XML dosyasýnýn well formed olduðunu browser üzerinden görebiliriz*/

            //Dom'a göre çalýþýr

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