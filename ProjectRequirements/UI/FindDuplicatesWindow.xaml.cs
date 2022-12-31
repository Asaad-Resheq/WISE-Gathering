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
using System.ComponentModel;
using System.Collections.ObjectModel;
using ProjectRequirements.Models;
using ProjectRequirements.DataAccess;
using ProjectRequirements.UIModels;


namespace ProjectRequirements.UI
{
    /// <summary>
    /// Interaction logic for FindDuplicatesWindow.xaml
    /// </summary>
    public partial class FindDuplicatesWindow : Window, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        #endregion


        public int ProjectID { get; set; }

        private ICollectionView _requirements_CollectionView;

        public ICollectionView requirements_CollectionView
        {
            get { return _requirements_CollectionView; }
            set
            {
                _requirements_CollectionView = value;
                OnPropertyChanged(new PropertyChangedEventArgs("requirements_CollectionView"));
            }
        }
        private List<Requirement> _requirements;
        public List<Requirement> requirements
        {
            get { return _requirements; }
            set { _requirements = value;
            OnPropertyChanged(new PropertyChangedEventArgs("requirements"));
            }
        }


        private List<ReqKey> _requirementKeywords;
        public List<ReqKey> requirementKeywords
        {
            get { return _requirementKeywords; }
            set
            {
                _requirementKeywords = value;
                OnPropertyChanged(new PropertyChangedEventArgs("requirementKeywords"));
            }
        }

        RequirementDA requirementDA = new RequirementDA();


        private int totalRequirementsCount = 0;
        private int resultRequirementCount = 0;


        public string P_Projectname { get; set; }
        public string P_companyname { get; set; }
        public string P_keywordtext { get; set; }
        public string P_requirementtext { get; set; }
        public double P_percentage { get; set; }
        public List<Requirement> P_requiremets { get; set; }



        public FindDuplicatesWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            try
            {
                // get all keywords , we will use this list as a suggestion source for the combobox
                requirementKeywords = (requirementDA.get_Keywords());

                // get all requirements for specific project....
                requirements = requirementDA.GetRequirementsWithStickholderByProject(ProjectID);
                totalRequirementsCount = requirements.Count;
                requirements_CollectionView = (ICollectionView)CollectionViewSource.GetDefaultView(requirements);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + Environment.NewLine + ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




        // perform search (filtering) on the Project Requirements Based on User Filtering options.....
        private void PerformSearch()
        {
            string requirementSearchText = txt_searchReq.Text;
            string keywordSearchtext = cmb_keyword.Text;

            P_keywordtext = keywordSearchtext;
            P_requirementtext = requirementSearchText;


            requirements_CollectionView.Filter = o =>
                {
                    Requirement req = o as Requirement;
                    return (req.BaseKeyWord == keywordSearchtext && req.ReqText.ToLower().Contains(requirementSearchText.ToLower()));
                };


            var filteredcount = requirements_CollectionView.Cast<Requirement>().Count();
            P_requiremets= requirements_CollectionView.Cast<Requirement>().ToList();
            resultRequirementCount = filteredcount;


            displayresult();
        }

        private void displayresult()
        {
            try
            {
                double percentage = (double)resultRequirementCount / (double)totalRequirementsCount;
                P_percentage = percentage;
                double percentagedisplaye = percentage * 100.0;
              
                percentagedisplaye= Math.Round(percentagedisplaye, 2); // taking only two decimal places ....
                string result = string.Format("{0}  from total {1}    ({2}%)", resultRequirementCount, totalRequirementsCount, percentagedisplaye);
                lblResult.Content = result;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
          
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // search button click event handler
            PerformSearch();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // reset filter....
            requirements_CollectionView.Filter = null;
            lblResult.Content = "";
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            // report button click....
            FindDuplicatesReportWindow findduplicatesreportWindow = new FindDuplicatesReportWindow();
            findduplicatesreportWindow.requiremetns = P_requiremets;
            findduplicatesreportWindow.percentage = P_percentage;
            findduplicatesreportWindow.projecName = P_Projectname;
            findduplicatesreportWindow.companyname = P_companyname;
            findduplicatesreportWindow.kewordtext = P_keywordtext;
            findduplicatesreportWindow.requirementtext = P_requirementtext;
            findduplicatesreportWindow.ShowDialog();
        }
    }
}
