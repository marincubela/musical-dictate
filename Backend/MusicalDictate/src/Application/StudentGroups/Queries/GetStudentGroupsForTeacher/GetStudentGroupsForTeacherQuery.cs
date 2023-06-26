using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.StudentGroups.Queries.GetStudentGroupsForTeacher;

public record GetStudentGroupsForTeacherQuery : IRequest<IEnumerable<GetStudentGroupsForTeacherDto>>;

public class GetStudentGroupsForTeacherQueryHandler : IRequestHandler<GetStudentGroupsForTeacherQuery, IEnumerable<GetStudentGroupsForTeacherDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetStudentGroupsForTeacherQueryHandler(IMapper mapper, IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _mapper = mapper;
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<IEnumerable<GetStudentGroupsForTeacherDto>> Handle(GetStudentGroupsForTeacherQuery request, CancellationToken cancellationToken)
    {
        return await _context.StudentGroups
            .Include(group => group.Students)
            .Include(group => group.Assignments).ThenInclude(assignment => assignment.Exercise)
            .Where(group => group.TeacherId == _currentUserService.UserId)
            .ProjectTo<GetStudentGroupsForTeacherDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}