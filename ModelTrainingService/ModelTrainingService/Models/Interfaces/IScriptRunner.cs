namespace ModelTrainingService.Models.Interfaces
{
    public interface IScriptRunner
    {
        void Execute(IPythonScript script);
    }
}