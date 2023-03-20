using aspCrud.Models.DAO;
using AutoMapper;

namespace aspCrud.Mappers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Role
        CreateMap<RoleDTO, RoleDAO>();
        CreateMap<RoleDAO, RoleDTO>();
        CreateMap<RoleDAO, RoleResponseDTO>();
        CreateMap<RoleDTO, RoleResponseDTO>();
        
        // User
        CreateMap<UserDAO, UserDTO>();
        CreateMap<UserDTO, UserDAO>();
        CreateMap<UserDAO, UserResponseDTO>();
        CreateMap<UserDTO, UserResponseDTO>();
    }
    
}