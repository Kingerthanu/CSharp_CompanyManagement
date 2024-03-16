/*
    Invariants:
        1.)  A Employee will never lose skills learned/done.
        2.)  A Employee will always be initialized with zero hours worked on tasks.
*/
public class Employee
{


    private string name;
    private float hoursOfTasks = 0.0f;
    private Skill[] skillSet = new Skill[0];

    public float HoursOfTasks
    {
        get { return this.hoursOfTasks; }
    }

    public virtual Skill[] SkillSet
    {
        get { return this.skillSet; }
    }

    public virtual string Name
    {
        get { return this.name; }
    }


    // Applied for mocks
    public Employee()
    {

        this.name = "";

    }

    /*
        Preconditions:
            1.)  name is non-empty ("").
        Postconditions:
            1.)  this.skillSet initialized to either length 0 if no skills supplied, or to argument skills contents.
    */
    public Employee(string name, Skill[]? skills = null)
    {
        this.name = name;

        if (skills != null){

            this.skillSet = skills;

        }

    }

    /*
        Postconditions:
            1.)  this.skillSet will be a deep-copy of toCopies Skills..
    */
    public Employee(Employee toCopy)
    {

        this.name = toCopy.name; 
        this.hoursOfTasks = toCopy.hoursOfTasks;

        this.skillSet = new Skill[toCopy.skillSet.Length];

        for(uint i = 0; i  < toCopy.skillSet.Length; i++) {

            // Deep-copy skills
            this.skillSet[i] = new Skill(toCopy.skillSet[i]);

        }

    }

    /*
        Preconditions:
            1.) Task's runtime is measured in minutes.
        Postconditions:
            1.)  Return task runtime in minutes.
            2.)  If taskTime falls into negatives (overly proficient), will return 0 to show instant finishing of task.
    */
    public virtual float estimateTask(Task givenTask){

        float taskTime = givenTask.RunTime;

        // For each skill in a given task, calculate time it'll take to do given skill of task based on proficiency
        for (int index = 0; index < givenTask.SkillSet.Length; index++){

            // Give our base time. which will cycle around as each skill takes a dent out of our given baseCost.
            taskTime = givenTask.SkillSet[index].Curve.CalculateCost(taskTime, givenTask.SkillSet[index].Level, this.findProficiency(givenTask.SkillSet[index].Name));

        }

        // If negative, return instantanous finishing of task (0.0f)
        if (taskTime < 0.0f){
            return 0.0f;
        }

        return taskTime;

    }

    private uint findProficiency(string skillName)
    {

        // For each skill in our skills
        for (int i = 0; i < skillSet.Length; i++){

            if (skillSet[i].Name == skillName){
                return skillSet[i].Level;
            }

        }

        // Else return no proficiency
        return 0;

    }


    private void addSkill(Skill newSkill)
    {

        // Create new skill array of one extra spot
        Skill[] tmp = new Skill[this.skillSet.Length + 1];

        // Copy over all previous entries
        for (int i = 0; i < this.skillSet.Length; i++){

            tmp[i] = this.skillSet[i];

        }

        // Add new entry
        tmp[this.skillSet.Length] = newSkill;

        // Set as our employees skillset
        this.skillSet = tmp;

    }

    /*
        Preconditions:
            1.)  Name(s) is a unique identifier for a skill query.
            2.)  taskTime is given in minutes.
        Postconditions:
            1.)  Won't change anything if already done task.
            2.)  First time seeing skill, proficiency will start at 0 for that given skill.
            3.)  Task will set this done = true if not added already to employee doneTasks.
            4.)  All task skills will be known to employee after execution.
    */
    public void doTask(Task toDo)
    {
        // If task is already been done, don't do it again.
        if (toDo.Done){

            return;

        }

        // Calculate task run time based upon passed Tasks skills and this.employee's skills
        float taskTime = this.estimateTask(toDo);
        this.hoursOfTasks += (taskTime / 60.0f);

        bool flag = false;

        for (uint indexN = 0; indexN < toDo.SkillSet.Length; indexN++, flag = false){

            for (uint indexO = 0; indexO < this.skillSet.Length; indexO++){

                // If we have a skill of a task in our skillset already, add proficiency for redoing it.
                if (toDo.SkillSet[indexN].Name == this.skillSet[indexO].Name){

                    // Proficiency += minimum 1 increase for redoing + ( (difference between difficulty and proficiency in skill) / 3 )
                    //     If diff is less than 3, then go slowly by increments of one as to scale.
                    this.skillSet[indexO].Level += (uint)(1 + (Math.Abs((int)((toDo.SkillSet[indexN].Level) - (this.skillSet[indexO].Level))) / 3));
                    flag = true;
                    break;

                }


            }

            // If not known skill, add it with a new proficiency of 0 for first time learning concept.
            if (flag == false){

                // Add new skil, but set at 0 proficiency. Also switch ID from true to false to symbolize this now being a proficiency.
                this.addSkill(new Skill(toDo.SkillSet[indexN].Name, toDo.SkillSet[indexN].Description, false, 0));
            
            }

        }

        // After doing / adding task skills; mark task as done.
        toDo.markDone();
    }

}