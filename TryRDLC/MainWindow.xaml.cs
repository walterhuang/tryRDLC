using Microsoft.Reporting.WinForms;
using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZXing;

namespace TryRDLC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Fixture fixture = new Fixture();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateReportButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // clean data source first
                ////ReportViewer.LocalReport.DataSources.Clear();
                ReportViewer.Reset();

                // set report
                ReportViewer.LocalReport.ReportEmbeddedResource = "TryRDLC.BOL.rdlc";

                ReportViewer.LocalReport.DataSources.Add(new ReportDataSource(
                    "BOLHeader",
                    fixture.Build<BillOfLadingHeader>()
                        .With(b => b.BillOfLadingNumberBarcodeImage, GetGS1128BarcodeImage("40206141411234567890"))
                        .CreateMany(1)));

                ReportViewer.LocalReport.DataSources.Add(new ReportDataSource(
                    "BOLOrders",
                    fixture.CreateMany<BillOfLadingCustomerOrderInformation>(2)));

                ReportViewer.LocalReport.DataSources.Add(new ReportDataSource(
                    "BOLCarriers",
                    fixture.CreateMany<BillOfLadingCarrierInformation>(3)));

                ReportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private byte[] GetGS1128BarcodeImage(string code)
        {
            BarcodeWriter writer = new BarcodeWriter { Format = BarcodeFormat.CODE_128 };
            using (var img = writer.Write($"{(char)0x00F1}{code}"))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    img.Save(ms, ImageFormat.Png);
                    return ms.ToArray();
                }
            }
        }

        private void PrintReportButton_Click(object sender, RoutedEventArgs e)
        {
            new LocalReportHelper(ReportViewer.LocalReport).Print();
        }
    }
}
