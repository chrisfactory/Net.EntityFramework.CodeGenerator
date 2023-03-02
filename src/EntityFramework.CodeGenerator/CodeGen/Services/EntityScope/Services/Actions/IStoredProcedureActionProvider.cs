using EntityFramework.CodeGenerator.Core;
namespace EntityFramework.CodeGenerator
{
    public interface IStoredProcedureActionProvider : IActionProvider
    {
        IStoredProcedureCallerProvider CallerActionProvider { get; }
    }

  
    public interface IStoredProcedureCallerProvider
    {
        IEnumerable<IContentFileSegment> Get();
    }

     

    public interface ICodeAction
    {
        IContentFileSegment Segment { get; }
    }

    internal class StoredProcedureActionProvider : IStoredProcedureActionProvider
    {
        private readonly IActionProvider _codeAction;
        public StoredProcedureActionProvider(IActionProvider codeGenAction, IStoredProcedureCallerProvider callerActionProvider)
        {
            _codeAction = codeGenAction;
            CallerActionProvider = callerActionProvider;
        }
        public IStoredProcedureCallerProvider CallerActionProvider { get; }
        public IEnumerable<IAction> Get() => _codeAction.Get();

    }
}
