using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace _UT
{
    [TestClass]
    public class TeamMOQTest
    {
        [TestMethod]
        public void TestContainsEmployee()
        {
            
            // Create employee
            Mock<Employee> employeeMock1 = new();
            employeeMock1.SetupGet(e => e.Name).Returns("John");

            // Create another employee
            Mock<Employee> employeeMock2 = new();
            employeeMock2.SetupGet(e => e.Name).Returns("Alice");

            // Create another employee which isn't on team
            Mock<Employee> employeeMock3 = new();
            employeeMock3.SetupGet(e => e.Name).Returns("Bert");

            // Create a team with the mocked employees
            Team team = new Team("Test Team", new[] { employeeMock1.Object});

            // Check if employee is inside
            bool containsJohn = team.containsEmployee(employeeMock1.Object);
            Assert.IsTrue(containsJohn);


            team.addEmployee(employeeMock2.Object);
            bool containsAlice = team.containsEmployee(employeeMock2.Object);
            Assert.IsTrue(containsAlice);


            bool containsBert = team.containsEmployee(employeeMock3.Object);
            Assert.IsFalse(containsBert);

        }

        
        [TestMethod]
        public void TestAddEmployee()
        {
            
            Team team = new Team("Team 1");
            Mock<Employee> employeeMock = new();

            // Add our employee
            team.addEmployee(employeeMock.Object);

            // Check if he was actually added properly
            Assert.IsTrue(team.Employees.Length == 1);
            Assert.IsTrue(team.Employees[0] == employeeMock.Object);

        }

        [TestMethod]
        public void TestRemoveEmployee()
        {
            
            // Create two employees
            Mock<Employee> employeeMock1 = new();
            employeeMock1.SetupGet(e => e.Name).Returns("John");

            Mock<Employee> employeeMock2 = new();
            employeeMock2.SetupGet(e => e.Name).Returns("Alice");

            // Add them to the team
            Team team = new Team("Team 1", new[] { employeeMock1.Object, employeeMock2.Object });

            // Remove John from the team
            team.removeEmployee("John");

            // Make sure he was properly removed from team
            Assert.IsTrue(team.Employees.Length == 1);
            Assert.IsTrue(team.Employees[0].Name == "Alice");

        }

        [TestMethod]
        public void TestEstimateTask()
        {

            // Create a task
            Mock<Task> taskMock = new();
            taskMock.SetupGet(t => t.RunTime).Returns(10.0f);
            
            // Create a employees
            Mock<Employee> employeeMock1 = new();
            employeeMock1.Setup(e => e.estimateTask(taskMock.Object)).Returns(8.0f);

            Mock<Employee> employeeMock2 = new();
            employeeMock2.Setup(e => e.estimateTask(taskMock.Object)).Returns(6.0f);

            // Add employees to a team
            Team team = new Team("Team 1", new[] { employeeMock1.Object, employeeMock2.Object });

            // Check them doing a task
            float estimatedTime = team.estimateTask(taskMock.Object);

            // Make sure our time aligns with what we intend.
            Assert.AreEqual(7.0f, estimatedTime);
        }


        [TestMethod]
        public void TestInitialization()
        {

            // No mock, just checking team states.
            Team teamHandler = new Team("Team 10");

            Assert.IsTrue(teamHandler.Name == "Team 10");
            Assert.IsTrue(teamHandler.Employees.Length == 0);

            // No mock, just checking team states.
            teamHandler = new Team("Team 15", new Employee[1] { new Employee("Samuel")});

            Assert.IsTrue(teamHandler.Name == "Team 15");
            Assert.IsTrue(teamHandler.Employees.Length == 1);


        }


        [TestMethod]
        public void testMOQCopy()
        {

            // Create two employees
            Mock<Employee> employeeMock1 = new();
            employeeMock1.SetupGet(e => e.Name).Returns("John");

            Mock<Employee> employeeMock2 = new();
            employeeMock2.SetupGet(e => e.Name).Returns("Alice");


            // Add them to the team
            Team mainTeam = new Team("Team 1", new[] { employeeMock1.Object, employeeMock2.Object }); 
            Team copyTeam = new Team(mainTeam);

            Assert.IsFalse(copyTeam.Equals(mainTeam));
            Assert.IsTrue(copyTeam.Name == mainTeam.Name);

            for (int i = 0; i < 2; i++)
            {

                // Assert we aren't shallow-copying
                Assert.IsFalse(copyTeam.Employees[i].Equals(mainTeam.Employees[i]));

            }



        }


    }
        
}
