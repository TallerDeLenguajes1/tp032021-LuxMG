using AutoMapper;
using CadeteriaWeb.Entities;
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
            CreateMap<Cadeteria, CadeteViewModel>().ReverseMap();
            CreateMap<Pedido, PedidoViewModel>().ReverseMap()
                .ForMember(dest => dest.Observacion, opt => opt.MapFrom(src => src.Obs)); 
            // asi miembro a miembro
            CreateMap<Cliente, ClienteViewModel>().ReverseMap();
        }
    }
}
