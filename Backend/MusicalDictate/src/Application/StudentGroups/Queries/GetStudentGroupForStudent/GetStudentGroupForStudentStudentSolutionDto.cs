using Application.Common.Mappings;
using Domain.Entities;

namespace Application.StudentGroups.Queries.GetStudentGroupForStudent;

public class GetStudentGroupForStudentStudentSolutionDto : IMapFrom<StudentSolution>
{
    public string Id { get; set; }
}