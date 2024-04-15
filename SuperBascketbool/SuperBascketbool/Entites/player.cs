using System;

namespace SuperBascketbool.Entites;

public class player
{
    public int PlayerId { get; set; }
    public string PlayerName { get; set; }
    public string Position { get; set; }
    public double Weight { get; set; }
    public double Height { get; set; }
    public DateTime Birthday { get; set; }
    public DateTime GameStart { get; set; }
    public string Team { get; set; }
}