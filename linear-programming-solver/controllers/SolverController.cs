using LinearProgrammingSolver.Models;
using LinearProgrammingSolver.Solvers;

namespace LinearProgrammingSolver.Controllers
{
    public class SolverController
    {
        private readonly FileModel fileModel;

        public SolverController()
        {
            fileModel = new FileModel();
        }

        public void Start()
        {
            string filePath = "data/input.txt";


            string[] fileLines = fileModel.ReadFile(filePath);
            string fileContents = string.Join(Environment.NewLine, fileLines);

            // Parse the linear program(s) from file
            var programs = LinearProgramParser.Parse(fileContents);


            int totalConstraints = programs.Sum(p => p.Constraints.Count);

            foreach (var program in programs)
            {


                var tableau = new Tableau(program);

                SimplexSolver.simpleSolver(tableau);
            }




        }
    }
}