using System.IO;
using System.Xml.Linq;

namespace EntityFramework.CodeGenerator.Readers
{
    internal interface IXmlProjectFileReader
    {
        Project ReadFile(FileInfo projectFile, XDocument projectXml);

    }
}