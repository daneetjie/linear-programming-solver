using LinearProgrammingSolver.Models;
using LinearProgrammingSolver.Views;

namespace LinearProgrammingSolver.Controllers
{
    public class SolverController
    {
        private readonly FileModel fileModel;
        private readonly ConsoleView consoleView;

        public SolverController()
        {
            fileModel = new FileModel();
            consoleView = new ConsoleView();
        }

        public void Start()
        {
            string filePath = "Data/input.txt";

            string fileContents = fileModel.ReadFile(filePath);

            consoleView.DisplayMessage(fileContents);
        }
    }
}