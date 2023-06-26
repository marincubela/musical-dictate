using Application.Common.Mappings;
using Domain.Entities;

namespace Application.StudentGroups.Queries.GetStudentGroupsForTeacher;

public class GetStudentGroupsForTeacherStudentDto : IMapFrom<Student>
{
    public string Id { get; set; }
    public string Jmbag { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NameClass { get; set; }
}