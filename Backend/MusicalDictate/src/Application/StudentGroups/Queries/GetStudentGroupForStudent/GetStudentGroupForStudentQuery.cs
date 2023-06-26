using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.StudentGroups.Queries.GetStudentGroupForStudent;

public record GetStudentGroupForStudentQuery(string Id) : IRequest<GetStudentGroupForStudentDto>;

public class GetStudentGroupForStudentQueryHandler : IRequestHandler<GetStudentGroupForStudentQuery, GetStudentGroupForStudentDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetStudentGroupForStudentQueryHandler(IMapper mapper, IApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<GetStudentGroupForStudentDto> Handle(GetStudentGroupForStudentQuery request, CancellationToken cancellationToken)
    {
        await _context.Exercises.LoadAsync(cancellationToken);
        await _context.Teachers.LoadAsync(cancellationToken);
        
        var group = await _context.StudentGroups
            .Include(group => group.Students)
            .Include(group => group.Assignments).ThenInclude(assignment => assignment.StudentSolutions)
            .SingleOrDefaultAsync(group => group.Id == request.Id, cancellationToken);

        if (group == null)
            throw new NotFoundException(nameof(StudentGroup), request.Id);

        return _mapper.Map<GetStudentGroupForStudentDto>(group);
    }
}