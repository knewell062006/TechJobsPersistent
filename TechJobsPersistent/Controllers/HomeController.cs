﻿using System;
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
            List<Job> jobs = context.Jobs.Include(j => j.Employer).ToList();

            return View(jobs);
        }

        [HttpGet("/Add")]
        public IActionResult AddJob()
        {
            List<Skill> skills = context.Skills.ToList();
            List<Employer> employers = context.Employers.ToList();

            AddJobViewModel jobs = new AddJobViewModel(employers, skills);
            return View(jobs);
        }

        public IActionResult ProcessAddJobForm(AddJobViewModel addJobViewModel, string[] selectedSkills )
        {
            if (ModelState.IsValid)
            {
                Job newJob = new Job
                {
                    EmployerId = addJobViewModel.Employer_Id,
                    Name = addJobViewModel.Name,
                };
                foreach(string skill in selectedSkills )
                {
                    JobSkill newJobSkill = new JobSkill
                    {
                        JobId = newJob.Id,
                        Job = newJob,

                        SkillId = int.Parse(skill),
                        Skill = context.Skills.Find(int.Parse(skill)),
                    };

                    context.JobSkills.Add(newJobSkill);
                };
                context.Jobs.Add(newJob);
                context.SaveChanges();
                return Redirect("/");
            }

            addJobViewModel.Skills = context.Skills.ToList();
            addJobViewModel.Employers = context.Employers.ToList();

            return View("AddJob", addJobViewModel);
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
