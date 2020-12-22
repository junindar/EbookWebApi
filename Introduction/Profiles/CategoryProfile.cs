using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Introduction.Entity;
using Introduction.Models;

namespace Introduction.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();


            CreateMap<Category, CategoryCreateDto>();
            CreateMap<CategoryCreateDto, Category>();
        }
    }

}
