using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Account.Queries.ExistsEmail;
public record struct ExistsEmailQuery(string Email) : IRequest<ExistsEmailDto> { }

public class ExistsEmailQueryHandler(IApplicationDbContext context) : IRequestHandler<ExistsEmailQuery, ExistsEmailDto>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<ExistsEmailDto> Handle(ExistsEmailQuery request, CancellationToken cancellationToken)
    {
        var email = await _context
            .Users
            .AsNoTracking()
            .Where(p => p.Email.Trim().ToLower() == request.Email.Trim().ToLower())
            .Select(p => p.Email)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);


        return new ExistsEmailDto() { Exists = email != null };

    }
}

