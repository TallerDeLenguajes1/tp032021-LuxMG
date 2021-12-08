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

            // --------------------MAPEO DE CADETES--------------------
            CreateMap<Cadete, CadeteCreateViewModel>().ReverseMap();
            CreateMap<Cadete, CadeteUpdateViewModel>().ReverseMap();

            // --------------------MAPEO DE PEDIDOS--------------------
            CreateMap<Pedido, PedidoCreateViewModel>().ReverseMap()
                .ForMember(dest => dest.Cliente.Id, opt => opt.MapFrom(src => src.IdCliente));
            CreateMap<Pedido, PedidoUpdateViewModel>().ReverseMap()
                .ForMember(dest => dest.Cliente.Id, opt => opt.MapFrom(src => src.IdCliente))
                .ForMember(dest => dest.Cadete.Id, opt => opt.MapFrom(src => src.IdCadete));
        }
    }
}
