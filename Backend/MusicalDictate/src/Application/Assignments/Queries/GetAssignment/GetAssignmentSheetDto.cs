using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Assignments.Queries.GetAssignment;

public class GetAssignmentSheetDto : IMapFrom<Sheet>
{
    public string Id { get; set; }
    public string MusicXml { get; set; }
}