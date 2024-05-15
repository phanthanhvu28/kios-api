using RestEase;
using VELA.WebCoreBase.Core.Models;

namespace ApplicationCore.Contracts.RestClients;
public interface IRfiSupplierApi
{
    [Post("/api/Customer/GetPaging")]
    public Task<HttpResponseMessage> Query([Body] Paging page);

    [Post("/api/Supplier/GetPaging")]
    public Task<HttpResponseMessage> QuerySupplier([Body] Paging page);
}

public class Paging
{
    public int skipCount { get; set; } = 0;
    public int pageSize { get; set; } = 100;
    public string sortKey { get; set; } = "";
    public string direction { get; set; } = "";
    public string filter { get; set; } = "";
}
public class ModelPartnerPaging
{
    public int page { get; set; } = 1;
    public int pageSize { get; set; } = 10000;
    public List<FilterModel>? Filters { get; set; }
}