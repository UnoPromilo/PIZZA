using Bogus;
using PIZZA.Enums;
using PIZZA.Models.Database;
using PIZZA.Models.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIZZA.DataAccess
{
    public class DataGenerator
    {
        Random random = new Random();
        Faker globalFaker;

        public DataGenerator(int seed = 0)
        {
            if (seed != 0)
            {
                random = new Random(seed);
                Randomizer.Seed = random;
                
            }
            globalFaker = new Faker();
        }

        public void GenerateData(int countOfUsers,
                                 int countOfTasks,
                                 int minTaskParticipants,
                                 int maxTaskParticipants,
                                 int countOfAdmins,
                                 int countOfManagers,
                                 out IEnumerable<ApplicationUser> users,
                                 out IEnumerable<ApplicationUser> admins,
                                 out IEnumerable<ApplicationUser> managers,
                                 out IEnumerable<TaskModelWithActualStateAndCreator> tasks,
                                 out IEnumerable<EmployeeTask> employeeTasks,
                                 out IEnumerable<TaskStateModel> taskHistory)
        {
            users = GenerateRandomApplicationUsers(countOfUsers);
            admins = GetRandomUsers(countOfAdmins, users);
            managers = GetRandomUsers(countOfManagers, users);

            tasks = GenerateRandomTasks(countOfTasks, managers);
            employeeTasks = GenerateEmployeeTask(tasks, users, minTaskParticipants, maxTaskParticipants);
            taskHistory = GenerateTaskStateHistory(tasks, employeeTasks);
        }


        public IEnumerable<ApplicationUser> GenerateRandomApplicationUsers(int count)
        {
            var faker = new Faker<ApplicationUser>()
                .RuleFor(o => o.FirstName, (f, u) => f.Name.FirstName())
                .RuleFor(o => o.LastName, (f, u) => f.Name.LastName())
                .RuleFor(o => o.UserName, (f, u) => f.Internet.UserName(u.FirstName, u.LastName))
                .RuleFor(o => o.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
                .RuleFor(o => o.PhoneNumber, (f, u) => f.Phone.PhoneNumber("#########"))
                .RuleFor(o => o.AddressLine, (f, u) => f.Address.StreetAddress())
                .RuleFor(o => o.PostalCode, (f, u) => f.Address.ZipCode("##-###"))
                .RuleFor(o => o.Town, (f, u) => f.Address.City());

            return faker.Generate(count);
        }

        public IEnumerable<ApplicationUser> GetRandomUsers(int count, IEnumerable<ApplicationUser> users)
        {
            return users.OrderBy(x => random.Next()).Take(count).ToList();
        }

        public IEnumerable<TaskModelWithActualStateAndCreator> GenerateRandomTasks(int count, 
                                                                                    IEnumerable<ApplicationUser> managers)
        {
            List<ApplicationUser> listOfManagers = managers.ToList();
            var tasksFaker = new Faker<TaskModelWithActualStateAndCreator>()
                .RuleFor(o => o.Deadline, (f, u) => f.Date.Between(DateTime.Now.AddDays(-300), DateTime.Now.AddDays(30)))
                .RuleFor(o => o.Priority, (f, u) => RandomPriority())
                .RuleFor(o => o.Name, (f, u) => f.Lorem.Sentence(f.Random.Number(3, 11)))
                .RuleFor(o => o.Description, (f, u) => f.Lorem.Paragraph(3))
                .RuleFor(o => o.TaskCreator, (f, u) => f.Random.ListItem(listOfManagers).ID);


            var tasks = tasksFaker.Generate(count);

            return tasks;
        }

        public IEnumerable<EmployeeTask> GenerateEmployeeTask(IEnumerable<TaskModelWithActualStateAndCreator> tasks,
                                                                                          IEnumerable<ApplicationUser> users,
                                                                                          int minimalParticipantsCount,
                                                                                          int maximalParticipantsCount)
        {
            List<EmployeeTask> employeeTask = new();
            var roles = Enum.GetValues(typeof(TaskRole)).OfType<TaskRole>().Where(o => o!=TaskRole.Creator).ToList();
            foreach (var item in tasks)
            {
                int count = random.Next(minimalParticipantsCount, maximalParticipantsCount);
                List<ApplicationUser> usersForActualIteration = users.Where(t => t.ID != item.TaskCreator).ToList();
                for(int i = 0; i < count; i++)
                {
                    var user = usersForActualIteration[random.Next(0, usersForActualIteration.Count)];
                    usersForActualIteration.Remove(user);
                    var taskRole = roles[random.Next(roles.Count())];
                    employeeTask.Add(new EmployeeTask
                    {
                        Employee = user.ToEmployeeModel(),
                        Task = item,
                        TaskRole = taskRole
                    });
                }
                employeeTask.Add(new EmployeeTask {Employee = users.Where(o=>o.ID == item.TaskCreator).FirstOrDefault().ToEmployeeModel(), Task = item, TaskRole = TaskRole.Creator });
            }

            return employeeTask;
        }

        public IEnumerable<TaskStateModel> GenerateTaskStateHistory(IEnumerable<TaskModelWithActualStateAndCreator> tasks,
                                                                        IEnumerable<EmployeeTask> taskParticipants)
        {
            List<TaskStateModel> states = new List<TaskStateModel>();
            foreach(var item in tasks)
            {
                var employees = taskParticipants.Where(o => o.Task == item && o.TaskRole != TaskRole.Visitor).ToList();
                bool dontOpen;
                bool onlyOpened;
                if (item.Deadline < DateTime.Now)
                {
                    onlyOpened = random.Next(200) == 0;
                    dontOpen = false;
                }

                else
                {
                    dontOpen = random.Next(5) == 0;
                    onlyOpened = random.Next(3) == 0;
                }
                if (!dontOpen)
                {
                    TaskStateModel openStatus = new()
                    {
                        NewTaskState = TaskState.Opened,
                        DateTime = globalFaker.Date.Between(DateTime.Now.AddDays(-300), DateTime.Now),
                        Task = item.ID,
                        Editor = globalFaker.Random.ListItem(employees).Employee.ID
                    };
                    states.Add(openStatus);

                    if (!onlyOpened)
                        states.Add(new TaskStateModel
                        {
                            NewTaskState = TaskState.Finished,
                            DateTime = globalFaker.Date.Between(openStatus.DateTime, DateTime.Now),
                            Task = item.ID,
                            Editor = globalFaker.Random.ListItem(employees).Employee.ID
                        });
                }
                
            }
            return states;
        }

        private TaskPriority RandomPriority()
        {
            var v = Enum.GetValues(typeof(TaskPriority));
            return (TaskPriority)v.GetValue(random.Next(v.Length));
        }

    }
}
