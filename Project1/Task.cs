/*
    Invariants:
        1.)  Task will always contain a name, description and the runtime of it.
        2.)  Runtime of a Task is measured in minutes.
*/
public class Task
{

    private string name;
    private string description;
    private float runTime;
    private bool done = false; // Determines if task is completed
    private Skill[] skillSet = new Skill[0];

    public virtual string Name
    {
        get { return this.name; }
    }

    public virtual string Description
    {
        set { this.description = value; }
        get { return this.description; }
    }

    public bool Done
    {
        get { return this.done; }
    }

    public virtual float RunTime
    {
        get { return this.runTime; }
    }

    public virtual Skill[] SkillSet
    {
        get { return this.skillSet; }
    }


    // Applied for mocks
    public Task()
    {

        this.name = "";
        this.description = "";

    }


    /*
        Preconditions:
            1.)  taskTime is measured in minutes.
            2.)  taskTime is non-negative.
            3.)  taskName is non-empty ("").
        Postconditions:
            1.)  Private members will be set to passed arguments.
            2.)  this.skillSet will be initialized to either length 0 if no skills supplied, or to argument skills contents.
    */
    public Task(string taskName, string taskDescription, float taskTime, Skill[]? skills = null)
    {

        this.name = taskName;
        this.description = taskDescription;
        this.runTime = taskTime;

        if (skills != null)
        {

            this.skillSet = skills;
        }


    }

    public Task(Task toCopy)
    {

        this.name = toCopy.name;
        this.description = toCopy.description;
        this.runTime = toCopy.runTime;

        this.skillSet = new Skill[toCopy.skillSet.Length];

        for(uint i = 0; i < toCopy.skillSet.Length; i++) {

            this.skillSet[i] = new Skill(toCopy.skillSet[i]);

        }


    }

    /*
        Postcondition:
            1.) this.done will now be true.
    */
    public void markDone()
    {

        this.done = true;

    }

}








