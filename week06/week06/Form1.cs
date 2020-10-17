using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using week06.MnbserviceReference;

namespace week06
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            WebszolgaltatoMeghivas();
        }
        void WebszolgaltatoMeghivas()
        {
            var mnbservice = new MNBArfolyamServiceSoapClient();

            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = "EUR",
                startDate = "2020-01-01",
                endDate = "2020-06-30"
            };

            var response = mnbservice.GetExchangeRates(request);
            var result = response.GetExchangeRatesResult;
            /*SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = Application.StartupPath;
            sfd.DefaultExt = "xml";
            sfd.AddExtension = true;
            if (sfd.ShowDialog() != DialogResult.OK) return;
            using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
            {
                sw.Write(result);
            }*/
            //XML vizsgálata
        }
    }
}
