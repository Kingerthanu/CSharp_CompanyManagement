# CSharp_CompanyManagement
This Program Works to Create Deep Hierachies Between Varying Interlinked Objects Using Principles of Programming-by-Contract and Using a Practical Design Model of Companies and Their Management of Associated Teams and Employees in Order To Create A Robust CodeBase (revolutionary ik).
This Program Basically Boils Down to The Comparison Relation Between Skills of Either a Employee and a Task to Estimate The Execution Time of Task
Based Upon Their Skill Differences For Shared Skills. A Employee Will Be Granted Proficiency In a Skill Either During It's Hiring Process (Initialization)
or By Doing Tasks, and Learning From Doing Them Their Associated Skills. This Means if A Task uses Skill 1 and Employee Doesn't Have Skill 1 in its skillset,
They Will Take Longer To Complete The Task, But Will Gain Some Proficiciency In It For Having Done Something Related To It (First Sight of A Skill Through
A Task Will Start That Employee At Proficiency Level-1 For That Skill Always). After This Initial Sighting of The Skill, Proficiency Then Will Be Added In a Scale
Where More Difficult Skills Will Give More Proficiency Then Easier Ones. This Process is Expanded Upon Through The Usage of Teams, in Which Will Hold Many Employees.
These Employees on The Team Will Work Together in Groups (scalable [have it at 4 right now]) to Complete the Task Quicker By Dividing Out Sub-Tasks To Each. Each
Teammate/Employee Will Then Still Be Benefitted By Proficiency Gains.
Teams Will Be Held Inside a Company Class Which Will Work to Manage Their States and Works as Means of a Interface to Work With its internal Teams, and associated Employees.
The Company Allows Easy Containment of Associated Teams and Employees by Employing Hiring Functionality as well as Team Creation Functionality. We Have A C++ Iteration of Our
Code Which Works Likewise (worked to transfer code from C++ -> C# then back to C# -> C++).
Using This Structure, We Commit Many Unit Tests and Integration Tests Using Microsoft's Provided TestCase Library to Ensure Lavish Branch and Line Coverage of All Our Code.
The Provided Version Also Includes The Utilization of the Moq Library for Moq Testing (in the code there may be some bloat for these Moq's to Work Properly).

This Program Taught Me Way Too Much I Didn't Need To Know; I Lear
