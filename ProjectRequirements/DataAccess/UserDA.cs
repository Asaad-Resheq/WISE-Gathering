using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectRequirements.Models;
using System.Data.Entity.Infrastructure;

namespace ProjectRequirements.DataAccess
{
    public class UserDA
    {
        public List<C_Users> get_users()
        {
            List<C_Users> users = new List<C_Users>();
            using (ProjectsRequirementsDBContext cntxt = new ProjectsRequirementsDBContext())
            {
                users = cntxt.C_Users.ToList();
            }
            return users;
        }
       
        public C_Users getUserbyCredentials(string username,string password)
        {
            C_Users user = new C_Users();



            // first we encrypt the entered password by the user so we can compare it with the one stored in the database....
            string encryptedpassword = Helpers.StringCipher.Encrypt(password, Settings.GeneralSettings.passPhrase);


            using (ProjectsRequirementsDBContext cntxt = new ProjectsRequirementsDBContext())
            {
                user = cntxt.C_Users.Where(x => x.UserName == username && x.UserPassword == encryptedpassword).FirstOrDefault();
            }
            return user;
        }

        public void AddUser(C_Users user, out string errormessage)
        {
            errormessage = string.Empty;
            try
            {
                //db logic

                using (ProjectsRequirementsDBContext cntxt = new ProjectsRequirementsDBContext())
                {
                    C_Users newUser = new C_Users
                    {

                        UserName = user.UserName,
                        UserPassword = Helpers.StringCipher.Encrypt(user.UserPassword,Settings.GeneralSettings.passPhrase),
                        UserImage = user.UserImage

                    };
                    cntxt.C_Users.Add(newUser);
                    cntxt.SaveChanges();
                }
            }
            catch (DbUpdateException ex)
            {
                errormessage = string.Format("{0} : {1}", "Inner Exception", ex.InnerException.InnerException.Message);
            }
            catch (Exception ex)
            {
                errormessage = string.Format("{0} : {1}", "General Exception", ex.Message);
            }
        }


    }
}
