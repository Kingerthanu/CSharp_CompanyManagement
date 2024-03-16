/*
    Invariant: 
        1.)   Calculated cost will be clamped to 0.0f if negative.
*/
public interface IDifficultyCurve
{

    /*
        Preconditions:
            1.)  baseCost is expected to be a positive number.
    */
    public abstract float CalculateCost(float baseCost, uint taskDifficulty = 0, uint employeeProficiency = 0);

}
