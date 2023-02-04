namespace SqlG
{
    public interface ICsTargetOutput
    {
        string? ProjectName { get; }
    }
    public class CsTargetOutput: ICsTargetOutput
    {
        public const string AnnotationKey = $"sqlg.{nameof(CsTargetOutput)}";

        public string? ProjectName { get; set; }

    }
}
