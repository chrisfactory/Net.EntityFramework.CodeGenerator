namespace EntityFramework.CodeGenerator.Core
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
            foreach (var module in _intentProvider.Get())
            {
                foreach (var intent in module.Intents)
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
                    }
                    else if (intent.Target is IServiceProjectTarget serviceProjTarget)
                    {
                       
                      
                    }
                }
            }
        }
    }
}
