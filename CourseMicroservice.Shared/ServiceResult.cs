using ProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;
using Refit;
using System.Net;
using System.Text.Json.Serialization;
using System.Text.Json;
using MediatR;

namespace CourseMicroservice.Shared
{
    public interface IRequestByServiceResult<T>: IRequest<ServiceResult<T>>;
    public interface IRequestByServiceResult: IRequest<ServiceResult>;

    public class ServiceResult
    {
        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; }

        public ProblemDetails? Fail { get; set; }

        [JsonIgnore]
        public bool IsSuccess => Fail is null;

        [JsonIgnore]
        public bool IsFail => !IsSuccess;

        // Static factory method.
        public static ServiceResult SuccessAsNoContent()
        {
            return new ServiceResult
            {
                StatusCode = HttpStatusCode.NoContent
            };
        }

        public static ServiceResult ErrorAsNotFound()
        {
            return new ServiceResult()
            {
                StatusCode = HttpStatusCode.NotFound,
                Fail = new ProblemDetails
                {
                    Title = "Not Found",
                    Status = (int)HttpStatusCode.NotFound,
                    Detail = "The requested resource was not found."
                }
            };
        }

        public static ServiceResult Error(ProblemDetails problemDetails, HttpStatusCode httpStatusCode)
        {
            return new ServiceResult
            {
                StatusCode = httpStatusCode,
                Fail = problemDetails
            };
        }

        public static ServiceResult Error(string title, string description, HttpStatusCode httpStatusCode)
        {
            return new ServiceResult
            {
                StatusCode = httpStatusCode,
                Fail = new ProblemDetails()
                {
                    Title = title,
                    Detail = description,
                    Status = httpStatusCode.GetHashCode(),
                }
            };
        }

        public static ServiceResult Error(string title, HttpStatusCode httpStatusCode)
        {
            return new ServiceResult
            {
                StatusCode = httpStatusCode,
                Fail = new ProblemDetails()
                {
                    Title = title,
                    Status = httpStatusCode.GetHashCode(),
                }
            };
        }

        public static ServiceResult ErrorFromValidation(IDictionary<string, object?> errors)
        {
            return new ServiceResult
            {
                StatusCode = HttpStatusCode.BadRequest,
                Fail = new ProblemDetails()
                {
                    Title = "Validation errors occurred.",
                    Detail = "Please check the errors property for more details.",
                    Status = HttpStatusCode.BadRequest.GetHashCode(),
                    Extensions = errors,
                }
            };
        }

        public static ServiceResult ErrorFromProblemDetails(ApiException exception)
        {
            if (string.IsNullOrEmpty(exception.Content))
            {
                return new ServiceResult
                {
                    StatusCode = exception.StatusCode,
                    Fail = new ProblemDetails
                    {
                        Title = exception.Message,
                        Status = exception.StatusCode.GetHashCode(),
                    }
                };
            }

            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(exception.Content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return new ServiceResult
            {
                StatusCode = exception.StatusCode,
                Fail = problemDetails,
            };
        }
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T? Data { get; set; }
        
        [JsonIgnore] 
        public string? UrlAsCreated { get; set; }

        // 200 OK with data.
        public static ServiceResult<T> SuccessAsOk(T data)
        {
            return new ServiceResult<T>
            {
                StatusCode = HttpStatusCode.OK,
                Data = data
            };
        }

        // 201 Created => response body header => location == api/course/{id} (Example)
        public static ServiceResult<T> SuccessAsCreated(T data, string url)
        {
            return new ServiceResult<T>
            {
                StatusCode = HttpStatusCode.Created,
                Data = data,
                UrlAsCreated = url
            };
        }

        public new static ServiceResult<T> Error(ProblemDetails problemDetails, HttpStatusCode httpStatusCode)
        {
            return new ServiceResult<T>
            {
                StatusCode = httpStatusCode,
                Fail = problemDetails
            };
        }

        public new static ServiceResult<T> Error(string title, string description, HttpStatusCode httpStatusCode)
        {
            return new ServiceResult<T>
            {
                StatusCode = httpStatusCode,
                Fail = new ProblemDetails()
                {
                    Title = title,
                    Detail = description,
                    Status = httpStatusCode.GetHashCode(),
                }
            };
        }

        public new static ServiceResult<T> Error(string title, HttpStatusCode httpStatusCode)
        {
            return new ServiceResult<T>
            {
                StatusCode = httpStatusCode,
                Fail = new ProblemDetails()
                {
                    Title = title,
                    Status = httpStatusCode.GetHashCode(),
                }
            };
        }

        public new static ServiceResult<T> ErrorFromValidation(IDictionary<string, object?> errors)
        {
            return new ServiceResult<T>
            {
                StatusCode = HttpStatusCode.BadRequest,
                Fail = new ProblemDetails()
                {
                    Title = "Validation errors occured.",
                    Detail = "Please check the erros property for more details.",
                    Status = HttpStatusCode.BadRequest.GetHashCode(),
                    Extensions = errors,
                }
            };
        }

        public new static ServiceResult<T> ErrorFromProblemDetails(ApiException exception)
        {
            if (string.IsNullOrEmpty(exception.Content))
            {
                return new ServiceResult<T>
                {
                    StatusCode = exception.StatusCode,
                    Fail = new ProblemDetails
                    {
                        Title = exception.Message,
                        Status = exception.StatusCode.GetHashCode(),
                    }
                };
            }

            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(exception.Content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return new ServiceResult<T>
            {
                StatusCode = exception.StatusCode,
                Fail = problemDetails,
            };
        }

        public object ToActionResult()
        {
            throw new NotImplementedException();
        }
    }
}
