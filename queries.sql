--Part 1
Jobs.EmployerId = int | Jobs.Id = int | Jobs.Name = longtext
--Part 2
SELECT Name FROM Employers WHERE Location = "St. Louis City";
--Part 3
SELECT * FROM Skills Left JOIN JobSkills ON Skills.ID = JobSkills.SkillId
WHERE JobSkills.JobId IS NOT NULL 
ORDER BY name DESC;
