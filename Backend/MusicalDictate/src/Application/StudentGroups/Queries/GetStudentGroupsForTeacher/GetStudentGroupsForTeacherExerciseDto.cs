using Application.Common.Mappings;
using Domain.Entities;

namespace Application.StudentGroups.Queries.GetStudentGroupsForTeacher;

public class GetStudentGroupsForTeacherExerciseDto : IMapFrom<Exercise>
{
    public string Id { get; set; }
    public string Title { get; set; }
}