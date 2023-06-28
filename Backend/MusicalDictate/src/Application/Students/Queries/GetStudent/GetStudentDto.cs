using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Students.Queries.GetStudent;

public class GetStudentDto : IMapFrom<Student>
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}