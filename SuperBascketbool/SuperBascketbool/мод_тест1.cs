using System.Diagnostics;

namespace SuperBascketbool;

public class мод_тест1
{
    public void Test1()
    {
        double t = 1;
        double expected = 0.204;

        double actual = CalcT(t);
        Assert.AreEqual(expected, actual, 0.001);
    }

    private static double CalcT(double t)
    {
        return 2 * t / 9.8;
    }
}