using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Queries.GetUsers;
public record struct GetUsersQuery(Guid WorkspaceId, Guid? UserId) : IRequest<IEnumerable<UserDto>> { }

public class GetUsersQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetUsersQuery, IEnumerable<UserDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _context
            .Users
            .AsNoTracking()
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return users;
    }
}
