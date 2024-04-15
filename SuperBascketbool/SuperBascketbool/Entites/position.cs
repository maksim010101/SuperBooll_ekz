namespace SuperBascketbool.Entites;

public class position
{
    public int PositionId { get; set; }
    public string PositionName { get; set; }
    public override string ToString()
    {
        return PositionName;
    }
}