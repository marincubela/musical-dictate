using System.Xml;

namespace SimpleGrader.Interfaces;

public interface IXmlReader
{
    public XmlDocument LoadXmlDocument(string data);
}