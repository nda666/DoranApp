namespace DoranApp.RequestParams;

public class PaginationDto
{
    public long Page { get; set; } = 1;
    public long PageSize { get; set; } = 50;
}