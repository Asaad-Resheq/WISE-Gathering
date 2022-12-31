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
    /// Interaction logic for StickholderUI.xaml
    /// </summary>
    public partial class StickholderUI : Window, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        #endregion


        private Stickholder _stickholder;
        public Stickholder stickholder
        {
            get { return _stickholder; }
            set
            {
                _stickholder = value;
                OnPropertyChanged(new PropertyChangedEventArgs("stickholder"));
            }
        }

        public int insertedstickholderID { get; set; }
        public int UpdatedstickholderID { get; set; }


        public int projectid { get; set; }

        StickholderDA stickholderDA = new StickholderDA();

        public StickholderUI()
        {
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            insertedstickholderID = 0;
            UpdatedstickholderID = 0;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // save stickholder...
            try
            {
                

                if (projectid==0)
                {
                     MessageBox.Show("Project Is Not Defined", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // some basic validation
                if (string.IsNullOrWhiteSpace(stickholder.StickholderName))
                {
                    MessageBox.Show("Insert stickholder Name...", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Email check
                if (!string.IsNullOrWhiteSpace(stickholder.Email))
                {

                    Helpers.MailAddressHelper mailaddresshelper = new Helpers.MailAddressHelper();
                    if (!mailaddresshelper.IsValidMail(stickholder.Email))
                    {
                        MessageBox.Show("Invalid Email Address...", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                   
                }

                // (if stickholder.ID == 0 )we insert new stickholder...
                if (stickholder.ID == 0)
                {

                    stickholder.ProjectId = projectid;
                    string errormessage = string.Empty;
                    int insertedid = 0;


                     
                    stickholderDA.AddStickholder(stickholder, out errormessage, out insertedid);
                    if (errormessage.Length == 0)
                    {
                        // inserted successfully
                        stickholder.ID = insertedid;
                        insertedstickholderID = insertedid;
                        // close this window and return to the calling window (dashboard)
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error:" + errormessage, "", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

                 // else (if stickholder.ID not equal to  0 )we update the specific stickholder...
                else
                {
                    
                    string errormessage = string.Empty;
                    stickholderDA.UpdateStickholder(stickholder, out errormessage);

                    if (errormessage.Length == 0)
                    {
                        // updated successfully
                        UpdatedstickholderID = stickholder.ID;
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
    }
}
