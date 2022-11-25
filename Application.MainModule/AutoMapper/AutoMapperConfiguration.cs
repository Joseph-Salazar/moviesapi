using AutoMapper;

namespace Application.MainModule.AutoMapper;

public static class AutoMapperConfiguration
{
    public static void Configure()
    {
        var configuration = new MapperConfiguration(x => { x.AddMaps(typeof(AutoMapperConfiguration)); });
    }
}