using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Application.Helper;
using QuizReviewApplication.Application.Services;
using QuizReviewApplication.Domain.Entities;

[ApiController]
[Route("api/[controller]")]
public class RefreshTokenController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly UserManager<ApplicationUser> _userManager;

    public RefreshTokenController(ITokenService tokenService, UserManager<ApplicationUser> userManager)
    {
        _tokenService = tokenService;
        _userManager = userManager;
    }

    [HttpPost("renew")]
    public async Task<IActionResult> RenewToken([FromBody] RefreshTokenRequestDto request)
    {
        if (string.IsNullOrEmpty(request.RefreshToken))
        {
            return BadRequest( ApiResponse<string>.FailureResponse("Refresh token is required") );
          
        }

        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
        {
            return Unauthorized(ApiResponse<string>.FailureResponse("User Not Found"));
        }

        var newRefreshToken = await _tokenService.RotateRefreshToken(user, request.RefreshToken, Request.HttpContext.Connection.RemoteIpAddress?.ToString());

        if (newRefreshToken == null)
        {
            return Unauthorized(ApiResponse<string>.FailureResponse("Invalid or expired refresh token"));
        }

        var newJwtToken = await _tokenService.CreateToken(user);

        return Ok(ApiResponse<AuthResponseDto>.SuccessResponse("Token refreshed successfully", new AuthResponseDto
        {
            Token = newJwtToken,
            RefreshToken = newRefreshToken.Token
        })
        );
       
    }
}
