﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PsicoAppAPI.Data;
using PsicoAppAPI.Models;
namespace PsicoAppAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FeedController : ControllerBase
{
    private readonly DataContext _context;
    
    public FeedController(DataContext context) => _context = context;
    
    /// <summary>
    /// Add a feed post in database context 
    /// </summary>
    /// <param name="post">Post to add</param>
    /// <returns>Post saved</returns>
    [HttpPost]
    public IActionResult AddFeedPost(FeedPost post)
    {
        _context.FeedPosts.Add(post);
        _context.SaveChanges();
        return Ok(post);
    }
}