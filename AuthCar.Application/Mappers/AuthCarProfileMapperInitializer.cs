using AuthCar.Application.Mappers;
using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;

namespace Authentication.Application.Mappers
{
    public static class AuthenticationLoginProfileMapperInitializer
    {
        private static readonly Lazy<IMapper> MapperLazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.AddProfile<AuthCarMapper>();
                },
                NullLoggerFactory.Instance
            );
            return config.CreateMapper();
        });

        public static IMapper Mapper => MapperLazy.Value;
    }
}