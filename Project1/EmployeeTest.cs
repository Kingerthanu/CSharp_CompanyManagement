using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace UT_
{
    [TestClass]
    public class EmployeeSkillTest
    {

        [TestMethod]
        public void testInitialization()
        {
            // Should initialize both skill name and skill proficiency 53
            Skill tstSkill = new Skill("Skill One", "Pringus", false, 53);

            // Should be able to get each one as constructor sets
            Assert.IsTrue(tstSkill.Name == "Skill One");
            Assert.IsTrue(tstSkill.Description == "Pringus");
            Assert.IsTrue(tstSkill.ID == false);
            Assert.IsTrue(tstSkill.Level == 53);

        }

        [TestMethod]
        public void testNoProf()
        {

            // Should initialize skill proficiency to 0 for default value
            Skill tstSkill = new Skill("Skill One", "Pringus", false);

            Assert.IsTrue(tstSkill.Name == "Skill One");
            Assert.IsTrue(tstSkill.Description == "Pringus");
            Assert.IsTrue(tstSkill.ID == false);
            Assert.IsTrue(tstSkill.Level == 0);

        }

        [TestMethod]
        public void testSetProf()
        {

            // Should initialize proficiency of 53 in constructor.
            Skill tstSkill = new Skill("Skill One", "Pringus", false, 53);
            Assert.IsTrue(tstSkill.Level == 53);

            // Should assign proficiency to 12.
            tstSkill.Level = 12;
            Assert.IsTrue(tstSkill.Level== 12);

        }

    }

    [TestClass]
    public class EmployeeTest
    {


    


        [TestMethod]
        public void testCopy()
        {
            Employee employeeOne = new Employee("Samson", new Skill[5] {  new Skill("Prapple", "Luke Warm Water", false), new Skill("Krampus", "Cold Beef", false),
            new Skill("Tingus", "Disassociating", false), new Skill("Bingus", "Edible Peanut Butter", false), new Skill("Fred Durst", "Disassociating", false)});

            Employee employeeTwo = new Employee(employeeOne);

            Assert.IsTrue(employeeTwo.Name == "Samson");
            Assert.IsTrue(employeeTwo.SkillSet.Length == 5);

            for(uint i = 0; i < employeeTwo.SkillSet.Length; i++)
            {

                Assert.IsTrue(employeeTwo.SkillSet[i].Name == employeeOne.SkillSet[i].Name);
                Assert.IsTrue(employeeTwo.SkillSet[i].Level == employeeOne.SkillSet[i].Level);
                Assert.IsTrue(!employeeTwo.SkillSet[i].Equals(employeeOne.SkillSet[i]));

            }

        }

        [TestMethod]
        public void testInitializingArguments()
        {
            // Should set name and skills to passed values (only one skill given) for employee instance
            Employee tstEmployee = new Employee("Hubert", new Skill[1] { new Skill("Skill One", "Peurmpus", false, 10) });

            Assert.IsTrue(tstEmployee.Name == "Hubert");
            Assert.IsTrue(tstEmployee.SkillSet.Length == 1);

        }

        [TestMethod]
        public void testInitializingNoSkill()
        {

            // Should set name and skills to zero for employee instance
            Employee tstEmployee = new Employee("Hubert");

            Assert.IsTrue(tstEmployee.Name == "Hubert");
            Assert.IsTrue(tstEmployee.SkillSet.Length == 0);

        }

        [TestMethod]
        public void testWithBaseTime()
        {

            // Employee starts with no hours of work done
            Employee newEmployee = new Employee("Weezer");
            Assert.IsTrue(newEmployee.HoursOfTasks == 0.0f);

            // Employee does a hour long task (60 minutes = +1 hour)
            newEmployee.doTask(new Task("Zero difficulty BaseTime0", "Apple Bees", 60.0f));
            Assert.IsTrue(newEmployee.HoursOfTasks == 1.00f);

            // Employee does a additional quarter-hour task (15 minutes = +0.25 hour)
            newEmployee.doTask(new Task("Zero difficulty BaseTime1", "Apple Bees", 15.0f));
            Assert.IsTrue(newEmployee.HoursOfTasks == 1.25f);

            // Employee does a additional quarter-hour task (15 minutes = +0.25 hour)
            newEmployee.doTask(new Task("Zero difficulty BaseTime2", "Apple Bees", 15.0f));
            Assert.IsTrue(newEmployee.HoursOfTasks == 1.50f);

            // Check if estimates return correct runtime in minutes
            Assert.IsTrue(newEmployee.estimateTask(new Task("Zero difficulty BaseTime1", "Apple Bees", 0.0f)) == 0.0f);
            Assert.IsTrue(newEmployee.estimateTask(new Task("Zero difficulty BaseTime2", "Apple Bees", 15.0f)) == 15.0f);
            Assert.IsTrue(newEmployee.estimateTask(new Task("Zero difficulty BaseTime3", "Apple Bees", 45.0f)) == 45.0f);
            Assert.IsTrue(newEmployee.estimateTask(new Task("Zero difficulty BaseTime4", "Apple Bees", 120.0f)) == 120.0f);

            // Check if negative minutes worktime defaults to a estimate of 0.0f (even though Tasks.runTime is = -1000 our estimate function will clamp to 0.0f [as returns only _ >= 0])
            Assert.IsTrue(newEmployee.estimateTask(new Task("Zero difficulty BaseTime1", "Apple Bees", -1000.0f)) == 0.0f);

        }

        [TestMethod]
        public void testWithNegativeBaseTime()
        {

            // Create employee of proficiency 15 in skill one
            Employee newEmployee = new Employee("Weezer", new Skill[1] { new Skill("Skill One", "Peurmpus", false, 15) });

            // Hours should start at zero
            Assert.IsTrue(newEmployee.HoursOfTasks == 0.0f);

            // Doing a task using skill one but of a difficulty 0 (very easy) [will be -15 minutes to do according to estimateTask but will clamp to 0 adding no addition hours worked]
            newEmployee.doTask(new Task("Zero difficulty BaseTime0", "Apple Bees", 5.0f, new Skill[1] { new Skill("Skill One", "Peurmpus", false) }));

            // Hours should stay at zero; 0 (unclamped -15 minutes) runtime task done.
            Assert.IsTrue(newEmployee.HoursOfTasks == 0.0f);

        }

        [TestMethod]
        public void testTimeWithSkill()
        {

            // Create employee with knowledge of Skill one and two but zero proficiency.
            Employee newEmployee = new Employee("Weezer", new Skill[2] { new Skill("Skill 1", "Prembus", false), new Skill("Skill 2", "Prombus", false) });

            // Check just for state change when just Task 1 (because has runtime of 60 minutes with zero proficiency and 10 difficulty; should add 10 to runtime making total runtime of 70 minutes)
            newEmployee.doTask(new Task("Difficulty-10 BaseTime 1st", "Apple Bees", 60.0f, new Skill[1] { new Skill("Skill 1", "Bombus", true, 10) }));
            Assert.IsTrue(newEmployee.HoursOfTasks == 1.1666666f); //   70 / 60 = 1.166666
                                                                   // Check if rate of increase from 0 profieicney was greater than one to make sure if harder skill, youll learn more.
            Assert.IsTrue(newEmployee.SkillSet[0].Level > 1);

            // Check just for state change when just Task 2 (reset employee)
            newEmployee = new Employee("Weezer", new Skill[2] { new Skill("Skill 1", "Blarmpus", false), new Skill("Skill 2", "Flarmpus", false) });
            newEmployee.doTask(new Task("Difficulty-5 BaseTime 1st", "Apple Bees", 60.0f, new Skill[1] { new Skill("Skill 2", "Drungpus", true, 5) }));
            Assert.IsTrue(newEmployee.HoursOfTasks == 1.0833334f); //   65 / 60 = 1.0833334

            // Readd task 1 after getting info about state after just Task 2.
            newEmployee.doTask(new Task("Difficulty-10 BaseTime 1st", "Apple Bees", 60.0f, new Skill[1] { new Skill("Skill 1", "Grompus", true, 10) }));

            // Make sure rate of proficiency increase is properly balanced (lower difficulty rating based on proficiency, lower the proficiency increase)
            Assert.IsTrue(newEmployee.SkillSet[1].Level < newEmployee.SkillSet[0].Level);

            // Add addtional tasks and see if invariant holds of skills.
            newEmployee.doTask(new Task("Difficulty-10 BaseTime 2nd", "Apple Bees", 60.0f, new Skill[1] { new Skill("Skill 1", "Drungpus", true, 10) }));
            newEmployee.doTask(new Task("Difficulty-10 BaseTime 3rd", "Apple Bees", 60.0f, new Skill[1] { new Skill("Skill 1", "Drungpus", true, 10) }));

            newEmployee.doTask(new Task("Difficulty-5 BaseTime 2nd", "Apple Bees", 60.0f, new Skill[1] { new Skill("Skill 2", "Drungpus", true, 5) }));
            newEmployee.doTask(new Task("Difficulty-5 BaseTime 3rd", "Apple Bees", 60.0f, new Skill[1] { new Skill("Skill 2", "Drungpus", true, 5) }));

            // Check if still holds
            Assert.IsTrue(newEmployee.SkillSet[1].Level < newEmployee.SkillSet[0].Level);

        }

        [TestMethod]
        public void testLearningSkill()
        {

            // Make employee with zero known skills initially
            Employee newEmployee = new Employee("Weezer");
            Assert.IsTrue(newEmployee.SkillSet.Length == 0);

            // Add task with new never done skill, check if added to employee skillset
            newEmployee.doTask(new Task("Task One", "Apple Bees", 10.0f, new Skill[1] { new Skill("Skill One", "Truntis", true, 5) }));

            Assert.IsTrue(newEmployee.SkillSet.Length == 1);
            Assert.IsTrue(newEmployee.SkillSet[0].Level == 0);
            float previousTime = newEmployee.HoursOfTasks;
            uint previousProf = newEmployee.SkillSet[0].Level;
            Assert.IsTrue(previousProf == 0);

            // Add new task with same skill, check if it didn't readd a skill. Should add at least 1 proficiency if diff. between difficulty and profieicny is below 3.
            newEmployee.doTask(new Task("Task Two", "Apple Bees", 10.0f, new Skill[1] { new Skill("Skill One", "Bonkus", true) }));
            Assert.IsTrue(newEmployee.SkillSet.Length == 1);
            Assert.IsTrue(newEmployee.SkillSet[0].Level == 1);


        }

        [TestMethod]
        public void testConstructorSkill()
        {

            // Create employee with no known skills
            Employee newEmployee = new Employee("Weezer");
            Assert.IsTrue(newEmployee.SkillSet.Length == 0);

            // Do task with Skill one skill
            newEmployee.doTask(new Task("Buddy Holly", "Apple Bees", 60.0f, new Skill[1] { new Skill("Skill One", "Blurbus", true, 5) }));
            Assert.IsTrue(newEmployee.SkillSet.Length == 1);

            // Now add a task with 4 skills, one being skill one. Make sure it doesn't overrite
            newEmployee.doTask(new Task("Say it aint so", "Apple Bees", 60.0f, new Skill[4] { new Skill("Skill Two", "Bloknus", true, 5), new Skill("Skill Three", "Dromput", true, 5), new Skill("Skill One", "Grangus", true, 5), new Skill("Skill Four", "Plearus", true, 5) }));

            Assert.IsTrue(newEmployee.SkillSet.Length == 4);
            Assert.IsTrue(newEmployee.SkillSet[0].Name == "Skill One");
            Assert.IsTrue(newEmployee.SkillSet[2].Name == "Skill Three");


        }


        [TestMethod]
        public void testMakingSkill()
        {

            // Randomly add 50-150 tasks to a employee and have repeating skills (make sure skills aren't added twice to skillset)
            Random random = new Random();
            int interval = random.Next(100, 150);

            // Create employee to do tasks
            Employee newEmployee = new Employee("Weezer");

            for (int i = 0; i < interval; i++)
            {

                // Keep doing new tasks, but if skill name number exceeds 20, wrap back around in order to have reoccuring skills to avoid
                newEmployee.doTask(new Task("Task " + i, "Apple Bees", 10.0f, new Skill[1] { new Skill("Skill " + (i % 20), "Parpus", true, 5) }));

            }

            // Employee skillset should have 20 distinct skills
            Assert.IsTrue(newEmployee.SkillSet.Length == 20);

        }


        [TestMethod]
        public void testRateOfProfGain()
        {

            // Create employee with no known skills
            Employee newEmployee = new Employee("Weezer", new Skill[1] { new Skill("Skill One", "Gorpus", false)});

            // See if proficiency starts at 0 when learning new skill
            newEmployee.doTask(new Task("Task One", "Apple Bees", 10.0f, new Skill[1] { new Skill("Skill One", "Tingus", true, 5) }));
            Assert.IsTrue(newEmployee.SkillSet[0].Level != 1);

            float previousTime = newEmployee.HoursOfTasks;
            uint previousProf = newEmployee.SkillSet[0].Level;

            newEmployee.doTask(new Task("Task One", "Apple Bees", 10.0f, new Skill[1] { new Skill("Skill One", "Tintus", true, 5) }));

            // Doing this task with known proficiency should always be less than how long it would take with no known proficiency
            Assert.IsTrue(((previousTime + (10.0f / 60.0f)) - previousTime) < (newEmployee.HoursOfTasks - previousTime));
            Assert.IsTrue(previousProf >= (newEmployee.SkillSet[0].Level - previousProf));

            // Test now on two different Employees with differing proficiencies
            newEmployee = new Employee("Weezer", new Skill[1] { new Skill("Skill One", "Prompus", false) });
            Employee secondNewEmployee = new Employee("RadioHead", new Skill[1] { new Skill("Skill One", "Clyde", false, 10) });

            Task testTask = new Task("Task One", "Apple Fritter", 45.0f, new Skill[1] { new Skill("Skill One", "Kurp", true, 5) });

            // Both do task
            newEmployee.doTask(testTask);
            secondNewEmployee.doTask(testTask);

            // Proficiency gain of employee with no skill should be greater than the profieicny gain of employee with higher proficiency
            Assert.IsTrue(newEmployee.SkillSet[0].Level > (secondNewEmployee.SkillSet[0].Level - 10));

            // Less proficient employee should take longer on task than proficient employee
            Assert.IsTrue(newEmployee.HoursOfTasks > secondNewEmployee.HoursOfTasks);

        }

        [TestMethod]
        public void testInitializedProf()
        {

            // Make employee with given skill proficiency 5
            Employee newEmployee = new Employee("Weezer", new Skill[1] { new Skill("Skill One", "Gugi", false, 5) });
            Assert.IsTrue(newEmployee.SkillSet[0].Level == 5);

            Task taskOne = new Task("Task One", "Apple Bees", 10.0f, new Skill[1] { new Skill("Skill One", "Aeprus", true, 5) });

            newEmployee.doTask(taskOne);

            // Make sure doing task made employee learn at least something (gain at least one proficiency)
            Assert.IsTrue(newEmployee.SkillSet[0].Level > 5);


        }


        [TestMethod]
        public void testAddingTaskAgain()
        {

            // Make employee which knows skill one with a profieicny in it of 5
            Employee newEmployee = new Employee("Weezer", new Skill[1] { new Skill("Skill One", "Mrapus", false, 5) });

            // Make task of skill one with a difficulty of 5
            Task taskOne = new Task("Task One", "Apple Bees", 10.0f, new Skill[1] { new Skill("Skill One", "Dook", true, 5) });

            newEmployee.doTask(taskOne);

            // Make sure employee learned
            Assert.IsTrue(newEmployee.SkillSet[0].Level > 5);

            // Make sure task is finished
            Assert.IsTrue(taskOne.Done);

            uint prevProf = newEmployee.SkillSet[0].Level;
            float prevHoursWorked = newEmployee.HoursOfTasks;

            // Do task again.
            newEmployee.doTask(taskOne);

            // Because task is already done, don't redo.
            Assert.IsTrue(prevHoursWorked == newEmployee.HoursOfTasks);
            Assert.IsTrue(newEmployee.SkillSet[0].Level == prevProf);


        }


        [TestMethod]
        public void testRuntime()
        {

            // Make two employees of differing proficencies in skill one
            Employee testOne = new Employee("Weezer", new Skill[1] { new Skill("Skill One", "Larus", false, 5) });
            Employee testTwo = new Employee("Fred", new Skill[1] { new Skill("Skill One", "Aeurus", false) });

            testOne.doTask(new Task("Task One", "Apple Bees", 10.0f, new Skill[1] { new Skill("Skill One", "Cramp", true, 5) }));
            testTwo.doTask(new Task("Task One", "Apple Bees", 10.0f, new Skill[1] { new Skill("Skill One", "Cramd", true, 5) }));

            // Make sure more proficient employee did tasks faster
            Assert.IsTrue(testOne.HoursOfTasks < testTwo.HoursOfTasks);

            testOne.doTask(new Task("Task Two", "Apple Bees", 10.0f, new Skill[1] { new Skill("Skill One", "Cramd", true, 1000) }));
            testTwo.doTask(new Task("Task Two", "Apple Bees", 10.0f, new Skill[1] { new Skill("Skill One", "Cramd", true, 1000) }));

            // Make sure more proficient employee did tasks faster
            Assert.IsTrue(testOne.HoursOfTasks < testTwo.HoursOfTasks);




        }

        [TestMethod]
        public void testGiveTaskEstimate()
        {

            // Make two employees of differing proficencies in skill one
            Employee testOne = new Employee("Weezer", new Skill[1] { new Skill("Skill One", "Apple", false, 5) });
            Employee testTwo = new Employee("Fred", new Skill[1] { new Skill("Skill One", "Apple", false) });

            Task taskOne = new Task("Task One", "Apple Bees", 10.0f, new Skill[1] { new Skill("Skill One", "Auple", true, 5) });

            // Make sure estimate returns a lower estimate for higher proficiency employees than less.
            Assert.IsTrue(testOne.estimateTask(taskOne) < testTwo.estimateTask(taskOne));


        }

        [TestMethod]
        public void testGiveTaskEstimateNoSkill()
        {

            // Create two employees of equal unknowing of a skill
            Employee testOne = new Employee("Weezer");
            Employee testTwo = new Employee("Fred");

            Task taskOne = new Task("Task One", "Apple Bees", 10.0f, new Skill[1] { new Skill("Skill One", "Astrup", true, 56) });

            // Make sure they both have a equal estimate if both unknowing
            Assert.IsTrue(testOne.estimateTask(taskOne) == testTwo.estimateTask(taskOne));


        }

        [TestMethod]
        public void testAddMultipleSkill()
        {

            // Create employee with known proficiency in skill one
            Employee testOne = new Employee("Weezer", new Skill[1] { new Skill("Skill One", "Eagel", false, 55) });

            string prevName = testOne.SkillSet[0].Name;
            uint prevProf = testOne.SkillSet[0].Level;

            // Do task which has new and known skills.
            testOne.doTask(new Task("Task One", "Grape Seeds", 60.0f, new Skill[3] { new Skill("Skill One", "Aetel", true, 5), new Skill("Skill Two", "Atrup", true, 15), new Skill("Skill Three", "Iergle", true, 25) }));

            // Make sure skill one is in skillset
            Assert.IsTrue(prevName == testOne.SkillSet[0].Name);

            // Make sure learned.
            Assert.IsTrue(prevProf < testOne.SkillSet[0].Level);

            // Make sure added two new ones.
            Assert.IsTrue(testOne.SkillSet.Length == 3);
            Assert.IsTrue(prevName != testOne.SkillSet[1].Name);
            Assert.IsTrue(prevName != testOne.SkillSet[2].Name);

        }

        [TestMethod]
        public void testAddManyTasksAndSkill()
        {

            // Make employee with two known skills
            Employee testOne = new Employee("Weezer", new Skill[2] { new Skill("Skill 1", "Brapple", false, 55), new Skill("Skill 2", "Brungle", false, 59) });

            Random rand = new Random();

            // Create 1000 tasks in which each has two random skills from Skill 1
            //
            //
            // Skill 100. Difficulty of 0.
            for (int i = 0; i < 1000; i++)
            {

                testOne.doTask(new Task("Task " + rand.Next(0, 100), "Apple Bees", 120.0f, new Skill[2] { new Skill("Skill " + rand.Next(0, 100), "Brungle", true), new Skill("Skill " + rand.Next(0, 100), "Brunchle", true) }));

            }

            // Create container to signal what skills we've already learned.
            Dictionary<Skill, bool> skillsOccurances = new Dictionary<Skill, bool>();

            // For each skill learned (thats not skill 1 or 2), check to make sure its assigned 0 prof. and is not reoccuring.
            for (int i = 2; i < testOne.SkillSet.Length; i++)
            {

                Assert.IsTrue(!skillsOccurances.ContainsKey(testOne.SkillSet[i]));
                skillsOccurances[testOne.SkillSet[i]] = true;

            }

        }


        [TestMethod]
        public void testActivityOfTask()
        {

            // Create employee to do task
            Employee testOne = new Employee("Weezer");

            Task taskOne = new Task("Task One", "Apple Bees", 10.0f);

            // Make sure task is not done when initialized
            Assert.IsFalse(taskOne.Done);

            testOne.doTask(taskOne);

            // After doing task, make sure is now true
            Assert.IsTrue(taskOne.Done);

        }

        [TestMethod]
        public void testTaskDescription()
        {

            Task taskOne = new Task("Task One", "Apple Bees", 10.0f, new Skill[1] { new Skill("Skill One", "Trungle", true, 5) });

            Assert.IsTrue(taskOne.Description == "Apple Bees");

            taskOne.Description = "Quiznos";

            Assert.IsTrue(taskOne.Description == "Quiznos");

        }



    }
}
