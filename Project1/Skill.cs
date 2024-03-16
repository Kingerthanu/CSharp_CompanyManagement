/*
    Invariant:
        1.)  Name and description of a skill are const. through lifetime.
        2.)  Skill behavior is tied to difficulty traits or proficiency traits defined by isDifficulty.
        2.)  Uniqueness is defined by name and skill type.
*/
public class Skill
{

    private readonly string name;
    private readonly string description;
    private uint level;

    //  If have a chain reaction of someone's proficiency being someone else's difficulty, be able to swap; not readonly.
    //  isDifficulty = false => isProficiency/isDifficulty = true.
    private bool isDifficulty; 
    private readonly IDifficultyCurve difficultyCurve = new LinearDifficultyCurve(); // Default to linear
    

    public string Name
    { get { return name; } }

    public bool ID
    {

        get { return isDifficulty; }

    }

    public IDifficultyCurve Curve 
    {  get { return difficultyCurve; } }

    public uint Level
    { 
      get { return level; } 
      set 
        {

            // We cannot change our level of a difficulty as stated in previous labs behavior
            if (!isDifficulty)
            {

                level = value;

            }

        } 
    }

    public string Description
    { get { return description; } }


    public Skill(string name, string description, bool typeID, uint skillLevel = 0, IDifficultyCurve? skillCurve = null)
    {

        this.name = name;
        this.description = description;

        // If true, then is treated as difficulty; else if false treat as proficiency
        this.isDifficulty = typeID;
        this.level = skillLevel;

        
        if (skillCurve != null) { 
        
            this.difficultyCurve = skillCurve;
        
        }
        
        // Else... Use Linear curve as default

    }


    public Skill(Skill toCopy)
    {

        this.name = toCopy.Name;
        this.description = toCopy.description;
        this.level = toCopy.level;
        this.isDifficulty = toCopy.ID;

        //    Don't need to deep-copy over our interface
        this.difficultyCurve = toCopy.difficultyCurve;


    }


    /*
        Preconditions:
            1.)   Hashes don't include description as isn't uniqueness identifier.
            2.)   Hashes expect skills name as well as its type.
        Postconditions:
            1.)   Returns hash code derived from given skill name and skill type.
    */
    public override int GetHashCode()
    {

        // Hash only needs name and type as even if unique usage of skill (description), you still are just using skill.
        return HashCode.Combine(this.name, this.isDifficulty);

    }

    /*
        Postconditions:
            1.)   Returns true if are same reference to a given object instance.
    */
    public override bool Equals(object? obj)
    {

        // Use our unique object (base) to check if another object is of the same initialization
        return base.Equals(obj);


    }


}



