using System.Collections.Generic;
using System.Text;

namespace LinearProgrammingSolver.Models
{
    public class Tableau
    {
        public double[,] Matrix { get; } // rows = 1 (objective) + constraints; cols = vars + slacks + RHS

        public int NumVariables { get; } // original decision variables (from ObjectiveCoefficients.Length)

        public int NumConstraints { get; }

        public int NumSlackVariables { get; } // == NumConstraints, one slack per constraint,
        //TODO: add functionallity for more than just slack variables

        public string Objective { get; } // max or min

        public string[] VariableTypes { get; } // carried over as metadata
        //TODO: add functionallity to use output in lp

        public List<string> ColumnHeaders { get; }

        public Tableau(LinearProgram program)
        {
            NumVariables = program.ObjectiveCoefficients.Length;
            NumConstraints = program.Constraints.Count;
            NumSlackVariables = NumConstraints;
            Objective = program.Objective;
            VariableTypes = program.VariableTypes;

            int numColumns = NumVariables + NumSlackVariables + 1;
            Matrix = new double[NumConstraints + 1, numColumns];

            for (int j = 0; j < NumVariables; j++)
            {
                Matrix[0, j] = program.ObjectiveCoefficients[j];
            }

            for (int i = 0; i < NumConstraints; i++)
            {
                var constraint = program.Constraints[i];
                int row = i + 1;

                for (int j = 0; j < NumVariables; j++)
                {
                    Matrix[row, j] = constraint.Coefficients[j];
                }

                Matrix[row, NumVariables + i] = 1;
                Matrix[row, numColumns - 1] = constraint.RightHandSide;
            }

            ColumnHeaders = BuildColumnHeaders();
        }

        private List<string> BuildColumnHeaders()
        {
            var headers = new List<string>();

            for (int j = 0; j < NumVariables; j++)
                headers.Add($"x{j + 1}");

            for (int j = 0; j < NumSlackVariables; j++)
                headers.Add($"s{j + 1}");

            headers.Add("RHS");

            return headers;
        }

        public override string ToString()
        {
            int numColumns = ColumnHeaders.Count;
            var columnWidths = new int[numColumns];

            for (int j = 0; j < numColumns; j++)
            {
                columnWidths[j] = ColumnHeaders[j].Length;
            }

            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < numColumns; j++)
                {
                    int width = Matrix[i, j].ToString().Length;
                    if (width > columnWidths[j])
                        columnWidths[j] = width;
                }
            }

            var sb = new StringBuilder();

            for (int j = 0; j < numColumns; j++)
                sb.Append(ColumnHeaders[j].PadLeft(columnWidths[j] + 2));
            sb.AppendLine();

            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < numColumns; j++)
                    sb.Append(Matrix[i, j].ToString().PadLeft(columnWidths[j] + 2));
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
