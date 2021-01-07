namespace SmartSchool.WebAPI.Helpers
{
    public class PageParams
    {
        public const int MaxPageSize = 50;

        public int PageNumber { get; set; } = 1;

        private int PageSize_ = 10;

        public int PageSize
        {
            get { return PageSize_; }
            set { PageSize_ = (value > MaxPageSize) ? MaxPageSize : value; }
        }

        public int? Matricula { get; set; } = null;

        public string Nome { get; set; } = string.Empty;

        public int? Ativo { get; set; } = null;
    }
}