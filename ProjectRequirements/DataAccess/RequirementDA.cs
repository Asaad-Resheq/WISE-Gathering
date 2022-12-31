using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectRequirements.Models;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace ProjectRequirements.DataAccess
{
    public class RequirementDA
    {

        // get specific Requirement By ID
        public Requirement GetRequirement(int WID)
        {
            Requirement requirement = new Requirement();
            using (ProjectsRequirementsDBContext cntxt = new ProjectsRequirementsDBContext())
            {
                requirement = cntxt.Requirements.Where(a => a.ID == WID).FirstOrDefault();
            }
            return requirement;
        }

        // get All Requirements
        public List<Requirement> GetRequirements()
        {
            List<Requirement> Requirements = new List<Requirement>();
            using (ProjectsRequirementsDBContext cntxt = new ProjectsRequirementsDBContext())
            {
                Requirements = cntxt.Requirements.ToList();
            }
            return Requirements;
        }

        // Get All Requirements Wich Is related to specific Project ...
        public List<Requirement> GetRequirementsByProject(int WprojectID)
        {
            List<Requirement> requirements = new List<Requirement>();
            using (ProjectsRequirementsDBContext cntxt = new ProjectsRequirementsDBContext())
            {
                requirements = cntxt.Requirements.Where(x => x.ProjectID == WprojectID).ToList();
            }
            return requirements;
        }


        // Get All Requirements Wich Is related to specific Project Including The Stickholder Information ...
        public List<Requirement> GetRequirementsWithStickholderByProject(int WprojectID)
        {
            List<Requirement> requirements = new List<Requirement>();
            using (ProjectsRequirementsDBContext cntxt = new ProjectsRequirementsDBContext())
            {
                requirements = cntxt.Requirements.Include(o => o.Stickholder).Where(x => x.ProjectID == WprojectID).ToList();
            }
            return requirements;
        }


        public void AddRequirement(Requirement _requirement, out string errormessage, out int insertedID)
        {
            errormessage = string.Empty;
            insertedID = 0;
            try
            {
                //db logic
                using (ProjectsRequirementsDBContext cntxt = new ProjectsRequirementsDBContext())
                {
                    Requirement requirement = new Requirement()
                    {
                        BaseKeyWord = _requirement.BaseKeyWord,
                        CreattionDate=_requirement.CreattionDate,
                        LastModificationDate=_requirement.LastModificationDate,
                        ProjectID=_requirement.ProjectID,
                        StickholderID=_requirement.StickholderID,
                        ReqText=_requirement.ReqText

                    };
                    cntxt.Requirements.Add(requirement);
                    cntxt.SaveChanges();
                    insertedID = requirement.ID;
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

        public void UpdateRequirement(Requirement W_Requirement, out string errormessage)
        {

            errormessage = string.Empty;
            try
            {
                //db logic
                using (ProjectsRequirementsDBContext cntxt = new ProjectsRequirementsDBContext())
                {
                    var Original_Requirement = cntxt.Requirements.Where(a => a.ID == W_Requirement.ID).FirstOrDefault();
                    if (Original_Requirement != null)
                    {
                        cntxt.Entry(Original_Requirement).CurrentValues.SetValues(W_Requirement);

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

        public void DeleteRequirement(Requirement W_Requirement, out string errormessage)
        {

            errormessage = string.Empty;
            try
            {
                //db logic
                if (W_Requirement != null)
                {
                    using (ProjectsRequirementsDBContext cntxt = new ProjectsRequirementsDBContext())
                    {
                        var x = cntxt.Requirements.Find(W_Requirement.ID);
                        if (x != null)
                        {
                            cntxt.Requirements.Remove(x);
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




        // get the list of all entered (and distinct) Keywords
        public List<UIModels.ReqKey> get_Keywords( )
        {
            List<UIModels.ReqKey> reqKeywords = new List<UIModels.ReqKey>();
            using (ProjectsRequirementsDBContext cntxt = new ProjectsRequirementsDBContext())
            {

               var reqKeywordslst = cntxt.Requirements.Select(x => x.BaseKeyWord).Distinct().ToList();
               foreach (var item in reqKeywordslst)
               {
                   reqKeywords.Add(new UIModels.ReqKey() { rKey = item });
               }

            }
            return reqKeywords;
        }


        // get the list of all entered (and distinct) Keywords that is related to specific Requirement...
        public List<UIModels.ReqKey> get_requirementKeywords(int wid)
        {
            /*
            List<string> reqKeywords = new List<string>();
            using (ProjectsRequirementsDBContext cntxt = new ProjectsRequirementsDBContext())
            {

                reqKeywords = cntxt.Requirements.Where(x=>x.ID==wid).Select(xx => xx.BaseKeyWord).Distinct().ToList();

            }
            */
            List<UIModels.ReqKey> reqKeywords = new List<UIModels.ReqKey>();
            using (ProjectsRequirementsDBContext cntxt = new ProjectsRequirementsDBContext())
            {

                var reqKeywordslst = cntxt.Requirements.Where(x => x.ID == wid).Select(xx => xx.BaseKeyWord).Distinct().ToList();
                foreach (var item in reqKeywordslst)
                {
                    reqKeywords.Add(new UIModels.ReqKey() { rKey = item });
                }

            }
            return reqKeywords;
        }
    }
}
