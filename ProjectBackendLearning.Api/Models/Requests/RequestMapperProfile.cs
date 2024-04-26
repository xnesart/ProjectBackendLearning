using AutoMapper;
using ProjectBackendLearning.Core.DTOs;

namespace ProjectBackendLearning.Models.Requests;

public class RequestMapperProfile:Profile
{
    public RequestMapperProfile()
    {
        CreateMap<CreateUserRequest, UserDto>();
    }
}