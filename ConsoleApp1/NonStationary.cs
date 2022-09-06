namespace ConsoleApp1;

public class NonStationary
{
    public static void KrankNicolson(Area area, Initial initial, TimeGrid timeGrid, Grid grid, Slae slae, int index, int func)
    {
        var bPred = new double[(2 * area.Rpoint - 1) * (2 * area.Zpoint - 1)];
        var bNext = new double[(2 * area.Rpoint - 1) * (2 * area.Zpoint - 1)];
        Slae.GlobalBildBandF(area,grid,slae,ref bNext, timeGrid.TimeNode[index], func);
        Slae.GlobalBildBandF(area,grid,slae,ref bPred, timeGrid.TimeNode[index-1], func);
        var dt = timeGrid.TimeNode[index] - timeGrid.TimeNode[index - 1];

        for (int i = 0; i < slae.A.Di.Length; i++)
            slae.A.Di[i] = slae.GlobalG.Di[i]/2.0 + slae.GlobalM.Di[i] / dt;
        for (int i = 0; i < slae.GlobalG.Al.Length; i++)
            slae.A.Al[i] = slae.GlobalG.Al[i]/2.0 + slae.GlobalM.Al[i] / dt;
        for (int i = 0; i < slae.GlobalG.Au.Length; i++)
            slae.A.Au[i] = slae.GlobalG.Au[i]/2.0 + slae.GlobalM.Au[i] / dt;

        var mqPred = Matrix.Mult(slae.GlobalM, slae.Qpred);
        var gqPred = Matrix.Mult(slae.GlobalG, slae.Qpred);
        for (int i = 0; i < slae.B.Length; i++)
        {
            slae.B[i] = (bNext[i] + bPred[i] + 2 * mqPred[i] / dt - gqPred[i])/(2.0);
        }
    }
}