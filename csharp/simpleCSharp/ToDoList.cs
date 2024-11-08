public partial class SimpleProjects
{
    /*
    TODO: UI * In-Progress
    TODO: input validation
    TODO: persistent data storage
    TODO: error handling
    TODO: task sorting?
    */

    public void ConsoleToDoList()
    {
        ToDoList toDoList = new();
        ConsoleKeyInfo keyPressed;

        do
        {
            Console.Clear();
            Console.WriteLine("You're using a console ToDo list\n\nCurrently, you're able to:\n\n\t(1) Add task\n\t(2) Remove task\n\t\t(3) or all of them\n\t(4) Update task\n\t(5) Display all tasks\n\t\t(6) or based on criteria\n\t(7) Update task status\n\t(8) Run example tests\n\nPress Escape to exit the program.");

            keyPressed = Console.ReadKey(true);
            Console.WriteLine();

            switch (keyPressed.Key)
            {
                case ConsoleKey.D1: // TODO: null check
                    Console.Clear();
                    Console.WriteLine("You're now in the \"Add task\" menu.");

                    Console.Write("Please enter task's title: ");
                    string title = Console.ReadLine();

                    Console.WriteLine("Please enter task's description:");
                    string description = Console.ReadLine();

                    Console.Write("Please enter task's due date(yyyy/mm/dd): ");
                    DateTime date = DateTime.Parse(Console.ReadLine());

                    Task newTask = new(title, description, TaskStatus.InProgress, date);
                    toDoList.AddTask(newTask);

                    Console.WriteLine(_pressToContinue);
                    Console.ReadKey(true);
                    break;

                case ConsoleKey.D2:
                    Console.Clear();
                    Console.WriteLine("You're now in the \"Remove task\" menu.");

                    // TODO: remove what task?

                    Console.WriteLine(_pressToContinue);
                    Console.ReadKey(true);
                    break;

                case ConsoleKey.D3:

                    Console.Clear();
                    Console.WriteLine("You're now in the \"Remove all tasks\" menu.\n");
                    bool exit = false;

                    Console.WriteLine("Please confirm that you want to remove all tasks (y/n):");
                    do
                    {
                        keyPressed = Console.ReadKey();
                        Console.WriteLine();

                        if (keyPressed.Key == ConsoleKey.Y)
                        {
                            toDoList.RemoveAllTasks();
                            Console.WriteLine("All tasks removed.");
                            exit = true;
                        }

                        else if (keyPressed.Key == ConsoleKey.N)
                        {
                            Console.WriteLine("Returning to the menu.");
                            exit = true;
                        }

                        else
                            Console.WriteLine("Please press the correct button (y/n).");
                    }
                    while (!exit);


                    Console.WriteLine(_pressToContinue);
                    Console.ReadKey(true);
                    break;

                case ConsoleKey.D4:
                    Console.Clear();
                    Console.WriteLine("You're now in the \"Remove task\" menu.");

                    Console.WriteLine(_pressToContinue);
                    Console.ReadKey(true);
                    break;

                case ConsoleKey.D5:
                    Console.Clear();
                    Console.WriteLine("You're now in the \"Display all tasks\" menu.");

                    Console.WriteLine(_pressToContinue);
                    Console.ReadKey(true);
                    break;

                case ConsoleKey.D6:
                    Console.Clear();
                    Console.WriteLine("You're now in the \"Display tasks by criteria\" menu.");

                    // TODO: By id? By status? Or implement title/due date as well?

                    Console.WriteLine(_pressToContinue);
                    Console.ReadKey(true);
                    break;

                case ConsoleKey.D7:
                    Console.Clear();
                    Console.WriteLine("You're now in the \"Update task status\" menu.");

                    // TODO: update what task?

                    Console.WriteLine(_pressToContinue);
                    Console.ReadKey(true);
                    break;

                case ConsoleKey.D8:
                    {
                        Console.Clear();
                        Console.WriteLine("You're now in the \"Run example tests\" menu.\n");
                        Thread.Sleep(500);

                        ExampleTest();

                        Console.WriteLine("\n" + _pressToContinue);
                        Console.ReadKey(true);
                    }
                    break;
            }
        } while (keyPressed.Key != ConsoleKey.Escape);
    }

    public static void ExampleTest() // TODO: showcase UpdateTask method
    {
        ToDoList exampletoDoList = new();

        Task task1 = new("Buy groceries", "Milk, eggs, bread", TaskStatus.InProgress, DateTime.Now.AddDays(1));
        Task task2 = new("Finish report", "Complete the quarterly report", TaskStatus.InProgress, DateTime.Now.AddDays(2));

        Console.WriteLine("Adding two example tasks.\n");
        exampletoDoList.AddTask(task1);
        exampletoDoList.AddTask(task2);

        Console.WriteLine("All tasks:\n");
        exampletoDoList.DisplayAllTasks();

        Console.WriteLine("Removing second task.\n");
        exampletoDoList.RemoveTask(task2);

        Console.WriteLine("All tasks:\n");
        exampletoDoList.DisplayAllTasks();

        Console.WriteLine("\nUpdating status for first task to Completed.\n");
        Task? completedTask = exampletoDoList.GetTaskById(0);
        completedTask?.UpdateStatus(TaskStatus.Completed);

        Console.WriteLine("\nCompleted tasks:\n");
        foreach (var task in exampletoDoList.GetTasksByStatus(TaskStatus.Completed))
            task.Display();

        Console.WriteLine("Clearing the ToDo list.");
        exampletoDoList.RemoveAllTasks();
    }

    public class ToDoList
    {
        private readonly List<Task> _tasks;

        public ToDoList()
        {
            _tasks = [];
        }

        public void AddTask(Task task)
        {
            _tasks.Add(task);
        }
        public void RemoveTask(Task task)
        {
            _tasks.Remove(task);
        }
        public void RemoveAllTasks()
        {
            _tasks.Clear();
            Task.ResetCounter();
        }
        public void DisplayAllTasks()
        {
            foreach (var task in _tasks)
            {
                task.Display();
            }
        }
        public IReadOnlyList<Task> GetTasksByStatus(TaskStatus status)
        {
            return _tasks.Where(task => task.Status == status).ToList().AsReadOnly();
        }
        public Task? GetTaskById(int id)
        {
            return _tasks.First(task => task.Id == id);
        }
    }

    public class Task(string title, string description, TaskStatus status, DateTime dueDate)
    {
        private static int s_idCounter = 0;
        public int Id { get; set; } = Interlocked.Increment(ref s_idCounter); // ? was it necessary?
        public string Title { get; private set; } = title; // TODO: validation
        public string Description { get; private set; } = description;
        public TaskStatus Status { get; set; } = status;
        public DateTime DueDate { get; private set; } = dueDate; // TODO: validation

        public static void ResetCounter()
        {
            s_idCounter = 0;
        }
        public void UpdateTask(string? newTitle = null, string? newDesc = null, DateTime? newDue = null)
        {
            if (!string.IsNullOrEmpty(newTitle))
                Title = newTitle;

            if (!string.IsNullOrEmpty(newDesc))
                Description = newDesc;

            if (newDue.HasValue)
                DueDate = newDue.Value;
        }
        public void UpdateStatus(TaskStatus newStatus)
        {
            Status = newStatus;
        }
        public void Display() // TODO: null check, enum formatting?
        {
            Console.WriteLine("Task Details:");
            Console.WriteLine($"ID       : {Id}");
            Console.WriteLine($"Title    : {Title}");
            Console.WriteLine($"Due Date : {DueDate.ToShortDateString()}");
            Console.WriteLine($"Status   : {Status}");
            Console.WriteLine($"Description:\n{Description}");
            Console.WriteLine("=====================================\n");
        }
    }
    public enum TaskStatus
    {
        InProgress,
        Completed,
        Failed
    }
}
