using ModelPredictingService.Models;
using System;

namespace ModelPredictingService.Helpers
{
    public class ScriptRunner : IScriptRunner
    {
        private readonly string _pythonPath;

        public ScriptRunner(Options options)
        {
            _pythonPath = options.PythonFullPath;
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