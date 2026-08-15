namespace LinearProgrammingSolver.Models
{
    public class FileModel
    {
        public string ReadFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return "Error: File could not be found.";
            }

            return File.ReadAllText(filePath);
        }
    }
}