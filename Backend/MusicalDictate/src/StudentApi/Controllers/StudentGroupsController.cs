using Application.StudentGroups.Queries.GetStudentGroupForStudent;
using Application.StudentGroups.Queries.GetStudentGroupForTeacher;
using Application.StudentGroups.Queries.GetStudentGroupsForStudent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StudentApi.Controllers;

[Authorize(Roles = "Student")]
public class StudentGroupsController : ApiControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<GetStudentGroupForStudentDto>> GetStudentGroup(string id)
    {
        return Ok(await Mediator.Send(new GetStudentGroupForStudentQuery(id)));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetStudentGroupsForStudentDto>>> GetStudentGroups()
    {
        return Ok(await Mediator.Send(new GetStudentGroupsForStudentQuery()));
    }
}