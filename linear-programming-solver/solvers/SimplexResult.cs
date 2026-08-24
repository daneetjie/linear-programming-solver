using System;
using System.Collections.Generic;
using System.Text;

namespace LinearProgrammingSolver.Solvers
{
    public class SimplexResult
    {

        public Dictionary<string, double> Solution { get; set;}
        public int[] Basis { get; set;  }
    }
}
