namespace SqlG
{
    public interface IProjectResolver
    {
        string GetCurrentProjectPath();

        string GetProjectPath(string name);

    }
}
