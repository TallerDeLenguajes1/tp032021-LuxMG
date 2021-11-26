using AutoMapper;
using CadeteriaWeb.Entities;
using CadeteriaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb
{
    public class PerfilDeMapeo : Profile
    {
        public PerfilDeMapeo()
        {
            CreateMap<Usuario, UsuarioLoginViewModel>().ReverseMap()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));

            CreateMap<Usuario, UsuarioCreateViewModel>().ReverseMap()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));

            CreateMap<Usuario, UsuarioUpdateViewModel>().ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.Rol, opt => opt.MapFrom(src => src.Rol));



            //CreateMap<Cadete, CadeteViewModel>().ReverseMap();
            //CreateMap<Pedido, PedidoViewModel>().ReverseMap()
            //    .ForMember(dest => dest.Observacion, opt => opt.MapFrom(src => src.)); 
            //// asi miembro a miembro
            //CreateMap<Cliente, ClienteViewModel>().ReverseMap();
        }
    }
}
