using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Application.Exercises.Commands.UpdateExercise;

public class UpdateExerciseCommand : IRequest
{
    public string Id { get; set; }
    public string? Title { get; set; } = null;
    public string Sheet { get; set; }
    public string? Parts { get; set; }
}

public class UpdateExerciseCommandHandler : IRequestHandler<UpdateExerciseCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateExerciseCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateExerciseCommand request, CancellationToken cancellationToken)
    {
        var exercise = await _context.Exercises
            .Include(exercise => exercise.Parts)
            .SingleOrDefaultAsync(exercise => exercise.Id == request.Id, cancellationToken);

        if (exercise == null)
            throw new NotFoundException(nameof(Exercise), request.Id);

        var sheet = Sheet.Create(request.Sheet);

        var parts = request.Parts != null
            ? JsonConvert.DeserializeObject<IEnumerable<Part>>(request.Parts)
            : null;

        exercise.Update(request.Title, parts, sheet);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}