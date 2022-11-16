//List that adds and contains 6 Items

//List<Task> tempList = new List<Task>()
//{
//    new Task("Wash up",Convert.ToDateTime("12-29-2018"), "Active", "Home"),
//    new Task("Make bed",Convert.ToDateTime("11-12-2019"), "Completed", "Home"),
//    new Task("Mop floors",Convert.ToDateTime("03-04-2020"), "Active", "Home"),
//    new Task("Pick apples",Convert.ToDateTime("02-16-2022"), "Completed", "Garden"),
//    new Task("Plant seeds",Convert.ToDateTime("08-18-2021"), "Completed", "Garden"),
//    new Task("Cut grass",Convert.ToDateTime("10-21-2017"), "Active", "Garden"),
//};

//Task[] myArray = tempList.ToArray();
//File.WriteAllLines("tasklist.txt",
//  Array.ConvertAll(myArray, x => x.Title.ToString() + "," + x.DueDate.ToString() + "," + x.Status.ToString() + "," + x.Project.ToString()));

List<Task> taskList = new List<Task>(); // initialises taskList 
readList(); // reads in tasklist.txt file and puts the contents into <Task>taskList

bool loopValue = true;
while (loopValue == true)
{
    titleText();
    menuText();
    string data = Console.ReadLine();
    bool isInt = int.TryParse(data, out int value);
    if (isInt && value >= 1 && value <= 4 && data != null)
    {
        loopValue = menuChoice(data);
    }
    else
    {
        Console.WriteLine("Invalid choice!");
    }
    Thread.Sleep(500);
}

void titleText() {
    Console.Clear();
    Console.WriteLine("Welcome to ToDoLy\n");
    IEnumerable<Task> active = taskList.Where(task => task.Status == "Active");
    IEnumerable<Task> completed = taskList.Where(task => task.Status == "Completed");
    Console.WriteLine("You have " +active.Count() + " active tasks and "+ completed.Count() + " tasks are completed");
    Console.WriteLine("-------------------------------------------------\n");
}

void menuText()
{
    Console.WriteLine("Please pick an option:\n");
    Console.Write("(");
    purpleText("1");
    Console.WriteLine(") Show Task List (by date or project)");
    Console.Write("(");
    purpleText("2");
    Console.WriteLine(") Add New Task");
    Console.Write("(");
    purpleText("3");
    Console.WriteLine(") Edit Task (update, mark as done, remove)");
    Console.Write("(");
    purpleText("4");
    Console.WriteLine(") Save and Quit");
}

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

void purpleText(string text)
{
    Console.ForegroundColor = ConsoleColor.DarkMagenta;
    Console.Write(text);
    Console.ResetColor();
}

