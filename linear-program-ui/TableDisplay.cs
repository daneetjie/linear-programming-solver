using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using LinearProgrammingSolver.Models;

namespace linear_programming_solver.UI
{
    public static class TableDisplay
    {
        public static void PopulateTableau(DataGridView dgv, Tableau tableau, double[,] matrix)
        {
            dgv.Columns.Clear();
            dgv.Rows.Clear();

            dgv.Columns.Add("RowLabel", "");
            foreach(var header in tableau.ColumnHeaders)
            dgv.Columns.Add(header, header);

            int numRows = matrix.GetLength(0);
            int numCols = matrix.GetLength(1);

            for (int i = 0; i < numRows; i++)
            {
                var rowValues = new object[numCols + 1];
                rowValues[0] = i == 0 ? "Z" : $"C{i}";

                for (int j = 0; j < numCols; j++)
                {
                    rowValues[j + 1] = matrix[i, j];
                 
                }
                dgv.Rows.Add(rowValues);
            }

                dgv.AutoResizeColumns();
                dgv.ReadOnly = true;


            }


        public static string FormatAllIterations(Tableau tableau, string heading = null)
        {
            var sb = new StringBuilder();
            if (!string.IsNullOrEmpty(heading))
            {
                sb.AppendLine(heading);
                sb.AppendLine();
            }

            for (int i = 0; i < tableau.IterationHistory.Count; i++)
            {
                string title = i == 0 ? "Initial Tableau" : $"Iteration {i}";
                sb.AppendLine(title);
                sb.AppendLine(Tableau.Format(tableau.ColumnHeaders, tableau.IterationHistory[i]));
                sb.AppendLine(new string('=', 50));
            }
            return sb.ToString();
        }
    }
}
