using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;
using ProjectRequirements.Models;
using ProjectRequirements.DataAccess;
using ProjectRequirements.UIModels;


namespace ProjectRequirements.UI
{
    /// <summary>
    /// Interaction logic for FindConflictsWindow.xaml
    /// </summary>
    public partial class FindConflictsWindow : Window, INotifyPropertyChanged
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
            set
            {
                _requirements = value;
                OnPropertyChanged(new PropertyChangedEventArgs("requirements"));
            }
        }

        private ICollectionView _requirementsOpposite_CollectionView;
        public ICollectionView requirementsOpposite_CollectionView
        {
            get { return _requirementsOpposite_CollectionView; }
            set
            {
                _requirementsOpposite_CollectionView = value;
                OnPropertyChanged(new PropertyChangedEventArgs("requirementsOpposite_CollectionView"));
            }
        }
        private List<Requirement> _requirementsOpposite;
        public List<Requirement> requirementsOpposite
        {
            get { return _requirementsOpposite; }
            set
            {
                _requirementsOpposite = value;
                OnPropertyChanged(new PropertyChangedEventArgs("requirementsOpposite"));
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
        private int resultOppositeRequirementCount = 0;




        public string P_Projectname { get; set; }
        public string P_companyname { get; set; }
        public string P_keywordtext { get; set; }
      //  public string P_requirementtext { get; set; }
     //   public double P_percentage { get; set; }
        public List<Requirement> P_requiremets { get; set; }
        public List<Requirement> P_oppositrequiremets { get; set; }



        public FindConflictsWindow()
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


                  // get all requirements(As Opposite) for specific project....
                 // copy the requirements we got from database to the opposite requirements
                requirementsOpposite = requirements.Select(x => new Requirement()
                {
                    ID = x.ID,
                    BaseKeyWord = x.BaseKeyWord,
                    CreattionDate = x.CreattionDate,
                    LastModificationDate = x.LastModificationDate,
                    ProjectID = x.ProjectID,
                    ReqText = x.ReqText,
                    StickholderID = x.StickholderID,
                    Stickholder = x.Stickholder
                }).ToList();
                 


                requirements_CollectionView = (ICollectionView)CollectionViewSource.GetDefaultView(requirements);
                requirementsOpposite_CollectionView = (ICollectionView)CollectionViewSource.GetDefaultView(requirementsOpposite);

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

            requirements_CollectionView.Filter = o =>
            {
                Requirement req = o as Requirement;
                return (req.BaseKeyWord == keywordSearchtext && req.ReqText.ToLower().Contains(requirementSearchText.ToLower()));
            };


            var filteredcount = requirements_CollectionView.Cast<Requirement>().Count();
            P_requiremets = requirements_CollectionView.Cast<Requirement>().ToList();
            resultRequirementCount = filteredcount;


            
        }


        // perform search (filtering) on the Project  Requirements (opposite) Based on User Filtering options.....
        private void PerformSearchOpposite()
        {
            string requirementSearchText = txt_searchReqOpposite.Text;
            string keywordSearchtext = cmb_keyword.Text;

             requirementsOpposite_CollectionView.Filter = o =>
            {
                Requirement req = o as Requirement;
                return (req.BaseKeyWord == keywordSearchtext && req.ReqText.ToLower().Contains(requirementSearchText.ToLower()));
            };


             var filteredcount = requirementsOpposite_CollectionView.Cast<Requirement>().Count();
             P_oppositrequiremets = requirementsOpposite_CollectionView.Cast<Requirement>().ToList();
            resultOppositeRequirementCount = filteredcount;


            
        }

        private void displayresult()
        {
            try
            {
               // double percentage = (double)resultRequirementCount / (double)totalRequirementsCount;
              //  double percentagedisplaye = percentage * 100.0;
               // percentagedisplaye = Math.Round(percentagedisplaye, 2);
                string result = string.Format("{0}", resultRequirementCount);

              //  percentage = (double)resultOppositeRequirementCount / (double)totalRequirementsCount;
             //   percentagedisplaye = percentage * 100.0;
             //   percentagedisplaye = Math.Round(percentagedisplaye, 2);
                string resultopposite = string.Format("{0}", resultOppositeRequirementCount);
                lblResult.Content = string.Format("{0} {1} And {2} {3} from Total : {4}", result,"Requirement(s)", resultopposite,"Opposite(s)",totalRequirementsCount);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // search click event handler



            PerformSearch();
            PerformSearchOpposite();

            displayresult();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // reset click event handler
            // reset filter....
            requirements_CollectionView.Filter = null
                ;
            requirementsOpposite_CollectionView.Filter = null;
            lblResult.Content = "";
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
        // report..

            FindConflictsReportWindow findConflictsReportWindow = new FindConflictsReportWindow();


            findConflictsReportWindow.projecName = P_Projectname;
            findConflictsReportWindow.companyname = P_companyname;
            findConflictsReportWindow.kewordtext = P_keywordtext;
            findConflictsReportWindow.requiremetns = P_requiremets;
            findConflictsReportWindow.oppositerequiremetns = P_oppositrequiremets;
            findConflictsReportWindow.ShowDialog();
        }
    }
}
