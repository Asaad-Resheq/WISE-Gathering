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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.ObjectModel;
using ProjectRequirements.Models;
using ProjectRequirements.DataAccess;

namespace ProjectRequirements
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window,INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        #endregion



        private ObservableCollection<Project> _projects;
        public ObservableCollection<Project> Projects
        {
            get { return _projects; }
            set { _projects = value;
            OnPropertyChanged(new PropertyChangedEventArgs("Projects"));
            }
        }



        private string _selectedprojectname;
        public string selectedprojectname
        {
            get { return _selectedprojectname; }
            set
            {
                _selectedprojectname = value;
                OnPropertyChanged(new PropertyChangedEventArgs("selectedprojectname"));
            }
        }

        private List<Requirement> _projectrequirements;
        public List<Requirement> projectrequirements
        {
            get { return _projectrequirements; }
            set
            {
                _projectrequirements = value;
                OnPropertyChanged(new PropertyChangedEventArgs("projectrequirements"));
            }
        }

        private List<Stickholder> _projectstickholders;
        public List<Stickholder> projectstickholders
        {
            get { return _projectstickholders; }
            set
            {
                _projectstickholders = value;
                OnPropertyChanged(new PropertyChangedEventArgs("projectstickholders"));
            }
        }


        private ICollectionView _projects_CollectionView;
        public ICollectionView Projects_CollectionView
        {
            get { return _projects_CollectionView; }
            set
            {
                _projects_CollectionView = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Projects_CollectionView"));
            }
        }


        private string _displayedusername;
        public string displayedusername
        {
            get { return _displayedusername; }
            set
            {
                _displayedusername = value;
                OnPropertyChanged(new PropertyChangedEventArgs("displayedusername"));
            }
        }

        private string _displayednumberofprojects;
        public string displayednumberofprojects
        {
            get { return _displayednumberofprojects; }
            set
            {
                _displayednumberofprojects = value;
                OnPropertyChanged(new PropertyChangedEventArgs("displayednumberofprojects"));
            }
        }

        private byte[] _userimge;
        public byte[] userimg
        {
            get { return _userimge; }
            set
            {
                _userimge = value;
                OnPropertyChanged(new PropertyChangedEventArgs("userimg"));
            }
        }

        public Project SelectedProject { get; set; }


        ProjectDA projectsDA = new ProjectDA();
        StickholderDA stickholderDA = new StickholderDA();




        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // log out button click event handler
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            try
            {
                getuserdataAndsummary();
                loadUserprojects();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + Environment.NewLine + ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
		     

        }

        private void loadUserprojects()
        {
            Projects = new ObservableCollection<Project>(projectsDA.GetProjectsByUser(Settings.GeneralSettings.LoggedUser.ID));
            Projects_CollectionView = (ICollectionView)CollectionViewSource.GetDefaultView(Projects);
        }

        private void getuserdataAndsummary()
        {
            userimg = Settings.GeneralSettings.LoggedUser.UserImage;
            displayedusername = string.Format("{0} : {1}", "User:", Settings.GeneralSettings.LoggedUser.UserName);
        }

        private void grd_projects_AutoGeneratingColumn_1(object sender, DataGridAutoGeneratingColumnEventArgs e)
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
            if (e.Column.Header.ToString() == "ID")
            { e.Column.Visibility = System.Windows.Visibility.Hidden; }
            if (e.Column.Header.ToString() == "UserID")
            { e.Column.Visibility = System.Windows.Visibility.Hidden; }
            #endregion
        }

        private void grd_projects_AutoGeneratedColumns_1(object sender, EventArgs e)
        {
            // display index

            if (grd_projects.Columns.FirstOrDefault(x => x.Header.ToString() == "Modify") != null)
            {
                grd_projects.Columns.FirstOrDefault(x => x.Header.ToString() == "Modify").DisplayIndex = grd_projects.Columns.Count - 1;

            }
            if (grd_projects.Columns.FirstOrDefault(x => x.Header.ToString() == "Delete") != null)
            {
                grd_projects.Columns.FirstOrDefault(x => x.Header.ToString() == "Delete").DisplayIndex = grd_projects.Columns.Count - 1;

            }
            if (grd_projects.Columns.FirstOrDefault(x => x.Header.ToString() == "View Summary") != null)
            {
                grd_projects.Columns.FirstOrDefault(x => x.Header.ToString() == "View Summary").DisplayIndex = grd_projects.Columns.Count-1;

            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // delete project button clicked....
            var currentItem = grd_projects.CurrentItem as Project;
            // if no project is selected from the datagrid exit from function
            if (currentItem == null) { return; }

            MessageBoxResult msgbxR = MessageBox.Show("Are You Sure You Want To Delete This Project , This Will Delete All Related Requirements?","",MessageBoxButton.YesNo,MessageBoxImage.Warning);
            if (msgbxR == MessageBoxResult.Yes)
            {
                try
                {
                    string errmessage=string.Empty;

                    // delete from database
                    projectsDA.DeleteProject(currentItem, out errmessage);

                    //remove from the collection
                    Projects.Remove(currentItem);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:" + Environment.NewLine + ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            // modify project button clicked....
            var currentItem = grd_projects.CurrentItem as Project;

            // if no project is selected from the datagrid exit from function
            if (currentItem == null) { return; }


            UI.ProjectUI projectui = new UI.ProjectUI();

          //  projectui.project = currentItem;

            projectui.project = new Project() { 
            
            ID= currentItem.ID,
            CreationDate=currentItem.CreationDate,
            LastModificationDate=currentItem.LastModificationDate,
            Title=currentItem.Title,
            Company=currentItem.Company,
            UserID=currentItem.UserID
            };
            projectui.ShowDialog();


            // return from projectui window .........
            //..... 

            if (projectui.UpdatedProjectID > 0)
            {

              //  currentItem = projectui.project;
                currentItem.LastModificationDate = projectui.project.LastModificationDate;
                currentItem.Title = projectui.project.Title;
                currentItem.Company = projectui.project.Company;
            }

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            // Add Project Button click event handler
            UI.ProjectUI projectui = new UI.ProjectUI();
            projectui.project = new Project();
            projectui.ShowDialog();


            // return from projectui window 
            //..... 

            if (projectui.insertedProjectID > 0)
            {
                 
                Projects.Add(projectui.project);
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            //  modify Requirements button click event handler

            // if no project is selected from the datagrid  cancel the event handler
            if (SelectedProject == null) { return; }
            if (SelectedProject.ID == 0) { return; }
             
            UI.RequirementsUI requirementsUI = new UI.RequirementsUI();
            requirementsUI.project = SelectedProject;
            requirementsUI.ShowDialog();

        }

        private void grd_projects_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var selecteditem = grd_projects.CurrentItem as Project;
            if (selecteditem != null)
            {
                SelectedProject = selecteditem;
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            // apply filter
            PerformSearch();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
           // reset filter
            Projects_CollectionView.Filter = null
                ;
           
        }

        private void PerformSearch()
        {
            string requirementSearchText = txtprojectnameSearch.Text;
            Projects_CollectionView.Filter = o =>
            {
                Project proj = o as Project;
                return (proj.Title.ToLower().Contains(requirementSearchText.ToLower()));
            };
        }

        private void Performsort(string propertyName, bool isAscending)
        {
            this.Projects_CollectionView.SortDescriptions.Clear();
            if (isAscending)
            {
                this.Projects_CollectionView.SortDescriptions.Add
             (new SortDescription(propertyName, ListSortDirection.Ascending));
            }
            else
            {
                this.Projects_CollectionView.SortDescriptions.Add
                          (new SortDescription(propertyName, ListSortDirection.Descending));
            }
          
        }


        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            // sort By property combobox selection changed
            string propertyname = string.Empty;
            bool isasc = true;
            getfiltersstatesfromcontrols(out propertyname, out isasc);
            Performsort(propertyname, isasc);
        }

        private void ComboBox_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
        {
            // sort asc desc combobox selection changed
            string propertyname = string.Empty;
            bool isasc = true;
            getfiltersstatesfromcontrols(out propertyname, out isasc);
            Performsort(propertyname, isasc);
        }

        private void getfiltersstatesfromcontrols(out string propertyName,out bool isasc)
        {
            isasc = true;
            propertyName = "CreationDate";
            // sort by combobox selection changed
            var selecteditem = cmbsortby.SelectedItem as ComboBoxItem;
            if (selecteditem == null) { return; }

            if (selecteditem.Tag.ToString() == "cd")
            {
                propertyName = "CreationDate";
            }
            else if (selecteditem.Tag.ToString() == "md")
            {
                propertyName = "LastModificationDate";
            }

            // get asc desc state...
            
            selecteditem = cmbsortmode.SelectedItem as ComboBoxItem;
            if (selecteditem != null)
            {
                if (selecteditem.Tag.ToString() == "asc")
                {
                    isasc = true;
                }
                else if (selecteditem.Tag.ToString() == "desc")
                {
                    isasc = false;
                }
            }
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            // view project summary
            try
            {
               var currentItem = grd_projects.CurrentItem as Project;
              // if no project is selected from the datagrid exit from function
               if (currentItem == null) { return; }
               var proj= projectsDA.GetProjectFullData(currentItem.ID);
               projectstickholders= stickholderDA.GetStickholdersByProject(currentItem.ID);
               selectedprojectname = proj.Title;
               projectrequirements = proj.Requirements.ToList();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + Environment.NewLine + ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