// function which allows a user to add an Task to taskList 
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
    purpleText("1");
    Console.WriteLine(") Today's date");
    Console.Write("(");
    purpleText("2");
    Console.WriteLine(") Enter a different date");
    var data = Console.ReadLine();
    bool isInt = int.TryParse(data, out int value);
    DateTime date = DateTime.Now;
    if (isInt && value >= 1 && value <= 2)
    {
        switch (value) // handles menu choices. 
        {
            case 1: // make a date from today's date
                Console.WriteLine("\nToday's date chosen");
                Thread.Sleep(1000);
                break;
            case 2: // make a date from user input. 
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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid date!");
                    Console.ResetColor();
                    Thread.Sleep(1000);
                    goto loopStart;
                }
        }
    }

    Console.WriteLine("\nSelect status:\n");
    Console.Write("(");
    purpleText("1");
    Console.WriteLine(") Active");
    Console.Write("(");
    purpleText("2");
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
                titleText();
                listHeader();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(taskIndex.ToString().PadRight(4) + task.Title.PadRight(15) + task.DueDate.ToString("yyyy-MM-dd").PadRight(15) + task.Status.PadRight(15) + task.Project.PadRight(15));
                Console.ResetColor();
                Console.Write("\n(");
                purpleText("1");
                Console.WriteLine(") Change title");
                Console.Write("(");
                purpleText("2");
                Console.WriteLine(") Flip Status");
                Console.Write("(");
                purpleText("3");
                Console.WriteLine(") Delete task");

                Console.WriteLine("\nChoose from menu (q) to return to previous page: ");
                int value = 0;
                data = Console.ReadLine();
                isInt = int.TryParse(data, out value);
                if (isInt && value >= 1 && value <= 3)
                {
                    switch(value)
                    {
                        case 1:
                            Console.WriteLine("Enter a new title");
                            data = Console.ReadLine();
                            if(data != null && data.Length>0)
                            {
                                taskList[taskIndex - 1].Title = data;
                            }
                            else
                            {
                                Console.WriteLine("Title can't be blank!");
                                Thread.Sleep(200);
                            }
                            break;
                        case 2:
                            if(task.Status == "Active")
                            {
                                taskList[taskIndex - 1].Status = "Completed";
                            }
                            else
                            {
                                taskList[taskIndex-1].Status = "Active";
                            }
                            break;
                        case 3:
                            Console.Write("\nAre you sure you want to delete task '");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write(task.Title);
                            Console.ResetColor();
                            Console.Write("'?");
                            Console.WriteLine("\nConfirm with (y) or any other key to return" );
                            data = Console.ReadLine();
                            if (data.ToLower() == "y")
                            {
                                taskList.RemoveAt(taskIndex - 1);
                                Console.WriteLine("The task has been deleted!");
                                Thread.Sleep(400);
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

void listHeader() {
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("    " + "Title".PadRight(15) + "Due Date".PadRight(15) + "Status".PadRight(15) + "Project".ToString().PadRight(15));
    Console.ResetColor();
    Console.WriteLine("    " + "-----".PadRight(15) + "--------".PadRight(15) + "------".PadRight(15) + "-------".PadRight(15));
}

// Function that allows a user to create an Task and adds it to taskList
void createTask(string title, DateTime date, string status, string project)
{
    taskList.Add(new Task(title,date, status, project));
}

// Fuction that loops through a sorted taskList to display all Tasks
void displayTasks(int order = 1, string sortedBy = "date ascending")
{
    while (true)
    {
        titleText();
        
        Console.Write("Sort options:\n\n(");
        purpleText("1");
        Console.Write(") date asc | (");
        purpleText("2");
        Console.Write(") date desc | (");
        purpleText("3");
        Console.Write(") project asc | (");
        purpleText("4");
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

void writeList()
{
    Task[] myArray = taskList.ToArray();
    File.WriteAllLines("tasklist.txt",
    Array.ConvertAll(myArray, x => x.Title.ToString() + "," + x.DueDate.ToString() + "," + x.Status.ToString() + "," + x.Project.ToString()));
}

void readList()
{
    string readText = File.ReadAllText("tasklist.txt");  // Read the contents of the file
    //Console.WriteLine(readText);  // Output the content

    // Read the file and display it line by line.  
    foreach (string line in System.IO.File.ReadLines("tasklist.txt"))
    {
        string thisLine = line;
        //Console.WriteLine(thisLine);
        int charPos = thisLine.IndexOf(",");
        string title = thisLine.Substring(0, charPos);
        //Console.WriteLine(title);
        thisLine = thisLine.Substring(charPos + 1);
        charPos = thisLine.IndexOf(",");
        string date = thisLine.Substring(0, charPos);
        DateTime dateDue = Convert.ToDateTime(date);
        //Console.WriteLine(date);
        //Console.WriteLine(dateDue);
        thisLine = thisLine.Substring(charPos + 1);
        charPos = thisLine.IndexOf(",");
        string status = thisLine.Substring(0, charPos);
        //Console.WriteLine(status);
        string project = thisLine.Substring(charPos + 1);
        //Console.WriteLine(project);
        createTask(title, dateDue, status, project);
    }
}

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

    public void printDetails(int i)
    {
        DateTime dateInThreeMonths = DateTime.Now.AddMonths(3);
        //TimeSpan diff = dateInThreeMonths - DueDate;
        int res = DateTime.Compare(DueDate, dateInThreeMonths);
        if (res < 0 && this.Status == "Active")
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }

        //TimeSpan diff2 = DateTime.Now - DueDate;
        res = DateTime.Compare(DueDate, DateTime.Now);
        if (res < 0 && this.Status == "Active")
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }

        if (this.Status == "Completed")
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }

        Console.Write((i + 1).ToString().PadRight(4));
        Console.WriteLine(this.Title.PadRight(15) + this.DueDate.ToString("yyyy-MM-dd").PadRight(15) + this.Status.PadRight(15) + this.Project.PadRight(15));
        Console.ResetColor();
    }
}
