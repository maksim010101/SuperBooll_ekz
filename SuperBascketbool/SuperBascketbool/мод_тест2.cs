namespace SuperBascketbool;

public class мод_тест2
{
    public void Test2()
    {
        double t = 0;
        double expected = 0;

        double actual = CalcT(t);
        Assert.AreEqual(expected, actual);
    }

    private static double CalcT(double t)
    {
        return 2 * t / 9.8;
    }
}