﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyBooksLibrary.Data.Services;
using MyBooksLibrary.Data.ViewModels;
using MyBooksLibrary.Exceptions;
using System;

namespace MyBooksLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly PublishersService _publishersService;
        private readonly ILogger<PublishersController> _logger;

        public PublishersController(PublishersService publishersService, ILogger<PublishersController> logger)
        {
            _publishersService = publishersService;
            _logger = logger;
        }

        [HttpGet("get-all-publishers")]
        public IActionResult GetAllPublishers(string sortBy, string searchString, int? pageNumber)
        {
            try
            {
                _logger.LogInformation("This is just log from GetAllPublishers");
                var result = _publishersService.GetAllPublishers(sortBy, searchString, pageNumber);
                return Ok(result);
            }
            catch
            {
                return BadRequest("Sorry, we couldn't load data");
            }
            
        }

        [HttpPost("add-publisher")]
        public IActionResult AddPublisher([FromBody] PublisherViewModel publisher)
        {
            var addedPublisher = _publishersService.AddPublisher(publisher);
            return Created(nameof(AddPublisher), addedPublisher);
            
        }

        [HttpGet("get-publisher-books-with-authors-by-id/{id}")]
        public IActionResult GetPublisherDataById(int id)
        {
            var response = _publishersService.GetPublisherDataById(id);

            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("get-publisher-by-id/{id}")]
        public IActionResult GetPublisherById(int id)
        {
            var response = _publishersService.GetPublisherById(id);

            if(response != null)
            {
                return Ok(response);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("delete-publisher-by-id/{id}")]
        public IActionResult DeletePublisherById(int id)
        {
            try
            {
                _publishersService.DeletePublisherById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
