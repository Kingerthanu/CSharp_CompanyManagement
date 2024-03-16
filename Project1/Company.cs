/*
	Invariant: 
		1.) Name of company will never change.
		2.) Team(s) and Employee(s) will be uniquely named.
*/
public class Company
{

        private string name;
        private Employee[] employeesList = new Employee[0];
        private Team[] teamsList = new Team[0];

		/*
			Preconditions: 
				1.) companyName is non-empty ("").
		*/
		public Company(string companyName, Employee[]? companyEmployees = null, Team[]? companyTeams = null)
        {

            this.name = companyName;


            
            if (companyEmployees != null)
            {

                this.employeesList = companyEmployees;

            }

            if (companyTeams != null)
            {

                this.teamsList = companyTeams;

            }

        }

    /*
        Postconditions:
            1.)  Employees of toCopy will be deep copied.
            2.)  Teams of toCopy will be deep copied.
    */
    public Company(Company toCopy)
        {

            this.name = toCopy.name;

            this.employeesList = new Employee[toCopy.employeesList.Length];
            this.teamsList = new Team[toCopy.teamsList.Length];

            for(uint i = 0; i < this.employeesList.Length; i++){

                // Deep-copy employees
                this.employeesList[i] = new Employee(toCopy.employeesList[i]);

            }

            for (uint i = 0; i < this.teamsList.Length; i++){
            
                // Deep-copy teams
                this.teamsList[i] = new Team(toCopy.teamsList[i]);

            }

        }

        public Team[] Teams
        {

            get { return teamsList; }

        }
        public Employee[] Employees
        {
            get { return employeesList; }

        }
        public string Name
        {

            get { return name; }

        }


        /*
            Preconditions:
                1.) newEmployee is not already in company.
            Postconditions:
                1.) newEmployee added to the end of this.employeesList.
                2.) this.employeesList will be a new container of + 1 size.
        */
        public void addEmployee(Employee newEmployee)
        {

            Employee[] handler = new Employee[this.employeesList.Length + 1];

            // Copy over pointers of previous.
            for (uint i = 0; i < this.employeesList.Length; i++)
            {

                handler[i] = this.employeesList[i];

            }

            // Now finally point to this (then increment our size up one).
            handler[this.employeesList.Length] = newEmployee;
            this.employeesList = handler;

        }

        /*
            Preconditions:
                1.) existingEmployee to be in this.employeesList.
                2.) existingEmployee is the reference of Employee stored in company.
            Postconditions:
                1.) existingEmployee will be removed from this.employeesList.
                2.) this.employeesList will decrease by size 1.
                3.) this.employeesList will be a new container.
        */
        public void removeEmployee(Employee existingEmployee)
        {

            // Remove employees from teams they are on.
            for (uint i = 0; i < this.teamsList.Length; i++)
            {

                if (this.teamsList[i].containsEmployee(existingEmployee))
                {

                    this.teamsList[i].removeEmployee(existingEmployee.Name);

                }

            }

            Employee[] handler = new Employee[this.employeesList.Length - 1];

            for (uint i = 0, handleI = 0; i < this.employeesList.Length; i++)
            {

                // If employee to remove, deallocate, as we are liable for lifetime.
                if (!existingEmployee.Equals(this.employeesList[i]))
                {
                    // Add and increment in new container of size - 1.
                    handler[handleI++] = this.employeesList[i];
                }
                

            }

            this.employeesList = handler;

        }

    /*
        Preconditions:
            1.) Arguments are passed in the Team constructor.
            2.) teamName to be unique for company.
            3.) teamName is non-empty ("").
            4.) teamEmployees already are in company.
        Postconditions:
            1.) new Team(...) added to the end of this.teamsList.
            2.) this.teamsList will increase by size 1.
            3.) this.teamsList will be a new container.
    */
    public void createTeam(string teamName, Employee[]? teamEmployees = null)
    {

        Team[] handler = new Team[this.teamsList.Length + 1];

        // Copy over other teams.
        for (uint i = 0; i < this.teamsList.Length; i++)
        {

            handler[i] = this.teamsList[i];

        }

        // Now add passed to end.
        handler[this.teamsList.Length] = new Team(teamName, teamEmployees);
        this.teamsList = handler;

    }


    /*
        Preconditions:
            1.) existingTeam existing in this.teamsList.
            2.) existingTeam is the same reference as in this.teamsList.
        Postconditions:
            1.) existingTeam is removed from this.teamsList.
            2.) this.teamsList will decrease by size 1.
            3.) this.teamsList will be a new container.
    */
    public void deleteTeam(Team existingTeam)
    {

        Team[] handler = new Team[this.teamsList.Length - 1];

        for (uint i = 0, handleI = 0; i < this.teamsList.Length; i++)
        {

            // If team to remove, deallocate, as we are liable for lifetime.
            if (!existingTeam.Equals(this.teamsList[i]))
            {

                // Add and increment in new container of size - 1.
                handler[handleI++] = this.teamsList[i];

            }

        }

        this.teamsList = handler;

    }

    /*
        Preconditions:
            1.) teamName is existing name in this.teamsList.
        Postconditions:
            1.) Will return a reference to the team index where the team with that name lies.
    */
    public Team getTeam(string teamName)
    {

        uint index = 0;

        while (index < this.teamsList.Length)
        {

            if (this.teamsList[index].Name == teamName)
            {

                break; // Jump out prematurey if we found it midway.

            }

            index++;

        }

        return this.teamsList[index];

    }

    /*
        Preconditions:
            1.) employeeName is existing name in this.teamsList.
            2.) employeeName is unique in this teamsList.
        Postconditions:
            1.) Will return a reference to the team index where the team with that name lies.
    */
    public Employee getEmployee(string employeeName)
    {

        uint index = 0;

        while (index < this.employeesList.Length)
        {

            if (this.employeesList[index].Name == employeeName)
            {

                break; // Jump out prematurey if we found it midway.

            }

            index++;

        }

        return this.employeesList[index];

    }

};

