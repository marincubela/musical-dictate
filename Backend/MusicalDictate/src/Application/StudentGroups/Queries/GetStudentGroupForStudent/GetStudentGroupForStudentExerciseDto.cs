using Application.Common.Mappings;
using Domain.Entities;

namespace Application.StudentGroups.Queries.GetStudentGroupForStudent;

public class GetStudentGroupForStudentExerciseDto : IMapFrom<Exercise>
{
    public string Id { get; set; }
    public string Title { get; set; }
    public GetStudentGroupForStudentTeacherDto Teacher { get; set; }
    public DateTime CreatedUtc { get; set; }
}