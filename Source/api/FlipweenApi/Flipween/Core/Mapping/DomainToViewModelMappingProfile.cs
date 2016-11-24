using AutoMapper;
using FlipWeen.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Flipween.Core.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {
            SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
            DestinationMemberNamingConvention = new PascalCaseNamingConvention();
            CreateMap<Project, ProjectViewModel>();
            CreateMap<ProjectCategory, ProjectCategoryViewModel>();

        }
    }
}