/*
	Purpose:
		- Test will look to make sure employees are not being cleaned up by Teams when removed, added or copied then removed.
		We want to make sure clean-up of Employees is independent of our teams.
*/
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace UT_
{

    [TestClass]
    public class TeamTest
    {

        [TestMethod]
        public void testTeamAddingAndRemoving()
        {

            // Get team members
            Employee[] teamEmployees = new Employee[100];
            for (uint i = 0; i < 100; i++)
            {

                teamEmployees[i] = new Employee("Employee " + i);

            }


            // Teams are not liable.
            Team teamHandler = new Team("Team 10", teamEmployees);
            Team secondTeamHandler = new Team(teamHandler);

            // Free all our employees
            for (uint i = 0; i < 100; i++)
            {

                teamHandler.removeEmployee("Employee " + i);

            }

            Assert.IsFalse(teamHandler.Employees.Length != 0);
            

            // Re-add all employees.
            for (uint i = 0; i < 100; i++)
            {

                teamHandler.addEmployee(teamEmployees[i]);

            }

            // Override employees with secondTeam's
            teamHandler = secondTeamHandler;

            Assert.IsFalse(teamHandler.Employees.Length != 100);
            

            // Make sure all employees are still here.
            for (uint i = 0; i < 100; i++)
            {

                Assert.IsFalse(teamEmployees[i].Name != ("Employee " + i));
                

            }

        }

        [TestMethod]
        /*
            Purpose:
                - Test will make sure that the time estimates for a given task when done by a team where theres no skills is lower with more teammates
                compared to doing it with a single employee (try showing divide-and-conquering of tasks within a team.
        */
        public void testTeamEstimatesNoSkill()
        {

            // Get team members
            Employee[] teamEmployeesMultiple = new Employee[100];
            Employee[] teamEmployeesSingle = new Employee[1] { new Employee("Employee " + '0') };
            for (uint i = 0; i < 100; i++)
            {

                teamEmployeesMultiple[i] = new Employee("Employee " + i);

            }

            // Set teams with employees.
            Team teamHandlerMultiple = new Team("Team 10", teamEmployeesMultiple);
            Team teamHandlerSingle = new Team("Team 10", teamEmployeesSingle);

            Task baseTimeTask = new Task("Base Time Task", "Seasoned Beef", 60.0f);

            // Get estimates with multiple vs with single teammate.
            float multipleEstimate = teamHandlerMultiple.estimateTask(baseTimeTask);
            float singleEstimate = teamHandlerSingle.estimateTask(baseTimeTask);

            Assert.IsFalse(singleEstimate != 60.0f);

            Assert.IsFalse(multipleEstimate >= 60.0f);

        }

        [TestMethod]
        /*
            Purpose:
                - Test will make sure that the time estimates for a given task when done by a team where theres skills vs no skills upholds the invariant
                that a skilled employee(s) will complete a task faster than a team of non-skilled employee(s).
        */
        public void testTeamEstimatesSkill()
        {

            // Get team members without skills
            Employee[] teamEmployeesMultipleNoSkill = new Employee[100];
            Employee[] teamEmployeesSingleNoSkill = new Employee[1] { new Employee("Employee " + '0') };
            for (uint i = 0; i < 100; i++)
            {

                teamEmployeesMultipleNoSkill[i] = new Employee("Employee " + i);

            }


            // Get team members with skills
            Employee[] teamEmployeesMultipleSkill = new Employee[100];
            Employee[] teamEmployeesSingleSkill = new Employee[1] { new Employee("Employee " + ('0'), new Skill[2] { new Skill("Skill One", "Poog", false, 2), new Skill("Skill Two", "Pramg", false, 5) }) };
            for (uint i = 0; i < 100; i++)
            {

                teamEmployeesMultipleSkill[i] = new Employee("Employee " + i, new Skill[2] { new Skill("Skill One", "Poog", false, 2), new Skill("Skill Two", "Pramg", false, 5) });

            }


            // Teams with employees of no skill
            Team teamHandlerMultipleNoSkill = new Team("Team 10", teamEmployeesMultipleNoSkill);
            Team teamHandlerSingleNoSkill = new Team("Team 10", teamEmployeesSingleNoSkill);

            // Teams with employees of skill
            Team teamHandlerMultipleSkill = new Team("Team 10", teamEmployeesMultipleSkill);
            Team teamHandlerSingleSkill = new Team("Team 10", teamEmployeesSingleSkill);

            // Task both teams will do with skills
            Task baseTimeTask = new Task("Base Time Task", "Seasoned Beef", 60.0f, new Skill[2] { new Skill("Skill One", "Banana", true, 1), new Skill("Skill Two", "Walnut", true, 3) });

            // Get estimates without skills
            float multipleEstimateNoSkill = teamHandlerMultipleNoSkill.estimateTask(baseTimeTask);
            float singleEstimateNoSkill = teamHandlerSingleNoSkill.estimateTask(baseTimeTask);

            // Get estimates with skills
            float multipleEstimateSkill = teamHandlerMultipleSkill.estimateTask(baseTimeTask);
            float singleEstimateSkill = teamHandlerSingleSkill.estimateTask(baseTimeTask);


            Assert.IsFalse((singleEstimateNoSkill <= 60.0f));

            Assert.IsFalse((multipleEstimateNoSkill > 60.0f));


            Assert.IsFalse((singleEstimateSkill > singleEstimateNoSkill));

            Assert.IsFalse((multipleEstimateSkill > multipleEstimateNoSkill));
         

        }

        [TestMethod]
        public void testCopy()
        {

            Team mainTeam = new Team("Team 10", new Employee[2] { new Employee("John"), new Employee("Greg", new Skill[2] { new Skill("Apple Curding", "Shaq", false), new Skill("Terb", "Qras", false) })});
            Team copyTeam = new Team(mainTeam);

            Assert.IsFalse(copyTeam.Equals(mainTeam));
            Assert.IsTrue(copyTeam.Name == mainTeam.Name);

            for(int i = 0; i < 2; i++)
            {
                // Assert we aren't shallow-copying
                Assert.IsFalse(copyTeam.Employees[i].Equals(mainTeam.Employees[i]));

            }



        }

        [TestMethod]
        /*
            Purpose:
                - Test will try addition and removal of employees from the company as well as their respective team. First trying reoccuring employees on many teams,
                and secondly many unique employees on many teams. We check for proper leak cleanup as well.
        */
        public void testCompanyHireAndFire()
        {

            // Start empty, fill with employees
            Company companyHandler = new Company("MANNCO");

            // Only allocate for 5 employees.
            for (uint i = 0; i < 5; i++)
            {
                Employee employeeHandler = new Employee("Employee " + i);
                companyHandler.addEmployee(employeeHandler);

            }

            // Make 1000 teams in which use 0-4 employees.
            for (uint i = 0; i < 1000; i++)
            {
                Employee[] employeeHandler = new Employee[5] { companyHandler.getEmployee("Employee " + 0), companyHandler.getEmployee("Employee " + 1), companyHandler.getEmployee("Employee " + 2),
            companyHandler.getEmployee("Employee " + 3), companyHandler.getEmployee("Employee " + 4) };

                companyHandler.createTeam("Team " + i , employeeHandler);

            }

            // Backward removal
            for (int i = 4; i != -1; i--)
            {

                companyHandler.removeEmployee(companyHandler.getEmployee("Employee " + i));

                for (uint j = 0; j < 1000; j++)
                {

                    // Should decrease by one per outer-loop increment.
                    Assert.IsFalse((companyHandler.getTeam("Team " + j).Employees.Length != (4 - (4 - i))));
                    

                }
            }


            // Make 1000 employees for teams.
            for (uint i = 0; i < 1000; i++)
            {
                Employee employeeHandler = new Employee("Employee " + i);
                companyHandler.addEmployee(employeeHandler);

            }

            // Make 200 teams of 5 unique employees.
            for (uint i = 0, teamIndex = 0; i < 1000;)
            {
                Employee[] employeeHandler = new Employee[5] { companyHandler.getEmployee("Employee " + i++), companyHandler.getEmployee("Employee " + i++), companyHandler.getEmployee("Employee " + i++),
            companyHandler.getEmployee("Employee " + i++), companyHandler.getEmployee("Employee " + i++) };
                companyHandler.createTeam("Team " + teamIndex++, employeeHandler);


            }

            // Remove everyone from team and check if all removed.
            for (uint i = 0; i < 1000;)
            {

                // Forward removal
                companyHandler.removeEmployee(companyHandler.getEmployee("Employee " + i++));
                companyHandler.removeEmployee(companyHandler.getEmployee("Employee " + i++));
                companyHandler.removeEmployee(companyHandler.getEmployee("Employee " + i++));
                companyHandler.removeEmployee(companyHandler.getEmployee("Employee " + i++));
                companyHandler.removeEmployee(companyHandler.getEmployee("Employee " + i++));

                for (uint j = 0; j < 200; j++)
                {

                    Assert.IsFalse((companyHandler.getTeam("Team " + j).Employees.Length != 0));
                    

                }
            }


        }

        [TestMethod]
        /*
            Purpose:
                - Test will check proper handling of empty teams with removal and retrieval.
        */
        public void testCompanyFindAndRemoveEmptyTeams()
        {

            Company companyHandler = new Company("MANNCO");


            // Make 1000 teams in which use 0 employees.
            for (uint i = 0; i < 1000; i++)
            {

                Assert.IsFalse((i != companyHandler.Teams.Length));
               

                companyHandler.createTeam("Team " + i);

            }

            // Remove all 1000 teams.
            for (uint i = 0; i < 1000; i++)
            {

                companyHandler.deleteTeam(companyHandler.getTeam("Team " + i));

                Assert.IsFalse((999 - i) != companyHandler.Teams.Length);

            }



        }


    }
}

