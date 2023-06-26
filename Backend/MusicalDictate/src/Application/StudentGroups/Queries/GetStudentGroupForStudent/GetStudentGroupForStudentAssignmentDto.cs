using Application.Common.Mappings;
using Domain.Entities;

namespace Application.StudentGroups.Queries.GetStudentGroupForStudent;

public class GetStudentGroupForStudentAssignmentDto : IMapFrom<Assignment>
{
    public string Id { get; set; }
    public string GraderType { get; set; }
    public GetStudentGroupForStudentExerciseDto Exercise { get; set; }
    public IEnumerable<GetStudentGroupForStudentStudentSolutionDto> StudentSolutions { get; set; }
}