using AutoMapper;
using ToDoApp.BLL.DTO;
using ToDoApp.BLL.MediatR.UserTask.Create;
using ToDoApp.BLL.MediatR.UserTask.Update;
using ToDoApp.DAL.Entities;

namespace ToDoApp.BLL.Mapping;

public class UserTaskProfile : Profile
{
    public UserTaskProfile()
    {
        CreateMap<UserTask, UserTaskFullDto>().ReverseMap();
        CreateMap<CreateUserTaskDto, UserTask>();
        CreateMap<UpdateUserTaskDto, UserTask>();
    }
}