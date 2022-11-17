// This is the code containing the Main menu loop
// taskList is initialised and is filled with contents of file tasklist.txt
// If tasklist.txt doesn't exist then a new file with 6 tasks is created
List<Task> taskList = new List<Task>(); // initialises taskList 
readList(); // reads in tasklist.txt file and puts the contents into <Task>taskList

bool loopValue = true;
while (loopValue == true) // loops until user chooses to quit
{
    titleText(); // displays title information on every page/view
    menuText(); // displays text of the main menu choices
    string data = Console.ReadLine();
    bool isInt = int.TryParse(data, out int value);
    if (isInt && value >= 1 && value <= 4 && data != null) // validates the user choice
    {
        loopValue = menuChoice(data); // handles the user choice and quits if loopvalue comes back false
    }
    else
    {
        Console.WriteLine("Invalid choice!");
    }
    Thread.Sleep(500);
}

// displays the top 3 lines of every page/view
// displays information regarding active/completed tasks
void titleText() {
    Console.Clear();
    Console.WriteLine("Welcome to ToDoLy\n");
    IEnumerable<Task> active = taskList.Where(task => task.Status == "Active");
    IEnumerable<Task> completed = taskList.Where(task => task.Status == "Completed");
    Console.WriteLine("You have " +active.Count() + " active tasks and "+ completed.Count() + " tasks are completed");
    Console.WriteLine("-------------------------------------------------\n");
}

// displays the main menu
void menuText()
{
    Console.WriteLine("Please pick an option:\n");
    Console.Write("(");
    colouredText("1", "purple");
    Console.WriteLine(") Show Task List (by date or project)");
    Console.Write("(");
    colouredText("2", "purple");
    Console.WriteLine(") Add New Task");
    Console.Write("(");
    colouredText("3", "purple");
    Console.WriteLine(") Edit Task (update, mark as done, remove)");
    Console.Write("(");
    colouredText("4", "purple");
    Console.WriteLine(") Save and Quit");
}

// handles user input for the main menu
bool menuChoice(string data)
{
    switch(data)
    {
        case "1":
            displayTasks();
            break;
        case "2":
            addTask();
            break;
        case "3":
            editTask();
            break;
        case "4":
            Console.WriteLine("\nSave and Quit");
            writeList();
            return false;
        default:
            break;
    }
    return true;
}

// displays text with chosen colour
void colouredText(string text, string colour)
{
    switch(colour)
    {
        case "purple":
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            break;
        case "red":
            Console.ForegroundColor = ConsoleColor.Red;
            break;
        case "green":
            Console.ForegroundColor = ConsoleColor.Green;
            break;
        case "blue":
            Console.ForegroundColor = ConsoleColor.Blue;
            break;
        case "yellow":
            Console.ForegroundColor = ConsoleColor.Yellow;
            break;
        default:
            Console.ForegroundColor = ConsoleColor.White;
            break;
    }
    Console.Write(text);
    Console.ResetColor();
}

