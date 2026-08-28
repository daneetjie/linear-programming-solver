namespace LinearProgrammingSolver.Models
{
    public class LinearProgram
    {
        public string Objective { get; set; }

        public double[] ObjectiveCoefficients { get; set; }

        // Support for multiple constraints
        public List<Constraint> Constraints { get; set; }

        public string[] VariableTypes { get; set; }

        public LinearProgram()
        {
            Objective = "";
            ObjectiveCoefficients = Array.Empty<double>();
            Constraints = new List<Constraint>();
            VariableTypes = Array.Empty<string>();
        }
    }

    public class Constraint
    {
        public double[] Coefficients { get; set; }

        public string Operator { get; set; } // "<=", ">=", "="

        public double RightHandSide { get; set; }

        public Constraint()
        {
            Coefficients = Array.Empty<double>();
            Operator = "";
            RightHandSide = 0;
        }

        public Constraint(double[] coefficients, string op, double rhs)
        {
            Coefficients = coefficients;
            Operator = op;
            RightHandSide = rhs;
        }
    }
}