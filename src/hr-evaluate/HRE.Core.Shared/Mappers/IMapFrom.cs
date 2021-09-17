
using AutoMapper;

namespace HRE.Core.Shared.Mappers
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile); //  => profile.CreateMap(typeof(T), GetType());
    }
}
