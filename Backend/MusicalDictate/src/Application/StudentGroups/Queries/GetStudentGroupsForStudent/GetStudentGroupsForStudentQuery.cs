using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.StudentGroups.Queries.GetStudentGroupsForStudent;

public record GetStudentGroupsForStudentQuery : IRequest<IEnumerable<GetStudentGroupsForStudentDto>>;

public class GetStudentGroupsForStudentQueryHandler : IRequestHandler<GetStudentGroupsForStudentQuery, IEnumerable<GetStudentGroupsForStudentDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public GetStudentGroupsForStudentQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IMapper mapper)
    {
        _context = context;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetStudentGroupsForStudentDto>> Handle(GetStudentGroupsForStudentQuery request, CancellationToken cancellationToken)
    {
        var student = await _context.Students
            .Include(student => student.MyGroups)
            .SingleOrDefaultAsync(student => student.Id == _currentUserService.UserId, cancellationToken);

        if (student == null)
            throw new NotFoundException(nameof(Student), _currentUserService.UserId);

        return student.MyGroups
            .AsQueryable()
            .ProjectTo<GetStudentGroupsForStudentDto>(_mapper.ConfigurationProvider)
            .ToList();
    }
}