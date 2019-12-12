﻿using System.Diagnostics;
using Anotar.NLog;
using ModelPredictingService.Models;

namespace ModelPredictingService.Helpers
{
    public class PredictionScript : IPythonScript
    {
        private readonly string _scriptPath;

        public PredictionScript(string scriptPath)
        {
            _scriptPath = scriptPath;
        }

        public void Run(string pythonPath)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo(pythonPath, _scriptPath)
                {
                    RedirectStandardOutput = true, UseShellExecute = false, CreateNoWindow = true
                }
            };

            process.Start();

            LogTo.Debug(process.StandardOutput.ReadToEnd());
        }
    }
}
