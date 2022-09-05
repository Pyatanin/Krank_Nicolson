using System.IO;
using System.Text.Json;

namespace ConsoleApp1;

public static class Program
{
    public static void Main()
    {
        Elepticheskai(1, "TEST");
        /*var funcEleptic = new[] 
        { 
            "u=5",
            "u=x+y",
            "u=x^2+y^2",
            "u=x^3+y^3",
            "u=x^4+y^4",
            "u=x^5+y^5",
            "u=sin(x)+sin(y)",
            "u=exp(x)+exp(y)"
        };
       for (int i = 1; i < 9; i++)
       {
           Elepticheskai(8*(i-1)+1, funcEleptic[i-1]);
       }*/
       /*var funcParabal = new[] 
       { 
           "u=5",
           "u=5+t",
           "u=5+t^2",
           "u=5+t^3",
           "u=5+t^4",
           "u=5+t^5",
           "u=5+sin(t)",
           "u=5+e^t",
           "u=x+y",
           "u=x+y+t",
           "u=x+y+t^2",
           "u=x+y+t^3",
           "u=x+y+t^4",
           "u=x+y+t^5",
           "u=x+y+sin(t)",
           "u=x+y+e^t",
           "u=x^2+y^2",
           "u=x^2+y^2+t",
           "u=x^2+y^2+t^2",
           "u=x^2+y^2+t^3",
           "u=x^2+y^2+t^4",
           "u=x^2+y^2+t^5",
           "u=x^2+y^2+sin(t)",
           "u=x^2+y^2+e^t",
           "u=x^3+y^3",
           "u=x^3+y^3+t",
           "u=x^3+y^3+t^2",
           "u=x^3+y^3+t^3",
           "u=x^3+y^3+t^4",
           "u=x^3+y^3+t^5",
           "u=x^3+y^3+sin(t)",
           "u=x^3+y^3+e^t",
           "u=x^4+y^4",
           "u=x^4+y^4+t",
           "u=x^4+y^4+t^2",
           "u=x^4+y^4+t^3",
           "u=x^4+y^4+t^4",
           "u=x^4+y^4+t^5",
           "u=x^4+y^4+sin(t)",
           "u=x^4+y^4+e^t",
           "u=x^5+y^5",
           "u=x^5+y^5+t",
           "u=x^5+y^5+t^2",
           "u=x^5+y^5+t^3",
           "u=x^5+y^5+t^4",
           "u=x^5+y^5+t^5",
           "u=x^5+y^5+sin(t)",
           "u=x^5+y^5+e^t",
           "u=sin(x)+sin(y)",
           "u=sin(x)+sin(y)+t",
           "u=sin(x)+sin(y)+t^2",
           "u=sin(x)+sin(y)+t^3",
           "u=sin(x)+sin(y)+t^4",
           "u=sin(x)+sin(y)+t^5",
           "u=sin(x)+sin(y)+sin(t)",
           "u=sin(x)+sin(y)+e^t",
           "u=exp(x)+exp(y)",
           "u=exp(x)+exp(y)+t",
           "u=exp(x)+exp(y)+t^2",
           "u=exp(x)+exp(y)+t^3",
           "u=exp(x)+exp(y)+t^4",
           "u=exp(x)+exp(y)+t^5",
           "u=exp(x)+exp(y)+sin(t)",
           "u=exp(x)+exp(y)+e^t",
           "u=x^3+y^3+t^10"
       };
       for (int i = 1; i < 66; i++)
       {
           KrankNicolson(i, funcParabal[i-1]);
       }*/
    }


