using Anotar.NLog;
using IronPython.Hosting;
using ModelTrainingService.Models;
using System;

namespace ModelTrainingService.Helpers
{
    public class PythonHelper : IPythonHelper
    {
        private readonly string _scriptPath;

        public PythonHelper(Options options)
        {
            _scriptPath = options.ScriptPath;
        }

        public void RunScript()
        {
            try
            {
                var engine = Python.CreateEngine();
                var scope = engine.CreateScope();
                var source = engine.CreateScriptSourceFromFile(_scriptPath);
                source.Execute(scope);
            }
            catch (Exception ex)
            {
                LogTo.Debug(ex.Message);
            }
        }
    }
}