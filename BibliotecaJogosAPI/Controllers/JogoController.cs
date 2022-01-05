using AutoMapper;
using BibliotecaJogosAPI.Models;
using BibliotecaJogosAPI.Repository.Context;
using BibliotecaJogosAPI.Repository.DTO.Jogo.Read;
using BibliotecaJogosAPI.Repository.DTO.Jogo.Update;
using BibliotecaJogosAPI.Repository.DTO.Jogo.Write;
using BibliotecaJogosAPI.Repository.Interface;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaJogosAPI.Controllers
{
    [Route("api/jogos")]
    [ApiController]
    public class JogoController : Controller
    {
        private readonly IJogoRepository _repository;
        private readonly BibliotecaDBContext _context;
        private readonly IMapper _mapper;

        public JogoController(BibliotecaDBContext context, IMapper mapper, IJogoRepository repository)
        {
            _repository = repository;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<JogoReadDTO>> SelectAllJogos(string? nome)
        {
            var jogos = new List<JogoReadDTO>();
            if (string.IsNullOrWhiteSpace(nome))
            {
                jogos = (from jogo in _repository.SelectTodosJogos(_context)
                            select _mapper.Map<JogoReadDTO>(jogo)).ToList();
            }
            else
            {
                jogos = (from jogo in _repository.SelectJogoPorNome(_context, nome.Trim())
                            select _mapper.Map<JogoReadDTO>(jogo)).ToList();
            }
            return Ok(jogos);
        }

        [HttpGet("{id}", Name = "SelectJogoById")]
        public ActionResult<JogoReadDTO> SelectJogoById(int id)
        {
            var jogo = _repository.SelectJogoPorID(_context, id);
            if(jogo == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<JogoReadDTO>(jogo));
        }
        
        [HttpPost]
        public ActionResult<JogoReadDTO> InsertJogo(JogoWriteDTO jogoWriteDTO)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    //var jogo = _mapper.Map<Jogo>(jogoWriteDTO);
                    var jogo = MapJogoPost(jogoWriteDTO);

                    if (_repository.InsertJogo(_context, jogo))
                    {
                        var jogoReadDTO = _mapper.Map<JogoReadDTO>(jogo);
                        return CreatedAtAction(nameof(SelectJogoById), new { id = jogo.Id }, jogoReadDTO);
                    }
                } catch (Exception)
                {
                    return StatusCode(500);
                }
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteJogo(int id)
        {
            if(_repository.DeleteJogo(_context, id))
            {
                return NoContent();
            }
            return NotFound();
        }
        [HttpPatch("{id}")]
        public ActionResult PartialUpdateJogo(int id, JsonPatchDocument<JogoUpdateDTO> patchDoc)
        {
            var jogo = _repository.SelectJogoPorID(_context, id);
            if(jogo != null)
            {
                var jogoUpdateDTO = _mapper.Map<JogoUpdateDTO>(jogo);
                patchDoc.ApplyTo(jogoUpdateDTO, ModelState);

                if (!TryValidateModel(jogoUpdateDTO))
                {
                    return ValidationProblem(ModelState);
                }

                //This is where the update happens//
                //_mapper.Map(jogoUpdateDTO, jogo);
                MapJogoUpdate(jogo, jogoUpdateDTO);
                _repository.UpdateJogo(_context, jogo);
                return NoContent();
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateJogo(int id, JogoUpdateDTO jogoDTO)
        {
            var jogo = _repository.SelectJogoPorID(_context, id);
            if (jogo != null)
            {
                //Metodo 1
                //_mapper.Map(jogoDTO, jogo);
                //Metodo 2
                MapJogoUpdate(jogo, jogoDTO);
                _repository.UpdateJogo(_context, jogo);
                return NoContent();
            }
            return NotFound();
        }

        private Jogo MapJogoUpdate(Jogo jogo, JogoUpdateDTO jogoDTO)  {

            jogo.Nome = jogoDTO.Nome;
            jogo.PrecoCompra = jogoDTO.PrecoCompra;
            jogo.DataCompra = jogoDTO.DataCompra;
            jogo.IdEstudio = jogoDTO.Estudio != null ? jogoDTO.Estudio.Id : null;
            jogo.Estudio = jogoDTO.Estudio != null ? (from estudio in _context.Estudio
                                                      where estudio.Id == jogoDTO.Estudio.Id
                                                      select estudio).FirstOrDefault() : null;
            jogo.Generos = (from genero in _context.Genero
                            where jogoDTO.Generos.Select(g => g.Id).Contains(genero.Id)
                            select genero).ToList();
            return jogo;
        }






        private Jogo MapJogoPost(JogoWriteDTO jogoWriteDTO)
        {
            var jogo = new Jogo
            {
                Nome = jogoWriteDTO.Nome,
                DataCompra = jogoWriteDTO.DataCompra,
                PrecoCompra = jogoWriteDTO.PrecoCompra,
                IdEstudio = jogoWriteDTO.Estudio?.Id,
                Estudio = jogoWriteDTO.Estudio != null ? (from estudio in _context.Estudio
                                                          where estudio.Id == jogoWriteDTO.Estudio.Id
                                                          select estudio).FirstOrDefault() : null,
                Generos = (from genero in _context.Genero
                           where jogoWriteDTO.Generos.Select(g => g.Id).Contains(genero.Id)
                           select genero).ToList()
            };

            return jogo;
        }
    }
}