// handles adding a Task to taskList 
void addTask()
{
loopStart:
    titleText();
    Console.Write("Title: ");
    string title = Console.ReadLine();
    if (title == "" || title == null)
    {
        Console.WriteLine("\nInvalid entry - Title can't be empty!\n");
        Thread.Sleep(1000);
        goto loopStart;
    }

    Console.WriteLine("\nDue date:\n");
    Console.Write("(");
    colouredText("1", "purple");
    Console.WriteLine(") 1 week to complete task");
    Console.Write("(");
    colouredText("2", "purple");
    Console.WriteLine(") 2 weeks to complete task");
    Console.Write("(");
    colouredText("3", "purple");
    Console.WriteLine(") 1 month to complete task");
    Console.Write("(");
    colouredText("4", "purple");
    Console.WriteLine(") 2 months to complete task");
    Console.Write("(");
    colouredText("5", "purple");
    Console.WriteLine(") Enter a different date");
    var data = Console.ReadLine();
    bool isInt = int.TryParse(data, out int value);
    DateTime date = DateTime.Now;
    if (isInt && value >= 1 && value <= 5)
    {
        switch (value) // handles menu choices. 
        {
            case 1: // make a date from today's date
                Console.WriteLine("\n1 week chosen");
                date=date.AddDays(7);
                Thread.Sleep(1000);
                break;
            case 2: // make a date from today's date
                Console.WriteLine("\n2 weeks chosen");
                date = date.AddDays(14);
                Thread.Sleep(1000);
                break;
            case 3: // make a date from today's date
                Console.WriteLine("\n1 month chosen");
                date = date.AddMonths(1);
                Thread.Sleep(1000);
                break;
            case 4: // make a date from today's date
                Console.WriteLine("\n2 months chosen");
                date = date.AddMonths(2);
                Thread.Sleep(1000);
                break;
            case 5: // make a date from user input. 
                int day = 0;
                int month = 0;
                int year = 0;
                Console.Write("Enter day dd: ".PadRight(14));
                data = Console.ReadLine();
                isInt = int.TryParse(data, out value);
                if (isInt && value >= 1 && value <= 31) day = value;
                Console.Write("Enter month MM: ".PadRight(14));
                data = Console.ReadLine();
                isInt = int.TryParse(data, out value);
                if (isInt && value >= 1 && value <= 12) month = value;
                Console.Write("Enter year YYYY: ".PadRight(14));
                data = Console.ReadLine();
                isInt = int.TryParse(data, out value);
                if (value.ToString().Length == 4 && isInt && value >= 1970 && value <= 2099) year = value; // validates date and year is within an interval

                try // tries to create a date 
                {
                    date = new DateTime(year, month, day);
                    Console.WriteLine("\nDate selected is " + date.ToString("yyyy-MM-dd"));
                    Thread.Sleep(1000);
                    break;
                }
                catch (Exception e)  // handles an invalid date by informing user and moving back to loopStart; 
                {
                    colouredText("\nInvalid date!", "red");
                    Thread.Sleep(1000);
                    goto loopStart;
                }
        }
    }

    Console.WriteLine("\nSelect status:\n");
    Console.Write("(");
    colouredText("1", "purple");
    Console.WriteLine(") Active");
    Console.Write("(");
    colouredText("2", "purple");
    Console.WriteLine(") Completed");
    string status = "";
    bool menuLoop = true;

    value = 0;
    data = Console.ReadLine();
    isInt = int.TryParse(data, out value);
    if (isInt && value >= 1 && value <= 2 && data != null)
    {
        while (menuLoop)
        {
            switch (value)
            {
                case 1:
                    status = "Active";
                    Console.WriteLine(status + "\n");
                    Thread.Sleep(200);
                    menuLoop = false;
                    break;
                case 2:
                    status = "Completed";
                    Console.WriteLine(status + "\n");
                    Thread.Sleep(200);
                    menuLoop = false;
                    break;
            }
        }
    }
    else
    {
        Console.WriteLine("Invalid choice!");
        Thread.Sleep(200);
        goto loopStart;
    }

    Console.Write("Project: ");
    string project = Console.ReadLine();
    if (project == "" || project == null)
    {
        Console.WriteLine("\nInvalid entry - Project can't be empty!\n");
        Thread.Sleep(1000);
        goto loopStart;
    }


    createTask(title, date, status, project);
    Console.WriteLine("\nTask added!");
    Thread.Sleep(1000);
}

