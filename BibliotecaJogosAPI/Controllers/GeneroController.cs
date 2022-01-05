using AutoMapper;
using BibliotecaJogosAPI.Models;
using BibliotecaJogosAPI.Repository.Context;
using BibliotecaJogosAPI.Repository.DTO.Genero.Read;
using BibliotecaJogosAPI.Repository.DTO.Genero.Update;
using BibliotecaJogosAPI.Repository.DTO.Genero.Write;
using BibliotecaJogosAPI.Repository.Interface;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaJogosAPI.Controllers
{
    [ApiController]
    [Route("api/generos")]
    public class GeneroController : Controller
    {
        private readonly BibliotecaDBContext _context;
        private readonly IMapper _mapper;
        private readonly IGeneroRepository _repository;

        public GeneroController(BibliotecaDBContext context, IMapper mapper, IGeneroRepository genero)
        {
            _context = context;
            _mapper = mapper;
            _repository = genero;
        }

        [HttpGet]
        public ActionResult<IEnumerable<GeneroReadDTO>> SelectAllGenero(string? nome)
        {
            var generos = new List<GeneroReadDTO>();
            if (string.IsNullOrWhiteSpace(nome))
            {
                generos = (from genero in _repository.SelectTodosGeneros(_context)
                           select _mapper.Map<GeneroReadDTO>(genero)).ToList();

            }
            else
            {
                generos = (from genero in _repository.SelectGeneroPorNome(_context, nome.Trim())
                           select _mapper.Map<GeneroReadDTO>(genero)).ToList();

            }
            return Ok(generos);
        }

        [HttpGet("{id}", Name = "SelectGeneroById")]
        public ActionResult<GeneroReadDTO> SelectGeneroById(int id)
        {
            var genero = _repository.SelectGeneroPorID(_context, id);
            if (genero == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GeneroReadDTO>(genero));
        }

        [HttpPost]
        public ActionResult<GeneroReadDTO> InsertJogo(GeneroWriteDTO generoWriteDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var genero = _mapper.Map<Genero>(generoWriteDTO);
                    if (_repository.InsertGenero(_context, genero))
                    {
                        var generoReadDTO = _mapper.Map<GeneroReadDTO>(genero);
                        return CreatedAtAction(nameof(SelectGeneroById), new { id = genero.Id }, generoReadDTO);
                    }
                }
                catch (Exception)
                {
                    return StatusCode(500);
                }
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteGenero(int id)
        {
            if (_repository.DeleteGenero(_context, id))
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialUpdateGenero(int id, JsonPatchDocument<GeneroUpdateDTO> patchDoc)
        {
            var genero = _repository.SelectGeneroPorID(_context, id);
            if (genero != null)
            {
                var generoUpdateDTO = _mapper.Map<GeneroUpdateDTO>(genero);
                patchDoc.ApplyTo(generoUpdateDTO, ModelState);

                if (!TryValidateModel(generoUpdateDTO))
                {
                    return ValidationProblem(ModelState);
                }
                //This is where the update happens//
                _mapper.Map(generoUpdateDTO, genero);
                //===============================//

                _repository.UpdateGenero(_context, genero);
                return NoContent();
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateGenero(int id, GeneroUpdateDTO generoDTO)
        {
            var genero = _repository.SelectGeneroPorID(_context, id);
            if(genero != null)
            {
                _mapper.Map(generoDTO, genero);
                if (_repository.UpdateGenero(_context, genero))
                {
                    return NoContent();
                }
            } else
            {
                return NotFound();
            }
            return BadRequest();
        }
    }
}
