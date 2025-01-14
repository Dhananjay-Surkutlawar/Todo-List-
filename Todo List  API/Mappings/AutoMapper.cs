using AutoMapper;
using Todo_List__API.Contracts;
using Todo_List__API.Entity;

namespace Todo_List__API.Mappings
{
    public class AutoMapper : Profile
    {
        public AutoMapper() {
            CreateMap<TodoEntity, TodoContract>().ReverseMap();

        }
       
    }
}
