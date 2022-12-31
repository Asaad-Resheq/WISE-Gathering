using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectRequirements.Models;
using System.Data.Entity.Infrastructure;


namespace ProjectRequirements.DataAccess
{
    public class StickholderDA
    {
        // get specific Stickholder By ID
        public Stickholder GetStickholder(int WID)
        {
            Stickholder stickholder = new Stickholder();
            using (ProjectsRequirementsDBContext cntxt = new ProjectsRequirementsDBContext())
            {
                stickholder = cntxt.Stickholders.Where(a => a.ID == WID).FirstOrDefault();
            }
            return stickholder;
        }

        // get All Stickholders
        public List<Stickholder> GetStickholders()
        {
            List<Stickholder> stickholders = new List<Stickholder>();
            using (ProjectsRequirementsDBContext cntxt = new ProjectsRequirementsDBContext())
            {
                stickholders = cntxt.Stickholders.ToList();
            }
            return stickholders;
        }

        // Get All Stickholders Wich Is related to specific Project ...
        public List<Stickholder> GetStickholdersByProject(int WprojectID)
        {
            List<Stickholder> stickholders = new List<Stickholder>();
            using (ProjectsRequirementsDBContext cntxt = new ProjectsRequirementsDBContext())
            {
                stickholders = cntxt.Stickholders.Where(x => x.ProjectId == WprojectID).ToList();
            }
            return stickholders;
        }

        public void AddStickholder(Stickholder _stickholder, out string errormessage, out int insertedID)
        {
            errormessage = string.Empty;
            insertedID = 0;
            try
            {
                //db logic
                using (ProjectsRequirementsDBContext cntxt = new ProjectsRequirementsDBContext())
                {
                    Stickholder stickholder = new Stickholder()
                    {
                        StickholderName = _stickholder.StickholderName,
                        ProjectId=_stickholder.ProjectId,
                        Email=_stickholder.Email,
                        Mobile=_stickholder.Mobile,
                      
                    };
                    cntxt.Stickholders.Add(stickholder);
                    cntxt.SaveChanges();
                    insertedID = stickholder.ID;
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

        public void UpdateStickholder(Stickholder W_stickholder, out string errormessage)
        {

            errormessage = string.Empty;
            try
            {
                //db logic
                using (ProjectsRequirementsDBContext cntxt = new ProjectsRequirementsDBContext())
                {
                    var Original_stickholder = cntxt.Stickholders.Where(a => a.ID == W_stickholder.ID).FirstOrDefault();
                    if (Original_stickholder != null)
                    {
                        cntxt.Entry(Original_stickholder).CurrentValues.SetValues(W_stickholder);

                        cntxt.SaveChanges();
                    }
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

        public void DeleteStickholder(Stickholder W_stickholder, out string errormessage)
        {

            errormessage = string.Empty;
            try
            {
                //db logic
                if (W_stickholder != null)
                {
                    using (ProjectsRequirementsDBContext cntxt = new ProjectsRequirementsDBContext())
                    {
                        var x = cntxt.Stickholders.Find(W_stickholder.ID);
                        if (x != null)
                        {
                            cntxt.Stickholders.Remove(x);
                            cntxt.SaveChanges();
                        }
                    }
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


        public List<Requirement> get_stickholderRequiremetns(int WID)
        {
            List<Requirement> reqs = new List<Requirement>();
            using (ProjectsRequirementsDBContext cntxt = new ProjectsRequirementsDBContext())
            {
                reqs = cntxt.Requirements.Where(x => x.StickholderID == WID).ToList();
            }
            return reqs;
        }
    }
}
