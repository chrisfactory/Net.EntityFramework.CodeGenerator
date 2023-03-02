namespace EntityFramework.CodeGenerator.Core
{
    public interface IPackageTarget
    {
        //ITargetInfos? TargetInfos { get; } 
    }

    //public interface ITargetInfos
    //{
    //    string? ProjectName { get; }
    //    string? Folder { get; }
    //    string? FileName { get; }

    //}
    public interface IServiceProjectTarget : IPackageTarget
    {

    }

    public interface IDbServiceTarget : IServiceProjectTarget
    {

    }
}
