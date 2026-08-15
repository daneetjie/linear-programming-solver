using LinearProgrammingSolver.Controllers;

namespace LinearProgrammingSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            SolverController controller = new SolverController();

            controller.Start();
        }
    }
}