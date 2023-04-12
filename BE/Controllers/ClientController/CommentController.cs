using BE.Application.Interfaces;
using BE.Application.ViewModels.Client;
using BE.Application.ViewModels.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.Controllers.ClientController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        [Route("GetComments/{propertyId}")]
        public async Task<List<CommentViewModel>> GetComments(int propertyId)
        {
            return await _commentService.GetComments(propertyId);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Client")]
        [Route("AddComment")]
        public async Task<IActionResult> AddComment(AddCommentViewModel viewModel)
        {
            var result = await _commentService.AddComment(viewModel);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}