    public static void KrankNicolson(int func, string str)
    {
        // считываем данные
        var area = JsonSerializer.Deserialize<Area>(File.ReadAllText("Data/Area.json"))!;
        var boundary = JsonSerializer.Deserialize<BoundaryData>(File.ReadAllText("Data/Boundary.json"))!;
        var initial = JsonSerializer.Deserialize<Initial>(File.ReadAllText("Data/Initial.json"))!;
        var fem = new Fem(area, boundary, initial, true, 0, func);
        for (int i = 1; i < fem.TimeGrid.TimeNode.Length; i++)
        {
            var time = fem.TimeGrid.TimeNode[i];

            NonStationary.KrankNicolson(area, initial, fem.TimeGrid, fem.Grid, fem.Slae, i, func);
            Boundary.Two(area, fem.Grid, boundary, ref fem.Slae, time);
            Boundary.Three(area, fem.Grid, boundary, ref fem.Slae, time);
            Boundary.One(area, fem.Grid, boundary, ref fem.Slae, time, func);
            var factoriz = new double[3][];
            factoriz[0] = new double[fem.Slae.A.Di.Length];
            factoriz[1] = new double[fem.Slae.A.Al.Length];
            factoriz[2] = new double[fem.Slae.A.Au.Length];
            var k = 0;
            var error = 0.0;
            fem.Slae.LUsq(factoriz[0], factoriz[1], factoriz[2]);
            fem.Slae.LoSPrecond(factoriz[0], factoriz[1], factoriz[2], ref fem.Slae.Q, ref k, ref error);
            Answer(fem, area, k, fem.Slae.Q, error, fem.TimeGrid.TimeNode[i], "Parobolicheskai", func, str);
        }
    }

    public static void Elepticheskai(int func, string str)
    {
        var area = JsonSerializer.Deserialize<Area>(File.ReadAllText("Data/Area.json"))!;
        var boundary = JsonSerializer.Deserialize<BoundaryData>(File.ReadAllText("Data/Boundary.json"))!;
        var fem = new Fem(area, boundary, func);
        Boundary.Two(area, fem.Grid, boundary, ref fem.Slae, 0);
        Boundary.Three(area, fem.Grid, boundary, ref fem.Slae, 0);
        Boundary.One(area, fem.Grid, boundary, ref fem.Slae, 0, func);
        var factoriz = new double[3][];
        factoriz[0] = new double[fem.Slae.A.Di.Length];
        factoriz[1] = new double[fem.Slae.A.Al.Length];
        factoriz[2] = new double[fem.Slae.A.Au.Length];
        fem.Slae.LUsq(factoriz[0], factoriz[1], factoriz[2]);
        var iteration = 0;
        var error = 0.0;
        fem.Slae.LoSPrecond(factoriz[0], factoriz[1], factoriz[2], ref fem.Slae.Q, ref iteration, ref error);
        Answer(fem, area, iteration, fem.Slae.Q, error, 0, "Elepticheskai", func, str);
    }

    public static void Answer(Fem fem, Area area, int iteration, double[] q, double error, double time, string Metod, int func,string str)
    {
        using (System.IO.StreamWriter sw = new System.IO.StreamWriter($"{Metod}.txt", true))
        {
            var realf = new double[q.Length];
            sw.WriteLine($"\n{str}\n{Metod}:\ntime:{time}\niteration:{iteration}\nerror:{error}");
            sw.Write("____________________________________________________________\n");
            sw.Write("|__________(r,z)|____________Q|________RealF|_______Q-RealF|\n");
            var k = 0;
            foreach (var z in fem.Grid.Y)
            {
                foreach (var r in fem.Grid.X)
                {
                    realf[k]= Func.RealF(r, z, time, func);
                    if (q[k] - realf[k] < 0.0)
                        sw.Write(
                            $"|({r:0.0000};{z:0.0000})|  " +
                            "{0,10:0.000000000}|  {1,10:0.000000000}|  {2,10:0.000000000}|\n", q[k], realf[k],
                            q[k] - realf[k]);
                    else
                        sw.Write(
                            $"|({r:0.0000};{z:0.0000})|  " +
                            "{0,10:0.000000000}|  {1,10:0.000000000}|   {2,10:0.000000000}|\n", q[k], realf[k],
                            q[k] - realf[k]);
                    k++;
                }
            }

            var residual = Slae.scalar_prod(realf, realf);

            for (int i = 0; i < q.Length; i++)
            {
                realf[i] -= q[i];
            }

            residual = Slae.scalar_prod(realf, realf) / residual;
            sw.WriteLine($"\nresidual={residual}");
            sw.Close();
        }
    }
}