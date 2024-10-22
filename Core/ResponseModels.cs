using Server.Application.Models;

namespace Server.Core
{
    public class ResponseModels
    {
        public class CreateCustomer_Result
        {
            public bool IsExisting { get; set; }
            public string? Message { get; set; }
        }
        public class LoginUser_Result
        {
            public bool IsInvalid { get; set; }
            public string? Token { get; set; }
            public string? ErrorMessage { get; set; }
        }
        public class CreateProduct_Result
        {
            public bool IsExisting { get; set; }
            public string? Message { get; set; }
        }
    }
}
