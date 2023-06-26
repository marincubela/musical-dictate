using Application.Common.Mappings;
using Domain.Entities;

namespace Application.StudentGroups.Queries.GetStudentGroupsForTeacher;

public class GetStudentGroupsForTeacherAssignmentDto : IMapFrom<Assignment>
{
    public string Id { get; set; }
    public string GraderType { get; set; }
    public GetStudentGroupsForTeacherExerciseDto Exercise { get; set; }
}