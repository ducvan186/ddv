using Ex04.StudentManagement.Managers;

namespace Ex04.StudentManagement
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var menuManager = new MenuManager();
            menuManager.Run();
        }
    }
}