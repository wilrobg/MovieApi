namespace Movies.Core.Common
{
    public class PaginationResult<T>
    {
        public PaginationResult() { }
        private PaginationResult(List<T> data, int pageNumber, int pageSize, int totalRecords)
        {
            Data = data;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalRecords = totalRecords;

            TotalPages = Convert.ToInt32(Math.Ceiling((double)totalRecords / pageSize));
        }

        public List<T> Data { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }


        public static PaginationResult<T> GetPagination(List<T> data, int pageNumber, int pageSize, int totalRecords)
        {
            return new PaginationResult<T>(data, pageNumber, pageSize, totalRecords);
        }
    };

    public abstract class PaginationRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
