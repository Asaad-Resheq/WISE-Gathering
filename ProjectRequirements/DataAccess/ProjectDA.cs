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
    public class ProjectDA
    {
        // get specific project By ID
        public Project GetProject(int WID)
        {
            Project project = new Project();
            using (ProjectsRequirementsDBContext cntxt = new ProjectsRequirementsDBContext())
            {
                project = cntxt.Projects.Where(a => a.ID == WID).FirstOrDefault();
            }
            return project;
        }

        // get All Projects
        public List<Project> GetProjects()
        {
            List<Project> projects = new List<Project>();
            using (ProjectsRequirementsDBContext cntxt = new ProjectsRequirementsDBContext())
            {
                projects = cntxt.Projects.ToList();
            }
            return projects;
        }

        // Get All Project Wich Is related to specific User ...
        public List<Project> GetProjectsByUser(int WuserID)
        {
            List<Project> projects = new List<Project>();
            using (ProjectsRequirementsDBContext cntxt = new ProjectsRequirementsDBContext())
            {
                projects = cntxt.Projects.Where(x=>x.UserID==WuserID).ToList();
            }
            return projects;
        }

        public void Addproject(Project _project, out string errormessage,out int insertedID)
        {
            errormessage = string.Empty;
            insertedID=0;
            try
            {
                //db logic
                using (ProjectsRequirementsDBContext cntxt = new ProjectsRequirementsDBContext())
                {
                    Project project = new Project()
                    {
                        Title = _project.Title,
                        Company=_project.Company,
                        CreationDate = _project.CreationDate,
                        UserID=_project.UserID,
                        
                    };
                    cntxt.Projects.Add(project);
                    cntxt.SaveChanges();
                    insertedID = project.ID;
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

        public void UpdateProject(Project W_project, out string errormessage)
        {

            errormessage = string.Empty;
            try
            {
                //db logic
                using (ProjectsRequirementsDBContext cntxt = new ProjectsRequirementsDBContext())
                {
                    var Original_project = cntxt.Projects.Where(a => a.ID == W_project.ID).FirstOrDefault();
                    if (Original_project != null)
                    {
                        cntxt.Entry(Original_project).CurrentValues.SetValues(W_project);
                   
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

        public void DeleteProject(Project W_project, out string errormessage)
        {

            errormessage = string.Empty;
            try
            {
                //db logic
                if (W_project != null)
                {
                    using (ProjectsRequirementsDBContext cntxt = new ProjectsRequirementsDBContext())
                    {
                        var x = cntxt.Projects.Find(W_project.ID);
                        if (x != null)
                        {
                            cntxt.Projects.Remove(x);
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

        //get project with requirements data
        public Project GetProjectFullData(int WID)
        {
            Project project = new Project();
            using (ProjectsRequirementsDBContext cntxt = new ProjectsRequirementsDBContext())
            {
                project = cntxt.Projects.Include(o => o.Requirements).Where(o => o.ID == WID).FirstOrDefault();
            }
            return project;
        }


    }
}
