using Application.Common.Mappings;
using Domain.Entities;

namespace Application.StudentGroups.Queries.GetStudentGroupForTeacher;

public class GetStudentGroupDto : IMapFrom<StudentGroup>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string TeacherId { get; set; }
    public List<GetStudentGroupForTeacherStudentDto> Students { get; set; }
    public List<GetStudentGroupForTeacherAssignmentDto> Assignments { get; set; }
}