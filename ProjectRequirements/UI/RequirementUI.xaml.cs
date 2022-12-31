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
    /// Interaction logic for RequirementUI.xaml
    /// </summary>
    public partial class RequirementUI : Window, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        #endregion


        public Project project { get; set; }

        private List<Stickholder> _stickholders;
        public List<Stickholder> stickholders
        {
            get { return _stickholders; }
            set
            {
                _stickholders = value;
                OnPropertyChanged(new PropertyChangedEventArgs("stickholders"));
            }
        }

        /*
        private ObservableCollection<string> _requirementKeywords;
        public ObservableCollection<string> requirementKeywords
        {
            get { return _requirementKeywords; }
            set
            {
                _requirementKeywords = value;
                OnPropertyChanged(new PropertyChangedEventArgs("requirementKeywords"));
            }
        }
        */
        private ObservableCollection<ReqKey> _requirementKeywords;
        public ObservableCollection<ReqKey> requirementKeywords
        {
            get { return _requirementKeywords; }
            set
            {
                _requirementKeywords = value;
                OnPropertyChanged(new PropertyChangedEventArgs("requirementKeywords"));
            }
        }


        private Requirement _requirement;
        public Requirement requirement
        {
            get { return _requirement; }
            set
            {
                _requirement = value;
                OnPropertyChanged(new PropertyChangedEventArgs("requirement"));
            }
        }



        StickholderDA stickholderDA = new StickholderDA();
        RequirementDA requirementDA = new RequirementDA();



        public int insertedRequirementID { get; set; }
        public int UpdatedRequirementID { get; set; }


        public RequirementUI()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // save Requirement..
           
            try
            {
                
                // some basic validation
                if (requirement.BaseKeyWord.Length<3)
                {
                    MessageBox.Show("Requirement Keyword Must Be Greater Than Two Charachters", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(requirement.ReqText))
                {
                    MessageBox.Show("Insert requirement Text...", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (requirement.StickholderID == null)
                {
                    MessageBox.Show("select requirement Stickholder...", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (requirement.StickholderID == 0)
                {
                    MessageBox.Show("select requirement Stickholder...", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                //..............






                // (if requirement.ID == 0 )we insert new requirement...
                if (requirement.ID == 0)
                {
                    requirement.CreattionDate = DateTime.Now;
                    requirement.ProjectID = project.ID;
                    string errormessage = string.Empty;
                    int insertedid = 0;

                    requirementDA.AddRequirement(requirement, out errormessage, out insertedid);
                    
                    if (errormessage.Length == 0)
                    {
                        // inserted successfully
                        requirement.ID = insertedid;
                        insertedRequirementID = insertedid;
                        // close this window and return to the calling window (dashboard)
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error:" + errormessage, "", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

                 // else (if requirement.ID not equal to  0 )we update the specific requirement...
                else
                {
                    requirement.LastModificationDate = DateTime.Now;
                    string errormessage = string.Empty;

                    requirementDA.UpdateRequirement(requirement, out errormessage);
                     

                    if (errormessage.Length == 0)
                    {
                        // updated successfully
                        UpdatedRequirementID = requirement.ID;// not necessary though ....


                        // close this window and return to the calling window (dashboard)
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error:" + errormessage, "", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + Environment.NewLine + ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {

            insertedRequirementID = 0;
            UpdatedRequirementID = 0;

            try
            {
                stickholders = stickholderDA.GetStickholdersByProject(project.ID);
                requirementKeywords = new ObservableCollection<ReqKey>(requirementDA.get_Keywords());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + Environment.NewLine + ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ComboBox_SourceUpdated_2(object sender, DataTransferEventArgs e)
        {

        }

        private void ComboBox_LostKeyboardFocus_2(object sender, KeyboardFocusChangedEventArgs e)
        {

        }
    }
}
