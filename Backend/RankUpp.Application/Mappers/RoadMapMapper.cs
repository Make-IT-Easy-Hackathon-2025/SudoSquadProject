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
    public class RoadMapMapper : Profile
    {
        public RoadMapMapper()
        {
            CreateMap<RoadMap, CreateRoadMapDTO>().ForMember(dest => dest.Items, act => act.MapFrom(src => src.Items));

            CreateMap<RoadMapItem, CreateRoadMapItemDTO>();

            CreateMap<CreateRoadMapDTO, RoadMap>().ForMember(dest => dest.Items, act => act.MapFrom(src => src.Items));

            CreateMap<CreateRoadMapItemDTO, RoadMapItem>();

            CreateMap<RoadMap, RoadMapDTO>().ForMember(dest => dest.Items, act => act.MapFrom(src => src.Items));

            CreateMap<RoadMapItem, RoadMapItemDTO>();

            CreateMap<RoadMap, RoadMapRepayDTO>().ForMember(dest => dest.Items, act => act.MapFrom(src => src.Items));

            CreateMap<RoadMapItem, RoadMapItemReplayDTO>();
        }
    }
}
