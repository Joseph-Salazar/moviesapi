﻿using Domain.Core.Entity;

namespace Domain.MainModule.Entity;

public class Movie : Entity<int>
{
    public Movie()
    {
        this.Genres = new HashSet<Genre>();
    }
    public string Title { get; set; }
    public int Budget { get; set; }
    public string HomePage { get; set; }
    public string Overview { get; set; }
    public float Popularity { get; set; }
    public DateTime ReleaseDate { get; set; }
    public long Revenue { get; set; }
    public int RunTime { get; set; }
    public string MovieStatus { get; set; }
    public string TagLine { get; set; }
    public float VoteAverage { get; set; } = 0;
    public int VoteCount { get; set; } = 0;
    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
}