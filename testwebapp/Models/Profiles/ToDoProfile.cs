using AutoMapper;
using testwebapp.Entites;

namespace testwebapp.Models.Profiles
{
    public class ToDoProfile : Profile
    {
        public ToDoProfile()
        {
            CreateMap<TodoTask, ToDoEntity>();
            CreateMap<ToDoEntity, TodoTask>();
        }
    }
}
