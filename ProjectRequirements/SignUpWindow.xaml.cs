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
using Microsoft.Win32;
using System.IO;
using ProjectRequirements.DataAccess;


namespace ProjectRequirements
{
    /// <summary>
    /// Interaction logic for SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow : Window, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged  implementation ..
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
        #endregion


        private C_Users _User;
        public C_Users User
        {
            get { return _User; }
            set
            {
                _User = value;
                OnPropertyChanged(new PropertyChangedEventArgs("User"));
            }
        }


        UserDA userDA = new UserDA();


        public SignUpWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            User = new C_Users();
           
        }

        private void Hyperlink_Click_1(object sender, RoutedEventArgs e)
        {
            // choose user image 
 
            var ofdlg = new OpenFileDialog()
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"
            };
            if (ofdlg.ShowDialog() == true)
            {
                // ImageSource imageSource = BitmapFromUri(new Uri(ofdlg.FileName));
                byte[] data;
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(new Uri(ofdlg.FileName)));
                using (MemoryStream ms = new MemoryStream())
                {
                    encoder.Save(ms);
                    data = ms.ToArray();
                }

                User.UserImage = data;

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Save User Data Button Click
            try
            {

                if (string.IsNullOrWhiteSpace(User.UserName))
                {
                    MessageBox.Show("Enter User Name", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }



                // before saving new user data we must check if password matches....
                string password = txtuserpassword.Password;
                string passwordretype = txtuserpasswordRetype.Password;

                

                if (password.Length == 0)
                {
                    MessageBox.Show("Enter Password", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (password != passwordretype)
                {
                    MessageBox.Show("Password Mismatch....Please Retype Password", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }


                // Add User To Database
                string errormessage=string.Empty;
                User.UserPassword = txtuserpassword.Password;
                userDA.AddUser(User, out errormessage);

                if (errormessage.Length != 0)
                {
                    MessageBox.Show("Error Adding User,Error Message:" + Environment.NewLine + errormessage, "", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                this.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + Environment.NewLine + ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
