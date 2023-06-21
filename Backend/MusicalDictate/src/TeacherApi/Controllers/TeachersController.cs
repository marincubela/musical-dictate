﻿using Application.Teachers.Commands.CreateTeacher;
using Application.Teachers.Queries.GetTeacher;
using Application.Teachers.Queries.GetTeachers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TeacherApi.Controllers;

[Authorize(Roles = "Teacher")]
public class TeachersController : ApiControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<GetTeacherDto>> GetTeacher(string id)
    {
        return Ok(await Mediator.Send(new GetTeacherQuery(id)));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetTeacherDto>>> GetTeachers()
    {
        return Ok(await Mediator.Send(new GetTeachersQuery()));
    }

    [HttpPost]
    public async Task<ActionResult<string>> CreateTeacher([FromBody] CreateTeacherCommand command)
    {
        var id = await Mediator.Send(command);

        return new CreatedAtActionResult(nameof(GetTeacher),
            "Teachers",
            new {id},
            id);
    }
}