using System.ComponentModel.DataAnnotations;

namespace EmployeeDetils_Mangement.Helper
{
    public class BaseResponse
    {
        private bool _success;
        [Required]
        public bool Success
        {
            get => _success;
            set
            {
                _success = value;
                Code = _success ? 200 : 400;
            }
        }

        [Required]
        public string Message { get; set; }

        [Required]
        public int Code { get; set; }
    }
    public class BaseResponseObject<T>
    {
        private bool _success;

        [Required]
        public bool Success
        {
            get => _success;
            set
            {
                _success = value;
                Code = _success ? 200 : 400;
            }
        }

        [Required]
        public string Message { get; set; }

        [Required]
        public int Code { get; set; }
        public T Data { get; set; }
    }
    public class BaseResponseModel<T>
    {
        private bool _success;

        [Required]
        public bool Success
        {
            get => _success;
            set
            {
                _success = value;
                Code = _success ? 200 : 400;
            }
        }

        [Required]
        public string? Message { get; set; }
        public int TotalRecords { get; set; }

        [Required]
        public int Code { get; set; }
        public T? Data { get; set; }
    }

    public class PropBaseResponse<T>
    {
        public bool success { get; set; }
        public int code { get; set; }
        public List<T> data { get; set; }
        public string error { get; set; }
        public string message { get; set; }
    }
}
