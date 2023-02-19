namespace EntityFramework.CodeGenerator
{
    public interface IProjectResolver
    {
        string GetCurrentProjectPath(); 
        string GetProjectPath(string name); 
    }
}