// handles editing and deleting a task
void editTask() 
{
    while (true)
    {
        titleText();
        listHeader();
        for (int i = 0; i < taskList.Count; i++)
        {
            taskList[i].printDetails(i);
        }
        Console.WriteLine("\nChoose a task to edit or (q) to return to main menu: ");
        var data = Console.ReadLine();
        bool isInt = int.TryParse(data, out int taskIndex);
        if (isInt && taskIndex >= 1 && taskIndex <= taskList.Count)
        {
            Task task = taskList[taskIndex - 1];
            bool keepLooping = true;
            while (keepLooping == true)
            {
                loopStart:
                titleText();
                listHeader();
                taskList[taskIndex-1].printDetails(taskIndex-1);
                Console.Write("\n\nEdit task options:\n\n(");
                colouredText("1", "purple");
                Console.WriteLine(") Change title");
                Console.Write("(");
                colouredText("2", "purple");
                Console.WriteLine(") Change date");
                Console.Write("(");
                colouredText("3", "purple");
                Console.WriteLine(") Flip status");
                Console.Write("(");
                colouredText("4", "purple");
                Console.WriteLine(") Change project");
                Console.WriteLine("    --------------");
                Console.Write("(");
                colouredText("5", "purple");
                Console.WriteLine(") Delete task");

                Console.WriteLine("\nChoose from menu (q) to return to previous page: ");
                int value = 0;
                data = Console.ReadLine();
                isInt = int.TryParse(data, out value);
                if (isInt && value >= 1 && value <= 5)
                {
                    switch(value)
                    {
                        case 1:
                            Console.WriteLine("\nEnter a new title");
                            data = Console.ReadLine();
                            if(data != null && data.Length>0)
                            {
                                taskList[taskIndex - 1].Title = data;
                                colouredText("\nTitle changed to '" + data + "'", "green");
                                Thread.Sleep(1000);
                            }
                            else
                            {
                                colouredText("Title can't be blank!", "red");
                                Thread.Sleep(1000);
                            }
                            break;
                        case 2:
                            int day = 0;
                            int month = 0;
                            int year = 0;
                            value = 0;
                            Console.Write("Enter day dd: ".PadRight(14));
                            data = Console.ReadLine();
                            isInt = int.TryParse(data, out value);
                            if (isInt && value >= 1 && value <= 31) day = value;
                            Console.Write("Enter month MM: ".PadRight(14));
                            data = Console.ReadLine();
                            isInt = int.TryParse(data, out value);
                            if (isInt && value >= 1 && value <= 12) month = value;
                            Console.Write("Enter year YYYY: ".PadRight(14));
                            data = Console.ReadLine();
                            isInt = int.TryParse(data, out value);
                            if (isInt && value >= 1900 && value <= 2099) year = value;
                            try // tries to create a date 
                            {
                                taskList[taskIndex - 1].DueDate = new DateTime(year, month, day);
                                colouredText("\nNew due date is " + taskList[taskIndex - 1].DueDate.ToString("yyyy-MM-dd"), "green");
                                Thread.Sleep(1000);
                                break;
                            }
                            catch (Exception e)  // handles an invalid date by informing user and moving back to loopStart; 
                            {
                                colouredText("\nInvalid date!", "red");
                                Thread.Sleep(1000);
                                goto loopStart;
                            }

                        case 3:
                            if(task.Status == "Active")
                            {
                                taskList[taskIndex - 1].Status = "Completed";
                                colouredText("\nStatus flipped to 'Completed'", "green");
                                Thread.Sleep(1000);
                            }
                            else
                            {
                                taskList[taskIndex-1].Status = "Active";
                                colouredText("\nStatus flipped to 'Active'", "green");
                                Thread.Sleep(1000);
                            }
                            break;
                        case 4:
                            Console.WriteLine("\nEnter a new project");
                            data = Console.ReadLine();
                            if (data != null && data.Length > 0)
                            {
                                taskList[taskIndex - 1].Project = data;
                                colouredText("\nProject changed to '" + data + "'", "green");
                                Thread.Sleep(1000);
                            }
                            else
                            {
                                colouredText("Project can't be blank!", "red");
                                Thread.Sleep(1000);
                            }
                            break;
                        case 5:
                            Console.Write("\nAre you sure you want to delete task '");
                            colouredText(task.Title, "blue");
                            Console.Write("'?");
                            Console.WriteLine("\n\nConfirm with (y) or any other key to return" );
                            data = Console.ReadLine();
                            if (data.ToLower() == "y")
                            {
                                taskList.RemoveAt(taskIndex - 1);
                                colouredText("\nThe task has been deleted!", "green");
                                Thread.Sleep(1000);
                            }
                            keepLooping = false;
                            break;
                    }
                }
                else
                {
                    if (data == "q") break;
                }
                if (keepLooping == false) break;
            }
        }
        else
        {
            if (data == "q") break;
        }
        Thread.Sleep(200);
    }
}

// displays header rows to displayed Item lists
void listHeader() {
    colouredText("    " + "Title".PadRight(15) + "Due Date".PadRight(15) + "Status".PadRight(15) + "Project".ToString().PadRight(15), "blue");
    Console.WriteLine("\n    " + "-----".PadRight(15) + "--------".PadRight(15) + "------".PadRight(15) + "-------".PadRight(15));
}

// creates a new Task and adds it to taskList
void createTask(string title, DateTime date, string status, string project)
{
    taskList.Add(new Task(title,date, status, project));
}

// allows user to choose how Task list is sorted and then displays all rows
// also creates a menu to allow user to continue choosing sort order
void displayTasks(int order = 1, string sortedBy = "date ascending")
{
    while (true)
    {
        titleText();
        
        Console.Write("Sort options:\n\n(");
        colouredText("1", "purple");
        Console.Write(") date asc | (");
        colouredText("2", "purple");
        Console.Write(") date desc | (");
        colouredText("3", "purple");
        Console.Write(") project asc | (");
        colouredText("4", "purple");
        Console.WriteLine(") project desc\n");

        listHeader();

        List<Task> sortedList = taskList.OrderBy(o => o.DueDate.ToString()).ToList();

        switch (order)
        {
            case 2:
                sortedList = taskList.OrderByDescending(o => o.DueDate.ToString()).ToList();
                sortedBy = "Date descending";
                break;
            case 3:
                sortedList = taskList.OrderBy(o => o.Project.ToString()).ThenBy(o => o.DueDate.ToString()).ToList();
                sortedBy = "Project - Date ascending";
                break;
            case 4:
                sortedList = taskList.OrderBy(o => o.Project.ToString()).ThenByDescending(o => o.DueDate.ToString()).ToList();
                sortedBy = "Project - Date descending";
                break;
            default:
                sortedList = taskList.OrderBy(o => o.DueDate.ToString()).ToList();
                sortedBy = "Date ascending";
                break;
        }

        for (int i = 0; i < sortedList.Count; i++)
        {
            sortedList[i].printDetails(i);
        }

        Console.WriteLine("\nListing all tasks sorted by '" + sortedBy + "'");
        Console.WriteLine("\nChoose a sort order or press any other key to return to main menu");
        var data = Console.ReadLine();

        bool isInt = int.TryParse(data, out int value);
        if (isInt && value >= 1 && value <= 4)
        {
            switch(value)
            {
                case 1:
                    order = 1;
                    break;
                case 2:
                    order = 2;
                    break;
                case 3:
                    order = 3;
                    break;
                case 4:
                    order = 4;
                    break;
            }
        }
        else
        {
            break;
        }
        Thread.Sleep(200);
    }
}

