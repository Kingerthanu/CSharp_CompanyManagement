using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace UT_
{

    [TestClass]
    public class CompanyTest
    {


        [TestMethod]
        /*
	        Purpose:
		        - Test tries checking to make sure that rehiring a employee will still keep the same estimate as before hiring without skills.
        */
        public void testCompanyEstimateWithRehires()
        {

            // Allocate 5 non-skills employees to give to new company
            Employee[] currentEmployees = new Employee[5] { new Employee("Employee 1"), new Employee("Employee 2"),
            new Employee("Employee 3"), new Employee("Employee 4"), new Employee("Employee 5") };

            // Create company with employees
            Company companyHandler = new Company("MANNCO", currentEmployees);

            Assert.IsTrue(companyHandler.Employees.Length == 5);

            // Create non-skilled task todo (basically neutral skill-level so can infer same invariants with skills).
            Task taskHandler = new Task("Task One", "Wet Towel", 60.0f);

            // Create team with all current company employees
            companyHandler.createTeam("Team 10", currentEmployees);

            Assert.IsTrue(companyHandler.Teams.Length == 1);
            Assert.IsTrue(companyHandler.Teams[0].Employees.Length == 5);
            Assert.IsTrue(companyHandler.Teams[0].Name == "Team 10");

            // Grab estimate of our task with team before removing employee
            float estimateBeforeFired = companyHandler.getTeam("Team 10").estimateTask(taskHandler);

            // Copy over to-be-removed employee for re-adding when de-alllocated by company with removal
            Employee employeeOneCopy = (currentEmployees[2]);
            companyHandler.removeEmployee(currentEmployees[2]);

            Assert.IsTrue(companyHandler.Teams[0].Employees.Length == 4);
            Assert.IsTrue(companyHandler.Employees.Length == 4);

            // Re-add employee after removal
            companyHandler.addEmployee(employeeOneCopy);
            companyHandler.getTeam("Team 10").addEmployee(employeeOneCopy);

            Assert.IsTrue(companyHandler.Employees.Length == 5);
            Assert.IsTrue(companyHandler.Teams[0].Employees.Length == 5);

            // Grab estimate of our task with team after removing employee but re-adding
            float estimateAfterFired = companyHandler.getTeam("Team 10").estimateTask(taskHandler);

            Assert.IsTrue(estimateAfterFired == estimateBeforeFired);

        }


        [TestMethod]
        public void testCopy()
        {
            // Should set name and skills to passed values (only one skill given) for employee instance
            Company companyOne = new Company("Rumpus LLC", new Employee[4] {  new Employee("John"), new Employee("Greg", new Skill[2] { new Skill("Apple Curding", "Shaq", false), new Skill("Terb", "Qras", false) }), new Employee("Prexton"), new Employee("Praptus") }, new Team[1] { new Team("Task one") });

            companyOne.createTeam("Ich", companyOne.Employees);

            Company companyTwo = new Company(companyOne);


            Assert.IsTrue(companyTwo.Name == "Rumpus LLC");
            Assert.IsTrue(companyTwo.Employees.Length == 4);

            for(uint i = 0; i < companyTwo.Employees.Length; i++)
            {
                // Assert we aren't shallow-copying
                Assert.IsTrue(companyTwo.Employees[i].Name == companyOne.Employees[i].Name);
                Assert.IsFalse(companyOne.Employees[i].Equals(companyTwo.Employees[i]));

            }


            for (uint i = 0; i < companyTwo.Teams.Length; i++)
            {

                // Assert we aren't shallow-copying
                Assert.IsTrue(companyTwo.Teams[i].Name == companyOne.Teams[i].Name);
                Assert.IsFalse(companyOne.Teams[i].Equals(companyTwo.Teams[i]));

            }

            Assert.IsTrue(companyTwo.Teams.Length == 2);
            Assert.IsTrue(!companyOne.Equals(companyTwo));

        }


        [TestMethod]
        /*
            Purpose:
                - Test will create teams and randomly assign a employee to a given random team.
                We want to make sure that teams are being cleaned up properly under random assignment of unique employees. The removal of employees
                during checks in teams is to see if reallocation and reordering of non-deleted elements is corrupt.
        */
        public void testCompanyRetrievals()
        {

            Company companyHandler = new Company("MANNCO");
            Employee[] employeeBin = new Employee[100];
            Task taskHandler = new Task("Task one", "Salmonella", 60.0f);

            // Make 1000 teams in which use 0 employees.
            for (uint i = 0; i < 100; i++)
            {

                companyHandler.createTeam("Team " + i);

            }


            Random gen = new Random();


            // Add 1000 employees randomly to a team.
            for (uint i = 0; i < 100; i++)
            {

                employeeBin[i] = new Employee(" " + i);
                companyHandler.addEmployee(employeeBin[i]);

                companyHandler.getTeam("Team " + gen.Next(0, 99)).addEmployee(employeeBin[i]);

            }

            bool foundFlag = false;
            float prevRuntime = 0.0f;

            for (uint i = 0; i < 100; i++, foundFlag = false)
            { // For each employee...

                for (uint j = 0; j < 100; j++)
                { // For each team...

                    // If team contains employee, remove from team and set found flag as true
                    if (companyHandler.getTeam("Team " + j).containsEmployee(employeeBin[i]))
                    {

                        prevRuntime = companyHandler.getTeam("Team " + j).estimateTask(taskHandler);

                        companyHandler.getTeam("Team " + j).removeEmployee(companyHandler.getEmployee(employeeBin[i].Name).Name);

                        Assert.IsFalse((companyHandler.getTeam("Team " + j).Employees.Length != 0) && (prevRuntime > companyHandler.getTeam("Team " + j).estimateTask(taskHandler)));

                        foundFlag = true;
                        break;

                    }

                }

                Assert.IsTrue(foundFlag);

            }

            
        }


        [TestMethod]
        /*
            Purpose:
                - Test will make sure that even when teams are dissolved and have associated employees that the employees are still in the company and aren't
                tampered with through team clean-up.
        */
        public void testCompanyTeamDestruction()
        {

            Company companyHandler = new Company("MANNCO");
            Employee[] employeeBin = new Employee[100];


            // Make 1000 teams in which use 0 employees.
            for (uint i = 0; i < 100; i++)
            {

                Assert.IsTrue(companyHandler.Teams.Length == i);
                companyHandler.createTeam("Team " + i);
                

            }


            Random gen = new Random();

            // Add 1000 employees randomly to a team.
            for (uint i = 0; i < 100; i++)
            {

                Assert.IsTrue(companyHandler.Employees.Length == i);

                employeeBin[i] = new Employee(" " + i);
                companyHandler.addEmployee(employeeBin[i]);

                companyHandler.getTeam("Team " + gen.Next(0,99)).addEmployee(employeeBin[i]);



            }


            // Delete all teams..
            for (uint i = 0; i < 100; i++)
            {

                Assert.IsTrue(companyHandler.Teams.Length == (100 - i));
                companyHandler.deleteTeam(companyHandler.getTeam("Team " + i));


            }

            // Make sure employees are still in our company even throughout team dissolvement.
            for (uint i = 0; i < 100; i++)
            {

                // Should not throw error as assert.
                companyHandler.getEmployee(" " + i);

            }


        }


    }

}