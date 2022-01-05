using AutoMapper;
using BibliotecaJogosAPI.Models;
using BibliotecaJogosAPI.Repository.Context;
using BibliotecaJogosAPI.Repository.DTO.Estudio.Read;
using BibliotecaJogosAPI.Repository.DTO.Estudio.Update;
using BibliotecaJogosAPI.Repository.DTO.Estudio.Write;
using BibliotecaJogosAPI.Repository.Interface;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaJogosAPI.Controllers
{
    //The ApiController annotation makes it so we dont need to use modelstate.isvalid, it validates the model automatically
    [ApiController]
    [Route("api/estudios")]
    public class EstudioController : Controller
    {
        private readonly IEstudioRepository _repository;
        private readonly BibliotecaDBContext _context;
        private readonly IMapper _mapper;

        public EstudioController(IEstudioRepository repository, BibliotecaDBContext context, IMapper mapper)
        {
            _repository = repository;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<EstudioReadDTO>> SelectAllEstudios(string? nome)
        {
            var estudios = new List<EstudioReadDTO>();
            if(string.IsNullOrWhiteSpace(nome))
            {
                estudios = (from estudio in _repository.SelectTodosEstudios(_context)
                           select _mapper.Map<EstudioReadDTO>(estudio)).ToList();

            } else
            {
                estudios = (from estudio in _repository.SelectEstudioPorNome(_context, nome.Trim())
                           select _mapper.Map<EstudioReadDTO>(estudio)).ToList();
            }

            return Ok(estudios);
        }

        [HttpGet("{id}", Name = "SelectEstudioPorId")]
        public ActionResult<EstudioReadDTO> SelectEstudioPorId(int id)
        {
            var estudio = _repository.SelectEstudioPorID(_context, id);
            if (estudio == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<EstudioReadDTO>(estudio));
        }

        [HttpPost]
        public ActionResult<EstudioReadDTO> InsertEstudio(EstudioWriteDTO estudioWriteDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var estudio = _mapper.Map<Estudio>(estudioWriteDTO);

                    if (_repository.InsertEstudio(_context, estudio))
                    {
                        var estudioReadDTO = _mapper.Map<EstudioReadDTO>(estudio);
                        return CreatedAtAction(nameof(SelectEstudioPorId), new { id = estudio.Id }, estudioReadDTO);
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
        public ActionResult DeleteEstudio(int id)
        {
            if (_repository.DeleteEstudio(_context, id))
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialUpdateEstudio(int id, JsonPatchDocument<EstudioUpdateDTO> patchDoc)
        {
            var estudio = _repository.SelectEstudioPorID(_context, id);
            if (estudio != null)
            {
                var estudioUpdateDTO = _mapper.Map<EstudioUpdateDTO>(estudio);
                patchDoc.ApplyTo(estudioUpdateDTO, ModelState);

                if (!TryValidateModel(estudioUpdateDTO))
                {
                    return ValidationProblem(ModelState);
                }

                //This is where the update happens//
                _mapper.Map(estudioUpdateDTO, estudio);

                _repository.UpdateEstudio(_context, estudio);
                return NoContent();
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateEstudio(int id, EstudioUpdateDTO estudioDTO)
        {
            var estudio = _repository.SelectEstudioPorID(_context, id);
            if (estudio != null)
            {
                _mapper.Map(estudioDTO, estudio);
                //===============================//
                _repository.UpdateEstudio(_context, estudio);
                return NoContent();
            }
            return NotFound();
        }
    }
}

