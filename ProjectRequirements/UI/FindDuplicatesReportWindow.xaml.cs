using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using ProjectRequirements.Models;

namespace ProjectRequirements.UI
{
    /// <summary>
    /// Interaction logic for FindDuplicatesReportWindow.xaml
    /// </summary>
    public partial class FindDuplicatesReportWindow : Window
    {

        public string projecName { get; set; }
        public string companyname { get; set; }
        public string kewordtext { get; set; }
        public string requirementtext { get; set; }
        public double percentage { get; set; }
        public List<Requirement> requiremetns { get; set; }







        public FindDuplicatesReportWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            Presentation.DuplicatesPRSN duplicatesprsn = new Presentation.DuplicatesPRSN();
            duplicatesprsn.generateDocument(flowdocument, projecName, companyname, kewordtext, requirementtext,percentage, requiremetns);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
