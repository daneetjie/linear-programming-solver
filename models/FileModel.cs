using System;
using System.Collections.Generic;
using System.Linq;

namespace LinearProgrammingSolver.Models
{
    public class FileModel
    {
        public string[] ReadFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException($"File not found: {filePath}");
                }

                return File.ReadAllLines(filePath);
            }
            catch (Exception ex)
            {
                throw new IOException($"Error reading file '{filePath}': {ex.Message}", ex);
            }
        }
    }
}