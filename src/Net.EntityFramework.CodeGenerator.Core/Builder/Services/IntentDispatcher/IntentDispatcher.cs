using System.Text;

namespace Net.EntityFramework.CodeGenerator.Core
{
    internal class IntentDispatcher : IIntentDispatcher
    {
        private readonly IPackageModuleIntentsProvider _intentProvider;
        public IntentDispatcher(IPackageModuleIntentsProvider intentProvider)
        {
            _intentProvider = intentProvider;
            Dispatch();
        }
        public async Task CreateFileAsync(IContentFile file, CancellationToken token)
        {
            var sb = new StringBuilder();
            sb.AppendLine("-- Autogen");
            file.ContentBuilder.Build(sb);
            var content = sb.ToString();

            file.FileInfo.Directory?.Create();
            await File.WriteAllTextAsync(file.FileInfo.FullName, content, token);
        }

        private void Dispatch()
        {
            foreach (var package in _intentProvider.Get())
            {
                foreach (var intent in package.Intents)
                {

                    if (intent.Target is IDataProjectTarget dbProjTarget)
                    {
                        foreach (var content in intent.Contents)
                        {
                            if (content is IContentFile contentFile)
                            {
                                CreateFileAsync(contentFile, CancellationToken.None);
                            }
                        }

                    }
                    //else if (intent.Target is IServiceProjectTarget serviceProjTarget)
                    //{


                    //}
                    //else if (intent.Target is IDbServiceBuilderTarget serviceBuilderTarget)
                    //{
                    //    if (intent.Target is IDbServiceSpSelectTarget spSelect)
                    //    {

                    //    }
                    //}
                }
            }
        }
    }


}
