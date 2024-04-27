using AutoMapper;
using ProjectBackendLearning.Core.DTOs;

namespace ProjectBackendLearning.Core.Models.Requests;

public class RequestMapperProfile:Profile
{
    public RequestMapperProfile()
    {
        CreateMap<CreateUserRequest, UserDto>();
    }
}