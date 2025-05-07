using AutoMapper;
using DAL.Models;
using ElectionApp.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionApp.Mappings.Voter
{
    internal class VoterProfile : Profile
    {
        public VoterProfile()
        {
            CreateMap<DAL.Models.Voter, VoterViewModel>()
                .ForMember(dest => dest.CountyNumber, c => c.MapFrom(src => src.CountyNumber.ToString()))
                .ForMember(dest => dest.Age, c => c.MapFrom(src => src.Age.ToString()))
                .ForMember(dest => dest.ShowNumber, c => c.Ignore())
                .ForMember(dest => dest.Dispatcher, c => c.Ignore())
                .ForMember(dest => dest.Error, c => c.Ignore())
                .ForMember(dest => dest.IsValid, c => c.Ignore())
                .ForMember(dest => dest.Action, c => c.Ignore());


            CreateMap<VoterViewModel, DAL.Models.Voter>()
                .ForMember(d => d.Age, c => c.MapFrom(s => Convert.ToInt32(s.Age)))
                .ForMember(d => d.CountyNumber, c => c.MapFrom(s => Convert.ToInt32(s.CountyNumber)))
                .ForSourceMember(dest => dest.Dispatcher, c => c.DoNotValidate())
                .ForSourceMember(dest => dest.Error, c => c.DoNotValidate())
                .ForSourceMember(dest => dest.IsValid, c => c.DoNotValidate())
                .ForSourceMember(dest => dest.Action, c => c.DoNotValidate());            

        }
    }
}
