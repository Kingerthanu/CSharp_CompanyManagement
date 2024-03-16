using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace UT_
{


    [TestClass]
    public class SkillTest
    {

        [TestMethod]
        public void testSkillProficiencies()
        {

            // Should set default level if not provided to 0
            Skill skillDifficulty = new Skill("Skill One", "Pineapple", true);
            Skill skillProficiency = new Skill("Skill One", "Pineapple", false);

            Assert.IsTrue(skillDifficulty.Level == 0);
            Assert.IsTrue(skillProficiency.Level == 0);

            // Try changing both to one. Difficulty cant be changed
            skillDifficulty.Level = 1;
            skillProficiency.Level = 1;

            Assert.IsTrue(skillDifficulty.Level == 0);
            Assert.IsTrue(skillProficiency.Level == 1);

        }


        [TestMethod]
        public void testInitialization()
        {

            // Should set default level if not provided to 0
            Skill skillDifficulty = new Skill("Skill One", "Pineapple", true, 93);

            Assert.IsTrue(skillDifficulty.Name == "Skill One");
            Assert.IsTrue(skillDifficulty.Description == "Pineapple");
            Assert.IsTrue(skillDifficulty.ID == true);
            Assert.IsTrue(skillDifficulty.Level == 93);

        }


        [TestMethod]
        public void testHashes()
        {

            // Same skill name different description so hash same
            Skill skillPineapple = new Skill("Skill One", "Pineapple", true, 93);
            Skill skillApple = new Skill("Skill One", "Apple", true, 3);

            Assert.IsTrue(skillPineapple.GetHashCode() == skillApple.GetHashCode());

            // Different skill name hash differently
            Skill skillPineappleTwo = new Skill("Skill Two", "Pineapple", true, 123);

            Assert.IsTrue(skillPineapple.GetHashCode() != skillPineappleTwo.GetHashCode());


            // Skill type also determines position
            Skill skillPineappleProficient = new Skill("Skill One", "Pineapple", false, 93);

            Assert.IsTrue(skillPineapple.GetHashCode() != skillPineappleProficient.GetHashCode());

        }


        [TestMethod]
        public void testEqual()
        {

            // Create two skills with the same member values
            Skill skill1 = new Skill("Skill One", "Pineapple", true, 93);
            Skill skill2 = new Skill("Skill One", "Pineapple", true, 93);

            // Test if skill1 is equal to skill2
            Assert.IsFalse(skill1.Equals(skill2));


            // Re-initialize with copy construct
            skill2 = new Skill(skill1);

            // Test if skill1 is equal to skill2
            Assert.IsFalse(skill1.Equals(skill2));


            // Re-initialize as skill1
            skill2 = skill1;

            // Test if skill1 is equal to skill2
            Assert.IsTrue(skill1.Equals(skill2));

        }


        [TestMethod]
        public void testCopy()
        {

            // Create two skills with the same member values
            Skill skill1 = new Skill("Skill One", "Pineapple", false, 93);
            Skill skill2 = new Skill(skill1);

            Assert.IsTrue(skill2.Name == "Skill One");
            Assert.IsTrue(skill2.Description == "Pineapple");
            Assert.IsTrue(skill2.ID == false);
            Assert.IsTrue(skill2.Level == 93);

            // Make sure values aren't changed if one changes
            skill1.Level = 0;

            Assert.IsTrue(skill2.Level == 93);

            skill1 = new Skill("Skill Four", "Slinky", true, 0);

            // Make sure values aren't changed if copied changes
            Assert.IsTrue(skill2.Name == "Skill One");
            Assert.IsTrue(skill2.Description == "Pineapple");
            Assert.IsTrue(skill2.ID == false);
            Assert.IsTrue(skill2.Level == 93);


        }


        [TestMethod]
        public void testDefaultInterface()
        {

            // Should set interface to linear as default.
            Skill skillDefaultInterface = new Skill("Skill One", "Apple Bees", false, 0);

            // Equal amount should return base time.
            Assert.IsTrue(skillDefaultInterface.Curve.CalculateCost(60.0f, 0, 0) == 60.0f);

            // Add 3-minutes as is difficulty of 3 and 0 skill
            Assert.IsTrue(skillDefaultInterface.Curve.CalculateCost(60.0f, 3, 0) == 63.0f);

            // Remove 3-minutes as is difficulty of 0 and 3 skill
            Assert.IsTrue(skillDefaultInterface.Curve.CalculateCost(60.0f, 0, 3) == 57.0f);

            // If goes negative, should return 0.0f for instant finishing
            Assert.IsTrue(skillDefaultInterface.Curve.CalculateCost(60.0f, 0, 120) == 0.0f);

        }


        [TestMethod]
        public void testUnseededRandomizedInterface()
        {

            // Should set interface to linear as default.
            Skill skillRandomInterface = new Skill("Skill One", "Apple Bees", false, 0, new RandomizedDifficultyCurve());

            // Equal amount should return base time.
            Assert.IsTrue(skillRandomInterface.Curve.CalculateCost(60.0f, 0, 0) == 60.0f);

            // Should be always at least double so check from adding one up to see if properly randomizing
            Assert.IsTrue(skillRandomInterface.Curve.CalculateCost(60.0f, 3, 0) > 63.0f);

            // Should be always at least double so check from removing one up to see if properly randomizing
            Assert.IsTrue(skillRandomInterface.Curve.CalculateCost(60.0f, 0, 3) < 57.0f);

            // If goes negative, should return 0.0f for instant finishing
            Assert.IsTrue(skillRandomInterface.Curve.CalculateCost(60.0f, 0, 120) == 0.0f);



        }


        [TestMethod]
        public void testSeededRandomInterface()
        {

            Random seed = new Random();

            // Should set interface to linear as default.
            Skill skillRandomInterface = new Skill("Skill One", "Apple Bees", false, 0, new RandomizedDifficultyCurve(seed));

            // Equal amount should return base time.
            Assert.IsTrue(skillRandomInterface.Curve.CalculateCost(60.0f, 0, 0) == 60.0f);

            // Should be always at least double so check from adding one up to see if properly randomizing
            Assert.IsTrue(skillRandomInterface.Curve.CalculateCost(60.0f, 3, 0) > 63.0f);

            // Should be always at least double so check from removing one up to see if properly randomizing
            Assert.IsTrue(skillRandomInterface.Curve.CalculateCost(60.0f, 0, 3) < 57.0f);

            // If goes negative, should return 0.0f for instant finishing
            Assert.IsTrue(skillRandomInterface.Curve.CalculateCost(60.0f, 0, 120) == 0.0f);



        }


        [TestMethod]
        public void testRandomInterface()
        {

            // Same seeding; need two as if we pass one, it will work as generator and will track if either has called it messing with test consistency
            Random seedOne = new Random(1);
            Random seedTwo = new Random(1);

            // Should set interface to linear as default.
            Skill skillRandomInterface = new Skill("Skill One", "Apple Bees", false, 0, new RandomizedDifficultyCurve(seedOne));
            Skill skillRandomInterfaceSecond = new Skill("Skill One", "Apple Bees", false, 0, new RandomizedDifficultyCurve(seedTwo));

            // Need float to hold one of the most recent as want to check value twice and calling function again will give new val.
            float newestCost = skillRandomInterface.Curve.CalculateCost(60.0f, 0, 0);  

            // Equal amount should return base time.
            Assert.IsTrue(newestCost == 60.0f);
            Assert.IsTrue(newestCost == skillRandomInterfaceSecond.Curve.CalculateCost(60.0f, 0, 0));

            newestCost = skillRandomInterface.Curve.CalculateCost(60.0f, 3, 0);

            // Should be always at least double so check from adding one up to see if properly randomizing
            Assert.IsTrue(newestCost > 63.0f);
            Assert.IsTrue(newestCost == skillRandomInterfaceSecond.Curve.CalculateCost(60.0f, 3, 0));

            newestCost = skillRandomInterface.Curve.CalculateCost(60.0f, 0, 3);

            // Should be always at least double so check from removing one up to see if properly randomizing
            Assert.IsTrue(newestCost < 57.0f);
            Assert.IsTrue(newestCost == skillRandomInterfaceSecond.Curve.CalculateCost(60.0f, 0, 3));

            newestCost = skillRandomInterface.Curve.CalculateCost(60.0f, 0, 120);

            // If goes negative, should return 0.0f for instant finishing
            Assert.IsTrue(newestCost == 0.0f);
            Assert.IsTrue(newestCost == skillRandomInterfaceSecond.Curve.CalculateCost(60.0f, 0, 120));


        }


        [TestMethod]
        public void testAdjustedInterface()
        {


            Skill skillAdjustedInterface = new Skill("Skill One", "Apple Bees", false, 0, new AdjustedDifficultyCurve(1.5f));
            LinearDifficultyCurve handler = new LinearDifficultyCurve();

            // Equal amount should return base time (should be 60.0f * 1.5f as that's multiple)
            Assert.IsTrue(skillAdjustedInterface.Curve.CalculateCost(60.0f, 0, 0) == (handler.CalculateCost(60.0f, 0, 0) * 1.5f));

            // Should be linear, but with higher multiple so check with known multiple to see if properly did calculation
            Assert.IsTrue(skillAdjustedInterface.Curve.CalculateCost(60.0f, 3, 0) == (handler.CalculateCost(60.0f, 3, 0)) * 1.5f);

            // Should be linear, but with higher multiple so check with known multiple to see if properly did calculation
            Assert.IsTrue(skillAdjustedInterface.Curve.CalculateCost(60.0f, 0, 3) == (handler.CalculateCost(60.0f, 0, 3)) * 1.5f);

            // If goes negative, should return 0.0f for instant finishing
            Assert.IsTrue(skillAdjustedInterface.Curve.CalculateCost(60.0f, 0, 120) == handler.CalculateCost(60.0f, 0, 120) * 1.5f);



        }


        [TestMethod]
        public void testCopyInterface()
        {


            // Should set interface to linear as default.
            Skill skillAdjustedInterface = new Skill("Skill One", "Apple Bees", false, 0, new AdjustedDifficultyCurve(1.5f));

            Skill skillAdjustedInterfaceCopy = new Skill(skillAdjustedInterface);

            // We intend on the Curve being shared amongst Skills as no real need to copy over a interface or its general struct other than whats needed.
            Assert.IsTrue(skillAdjustedInterface.Curve.Equals(skillAdjustedInterfaceCopy.Curve));

            // Equal amount should return base time (should be 60.0f * 1.5f as that's multiple)
            Assert.IsTrue(skillAdjustedInterface.Curve.CalculateCost(60.0f, 0, 0) == (60.0f * 1.5f));
            Assert.IsTrue(skillAdjustedInterface.Curve.CalculateCost(60.0f, 0, 0) == skillAdjustedInterfaceCopy.Curve.CalculateCost(60.0f, 0, 0));


            // Should be linear, but with higher multiple so check with known multiple to see if properly did calculation
            Assert.IsTrue(skillAdjustedInterface.Curve.CalculateCost(60.0f, 3, 0) == ((60.0f * 1.5f) + (1.5f * 3)));
            Assert.IsTrue(skillAdjustedInterface.Curve.CalculateCost(60.0f, 3, 0) == skillAdjustedInterfaceCopy.Curve.CalculateCost(60.0f, 3, 0));

            // Should be linear, but with higher multiple so check with known multiple to see if properly did calculation
            Assert.IsTrue(skillAdjustedInterface.Curve.CalculateCost(60.0f, 0, 3) == ((60.0f * 1.5f) - (1.5f * 3)));
            Assert.IsTrue(skillAdjustedInterface.Curve.CalculateCost(60.0f, 0, 3) == skillAdjustedInterfaceCopy.Curve.CalculateCost(60.0f, 0, 3));

            // If goes negative, should return 0.0f for instant finishing
            Assert.IsTrue(skillAdjustedInterface.Curve.CalculateCost(60.0f, 0, 120) == 0.0f);
            Assert.IsTrue(skillAdjustedInterface.Curve.CalculateCost(60.0f, 0, 120) == skillAdjustedInterfaceCopy.Curve.CalculateCost(60.0f, 0, 120));


        }


        [TestMethod]
        public void testCopyRandomInterface()
        {


            // Should set interface to linear as default.
            Skill skillRandomInterface = new Skill("Skill One", "Apple Bees", false, 0, new RandomizedDifficultyCurve());

            Skill skillRandomInterfaceSecond = new Skill(skillRandomInterface);

            Assert.IsTrue(skillRandomInterface.Curve.Equals(skillRandomInterfaceSecond.Curve));


        }


        [TestMethod]
        public void testCopyLinearInterface()
        {


            // Should set interface to linear as default.
            Skill skillLinearInterface = new Skill("Skill One", "Apple Bees", false, 0, new LinearDifficultyCurve());

            Skill skillLineaInterfaceSecond = new Skill(skillLinearInterface);

            Assert.IsTrue(skillLinearInterface.Curve.Equals(skillLineaInterfaceSecond.Curve));


        }

    }

}