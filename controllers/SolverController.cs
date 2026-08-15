using LinearProgrammingSolver.Models;
using LinearProgrammingSolver.Views;
using LinearProgrammingSolver.Controllers;

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

            try
            {
                string[] fileLines = fileModel.ReadFile(filePath);
                string fileContents = string.Join(Environment.NewLine, fileLines);

                // Parse the linear program(s) from file
                var programs = LinearProgramParser.Parse(fileContents);

                // Display results
                consoleView.DisplayMessage($"Successfully parsed {programs.Count} constraint(s)\n");

                foreach (var program in programs)
                {
                    DisplayProgram(program);
                }
            }
            catch (Exception ex)
            {
                consoleView.DisplayMessage($"Error: {ex.Message}");
            }
        }

        private void DisplayProgram(LinearProgram program)
        {
            consoleView.DisplayMessage($"Objective: {program.Objective.ToUpper()}");
            consoleView.DisplayMessage($"Coefficients: {string.Join(", ", program.ObjectiveCoefficients)}");

            for (int i = 0; i < program.Constraints.Count; i++)
            {
                var constraint = program.Constraints[i];
                consoleView.DisplayMessage($"\nConstraint {i + 1}:");
                consoleView.DisplayMessage($"  Coefficients: {string.Join(", ", constraint.Coefficients)}");
                consoleView.DisplayMessage($"  Operator: {constraint.Operator}");
                consoleView.DisplayMessage($"  RHS: {constraint.RightHandSide}");
            }

            consoleView.DisplayMessage($"\nVariable Types: {string.Join(", ", program.VariableTypes)}\n");
        }
    }
}