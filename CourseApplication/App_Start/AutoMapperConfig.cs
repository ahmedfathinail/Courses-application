using AutoMapper;
using courseApplication.Models;
using CourseApplication.Data;
using CourseApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseApplication
{
    public static class AutoMapperConfig
    {
        public static IMapper Mapper { get; private set; }

        public static void Init()
        {
            var config = new MapperConfiguration(cfg =>{
                cfg.CreateMap<Category, categoryModel>()
                .ForMember(dst => dst.Id, src => src.MapFrom(e => e.ID))
                .ForMember(dst => dst.ParentId, src => src.MapFrom(e => e.Category2.Parnt_Id))
                .ForMember(dst => dst.ParentName, src => src.MapFrom(e => e.Category2.Name))
                .ReverseMap();

                cfg.CreateMap<Trainer, TrainerModel>().ReverseMap();

                cfg.CreateMap<Course, CourseModel>()
                .ForMember(dst => dst.TrainerName, src => src.MapFrom(e => e.Trainer.Name))
                .ForMember(dst => dst.Category_Name, src => src.MapFrom(e => e.Category.Name)).ReverseMap();

                cfg.CreateMap<Trainee, TraineeModel>().ReverseMap();

                cfg.CreateMap<Trainee_Courses, TraineeCourseModel>()
                .ForMember(dst => dst.Course_Id, src => src.MapFrom(e => e.Course_Id))
                .ForMember(dst => dst.Registration_Date, src => src.MapFrom(e => e.Registration_Date))
                .ForMember(dst => dst.Trainee, src => src.MapFrom(e => e.Trainee));

                cfg.CreateMap<Course_Uints, CourseUnitsModel>()
                .ForMember(dst => dst.CourseName, src => src.MapFrom(e => e.Name));

            });
            Mapper = config.CreateMapper();
        }
    }
}