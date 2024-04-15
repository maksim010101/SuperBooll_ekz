namespace SuperBascketbool.Entites;

public class team
{
    public int TeamId { get; set; }
    public string TeamName { get; set; }
    public override string ToString()
    {
        return TeamName;
    }
}