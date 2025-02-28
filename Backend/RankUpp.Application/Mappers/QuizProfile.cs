using AutoMapper;
using RankUpp.Core.DTOs.Output;
using RankUpp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankUpp.Application.Mappers
{
    public class QuizProfile : Profile
    {
        public QuizProfile()
        {
            CreateMap<Quiz, QuizDTO>()
                .ForMember(dest => dest.Questions, act => act.MapFrom(src => src.Questions));

            CreateMap<QuizQuestion, QuizQuestionDTO>()
                                .ForMember(dest => dest.Options, act => act.MapFrom(src => src.Options));

            CreateMap<QuizOption, QuizOptionDTO>();

            CreateMap<UserMemory, UserMemoryDTO>().ForMember(dest => dest.Quiz, act => act.MapFrom(src => src.Quiz));


        }
    }
}
