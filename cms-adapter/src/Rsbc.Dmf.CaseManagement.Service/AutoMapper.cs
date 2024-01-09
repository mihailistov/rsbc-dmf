﻿using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq.Expressions;
using Rsbc.Dmf.CaseManagement.Dynamics;

namespace Rsbc.Dmf.CaseManagement.Service
{
    public class MappingProfile : Profile {
        public MappingProfile()
        {
            CreateMap<DateTimeOffset, Timestamp>()
                .ConvertUsing(x => Timestamp.FromDateTimeOffset(x));
            CreateMap<DateTimeOffset?, Timestamp>()
                .ConvertUsing(x => x == null ? null : Timestamp.FromDateTimeOffset(x.Value));
            CreateMap<DateTime, Timestamp>()
                .ConvertUsing(x => Timestamp.FromDateTime(x.ToUniversalTime()));
            CreateMap<CaseManagement.Address, Address>()
                .AddTransform(NullToEmptyStringConverter);
            CreateMap<CaseManagement.Driver, Driver>()
                .AddTransform(NullToEmptyStringConverter);
            CreateMap<CaseManagement.LegacyDocument, LegacyDocument>()
                .AddTransform(NullToEmptyStringConverter);
    }

        private Expression<Func<string, string>> NullToEmptyStringConverter = x => x ?? string.Empty;
    }

    public static class AutoMapper
    {
        public static void AddAutoMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
                mc.AddProfile(new AutoMapperProfile());
            });

            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}