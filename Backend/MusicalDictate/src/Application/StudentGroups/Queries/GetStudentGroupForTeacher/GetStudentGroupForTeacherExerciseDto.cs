using Application.Common.Mappings;
using Domain.Entities;

namespace Application.StudentGroups.Queries.GetStudentGroupForTeacher;

public class GetStudentGroupForTeacherExerciseDto : IMapFrom<Exercise>
{
    public string Id { get; set; }
    public string Title { get; set; }
    public GetStudentGroupForTeacherTeacherDto Teacher { get; set; }
    public DateTime CreatedUtc { get; set; }
}