// creates a new file from a template of 6 tasks. Will be used if no tasklist.txt is found
void createFile()
{
    List<Task> tempList = new List<Task>()
{
    new Task("Wash up",Convert.ToDateTime("12-29-2018"), "Active", "Home"),
    new Task("Make bed",Convert.ToDateTime("11-12-2019"), "Completed", "Home"),
    new Task("Mop floors",Convert.ToDateTime("03-04-2020"), "Active", "Home"),
    new Task("Pick apples",Convert.ToDateTime("02-16-2022"), "Completed", "Garden"),
    new Task("Plant seeds",Convert.ToDateTime("08-18-2021"), "Completed", "Garden"),
    new Task("Cut grass",Convert.ToDateTime("10-21-2017"), "Active", "Garden"),
};

    Task[] myArray = tempList.ToArray();
    File.WriteAllLines("tasklist.txt",
      Array.ConvertAll(myArray, x => x.Title.ToString() + "," + x.DueDate.ToString() + "," + x.Status.ToString() + "," + x.Project.ToString()));
}

// writes the contents of taskList to tasklist.txt
// this occurs when user quits from main menu
void writeList()
{
    Task[] myArray = taskList.ToArray();
    File.WriteAllLines("tasklist.txt",
    Array.ConvertAll(myArray, x => x.Title.ToString() + "," + x.DueDate.ToString() + "," + x.Status.ToString() + "," + x.Project.ToString()));
}

// tries to read tasks from tasklist.txt
// if no file exists then a file will be created from a template
void readList()
{
    if (File.Exists("tasklist.txt"))
    {
        string readText = File.ReadAllText("tasklist.txt");  // Read the contents of the file
    }
    else
    {
        Console.WriteLine("Specified file does not " +
                  "exist in the current directory.");
        createFile();
    }

    // Read the file and display it line by line.  
    foreach (string line in System.IO.File.ReadLines("tasklist.txt"))
    {
        string thisLine = line;
        int charPos = thisLine.IndexOf(",");
        string title = thisLine.Substring(0, charPos);
        thisLine = thisLine.Substring(charPos + 1);
        charPos = thisLine.IndexOf(",");
        string date = thisLine.Substring(0, charPos);
        DateTime dateDue = Convert.ToDateTime(date);
        thisLine = thisLine.Substring(charPos + 1);
        charPos = thisLine.IndexOf(",");
        string status = thisLine.Substring(0, charPos);
        string project = thisLine.Substring(charPos + 1);
        createTask(title, dateDue, status, project);
    }
}


// class describing how an Item should be created
class Task
{
    public Task(string title, DateTime dueDate, string status, string project)
    {
        Title = title;
        DueDate = dueDate;
        Status = status;
        Project = project;
    }

    public string Title { get; set; }
    public DateTime DueDate { get; set; }
    public string Status { get; set; }
    public string Project { get; set; }


    // method that handles formatting of rows with following colours:
    // white  - active and not overdue
    // yellow - active and is within 1 week of the due date
    // red    - active and past due date
    // green  - completed

    public void printDetails(int i)
    {
        DateTime dateInOneWeek = DateTime.Now.AddDays(7);
        int res = DateTime.Compare(DueDate, dateInOneWeek);
        if (res < 0 && this.Status == "Active")
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }

        res = DateTime.Compare(DueDate, DateTime.Now);
        if (res < 0 && this.Status == "Active")
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }

        if (this.Status == "Completed")
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }

        Console.WriteLine((i + 1).ToString().PadRight(4) + this.Title.PadRight(15) + this.DueDate.ToString("yyyy-MM-dd").PadRight(15) + this.Status.PadRight(15) + this.Project.PadRight(15));
        Console.ResetColor();
    }
}
