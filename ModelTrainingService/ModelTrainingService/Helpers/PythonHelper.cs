using Anotar.NLog;
using IronPython.Hosting;
using ModelTrainingService.Models;
using NLog;
using System;
using System.Diagnostics;
using System.IO;

namespace ModelTrainingService.Helpers
{
    public class PythonHelper : IPythonHelper
    {
        private readonly string _pythonPath;
        private readonly string _pythonExtractionScriptPath;
        private readonly string _pythonModelTrainingScriptPath;

        public PythonHelper(Options options)
        {
            _pythonModelTrainingScriptPath = options.ModelGenerationScriptPath;
            _pythonExtractionScriptPath = options.DataPreprocessingScriptPath;
            _pythonPath = options.PythonFullPath;
        }

        public void RunExtractionScript()
        {
            try
            {
                Process p = new Process();
                p.StartInfo = new ProcessStartInfo(_pythonPath, _pythonExtractionScriptPath)
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                p.Start();

                LogTo.Debug(p.StandardOutput.ReadToEnd());
            }
            catch (Exception ex)
            {
                LogTo.Fatal(ex.Message);
            }
        }

        public void RunModelTrainigScript()
        {
            try
            {
                Process p = new Process();
                p.StartInfo = new ProcessStartInfo(_pythonPath, _pythonModelTrainingScriptPath)
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                p.Start();

                LogTo.Debug(p.StandardOutput.ReadToEnd());
            }
            catch (Exception ex)
            {
                LogTo.Fatal(ex.Message);
            }
        }
    }
}