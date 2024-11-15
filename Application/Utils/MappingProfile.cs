﻿using AutoMapper;
using Domain.Entities.Models.Masters;
using Domain.Entities.Requests.Clients;
using Domain.Entities.Responses;

namespace Application.Utils
{
    public static class Mapping
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                // This line ensures that internal properties are also mapped over.
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;
    }

    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            //model to response
            CreateMap<Users, UserProfileResponse>();

            //request to model
            CreateMap<ProfileRequest, Domain.Entities.Models.Clients.Profile>();

            //DTO to request


            //DTO to model
        }
    }
}
