using System.Xml;
using Microsoft.Extensions.Logging;
using SimpleGrader.Interfaces;

namespace SimpleGrader.Services;

public class XmlReader : IXmlReader
{
    private readonly ILogger<XmlReader> _logger;

    public XmlReader(ILogger<XmlReader> logger) {
        _logger = logger;
    }

    public XmlDocument LoadXmlDocument(string data)
    {
        var document = new XmlDocument();

        _logger.LogInformation("{XmlString}", data);
        
        document.LoadXml(data);

        return document;
    }
}