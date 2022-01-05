using AutoMapper;
using BibliotecaJogosAPI.Models;
using BibliotecaJogosAPI.Repository.Context;
using BibliotecaJogosAPI.Repository.DTO.Estudio.Read;
using BibliotecaJogosAPI.Repository.DTO.Estudio.Update;
using BibliotecaJogosAPI.Repository.DTO.Estudio.Write;
using BibliotecaJogosAPI.Repository.DTO.Genero.Read;
using BibliotecaJogosAPI.Repository.DTO.Genero.Update;
using BibliotecaJogosAPI.Repository.DTO.Genero.Write;
using BibliotecaJogosAPI.Repository.DTO.Jogo.Read;
using BibliotecaJogosAPI.Repository.DTO.Jogo.Update;
using BibliotecaJogosAPI.Repository.DTO.Jogo.Write;

namespace BibliotecaJogosAPI.Repository.DTO
{
    public class BibliotecaProfile : Profile
    {
        public BibliotecaProfile()
        {
            //Jogo Read
            CreateMap<Models.Jogo, JogoReadDTO>().ForMember(dto => dto.Estudio, opt => opt.MapFrom(jogo => jogo.Estudio))
                                          .ForMember(dto => dto.Generos, opt => opt.MapFrom(jogo => jogo.Generos));
            CreateMap<Models.Estudio, JogoEstudioReadDTO>();
            CreateMap<Models.Genero, JogoGeneroReadDTO>();

            //Jogo Write
            CreateMap<JogoWriteDTO, Models.Jogo>().ForMember(dto => dto.Estudio, opt => opt.MapFrom(jogo => jogo.Estudio))
                              .ForMember(dto => dto.Generos, opt => opt.MapFrom(jogo => jogo.Generos));
            CreateMap<JogoEstudioWriteDTO, Models.Estudio>();
            CreateMap<JogoGeneroWriteDTO, Models.Genero>();
            
            //Jogo Update
            CreateMap<Models.Jogo, JogoUpdateDTO>().ForMember(dto => dto.Estudio, opt => opt.MapFrom(jogo => jogo.Estudio))
                                          .ForMember(dto => dto.Generos, opt => opt.MapFrom(jogo => jogo.Generos));
            CreateMap<Models.Estudio, JogoEstudioUpdateDTO>();
            CreateMap<Models.Genero, JogoGeneroUpdateDTO>();

            //Genero Read
            CreateMap<Models.Genero, GeneroReadDTO>().ForMember(dto => dto.Jogos, opt => opt.MapFrom(genero => genero.Jogos));
            CreateMap<Models.Jogo, GeneroJogoReadDTO>().ForMember(dto => dto.Estudio, opt => opt.MapFrom(jogo => jogo.Estudio));

            //Genero Write
            CreateMap<GeneroWriteDTO, Models.Genero>();

            //Genero Update
            CreateMap<GeneroUpdateDTO, Models.Genero>();
            CreateMap<Models.Genero, GeneroUpdateDTO>();

            //Estudio Read
            CreateMap<Models.Estudio, EstudioReadDTO>().ForMember(dto => dto.Jogos, opt => opt.MapFrom(estudio => estudio.Jogos));
            CreateMap<Models.Jogo, EstudioJogoReadDTO>().ForMember(dto => dto.Generos, opt => opt.MapFrom(jogo => jogo.Generos));

            //Estudio Write
            CreateMap<EstudioWriteDTO, Models.Estudio>();

            //Estudio Update
            CreateMap<EstudioUpdateDTO, Models.Estudio>();
            CreateMap<Models.Estudio, EstudioUpdateDTO>();
        }
    }
}
