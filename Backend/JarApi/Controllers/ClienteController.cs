using AutoMapper;
using Domain.entities;
using Domain.Interfaces;
using JarApi.Dtos;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/clientes")]
public class ClienteController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ClienteController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ClienteDto>>> Get()
    {
        var clientes = await _unitOfWork.Clientes.GetAllAsync();
        return Ok(_mapper.Map<List<ClienteDto>>(clientes));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClienteDto>> Get(string id)
    {
        var cliente = await _unitOfWork.Clientes.GetByIdSTring(id);
        if (cliente == null)
            return NotFound();

        return Ok(_mapper.Map<ClienteDto>(cliente));
    }
    [HttpGet("spanishClients")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<ClienteOnlyName>>> GetSpanishClientsNames()
    {
        try
        {
            var result = await _unitOfWork.Clientes.GetNameSpainCoustumers();

            return Ok(result);
        }
        catch (Exception ex)
        {
            // Manejar errores según tus necesidades
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClienteDto>> Post(ClienteDto clienteDto)
        {
            var cliente = _mapper.Map<Cliente>(clienteDto);
            _unitOfWork.Clientes.Add(cliente);
            await _unitOfWork.SaveAsync();

            clienteDto.CodigoCliente = cliente.CodigoCliente;
            return CreatedAtAction(nameof(Get), new { id = clienteDto.CodigoCliente }, clienteDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClienteDto>> Put(string id, [FromBody] ClienteDto clienteDto)
        {
            if (clienteDto == null)
                return BadRequest();

            var cliente = await _unitOfWork.Clientes.GetByIdSTring(id);
            if (cliente == null)
                return NotFound();

            _mapper.Map(clienteDto, cliente);
            _unitOfWork.Clientes.Update(cliente);
            await _unitOfWork.SaveAsync();

            return Ok(clienteDto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            var cliente = await _unitOfWork.Clientes.GetByIdSTring(id);
            if (cliente == null)
                return NotFound();

            _unitOfWork.Clientes.Remove(cliente);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
