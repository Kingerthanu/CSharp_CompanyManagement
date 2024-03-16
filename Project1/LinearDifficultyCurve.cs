
public class LinearDifficultyCurve : IDifficultyCurve
{
    public float CalculateCost(float baseCost, uint taskDifficulty, uint employeeProficiency)
    {

        float result = baseCost + (int)(taskDifficulty - employeeProficiency);

        // If below 0, clamp.
        if (result < 0.0f)
        {

            return 0.0f;

        }

        return result;
    }
}