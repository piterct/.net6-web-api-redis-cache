﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Redis.Cache.API.Commands;
using Redis.Cache.Application.Inrterfaces.Repositories;
using Redis.Cache.Application.Inrterfaces.Services;
using Redis.Cache.Application.Models;

namespace Redis.Cache.API.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("/api/[controller]")]
    public class LikeController : Controller
    {
        private readonly ILogger<LikeController> _logger;
        private readonly ILikeRepository _likeRepository;
        private readonly ILikeService _likeService;


        public LikeController(ILogger<LikeController> logger, ILikeRepository likeRepository,
            ILikeService likeService)
        {
            _logger = logger;
            _likeRepository = likeRepository;
            _likeService = likeService;
        }


        [HttpGet]
        [Route("likes/{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var like = await _likeService.GetLike(id);

                if (like == null)
                    return NotFound(like);

                return Ok(like);
            }
            catch (Exception exception)
            {
                _logger.LogError("An exception has occurred at {dateTime}. " +
                 "Exception message: {message}." +
                 "Exception Trace: {trace}", DateTime.UtcNow, exception.Message, exception.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("likes/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var like = await _likeService.GetLike(id);

                if (like is null)
                    return NotFound("It was not possible to found the id.");

                await _likeService.RemoveAsync(like);

                return Ok(like);
            }
            catch (Exception exception)
            {
                _logger.LogError("An exception has occurred at {dateTime}. " +
                 "Exception message: {message}." +
                 "Exception Trace: {trace}", DateTime.UtcNow, exception.Message, exception.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpGet]
        [Route("likes")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLikes()
        {
            try
            {
                var like = await _likeService.GetLikes();

                if (like == null || !like.Any())
                    return NotFound(like);

                return Ok(like);
            }
            catch (Exception exception)
            {
                _logger.LogError("An exception has occurred at {dateTime}. " +
                 "Exception message: {message}." +
                 "Exception Trace: {trace}", DateTime.UtcNow, exception.Message, exception.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post(CreateLikeCommand command)
        {
            try
            {
                var like = await _likeRepository.Add(new Like(command.Name));
                return Ok(like);
            }
            catch (Exception exception)
            {
                _logger.LogError("An exception has occurred at {dateTime}. " +
                 "Exception message: {message}." +
                 "Exception Trace: {trace}", DateTime.UtcNow, exception.Message, exception.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }



    }
}
