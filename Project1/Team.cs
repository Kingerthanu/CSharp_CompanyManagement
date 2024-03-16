/*
	Invariant: 
		1.) Name of team will never change.
*/
public class Team
{

    private readonly string name;
    private Employee[] employeeList = new Employee[0];

    public string Name
    {

        get { return this.name; }

    }

    /*
        Postconditions:
            1.)  Employees of toCopy will be deep copied.
    */
    public Team(Team toCopy)
    {

        this.name = toCopy.name;
        this.employeeList = new Employee[toCopy.employeeList.Length];

        for(uint i = 0; i < this.employeeList.Length; i++)
        {

            // Deep-copy over Employees
            this.employeeList[i] = new Employee(toCopy.employeeList[i]);

        }

    }

    public Employee[] Employees
    {

        get { return this.employeeList; } 

    }

	public bool containsEmployee(Employee toFind)
    {

        // Go through each employee in team.
        for (uint i = 0; i < this.employeeList.Length; i++)
        {

            if (this.employeeList[i].Equals(toFind))
            {   // If same name, return found.

                return true;

            }

        }

        // If this far, not in team.
        return false;

    }

    /*
        Precondition:
            1.) teamName is non-empty
    */
    public Team(string teamName, Employee[]? teamEmployees = null)
    {

        this.name = teamName;

        // If we have non-zero employees to add to team.
        if (teamEmployees != null)
        {

            this.employeeList = teamEmployees;

        }


    }

    /*
        Precondition:
            1.) newEmployee name is not already in this employeeList.
        Postconditions: 
            1.) Size of Employee list will increase by one and newEmployee will be at end.
            2.) this.employeeList will point to a new Employee container.
    */
    public void addEmployee(Employee newEmployee)
    {

        Employee[] handler = new Employee[this.employeeList.Length + 1];

        // Go through each employee and copy over to size + 1
        for (uint i = 0; i < this.employeeList.Length; i++)
        {

            handler[i] = this.employeeList[i];

        }

        // At end, add newest employee pointer and increment size up.
        handler[this.employeeList.Length] = newEmployee;

        this.employeeList = handler;

    }

    /*
        Preconditions: 
            1.) Employee name is in the team.
        Postconditions: 
            1.) this.employeeList will decrease size by one.
            2.) Named employee will be removed from team.
    */
    public void removeEmployee(string employeeName)
    {

        Employee[] handler = new Employee[this.employeeList.Length - 1];

        // Go through each employee and copy over all but employeeName.
        for (uint i = 0, handleI = 0; i < this.employeeList.Length; i++)
        {

            // Leak if is employee name as not liable...

            // If not employeeName add to new size - 1
            if (this.employeeList[i].Name != employeeName)
            {
                handler[handleI++] = this.employeeList[i];
            }

        }

        this.employeeList = handler;

    }

    /*
        Precondition:
            1.) this.employeeList is not empty.
        Postconditions: 
            1.) Estimate will be returned in hours based on average of worktime of each employee on team in team groups of 4.
    */
    public float estimateTask(Task givenTask)
    {

        // Holds sum of all employee estimations.
        float average = 0.0f;

        // For each employee, estimate time to take. Time is proportional to individual worktime + group team size which is statically 4.
        for (uint i = 0; i < this.employeeList.Length; i++)
        {

            average += (this.employeeList[i].estimateTask(givenTask) / (1 + (this.employeeList.Length / 4))); // Do task estimate by first summing all terms (infer team adds in groups of four)

        }

        // Return these individual estimates divided by amount of employees.
        return (average / this.employeeList.Length);

    }

};
