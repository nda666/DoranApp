namespace DoranApp.Dtos
{
    public class MasteruserOptionDto
    {
        public int? Kodeku { get; set; }
        public string Usernameku { get; set; }
    }

    public class CommonResultDto
    {
        public int? Kode { get; set; }
        public string Nama { get; set; }
    }

    public class MasterbarangOptionWithTipe
    {
        public int? BrgKode { get; set; }
        public string BrgNama { get; set; }
        public CommonResultDto Mastertipebarang { get; set; }
    }

    public class PaginationDto
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }

    public class PaginationResultDto
    {
        public long TotalPage { get; set; } = 1;

        public long TotalRow { get; set; } = 1;
        public long Page { get; set; } = 1;
        public long PageSize { get; set; } = 20;
    }
}