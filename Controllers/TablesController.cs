using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantProject.Exceptions;
using RestaurantProject.Models;
using RestaurantProject.Models.DTOs;
using RestaurantProject.Services;
using RestaurantProject.Services.IServices;
using System.ComponentModel.DataAnnotations;
using ValidationException = RestaurantProject.Exceptions.ValidationException;

namespace RestaurantProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        private readonly ITableService _tableService;//serv dep inj

        public TablesController(ITableService tableService)
        {
            _tableService = tableService;
        }

        [HttpGet("getAllTables")]//remember await after declaring...
        public async Task<ActionResult<IEnumerable<Table>>> GetAllTables()
        {
            var tables = await _tableService.GetAllTablesAsync();
            return Ok(tables);
        }

        [HttpPost("addTable")]
        public async Task<ActionResult> AddTable(TableDTO2 tableDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _tableService.AddTableAsync(tableDTO);
                return Created();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error in handling request.");
            }
        }

        [HttpGet("getTable/{tableId}")]
        public async Task<ActionResult<Table>> FindTableById(int tableId)
        {
            if (tableId == null)
            {
                return BadRequest("Input table ID, please.");
            }

            var table = await _tableService.FindTableByIdAsync(tableId);

            if (table == null)
            {
                return NotFound();
            }

            return table;
        }

        //[HttpGet("getTableByTableNo/{tableNo}")]
        //public async Task<ActionResult> FindTableByTableNo(int tableNo)
        //{
        //    var table = await _tableService.FindTableByTableNoAsync(tableNo);

        //    if (table == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(table);
        //}

        [HttpPut("updateTable/{tableId}")]
        public async Task<ActionResult<TableDTO>> UpdateTable(int tableId, TableDTO2 tableDTO)
        {
            if (tableId == null)
            {
                return BadRequest("Input table ID, please.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _tableService.UpdateTableAsync(tableId, tableDTO);
                return NoContent();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error in handling request");
            }
        }


        [HttpDelete("deleteTable/{tableId}")]
        public async Task<ActionResult> DeleteDish(int tableId)
        {
            if (tableId == null)
            {
                return BadRequest("Input table ID, please.");
            }

            try
            {
                await _tableService.DeleteTableAsync(tableId);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error in handling request");
            }
        }
    }
}
