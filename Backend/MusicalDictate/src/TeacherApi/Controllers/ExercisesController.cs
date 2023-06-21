using Application.Exercises.Commands.CreateExercise;
using Application.Exercises.Commands.DeleteExercise;
using Application.Exercises.Commands.UpdateExercise;
using Application.Exercises.Queries.GetExercise;
using Application.Exercises.Queries.GetExercises;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TeacherApi.Controllers;

[Authorize(Roles = "Teacher")]
public class ExercisesController : ApiControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<GetExerciseDto>> GetExercise(string id)
    {
        return Ok(await Mediator.Send(new GetExerciseQuery(id)));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetExerciseDto>>> GetExercises()
    {
        return Ok(await Mediator.Send(new GetExercisesQuery()));
    }

    [HttpPost]
    public async Task<ActionResult<GetExerciseDto>> CreateExercise([FromBody] CreateExerciseCommand command)
    {
        var exerciseDto = await Mediator.Send(command);

        return new CreatedAtActionResult(nameof(GetExercise),
            "Exercises",
            new {exerciseDto.Id},
            exerciseDto);
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromBody] UpdateExerciseCommand command)
    {
        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExercise(string id)
    {
        await Mediator.Send(new DeleteExerciseCommand(id));

        return NoContent();
    }
}