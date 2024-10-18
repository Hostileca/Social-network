﻿namespace Domain.Filters;

public class BlogFilter
{
    public string Username { get; set; }
    
    public int MinimalAge { get; set; }
    
    public int MaximumAge { get; set; }
    
    public int MinimalSubscribersCount { get; set; }
    
    public int MaximumSubscribersCount { get; set; }
    
    public int MinimalPostsCount { get; set; }
    
    public int MaximumPostsCount { get; set; }
}