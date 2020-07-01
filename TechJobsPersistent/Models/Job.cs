using System;
namespace TechJobsPersistent.Models
{
    public class Job
    {

        // id, name, employer, skills
        public int Id { get; set; }
        public string Name { get; set; }
        public string Employer { get; set; }
        public string Skills { get; set; }

        public Job()
        {
        }

        public Job(string name, string employer, string skills)
        {
            Name = name;
            Employer = employer;
            Skills = skills;
        }
    }
}
