namespace Movies.Core.Common
{
    public class PaginationResult<T>
    {
        private PaginationResult(List<T> data, int pageNumber, int pageSize, int totalRecords)
        {
            Data = data;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalRecords = totalRecords;

            TotalPages = Convert.ToInt32(Math.Ceiling((double)totalRecords / pageSize));
        }

        public List<T> Data { get; set; }
        public int PageNumber { get; private set; }
        public int PageSize { get; }
        public int TotalPages { get; }
        public int TotalRecords { get; }


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
