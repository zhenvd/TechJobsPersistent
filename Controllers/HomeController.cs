using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;
using TechJobsPersistent.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace TechJobsPersistent.Controllers
{
    public class HomeController : Controller
    {
        private JobDbContext context;

        public HomeController(JobDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Job> jobs = context.Jobs.Include(j => j.Employer).Include(s => s.JobSkills).ToList();
            return View(jobs);
        }

        [HttpGet("/Add")]
        public IActionResult AddJob()
        {
            //Part 3 Updating HomeController 1.
            //In the AddJob() method, update the AddJobViewModel object so that you pass all of the Skill objects in the database to the constructor.
            List<Skill> skills = context.Skills.ToList();
            List<Employer> employers = context.Employers.ToList();

            //Part 2 Adding a Job
            //2. In AddJob() pass an instance of AddJobViewModel to the view.
            AddJobViewModel addJobViewModel = new AddJobViewModel(employers, skills);
            return View(addJobViewModel);
        }


            //Part 3 Updating HomeController 2.
            //In the ProcessAddJobForm() method, pass in a new parameter: an array of strings called selectedSkills. 
        public IActionResult ProcessAddJobForm(AddJobViewModel addJobViewModel, string[] selectedSkills)
        {
            //Part 2 Adding a Job
            //4. In ProcessAddJobForm(), you need to take in an instance of AddJobViewModel and make sure that any validation
            //conditions you want to add are met before creating a new Job object and saving it to the database.
            if (ModelState.IsValid)
            {
                Employer theEmployer = context.Employers.Find(addJobViewModel.EmployerId);
                Job newJob = new Job
                {
                    Name = addJobViewModel.Name,
                    EmployerId = addJobViewModel.EmployerId,
                    Employer = theEmployer
                };
                //context.Jobs.Add(newJob);
                //context.SaveChanges();

                //Part 3 Updating HomeController 2a.
                //After you add a new parameter, you want to set up a loop to go through each item in selectedSkills.
                //This loop should go right after you create a new Job object and before you add that Job object to the database.
                foreach(var skill in selectedSkills)
                {
                    //Part 3 Updating HomeController 2b.
                    //Inside the loop, you will create a new JobSkill object with the newly-created Job object.
                    //You will also need to parse each item in selectedSkills as an integer to use for SkillId.
                    JobSkill jobSkill = new JobSkill
                    {
                        JobId = newJob.Id,
                        Job = newJob,
                        SkillId = Int32.Parse(skill)
                    };
                    //Part 3 Updating HomeController 2c.
                    //Add each new JobSkill object to the DbContext object,
                    //but do not add an additional call to SaveChanges() inside the loop!
                    //One call at the end of the method is enough to get the updated info to the database
                    context.JobSkills.Add(jobSkill);
                }

                context.Jobs.Add(newJob);
                context.SaveChanges();
                return Redirect("/Employer");
            }
            return View("Add", addJobViewModel);
        }

        public IActionResult Detail(int id)
        {
            Job theJob = context.Jobs
                .Include(j => j.Employer)
                .Single(j => j.Id == id);

            List<JobSkill> jobSkills = context.JobSkills
                .Where(js => js.JobId == id)
                .Include(js => js.Skill)
                .ToList();

            JobDetailViewModel viewModel = new JobDetailViewModel(theJob, jobSkills);
            return View(viewModel);
        }
    }
}
