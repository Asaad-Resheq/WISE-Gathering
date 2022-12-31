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
using ProjectRequirements.DataAccess;
 



namespace ProjectRequirements
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {

 
        UserDA userDA = new UserDA();




        public LoginWindow()
        {
            InitializeComponent();
        }








       // sign up Hyperlink click event handler....
        private void Hyperlink_Click_1(object sender, RoutedEventArgs e)
        {

            SignUpWindow signupwindow = new SignUpWindow();
            signupwindow.ShowDialog();
        }

        // Log in button click event handler....
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
               C_Users user=  userDA.getUserbyCredentials(txtusername.Text, txtuserpassword.Password);
               if (user == null) 
               {
                   MessageBox.Show("Invalid Log In Information", "", MessageBoxButton.OK, MessageBoxImage.Error);
                   return;

               }
               if (user.ID == 0)
               {
                   MessageBox.Show("Invalid Log In Information", "", MessageBoxButton.OK, MessageBoxImage.Error);
                   return;
               }



              // login information is correct ....


                // store the current user info  ....
               Settings.GeneralSettings.LoggedUser = user;


                // show main window (dashboard)
               MainWindow main = new MainWindow();
               main.Show();


                // close this window
               this.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + Environment.NewLine+ ex.Message,"",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
                       
        }




         
        
     
       
    }
}
