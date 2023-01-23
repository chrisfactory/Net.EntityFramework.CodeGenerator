using System.IO;
using System.Xml.Linq;

namespace SqlG.Readers
{
    internal interface IXmlProjectFileReader
    {
        Project ReadFile(FileInfo projectFile, XDocument projectXml);

    }
}