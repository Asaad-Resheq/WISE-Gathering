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


namespace ProjectRequirements.UI
{
    /// <summary>
    /// Interaction logic for RequirementsUI.xaml
    /// </summary>
    public partial class RequirementsUI : Window, INotifyPropertyChanged
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

        private ObservableCollection<Requirement> _requirements;
        public ObservableCollection<Requirement> requirements
        {
            get { return _requirements; }
            set
            {
                _requirements = value;
                OnPropertyChanged(new PropertyChangedEventArgs("requirements"));
            }
        }

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

        private ObservableCollection<Stickholder> _stickholders;
        public ObservableCollection<Stickholder> stickholders
        {
            get { return _stickholders; }
            set
            {
                _stickholders = value;
                OnPropertyChanged(new PropertyChangedEventArgs("stickholders"));
            }
        }


         
        private ObservableCollection<Stickholder> _stickholdersForLists;
        public ObservableCollection<Stickholder> stickholdersForLists
        {
            get { return _stickholdersForLists; }
            set
            {
                _stickholdersForLists = value;
                OnPropertyChanged(new PropertyChangedEventArgs("stickholdersForLists"));
            }
        }



        RequirementDA requirementDA = new RequirementDA();
        StickholderDA stickholderDA = new StickholderDA();



        public Stickholder SelectedStickholder { get; set; }
        public Requirement SelectedRequirement { get; set; }


        public RequirementsUI()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // add stick holder button click event handler


            if (project == null) { return; }
            if (project.ID == 0) { return; }



            UI.StickholderUI stickholderUI = new UI.StickholderUI();
            stickholderUI.projectid = project.ID;
            stickholderUI.stickholder = new Stickholder();
            stickholderUI.ShowDialog();

