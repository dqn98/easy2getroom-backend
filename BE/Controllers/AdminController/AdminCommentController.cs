using BE.Application.Interfaces;
using BE.Application.ViewModels.Admin.Comment;
using BE.Application.ViewModels.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BE.Controllers.AdminController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminCommentController : ControllerBase
    {
        private ICommentService _commentService;

        public AdminCommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("GetComments")]
        public async Task<IActionResult> GetComments(GetCommentViewModel viewModel)
        {
            var result = await _commentService.GetComments(viewModel);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("GetChildComments")]
        public async Task<IActionResult> GetChildComments(GetChildCommentsViewModel viewModel)
        {
            var result = await _commentService.GetChildComments(viewModel);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
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