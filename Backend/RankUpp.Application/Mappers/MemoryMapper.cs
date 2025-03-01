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
    public class MemoryMapper : Profile
    {
        public MemoryMapper()
        {
            CreateMap<CreateMemoryRequestDTO, UserMemory>();
        }
    }
}
