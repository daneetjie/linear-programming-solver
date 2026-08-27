using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinearProgrammingSolver.Models
{
    public class Tableau
    {
        public double[,] Matrix { get; } // rows = 1 (objective) + constraints; cols = vars + slacks + RHS

        public int NumVariables { get; } // original decision variables (from ObjectiveCoefficients.Length)

        public int NumConstraints { get; }

        public int NumConstraintRows { get; } // actual matrix constraint rows (an '=' constraint expands to 2)

        public int NumSlackVariables { get; } // one per <= row (and per <= half of an = row)

        public int NumExcessVariables { get; } // one per >= row (and per >= half of an = row)

        public string Objective { get; } // max or min

        public string[] VariableTypes { get; } // carried over as metadata
     

        public List<string> ColumnHeaders { get; }

        private readonly List<double[,]> history = new();

        public IReadOnlyList<double[,]> IterationHistory => history;

        private readonly struct ExpandedRow
        {
            public readonly double[] Coefficients;
            public readonly double RightHandSide;
            public readonly bool IsSlack; // true = slack (+1), false = excess (-1)

            public ExpandedRow(double[] coefficients, double rightHandSide, bool isSlack)
            {
                Coefficients = coefficients;
                RightHandSide = rightHandSide;
                IsSlack = isSlack;
            }
        }

        public Tableau(LinearProgram program)
        {
            NumVariables = program.ObjectiveCoefficients.Length;
            NumConstraints = program.Constraints.Count;
            Objective = program.Objective;
            VariableTypes = program.VariableTypes;

            var expandedRows = ExpandConstraints(WithBinaryBounds(program));
            NumConstraintRows = expandedRows.Count;
            NumSlackVariables = expandedRows.Count(r => r.IsSlack);
            NumExcessVariables = expandedRows.Count(r => !r.IsSlack);

            int numColumns = NumVariables + NumConstraintRows + 1;
            Matrix = new double[NumConstraintRows + 1, numColumns];

            for (int j = 0; j < NumVariables; j++)
            {
                Matrix[0, j] = -program.ObjectiveCoefficients[j];
            }

            for (int i = 0; i < NumConstraintRows; i++)
            {
                var expandedRow = expandedRows[i];
                int row = i + 1;
                double sign = expandedRow.IsSlack ? 1 : -1;

                for (int j = 0; j < NumVariables; j++)
                {
                    Matrix[row, j] = sign * expandedRow.Coefficients[j];
                }

                Matrix[row, NumVariables + i] = 1;
                Matrix[row, numColumns - 1] = sign * expandedRow.RightHandSide;
            }

            ColumnHeaders = BuildColumnHeaders(expandedRows);

            history.Add(CloneMatrix());
        }

        public void RecordIteration() => history.Add(CloneMatrix());

        private double[,] CloneMatrix()
        {
            var clone = new double[Matrix.GetLength(0), Matrix.GetLength(1)];
            Array.Copy(Matrix, clone, Matrix.Length);
            return clone;
        }

        // Appends an x_j <= 1 row for every variable declared 'bin' so binaries stay in [0, 1].
        private static List<Constraint> WithBinaryBounds(LinearProgram program)
        {
            var constraints = new List<Constraint>(program.Constraints);

            if (program.VariableTypes == null)
                return constraints;

            int numVariables = program.ObjectiveCoefficients.Length;

            for (int j = 0; j < program.VariableTypes.Length && j < numVariables; j++)
            {
                if (!string.Equals(program.VariableTypes[j], "bin", StringComparison.OrdinalIgnoreCase))
                    continue;

                var coefficients = new double[numVariables];
                coefficients[j] = 1;
                constraints.Add(new Constraint(coefficients, "<=", 1));
            }

            return constraints;
        }

        private static List<ExpandedRow> ExpandConstraints(List<Constraint> constraints)
        {
            var expandedRows = new List<ExpandedRow>();

            foreach (var constraint in constraints)
            {
                switch (constraint.Operator)
                {
                    case "<=":
                        expandedRows.Add(new ExpandedRow(constraint.Coefficients, constraint.RightHandSide, isSlack: true));
                        break;
                    case ">=":
                        expandedRows.Add(new ExpandedRow(constraint.Coefficients, constraint.RightHandSide, isSlack: false));
                        break;
                    case "=":
                        expandedRows.Add(new ExpandedRow(constraint.Coefficients, constraint.RightHandSide, isSlack: true));
                        expandedRows.Add(new ExpandedRow(constraint.Coefficients, constraint.RightHandSide, isSlack: false));
                        break;
                }
            }

            return expandedRows;
        }

        private List<string> BuildColumnHeaders(List<ExpandedRow> expandedRows)
        {
            var headers = new List<string>();

            for (int j = 0; j < NumVariables; j++)
                headers.Add($"x{j + 1}");

            int counter = 0;
            foreach (var expandedRow in expandedRows)
            {
                counter++;
                headers.Add(expandedRow.IsSlack ? $"s{counter}" : $"e{counter}");
            }

            headers.Add("RHS");

            return headers;
        }

        public override string ToString() => Format(ColumnHeaders, Matrix);

        public static string Format(List<string> headers, double[,] matrix)
        {
            int numColumns = headers.Count;
            var columnWidths = new int[numColumns];

            for (int j = 0; j < numColumns; j++)
            {
                columnWidths[j] = headers[j].Length;
            }

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < numColumns; j++)
                {
                    int width = matrix[i, j].ToString().Length;
                    if (width > columnWidths[j])
                        columnWidths[j] = width;
                }
            }

            var sb = new StringBuilder();

            for (int j = 0; j < numColumns; j++)
                sb.Append(headers[j].PadLeft(columnWidths[j] + 2));
            sb.AppendLine();

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < numColumns; j++)
                    sb.Append(matrix[i, j].ToString().PadLeft(columnWidths[j] + 2));
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
