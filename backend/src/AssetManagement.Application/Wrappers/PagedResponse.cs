namespace AssetManagement.Application.Wrappers
{
    public class PagedResponse<T> : Response<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }

        public PagedResponse(T data, int totalRecords)
        {
            TotalRecords = totalRecords;
            Data = data;
            Succeeded = true;
            Message = string.Empty;
            Errors = null;
        }
    }
}