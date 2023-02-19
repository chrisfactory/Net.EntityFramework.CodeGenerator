namespace EntityFramework.CodeGenerator
{
    internal class CurrentCSProjectResolver : IProjectResolver
    {
        private readonly IReadOnlyDictionary<string, DirectoryInfo> _projects;
        private string _currentProjectName;
        public CurrentCSProjectResolver()
        {
            var projects = new Dictionary<string, DirectoryInfo>();

            var proj = FindCurrentProject();
            if (proj != null && proj.Directory != null)
            {
                Console.WriteLine($"Current project found: {proj.Name}");
                _currentProjectName = proj.Name.Replace(proj.Extension,"");
                projects.Add(_currentProjectName, proj.Directory);

                var projectRootElement = Project.Construct(proj);
                foreach (var property in projectRootElement.Items)
                {
                    if (property.ItemType == "ProjectReference")
                    {
                        var fi = new FileInfo(property.ResolvedIncludePath);
                        if(fi.Directory != null)
                        {
                            var name = fi.Name.Replace(fi.Extension, "");
                            Console.WriteLine($"Project reference found: {name}");
                            projects.Add(name, fi.Directory);
                        }
                    }

                }
            }
            else
                throw new InvalidOperationException();

            _projects = projects;
        }
        public string GetCurrentProjectPath()
        {
            return _projects[_currentProjectName].FullName;
        }

        public string GetProjectPath(string name)
        {
            return _projects[name].FullName;
        }



        private static FileInfo? FindCurrentProject()
        {
            var currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
            var projects = currentDirectory.GetFiles("*.csproj");
            while (projects.Length == 0)
            {

                currentDirectory = currentDirectory.Parent;
                if (currentDirectory == null)
                    return null;

                projects = currentDirectory.GetFiles("*.csproj");
            }
            if (projects.Length > 1)
            {
                return null;
            }
            return projects[0];
        }
    }
}
