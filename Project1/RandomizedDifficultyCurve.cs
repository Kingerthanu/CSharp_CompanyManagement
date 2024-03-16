/*
    Invariant:
        1.)   Random generator will be constant for lifetime after intialiazation
*/
public class RandomizedDifficultyCurve : IDifficultyCurve
{

    private readonly Random random;

    public RandomizedDifficultyCurve()
    {

        // Creates reference of random gen if not supplied
        this.random = new Random();

    }

    public RandomizedDifficultyCurve(Random random)
    {
        // Sets reference of passed random gen
        this.random = random;
    }

    public float CalculateCost(float baseCost, uint taskDifficulty, uint employeeProficiency)
    {

        int randomFactor = this.random.Next(2, 15);

        // Random formula where we 150% our proficiency and see difference scaled by a multiple.
        float result = baseCost + ((int)(((taskDifficulty)) - (employeeProficiency * 1.5f)) * randomFactor);

        // If below 0, clamp.
        if (result < 0.0f)
        {

            return 0.0f;

        }

        return result;
    }
}






