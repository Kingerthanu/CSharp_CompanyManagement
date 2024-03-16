/*
    Invariant:
        1.)  Curve multiple will not change during lifetime
*/
public class AdjustedDifficultyCurve : IDifficultyCurve
{
    // Contain Linear Curve member.
    private LinearDifficultyCurve linearCurve = new LinearDifficultyCurve();

    // Hold our constant multiple to curve a linear curve by
    private readonly float multiple;

    /*
        Preconditions:
            1.)  curveMultiple is positive.
        Postconditions:
            1.)  Multiple of linear curve will be set to argument.
            2.)  No multiple supplied, default to 1.0f
    */
    public AdjustedDifficultyCurve(float curveMultiple = 1.0f)
    {
        this.multiple = curveMultiple;
    }

    public float CalculateCost(float baseCost, uint taskDifficulty, uint employeeProficiency)
    {

        // Use LinearDifficultyCurve's CalculateCost and adjust the cost (relying on linear supplying positive calculation)
        float adjustedCost = linearCurve.CalculateCost(baseCost, taskDifficulty, employeeProficiency) * this.multiple;

        return adjustedCost;

    }
}
