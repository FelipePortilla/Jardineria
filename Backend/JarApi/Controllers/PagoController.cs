using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Interfaces;
using JarApi.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Domain.entities;
using ApiApolo.Controllers;

namespace JarApi.Controllers
{
    public class PagoController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PagoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<PagoDto>>> Get()
        {
            var pagos = await _unitOfWork.Pagos.GetAllAsync();

            return _mapper.Map<List<PagoDto>>(pagos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PagoDto>> Get(int id)
        {
            var pago = await _unitOfWork.Pagos.GetByIdAsync(id);
            if (pago == null)
                return NotFound();

            return _mapper.Map<PagoDto>(pago);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PagoDto>> Post(PagoDto pagoDto)
        {
            var pago = _mapper.Map<Pago>(pagoDto);
            _unitOfWork.Pagos.Add(pago);
            await _unitOfWork.SaveAsync();

            if (pago == null)
            {
                return BadRequest();
            }

            pagoDto.CodigoCliente = pago.CodigoCliente;
            pagoDto.IdTransaccion = pago.IdTransaccion;

            return CreatedAtAction(nameof(Post), new { id = pagoDto.CodigoCliente, idTransaccion = pagoDto.IdTransaccion }, pagoDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PagoDto>> Put(int id, [FromBody] PagoDto pagoDto)
        {
            if (pagoDto == null)
                return NotFound();

            var pago = await _unitOfWork.Pagos.GetByIdAsync(id);
            if (pago == null)
                return NotFound();

            var updatedPago = _mapper.Map<Pago>(pagoDto);
            _unitOfWork.Pagos.Update(updatedPago);
            await _unitOfWork.SaveAsync();

            return pagoDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var pago = await _unitOfWork.Pagos.GetByIdAsync(id);
            if (pago == null)
                return NotFound();

            _unitOfWork.Pagos.Remove(pago);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}
