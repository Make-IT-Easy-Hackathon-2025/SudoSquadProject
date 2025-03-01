using AutoMapper;
using RankUpp.Core.DTOs.Input;
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

            CreateMap<QuizDTO, Quiz>()
                        .ForMember(dest => dest.Questions, act => act.MapFrom(src => src.Questions));

            CreateMap<QuizQuestion, QuizQuestionDTO>()
                                .ForMember(dest => dest.Value, act => act.MapFrom(src => src.QuestionValue))
                                .ForMember(dest => dest.Options, act => act.MapFrom(src => src.Options));

            CreateMap<QuizQuestionDTO, QuizQuestion>()
                              .ForMember(dest => dest.Options, act => act.MapFrom(src => src.Options))
                              .ForMember(dest => dest.QuestionValue, act => act.MapFrom(src => src.Value));

            CreateMap<QuizOption, QuizOptionDTO>();


            CreateMap<QuizOptionDTO, QuizOption>();


            CreateMap<CreateQuizDTO, Quiz>()
                                .ForMember(dest => dest.Questions, act => act.MapFrom(src => src.Questions));

            CreateMap<CreateQuizQuestionDTO, QuizQuestion>()
                    .ForMember(dest => dest.QuestionValue, act => act.MapFrom(src => src.Value))
                    .ForMember(dest => dest.Options, act => act.MapFrom(src => src.Options));

            CreateMap<CreateQuizOptionDTO, QuizOption>();


        }
    }
}
