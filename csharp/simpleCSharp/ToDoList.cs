public partial class SimpleProjects
{
    public void ConsoleToDoList()
    {
        ToDoList toDoList = new();

        Task task1 = new("Buy groceries", "Milk, eggs, bread", TaskStatus.InProgress, DateTime.Now.AddDays(1));
        Task task2 = new("Finish report", "Complete the quarterly report", TaskStatus.InProgress, DateTime.Now.AddDays(2));

        toDoList.AddTask(task1);
        toDoList.AddTask(task2);

        Console.WriteLine("All tasks:");
        toDoList.DisplayAllTasks();

        Task? completedTask = toDoList.GetTaskById(0);
        completedTask?.UpdateStatus(TaskStatus.Completed);

        Console.WriteLine("Completed tasks:");
        foreach (var task in toDoList.GetTasksByStatus(TaskStatus.Completed))
            task.Display();
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
        private static int s_idCounter = 0; // TODO: make it thread-safe?
        public int Id { get; set; } = s_idCounter++;
        public string Title { get; private set; } = title; // TODO: validation
        public string Description { get; private set; } = description;
        public TaskStatus Status { get; set; } = status;
        public DateTime DueDate { get; private set; } = dueDate; // TODO: validation

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
            Console.WriteLine("=====================================");
        }
    }
    public enum TaskStatus
    {
        InProgress,
        Completed,
        Failed
    }
}
