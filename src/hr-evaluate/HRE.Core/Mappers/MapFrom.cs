using AutoMapper;
using HRE.Core.Shared.Mappers;

namespace HRE.Core.Mappers
{
    public abstract class MapFrom<T> : IMapFrom<T>
    {
        public virtual void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType()).IgnoreNoMap(GetType());

        //public void Mapping(Profile profile)
        //{
        //    var c = profile.CreateMap<Employee, EmployeeModel>()
        //        .ForMember(d => d.RegistrationDate, opt => opt.Ignore())
        //        .ForMember(d => d.Title, opt => opt.NullSubstitute("N/A")));
        //}
    }
}
