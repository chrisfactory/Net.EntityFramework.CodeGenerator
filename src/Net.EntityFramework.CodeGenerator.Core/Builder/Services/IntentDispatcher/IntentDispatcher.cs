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

        private void Dispatch()
        {
            foreach (var package in _intentProvider.Get())
            {
                foreach (var intent in package.Intents)
                {
                    if (intent.Target is IDataBaseProjectTarget dbProjTarget)
                    {
                        if (intent.Target is ITableTarget tableTarget)
                        {

                        }
                        else if (intent.Target is IIndexTarget indexTarget)
                        {

                        }
                        else if (intent.Target is ISequenceTarget sequenceTarget)
                        {

                        }
                        else if (intent.Target is IEnsureSchemaTarget ensureSchemaTarget)
                        {

                        }
                        else if (intent.Target is IDbProjSpSelectTarget spSelect)
                        {

                        }
                    }
                    else if (intent.Target is IServiceProjectTarget serviceProjTarget)
                    {


                    }
                    else if (intent.Target is IDbServiceBuilderTarget serviceBuilderTarget)
                    {
                        if (intent.Target is IDbServiceSpSelectTarget spSelect)
                        {

                        }
                    }
                }
            }
        }
    }
}
