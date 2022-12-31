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
using ProjectRequirements.Models;
using ProjectRequirements.DataAccess;



namespace ProjectRequirements.UI
{
    /// <summary>
    /// Interaction logic for ProjectUI.xaml
    /// </summary>
    public partial class ProjectUI : Window, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        #endregion



        private Project _project;
        public Project project
        {
            get { return _project; }
            set
            {
                _project = value;
                OnPropertyChanged(new PropertyChangedEventArgs("project"));
            }
        }

        public int insertedProjectID { get; set; }
        public int UpdatedProjectID { get; set; }
         

        ProjectDA projectda = new ProjectDA();

        public ProjectUI()
        {
            InitializeComponent();  
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // save project...
            try
            {
                // some basic validation
                if (string.IsNullOrWhiteSpace(project.Title))
                {
                    MessageBox.Show("Insert Project Title...", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // (if project.ID == 0 )we insert new project...
                if (project.ID == 0)
                {
                    project.CreationDate = DateTime.Now;
                    project.UserID = Settings.GeneralSettings.LoggedUser.ID;
                    string errormessage = string.Empty;
                    int insertedid = 0;

                    projectda.Addproject(project, out errormessage, out insertedid);
                    if (errormessage.Length == 0)
                    {
                        // inserted successfully
                        project.ID = insertedid;
                        insertedProjectID = insertedid;
                        // close this window and return to the calling window (dashboard)
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error:" + errormessage, "", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

                 // else (if project.ID not equal to  0 )we update the specific project...
                else
                {
                    project.LastModificationDate = DateTime.Now;
                    string errormessage = string.Empty;
                    projectda.UpdateProject(project, out errormessage);

                    if (errormessage.Length == 0)
                    {
                        // updated successfully
                        UpdatedProjectID = project.ID;
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
            insertedProjectID = 0;
            UpdatedProjectID = 0;
        }
    }
}
