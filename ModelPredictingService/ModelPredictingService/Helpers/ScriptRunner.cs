using ModelPredictingService.Models;
using System;

namespace ModelPredictingService.Helpers
{
    public class ScriptRunner
    {
        private readonly string _pythonPath;

        public ScriptRunner(string pythonPath)
        {
            _pythonPath = pythonPath;
        }

        public void Execute(IPythonScript script)
        {
            try
            {
                script.Run(_pythonPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}