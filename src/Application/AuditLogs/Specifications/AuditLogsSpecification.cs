using ShoppingProject.Application.Common.Specifications;
using ShoppingProject.Domain.Entities;

namespace ShoppingProject.Application.AuditLogs.Specifications;

public sealed class AuditLogsSpecification : BaseSpecification<AuditLog>
{
    private AuditLogsSpecification(int pageNumber, int pageSize)
    {
        ApplyOrderByDescending(x => x.Timestamp);
        ApplyPaging((pageNumber - 1) * pageSize, pageSize);
    }

    private AuditLogsSpecification()
    {
        // For count only
    }

    public static AuditLogsSpecification Create(int pageNumber, int pageSize)
    {
        return new AuditLogsSpecification(pageNumber, pageSize);
    }

    public static AuditLogsSpecification CreateForCount()
    {
        return new AuditLogsSpecification();
    }
}
