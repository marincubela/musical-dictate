using Application.StudentGroups.Commands.CreateStudentGroup;
using Application.StudentGroups.Commands.DeleteStudentGroup;
using Application.StudentGroups.Commands.UpdateStudentGroup;
using Application.StudentGroups.Queries.GetStudentGroupForStudent;
using Application.StudentGroups.Queries.GetStudentGroupForTeacher;
using Application.StudentGroups.Queries.GetStudentGroupsForStudent;
using Application.StudentGroups.Queries.GetStudentGroupsForTeacher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class StudentGroupsController : ApiControllerBase
{
    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<GetStudentGroupDto>> GetStudentGroup(string id)
    {
        return Ok(await Mediator.Send(new GetStudentGroupForTeacherQuery(id)));
    }

    [Authorize(Roles = "Teacher")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetStudentGroupsForTeacherDto>>> GetStudentGroups()
    {
        return Ok(await Mediator.Send(new GetStudentGroupsForTeacherQuery()));
    }

    [Authorize]
    [HttpGet("my/{id}")]
    public async Task<ActionResult<GetStudentGroupForStudentDto>> GetMyStudentGroup(string id)
    {
        return Ok(await Mediator.Send(new GetStudentGroupForStudentQuery(id)));
    }

    [Authorize(Roles = "Student")]
    [HttpGet("my")]
    public async Task<ActionResult<IEnumerable<GetStudentGroupsForStudentDto>>> GetMyStudentGroups()
    {
        return Ok(await Mediator.Send(new GetStudentGroupsForStudentQuery()));
    }

    [Authorize(Roles = "Teacher")]
    [HttpPost]
    public async Task<ActionResult<string>> CreateStudentGroup([FromBody] CreateStudentGroupCommand command)
    {
        var id = await Mediator.Send(command);

        return new CreatedAtActionResult(nameof(GetStudentGroup),
            "StudentGroups",
            new {id},
            id);
    }

    [Authorize(Roles = "Teacher")]
    [HttpPut]
    public async Task<ActionResult> UpdateStudentGroup([FromBody] UpdateStudentGroupCommand command)
    {
        await Mediator.Send(command);

        return NoContent();
    }

    [Authorize(Roles = "Teacher")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteStudentGroup(string id)
    {
        await Mediator.Send(new DeleteStudentGroupCommand(id));

        return NoContent();
    }
}