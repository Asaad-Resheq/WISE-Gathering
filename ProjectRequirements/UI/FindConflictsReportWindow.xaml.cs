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
    /// Interaction logic for FindConflictsReportWindow.xaml
    /// </summary>
    public partial class FindConflictsReportWindow : Window
    {

        public string projecName { get; set; }
        public string companyname { get; set; }
        public string kewordtext { get; set; }
        public string requirementtext { get; set; }
        public double percentage { get; set; }
        public List<Requirement> requiremetns { get; set; }
        public List<Requirement> oppositerequiremetns { get; set; }

        public FindConflictsReportWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            Presentation.ConflictsPRSN conflictsprsn = new Presentation.ConflictsPRSN();
            conflictsprsn.generateDocument(flowdocument, projecName, companyname, kewordtext, requiremetns, oppositerequiremetns);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
