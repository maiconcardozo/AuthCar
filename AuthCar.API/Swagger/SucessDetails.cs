using Microsoft.AspNetCore.Mvc;

namespace AuthCar.API.Swagger
{
    internal class SucessDetails : ProblemDetails
    {
        public object? Data { get; set; }
    }
}
