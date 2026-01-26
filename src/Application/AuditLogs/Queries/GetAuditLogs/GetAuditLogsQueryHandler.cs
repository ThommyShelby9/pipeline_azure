using AutoMapper;
using MediatR;
using ShoppingProject.Application.AuditLogs.Specifications;
using ShoppingProject.Application.Common.Interfaces;
using ShoppingProject.Application.Common.Mappings;
using ShoppingProject.Application.Common.Models;
using ShoppingProject.Application.Common.Specifications;
using ShoppingProject.Domain.Entities;

namespace ShoppingProject.Application.AuditLogs.Queries.GetAuditLogs;

public class GetAuditLogsQueryHandler
    : IRequestHandler<GetAuditLogsQuery, PaginatedList<AuditLogDto>>
{
    private readonly IAuditLogRepository _repository;
    private readonly IMapper _mapper;

    public GetAuditLogsQueryHandler(IAuditLogRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<AuditLogDto>> Handle(
        GetAuditLogsQuery request,
        CancellationToken cancellationToken
    )
    {
        var spec = AuditLogsSpecification.Create(request.PageNumber, request.PageSize);

        // Count all audit logs without pagination for total count
        var allAuditLogsSpec = AuditLogsSpecification.CreateForCount();
        var totalCount = await _repository.CountAsync(allAuditLogsSpec, cancellationToken);
        var auditLogs = await _repository.ListAsync(spec, cancellationToken);

        var mappedLogs = auditLogs.Select(log => _mapper.Map<AuditLogDto>(log)).ToList();

        return new PaginatedList<AuditLogDto>(
            mappedLogs,
            totalCount,
            request.PageNumber,
            request.PageSize
        );
    }
}
