using System.Threading.Tasks;
using System.Xml;
using Domain.Messages;
using MassTransit;
using Microsoft.Extensions.Logging;
using SimpleGrader.Interfaces;
using SimpleGrader.Models;

namespace SimpleGrader.Consumers;

public class SolutionCreatedConsumer : IConsumer<StudentSolutionCreated>
{
    private readonly ILogger<SolutionCreatedConsumer> _logger;
    private readonly IWebApiClient _webApiClient;
    private readonly IXmlReader _xmlReader;

    public SolutionCreatedConsumer(ILogger<SolutionCreatedConsumer> logger, IWebApiClient webApiClient, IXmlReader xmlReader)
    {
        _logger = logger;
        _webApiClient = webApiClient;
        _xmlReader = xmlReader;
    }

    public async Task Consume(ConsumeContext<StudentSolutionCreated> context)
    {
        _logger.LogInformation("Received Message: {Id}, {Type}", context.Message.StudentSolutionId, context.Message.GraderType);

        if (context.Message.GraderType != "simple")
            return;

        var solution = await _webApiClient.GetStudentSolution(context.Message.StudentSolutionId);

        var studentXml = _xmlReader.LoadXmlDocument(solution.Solution.MusicXml);
        var teacherXml = _xmlReader.LoadXmlDocument(solution.Assignment.Exercise.Solution.MusicXml);

        var percentage = CompareXmls(studentXml, teacherXml);
        int grade = percentage switch
        {
            > 0.90 => 5,
            > 0.80 => 4,
            > 0.65 => 3,
            > 0.50 => 2,
            _ => 1
        };

        var result = new UpdateResultDto {StudentSolutionId = context.Message.StudentSolutionId, Comment = $"Ovo je strojni pregled: imali ste {percentage * 100}% točnih nota", Percentage = percentage, Grade = grade};

        await _webApiClient.UpdateStudentSolutionResult(result);
    }

    private double CompareXmls(XmlDocument studentXml, XmlDocument teacherXml)
    {
        XmlNodeList studentNoteElements = studentXml.DocumentElement.GetElementsByTagName("note");
        XmlNodeList teacherNoteElements = teacherXml.DocumentElement.GetElementsByTagName("note");

        int countCorrect = 0, countAll = 0;

        for (int i = 0; i < studentNoteElements.Count; i++)
        {
            var note1 = studentNoteElements.Item(i);
            var note2 = teacherNoteElements.Item(i);

            if (note1 == null || note2 == null)
                continue;

            var duration1 = note1.SelectSingleNode("duration")?.InnerText;
            var duration2 = note2.SelectSingleNode("duration")?.InnerText;

            if (note1.SelectSingleNode("rest") != null && note2.SelectSingleNode("rest") != null)
            {
                if (duration1 == duration2)
                    countCorrect++;
            }
            else if (note1.SelectSingleNode("rest") == null || note2.SelectSingleNode("rest") == null)
            {
                var pitch1 = studentNoteElements.Item(i)?.SelectSingleNode("pitch")?.InnerText;
                var pitch2 = teacherNoteElements.Item(i)?.SelectSingleNode("pitch")?.InnerText;
                if (pitch1 == pitch2 && duration1 == duration2)
                    countCorrect++;
            }

            countAll++;
        }

        if (countAll == 0)
            return 0;

        return 1.0 * countCorrect / countAll;
    }
}