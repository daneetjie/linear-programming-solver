namespace LinearProgrammingSolver.Models
{
    public class LinearProgram
    { //Class for storing input file
        public string Objective { get; set; }

        public double[] ObjectiveCoefficients { get; set; }

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
    { //Class for storing constraints
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