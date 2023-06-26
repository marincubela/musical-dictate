using Application.Common.Mappings;
using Domain.Entities;

namespace Application.StudentGroups.Queries.GetStudentGroupForTeacher;

public class GetStudentGroupForTeacherAssignmentDto : IMapFrom<Assignment>
{
    public string Id { get; set; }
    public string GraderType { get; set; }
    public GetStudentGroupForTeacherExerciseDto Exercise { get; set; }
}