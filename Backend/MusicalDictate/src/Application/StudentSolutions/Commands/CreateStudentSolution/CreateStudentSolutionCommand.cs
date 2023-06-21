using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.StudentSolutions.Commands.CreateStudentSolution;

public class CreateStudentSolutionCommand : IRequest<string>
{
    public string AssignmentId { get; set; }
    public string Solution { get; set; }
}

public class CreateStudentSolutionCommandHandler : IRequestHandler<CreateStudentSolutionCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CreateStudentSolutionCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<string> Handle(CreateStudentSolutionCommand request, CancellationToken cancellationToken)
    {
        var student = await _context.Students
            .SingleOrDefaultAsync(student => student.Id == _currentUserService.UserId, cancellationToken);

        var sheet = Sheet.Create(request.Solution);

        var assignment = await _context.Assignments
            .SingleOrDefaultAsync(assignment => assignment.Id == request.AssignmentId, cancellationToken);

        if (assignment == null)
            throw new NotFoundException(nameof(Assignment), request.AssignmentId);

        var studentSolution = StudentSolution.Create(student, assignment, sheet);
        _context.StudentSolutions.Add(studentSolution);
        
        studentSolution.AddDomainEvent(new StudentSolutionCreatedEvent(studentSolution));

        await _context.SaveChangesAsync(cancellationToken);

        return studentSolution.Id;
    }
}