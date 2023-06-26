using Application.Common.Mappings;
using Domain.Entities;

namespace Application.StudentGroups.Queries.GetStudentGroupsForTeacher;

public class GetStudentGroupsForTeacherDto : IMapFrom<StudentGroup>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string TeacherId { get; set; }
    public List<GetStudentGroupsForTeacherStudentDto> Students { get; set; }
    public List<GetStudentGroupsForTeacherAssignmentDto> Assignments { get; set; }
}