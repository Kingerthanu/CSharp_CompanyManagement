using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace UT_
{

    [TestClass]
    public class TaskSkillTest
    {

        [TestMethod]
        public void testInitialization()
        {

            // Should initialize skill name and difficulty in constructor to arguments
            Skill tstSkill = new Skill("Skill One", "Drunchle", true, 534);

            Assert.IsTrue(tstSkill.Name == "Skill One");
            Assert.IsTrue(tstSkill.Description == "Drunchle");
            Assert.IsTrue(tstSkill.ID == true);
            Assert.IsTrue(tstSkill.Level == 534);

        }

        [TestMethod]
        public void testNoDifficultyInitialization()
        {

            // Should initialize difficulty to 0 in constructor using default value.
            Skill tstSkill = new Skill("Skill One", "Aeple", true);

            Assert.IsTrue(tstSkill.Name == "Skill One");
            Assert.IsTrue(tstSkill.Description == "Aeple");
            Assert.IsTrue(tstSkill.ID == true);
            Assert.IsTrue(tstSkill.Level == 0);

        }

    }

    [TestClass]
    public class TaskTest
    {

        [TestMethod]
        public void testInitializationArguments()
        {

            // Initialize task with one skill (skill one)
            Task tstTask = new Task("Task One", "Apple Bees", 60.0f, new Skill[1] { new Skill("Skill One", "Aerue", true, 55) });

            // Check task members
            Assert.IsTrue(tstTask.Name == "Task One");
            Assert.IsTrue(tstTask.Description == "Apple Bees");
            Assert.IsTrue(tstTask.RunTime == 60.0f);
            Assert.IsFalse(tstTask.Done);

            // Check skillset member values (should contain given skills passed from constructor)
            Assert.IsTrue(tstTask.SkillSet.Length == 1);
            Assert.IsTrue(tstTask.SkillSet[0].Name == "Skill One");
            Assert.IsTrue(tstTask.SkillSet[0].Level == 55);


            // Initilize new task with two skills (skill two, skill three)
            tstTask = new Task("Task Two", "Apple Bees", 30.0f, new Skill[2] { new Skill("Skill Two", "Turple", true, 4), new Skill("Skill Three", "Durkle", true, 12) });

            // Check task members
            Assert.IsTrue(tstTask.Name == "Task Two");
            Assert.IsTrue(tstTask.Description == "Apple Bees");
            Assert.IsTrue(tstTask.RunTime == 30.0f);
            Assert.IsFalse(tstTask.Done);

            // Check skillset member values (should contain given skills passed from constructor)
            Assert.IsTrue(tstTask.SkillSet.Length == 2);
            Assert.IsTrue(tstTask.SkillSet[0].Name == "Skill Two");
            Assert.IsTrue(tstTask.SkillSet[0].Level == 4);
            Assert.IsTrue(tstTask.SkillSet[1].Name == "Skill Three");
            Assert.IsTrue(tstTask.SkillSet[1].Level == 12);


        }


        [TestMethod]
        public void testCopy()
        {
            Task taskOne = new Task("Task one", "Wet Sandals", 60.0f, new Skill[2] { new Skill("Apple Curding", "Shaq", false), new Skill("Terb", "Qras", false) });


            Task taskTwo = new Task(taskOne);


            Assert.IsTrue(taskTwo.Name == "Task one");
            Assert.IsTrue(taskTwo.SkillSet.Length == 2);

            for (uint i = 0; i < taskTwo.SkillSet.Length; i++)
            {
                // Assert we aren't shallow-copying
                Assert.IsTrue(taskTwo.SkillSet[i].Name == taskOne.SkillSet[i].Name);
                Assert.IsFalse(taskOne.SkillSet[i].Equals(taskTwo.SkillSet[i]));

            }

            Assert.IsTrue(!taskOne.Equals(taskTwo));

        }


        [TestMethod]
        public void testSetters()
        {

            // Create task with just name, description, and runtime in minutes.
            Task tstTask = new Task("Task One", "Apple Bees", 5.0f);
            Assert.IsTrue(tstTask.Description == "Apple Bees");

            // Check if can set
            tstTask.Description = "Trader Joes";
            Assert.IsTrue(tstTask.Description == "Trader Joes");

        }

        [TestMethod]
        public void testActivation()
        {

            // Task should initialize with a done state of false.
            Task tstTask = new Task("Task One", "Apple Bees", 60.0f);
            Assert.IsFalse(tstTask.Done);

            // Mark done should set to true
            tstTask.markDone();
            Assert.IsTrue(tstTask.Done);

            // Mark done should stay true
            tstTask.markDone();
            Assert.IsTrue(tstTask.Done);


        }

        [TestMethod]
        public void testInitializationNoSkill()
        {

            // Task should initialize with a empty skillSet if not specified.
            Task tstTask = new Task("Task One", "Apple Bees", 60.0f);

            Assert.IsTrue(tstTask.Name == "Task One");
            Assert.IsTrue(tstTask.Description == "Apple Bees");
            Assert.IsTrue(tstTask.RunTime == 60.0f);
            Assert.IsFalse(tstTask.Done);

            Assert.IsTrue(tstTask.SkillSet.Length == 0);


        }

    }
}