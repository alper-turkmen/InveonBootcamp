using Microsoft.AspNetCore.Mvc;

namespace Bolum3.Models
{
    public class ServiceResult
    {
        public int Status { get; set; }
        public ProblemDetails? ProblemDetails { get; set; }

        public static ServiceResult Success(int status)
        {
            return new ServiceResult
            {
                Status = status
            };
        }

        public static ServiceResult Fail(string message, int status = 400)
        {
            return new ServiceResult
            {
                Status = status,
                ProblemDetails = new ProblemDetails
                {
                    Status = status,
                    Detail = message
                }
            };
        }
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T? Data { get; set; }

        public static ServiceResult<T> Success(T data, int status)
        {
            return new ServiceResult<T>
            {
                Data = data,
                Status = status
            };
        }

        public new static ServiceResult<T> Fail(string message, int status = 400)
        {
            return new ServiceResult<T>
            {
                Status = status,
                ProblemDetails = new ProblemDetails
                {
                    Status = status,
                    Detail = message
                }
            };
        }
    }
}