            // return from stickholderUI window 
            //..... 
            if (stickholderUI.insertedstickholderID > 0)
            {
                stickholders.Add(stickholderUI.stickholder);
                
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // add requirements button click event handler

            if (project == null) { return; }
            if (project.ID == 0) { return; }

            RequirementUI requirementUI = new RequirementUI();
            requirementUI.project = this.project;
            requirementUI.requirement = new Requirement();
            requirementUI.ShowDialog();

            if (requirementUI.insertedRequirementID > 0)
            {
                // update the collection
                requirements.Add(requirementUI.requirement);
            }

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            // Find Duplicates button click event handler

            if (project == null) { return; }
            if (project.ID == 0) { return; }

            FindDuplicatesWindow findduplicateswindow = new FindDuplicatesWindow();
            findduplicateswindow.ProjectID = project.ID;
            findduplicateswindow.P_Projectname = project.Title;
            findduplicateswindow.P_companyname = project.Company;
            findduplicateswindow.ShowDialog();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            // Find Conflicts button click event handler
            if (project == null) { return; }
            if (project.ID == 0) { return; }

            FindConflictsWindow findConflictsWindow = new FindConflictsWindow();
            findConflictsWindow.ProjectID = project.ID;
            findConflictsWindow.P_Projectname = project.Title;
            findConflictsWindow.P_companyname = project.Company;

            findConflictsWindow.ShowDialog();


           
            
 
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            lbl_project.Content = string.Format("{0} : {1}", project.Title,project.Company);
            try
            {
                loadprojectstickholders();
                loadprojectrequirements(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + Environment.NewLine + ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
      
          
        }

        private void loadprojectrequirements()
        {
            requirements=new ObservableCollection<Requirement> (requirementDA.GetRequirementsByProject(project.ID));
            requirements_CollectionView = (ICollectionView)CollectionViewSource.GetDefaultView(requirements);
        }

        private void loadprojectstickholders()
        {
            stickholders = new ObservableCollection<Stickholder>(stickholderDA.GetStickholdersByProject(project.ID));
        }

        private void grd_stickholders_AutoGeneratingColumn_1(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            #region cancel columns
            // cancel columns
            if (e.Column.Header.ToString() == "Requirements")
            {
                e.Cancel = true;
            }
            #endregion

            #region hiddencolumns
            // hidden columns
            if (e.Column.Header.ToString() == "ProjectId")
            { e.Column.Visibility = System.Windows.Visibility.Hidden; }

            if (e.Column.Header.ToString() == "ID")
            { e.Column.Visibility = System.Windows.Visibility.Hidden; }


            #endregion

            #region Headers

            if (grd_stickholders.Columns.FirstOrDefault(x => x.Header.ToString() == "StickholderName") != null)
            {
                grd_stickholders.Columns.FirstOrDefault(x => x.Header.ToString() == "StickholderName").Header = "Name";

            }
            #endregion
        }

        private void grd_stickholders_AutoGeneratedColumns_1(object sender, EventArgs e)
        {
            // display index

            if (grd_stickholders.Columns.FirstOrDefault(x => x.Header.ToString() == "Modify") != null)
            {
                grd_stickholders.Columns.FirstOrDefault(x => x.Header.ToString() == "Modify").DisplayIndex = grd_stickholders.Columns.Count - 1;

            }
            if (grd_stickholders.Columns.FirstOrDefault(x => x.Header.ToString() == "Delete") != null)
            {
                grd_stickholders.Columns.FirstOrDefault(x => x.Header.ToString() == "Delete").DisplayIndex = grd_stickholders.Columns.Count - 1;

            }

        }

        private void grd_stickholders_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var selecteditem = grd_stickholders.CurrentItem as Stickholder;
            if (selecteditem != null)
            {
                SelectedStickholder = selecteditem;
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            //Modify stickholder click
            
            var currentItem =  grd_stickholders.CurrentItem as Stickholder;

            // if no stickholder is selected from the datagrid exit from function
            if (currentItem == null) { return; }
            if (currentItem.ProjectId==null){return;}

            int currentItemProjectId = Convert.ToInt32( currentItem.ProjectId);
            UI.StickholderUI stickholderUI = new UI.StickholderUI();
            stickholderUI.projectid = currentItemProjectId;


          //  stickholderUI.stickholder = currentItem;
            stickholderUI.stickholder = new Stickholder() {
                ID = currentItem.ID,
                Email=currentItem.Email,
                Mobile=currentItem.Mobile,
                ProjectId=currentItem.ProjectId,
                StickholderName=currentItem.StickholderName
            };

            stickholderUI.ShowDialog();


            // return from projectui window 
            //..... 

            if (stickholderUI.UpdatedstickholderID > 0)
            {
                currentItem.StickholderName = stickholderUI.stickholder.StickholderName;
                currentItem.Mobile = stickholderUI.stickholder.Mobile;
                currentItem.Email = stickholderUI.stickholder.Email;
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {  
            //delete stickholder click
           
            var currentItem = grd_stickholders.CurrentItem as Stickholder;
            // if no stickholder is selected from the datagrid exit from function
            if (currentItem == null) { return; }



            MessageBoxResult msgbxR = MessageBox.Show("Are You Sure You Want To Delete This StickHolder?", "", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (msgbxR == MessageBoxResult.Yes)
            {
                try
                {

                    // check id the stickholder is associated with any requirements 

                    var reqs= stickholderDA.get_stickholderRequiremetns(currentItem.ID);
                    if (reqs.Count > 0)
                    {
                        MessageBox.Show("The StickHolder Is Associated With Requirements....can't Delete", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    string errmessage = string.Empty;

                    // delete from database
                    stickholderDA.DeleteStickholder(currentItem, out errmessage);

                    //remove from the collection
                    stickholders.Remove(currentItem);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:" + Environment.NewLine + ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            // modify requirement button click event handler
            

            if (project == null) { return; }
            if (project.ID == 0) { return; }

            var currentitem = grd_requirements.CurrentItem as Requirement;
            if (currentitem == null) { return; }
            if (currentitem.ProjectID == null) { return; }

            int currentItemProjectId = Convert.ToInt32(currentitem.ProjectID);

            RequirementUI requirementUI = new RequirementUI();
            requirementUI.project = this.project;

         //   requirementUI.requirement = currentitem;

            requirementUI.requirement = new Requirement()
            {
                ID = currentitem.ID,
                ProjectID = currentitem.ProjectID,
                BaseKeyWord =currentitem.BaseKeyWord,
                CreattionDate= currentitem.CreattionDate,
                LastModificationDate=currentitem.LastModificationDate,
                ReqText=currentitem.ReqText,
                StickholderID= currentitem.StickholderID
            };


            requirementUI.ShowDialog();

            if (requirementUI.UpdatedRequirementID > 0)
            {
                // update the collection
                // currentitem = requirementUI.requirement;
                currentitem.BaseKeyWord = requirementUI.requirement.BaseKeyWord;
                currentitem.LastModificationDate = requirementUI.requirement.LastModificationDate;
                currentitem.ReqText = requirementUI.requirement.ReqText;
                currentitem.StickholderID = requirementUI.requirement.StickholderID;
                 
            }
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            // delete requirement button click event handler



            var currentItem = grd_requirements.CurrentItem as Requirement;
            // if no Requirement is selected from the datagrid exit from function
            if (currentItem == null) { return; }



            MessageBoxResult msgbxR = MessageBox.Show("Are You Sure You Want To Delete This Requirement?", "", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (msgbxR == MessageBoxResult.Yes)
            {
                try
                {
                    string errmessage = string.Empty;

                    // delete from database
                    requirementDA.DeleteRequirement(currentItem,out errmessage);
                    
                    //remove from the collection
                    requirements.Remove(currentItem);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:" + Environment.NewLine + ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void grd_requirements_AutoGeneratingColumn_1(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            #region cancel columns
            // cancel columns
            if (e.Column.Header.ToString() == "Stickholder")
            {
                e.Cancel = true;
            }
            if (e.Column.Header.ToString() == "Project")
            {
                e.Cancel = true;
            }

            if (e.Column.Header.ToString() == "RequirementKeywords")
            {
                e.Cancel = true;
            }
            
            #endregion

            #region hiddencolumns
            // hidden columns
           // if (e.Column.Header.ToString() == "StickholderID")
           // { e.Column.Visibility = System.Windows.Visibility.Hidden; }


            // if (e.Column.Header.ToString() == "ProjectID")
            // { e.Column.Visibility = System.Windows.Visibility.Hidden; }

            if (e.Column.Header.ToString() == "ID")
            { e.Column.Visibility = System.Windows.Visibility.Hidden; }

            if (e.Column.Header.ToString() == "StickholderID")
            { e.Column.Visibility = System.Windows.Visibility.Hidden; }


            if (e.Column.Header.ToString() == "ProjectID")
            { e.Column.Visibility = System.Windows.Visibility.Hidden; }

          

            #endregion
        }

        private void grd_requirements_AutoGeneratedColumns_1(object sender, EventArgs e)
        {

            if (grd_requirements.Columns.FirstOrDefault(x => x.Header.ToString() == "Modify") != null)
            {
                grd_requirements.Columns.FirstOrDefault(x => x.Header.ToString() == "Modify").DisplayIndex = grd_requirements.Columns.Count - 1;

            }
            if (grd_requirements.Columns.FirstOrDefault(x => x.Header.ToString() == "Delete") != null)
            {
                grd_requirements.Columns.FirstOrDefault(x => x.Header.ToString() == "Delete").DisplayIndex = grd_requirements.Columns.Count - 1;

            }
        }

        private void grd_requirements_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var selecteditem = grd_requirements.CurrentItem as Requirement;
            if (selecteditem != null)
            {
                SelectedRequirement = selecteditem;
            }
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            // apply filter....
            PerformSearch();
        }


        private void PerformSearch()
        {
            var selectedstickholder = cmb_stickholdersearch.SelectedItem as Stickholder;
            int stickholderid = 0;
            if (selectedstickholder != null)
            { stickholderid = selectedstickholder.ID; }


            string keywordsearchtext = txtKeywordsearch.Text;

            if (stickholderid != 0) // specific stickholder....
            {
                if (string.IsNullOrWhiteSpace(keywordsearchtext))
                {
                    requirements_CollectionView.Filter = o =>
                    {
                        Requirement r = o as Requirement;
                        return ( r.StickholderID == stickholderid);
                    };
                }
                else
                {
                    requirements_CollectionView.Filter = o =>
                    {
                        Requirement r = o as Requirement;
                        return (r.BaseKeyWord == keywordsearchtext && r.StickholderID == stickholderid);
                    };
                }
                
            }
            else // no filtering for stickholder .... include all 
            {
                requirements_CollectionView.Filter = o =>
                {
                    Requirement r = o as Requirement;
                    return (r.BaseKeyWord == keywordsearchtext);
                };
            }
           
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
                //clear filter...
            requirements_CollectionView.Filter = null;
        }
         
           
    }
}
