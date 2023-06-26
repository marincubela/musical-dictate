using Application.Common.Mappings;
using Domain.Entities;

namespace Application.StudentGroups.Queries.GetStudentGroupForStudent;

public class GetStudentGroupForStudentDto : IMapFrom<StudentGroup>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string TeacherId { get; set; }
    public List<GetStudentGroupForStudentStudentDto> Students { get; set; }
    public List<GetStudentGroupForStudentAssignmentDto> Assignments { get; set; }
}