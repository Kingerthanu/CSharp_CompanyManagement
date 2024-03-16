/*
	Purpose:
		- Test will look to make sure employees are not being cleaned up by Teams when removed, added or copied then removed.
		We want to make sure clean-up of Employees is independent of our teams.
*/
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace UT_
{

    [TestClass]
    public class DemoIntegrationTest
    {

        [TestMethod]
        public void testDemo()
        {

            // Founding owners
            Employee[] companyOwners = new Employee[3]
            {

                new Employee("Jimmy"),
                new Employee("John"),
                new Employee("Grimpo")

            };


            // Owners start in company initially
            Company theCompany = new Company("MANNCO", companyOwners);

            // Get new hires in container before adding to company so can do checks on company
            Employee[] companyEmployees = new Employee[9]
            {

                // Team One
                new Employee("Tim"),
                new Employee("Lrim"),
                new Employee("Plirm"),

                // Team Two
                new Employee("Dlurm", new Skill[1] { new Skill("Skill Linear", "Spicy Hazlenut", false, 10) }),
                new Employee("Urm", new Skill[1] { new Skill("Skill Linear", "Spicy Hazlenut", false, 10) }),
                new Employee("Mur", new Skill[1] { new Skill("Skill Linear", "Spicy Hazlenut", false, 10) }),

                // Team Three
                new Employee("Glirm"),
                new Employee("Rurm"),
                new Employee("Germ")

            };


            // Add them all on first team
            theCompany.addEmployee(companyEmployees[0]);
            theCompany.addEmployee(companyEmployees[1]);
            theCompany.addEmployee(companyEmployees[2]);

            // Add them to a team
            theCompany.createTeam("Team 10", new Employee[3] { companyEmployees[0], companyEmployees[1], companyEmployees[2] });


            // Add them all on second team
            theCompany.addEmployee(companyEmployees[3]);
            theCompany.addEmployee(companyEmployees[4]);
            theCompany.addEmployee(companyEmployees[5]);

            // Add them to a team
            theCompany.createTeam("Team 11", new Employee[3] { companyEmployees[3], companyEmployees[4], companyEmployees[5] });

            // Add them all on third team
            theCompany.addEmployee(companyEmployees[6]);
            theCompany.addEmployee(companyEmployees[7]);
            theCompany.addEmployee(companyEmployees[8]);

            // Add them to a team
            theCompany.createTeam("Team 12", new Employee[3] { companyEmployees[7], companyEmployees[6], companyEmployees[8] });

            // Default passed interface override of curve is linear.
            Task linearTask = new Task("Task Linear", "Wet Fish", 60.0f, new Skill[1] { new Skill("Skill Linear", "Spicy Hazlenut", true, 10) });

            // Augment linear curve to half its original value.
            Task augmentedTask = new Task("Task Augmented", "Wet Fish", 60.0f, new Skill[1] { new Skill("Skill Augmented", "Spicy Hazlenut", true, 10, new AdjustedDifficultyCurve(0.5f)) });


            // Give a linear difficulty task to two teams; one with people of proficiency, and people of none. People with prof. should have a lower estimate.
            Assert.IsTrue(theCompany.Teams[0].estimateTask(linearTask) > theCompany.Teams[1].estimateTask(linearTask));

            // Give task to two differing teams of differing employees but same proficiencies so should be equal estimate.
            Assert.IsTrue(theCompany.Teams[0].estimateTask(linearTask) == theCompany.Teams[2].estimateTask(linearTask));

            // Adjusted Curve will have 0.5f multiple so should half estimate given it should give same estimate as linear but lower.
            Assert.IsTrue(theCompany.Teams[0].estimateTask(linearTask) > theCompany.Teams[0].estimateTask(augmentedTask));


            // Create randomly seeded curve.
            Task randomTask = new Task("Task Augmented", "Wet Fish", 60.0f, new Skill[1] { new Skill("Skill Augmented", "Spicy Hazlenut", true, 10, new RandomizedDifficultyCurve()) });

            // Adjusted Curve will have 0.5f multiple and random is 2 -> 10 so should always be true if random is pulling correctly.
            Assert.IsTrue(theCompany.Teams[0].estimateTask(randomTask) > theCompany.Teams[0].estimateTask(augmentedTask));


            // Disband employees
            for (int i = 0; i < 9; i++)
            {

                theCompany.removeEmployee(companyEmployees[i]);

            }

            // Make sure are removed from their associated teams as well.
            Assert.IsTrue(theCompany.Teams[0].Employees.Length == 0);
            Assert.IsTrue(theCompany.Teams[1].Employees.Length == 0);
            Assert.IsTrue(theCompany.Teams[2].Employees.Length == 0);

            // Disband teams.
            theCompany.deleteTeam(theCompany.Teams[0]);
            theCompany.deleteTeam(theCompany.Teams[0]);
            theCompany.deleteTeam(theCompany.Teams[0]);

            Assert.IsTrue(theCompany.Teams.Length == 0);

        }


    }
}


