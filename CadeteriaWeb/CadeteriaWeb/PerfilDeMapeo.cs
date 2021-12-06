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
            // --------------------MAPEO DE USUARIOS--------------------
            CreateMap<Usuario, UsuarioLoginViewModel>().ReverseMap();
            CreateMap<Usuario, UsuarioCreateViewModel>().ReverseMap();
            CreateMap<Usuario, UsuarioUpdateViewModel>().ReverseMap();


            //CreateMap<Cadete, CadeteViewModel>().ReverseMap();
            //CreateMap<Pedido, PedidoViewModel>().ReverseMap()
            //    .ForMember(dest => dest.Observacion, opt => opt.MapFrom(src => src.)); 
            //// asi miembro a miembro
            //CreateMap<Cliente, ClienteViewModel>().ReverseMap();
        }
    }
}
