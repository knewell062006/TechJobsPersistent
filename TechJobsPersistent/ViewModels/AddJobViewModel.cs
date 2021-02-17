using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TechJobsPersistent.Models;

namespace TechJobsPersistent.ViewModels
{
    public class AddJobViewModel 
    {
        public int JobId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 & 50 characters.")]
        public string Name { get; set; }
        public int Employer_Id { get; set; }
        public string EmployerName { get; set; }
        public int Skill_Id { get; set; }
        public List<Skill> Skills { get; set; }
        public List<Employer> Employers { get; set; }
        public AddJobViewModel(List<Employer> employers, List<Skill> possiblSkills)
        {
            Skills = possiblSkills;
            Employers = employers;
            




            }
        public AddJobViewModel()
        {

        }
        }
    }

