using System;
using System.Linq;
using System.Reflection;
using DbUp;

namespace TodoDbUp
{
    class Program
    {
        static int Main(string[] args)
        {
            var connectionString = 
                args.FirstOrDefault() ?? 
                "Host=localhost;Port=5432;Database=todo;Username=admin;Password=mypassword;";

            EnsureDatabase.For.PostgresqlDatabase(connectionString);

            var upgrader =
                DeployChanges.To
                    .PostgresqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogToConsole()
                    .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
#if DEBUG
                Console.ReadLine();
#endif                
                return -1;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
            return 0;
        }
    }
}
