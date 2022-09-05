namespace ConsoleApp1;

public class Slae
{
    public Matrix A;
    public Matrix GlobalM;
    public Matrix GlobalG;
    public double[] Q;
    public double[] Qpred;
    public double[] B;
    public double[] GlobalB;
    
    public Slae(Area area, Grid grid, int func)
    {
        Q = new double[(2 * area.Rpoint - 1) * (2 * area.Zpoint - 1)];
        Q.AsSpan().Fill(1);
        A = new Matrix();
        Matrix.Profile(area, grid, A);
        GlobalBuild(area, grid, ref A, ref B, func);
    }

    public Slae(Area area, Initial initial, Grid grid, TimeGrid timeGrid, double time, int func)
    {
        Qpred = new double[(2 * area.Rpoint - 1) * (2 * area.Zpoint - 1)];
        Qpred.AsSpan().Fill(1);
        Q = new double[(2 * area.Rpoint - 1) * (2 * area.Zpoint - 1)];
        Q.AsSpan().Fill(1);

        A = new Matrix();
        GlobalG = new Matrix();
        GlobalM = new Matrix();
        Matrix.Profile(area, grid, A);
        GlobalG.Ig = A.Ig;
        GlobalM.Ig = A.Ig;
        GlobalG.Jg = A.Jg;
        GlobalM.Jg = A.Jg;
        GlobalBildAll(area, grid, ref A, ref GlobalG, ref GlobalM, ref GlobalB, time, func);
        B = new double[(2 * area.Rpoint - 1) * (2 * area.Zpoint - 1)];
        GlobalB.AsSpan().CopyTo(B);
        var k = 0;
        foreach (var y in grid.Y)
        {
            foreach (var x in grid.X)
            {
                Qpred[k] = Func.RealF(x, y, timeGrid.TimeNode[0],func);
                k++;
            }
        }
    }
    private void GlobalBildAll(Area area, Grid grid, ref Matrix A, ref Matrix g, ref Matrix m, ref double[]? B,
        double time,int func)
    {
        double sigma = 0;
        var locB = new double[9];
        var locG = new double[][] { };
        var locMass = new double[][] { };
        var locStiffness = new double[][][] { };
        var bCopy = new double[(2 * area.Rpoint - 1) * (2 * area.Zpoint - 1)];


        g.Al = new double[g.Ig[(2 * area.Rpoint - 1) * (2 * area.Zpoint - 1)]];
        g.Au = new double[g.Ig[(2 * area.Rpoint - 1) * (2 * area.Zpoint - 1)]];
        g.Di = new double[(2 * area.Rpoint - 1) * (2 * area.Zpoint - 1)];

        m.Al = new double[m.Ig[(2 * area.Rpoint - 1) * (2 * area.Zpoint - 1)]];
        m.Au = new double[m.Ig[(2 * area.Rpoint - 1) * (2 * area.Zpoint - 1)]];
        m.Di = new double[(2 * area.Rpoint - 1) * (2 * area.Zpoint - 1)];

        A.Al = new double[g.Ig[(2 * area.Rpoint - 1) * (2 * area.Zpoint - 1)]];
        A.Au = new double[g.Ig[(2 * area.Rpoint - 1) * (2 * area.Zpoint - 1)]];

        for (int i = 0; i < (area.Rpoint - 1) * (area.Zpoint - 1); i++)
        {
            var rk = grid.Node[grid.ElPints[1][i][0]][0];
            var hx = grid.Node[grid.ElPints[1][i][1]][0] - rk;
            var hy = grid.Node[grid.ElPints[1][i][2]][1] - grid.Node[grid.ElPints[1][i][0]][1];
            loc_build(rk, hx, hy, ref locMass, ref locStiffness);
            loc_f(ref locB, i, grid, time,func);
            m_mult_v(ref locB, locMass);
            for (int k = 0; k < 9; k++)
            {
                bCopy[grid.ElPints[0][i][k]] += locB[k];
            }

            loc_G(i, grid, ref locG, locStiffness, time);
            loc_sigma(i, ref sigma, grid, time);

            for (int k = 0; k < 9; k++)
            {
                g.Di[grid.ElPints[0][i][k]] += locG[k][k];
                m.Di[grid.ElPints[0][i][k]] += sigma * locMass[k][k];
            }

            int Index = 0;
            for (int k = 1; k < 9; k++)
            {
                for (int j = 0; j < k; j++)
                {
                    for (Index = g.Ig[grid.ElPints[0][i][k]]; g.Jg[Index] != grid.ElPints[0][i][j];)
                    {
                        Index++;
                    }

                    g.Al[Index] += locG[k][j];
                    m.Al[Index] += sigma * locMass[k][j];
                }
            }
        }

        B = bCopy;
        g.Al.AsSpan().CopyTo(g.Au);
        m.Al.AsSpan().CopyTo(m.Au);
    }

    public static void GlobalBildBandF(Area area, Grid grid, Slae slae, ref double[]? B, double time,int func)
    {
        var bCopy = new double[(2 * area.Rpoint - 1) * (2 * area.Zpoint - 1)];
        var f = new double[(2 * area.Rpoint - 1) * (2 * area.Zpoint - 1)];

        var k = 0;
        foreach (var z in grid.Y)
        {
            foreach (var r in grid.X)
            {
                f[k] = Func.F(r,z,time, func);
                k++;
            }
        }

        bCopy = Matrix.Mult(slae.GlobalM, f);

        B = bCopy;
    }

    public static void GlobalBuild(Area area, Grid grid, ref Matrix matrix, ref double[] b, int func)
    {
        double gamma = 0;
        var locB = new double[9];
        var locG = new double[][] { };
        var locMass = new double[][] { };
        var locStiffness = new double[][][] { };

        var bCopy = new double[(2 * area.Rpoint - 1) * (2 * area.Zpoint - 1)];

        matrix.Al = new double[matrix.Ig[(2 * area.Rpoint - 1) * (2 * area.Zpoint - 1)]];
        matrix.Au = new double[matrix.Ig[(2 * area.Rpoint - 1) * (2 * area.Zpoint - 1)]];

        for (int i = 0; i < (area.Rpoint - 1) * (area.Zpoint - 1); i++)
        {
            var rk = grid.Node[grid.ElPints[1][i][0]][0];
            var hx = grid.Node[grid.ElPints[1][i][1]][0] - rk;
            var hy = grid.Node[grid.ElPints[1][i][2]][1] - grid.Node[grid.ElPints[1][i][0]][1];
            loc_build(rk, hx, hy, ref locMass, ref locStiffness);
            loc_gamma(i, ref gamma, grid, 0);
            loc_f(ref locB, i, grid, 0, func);
            m_mult_v(ref locB, locMass);
            for (int k = 0; k < 9; k++)
            {
                bCopy[grid.ElPints[0][i][k]] += locB[k];
            }

            loc_G(i, grid, ref locG, locStiffness, 0);
            for (int k = 0; k < 9; k++)
            {
                matrix.Di[grid.ElPints[0][i][k]] += gamma * locMass[k][k] + locG[k][k];
            }

            int Index = 0;
            for (int k = 1; k < 9; k++)
            {
                for (int j = 0; j < k; j++)
                {
                    for (Index = matrix.Ig[grid.ElPints[0][i][k]]; matrix.Jg[Index] != grid.ElPints[0][i][j];)
                    {
                        Index++;
                    }

                    matrix.Al[Index] += gamma * locMass[k][j] + locG[k][j];
                }
            }
        }

        matrix.Al.AsSpan().CopyTo(matrix.Au);
        b = bCopy;
    }

    private static void loc_G(int num, Grid grid, ref double[][] LocG, double[][][] locStiffness, double time)
    {
        var locPhiLam = new double[4];
        loc_lam(num, ref locPhiLam, grid, time);
        LocG = new double[9][];
        for (int i = 0; i < 9; i++)
        {
            LocG[i] = new double[9];
            for (int j = 0; j < 9; j++)
            {
                LocG[i][j] = locPhiLam[0] * locStiffness[0][i][j]
                             + locPhiLam[1] * locStiffness[1][i][j]
                             + locPhiLam[2] * locStiffness[2][i][j]
                             + locPhiLam[3] * locStiffness[3][i][j];
            }
        }
    }

    private static void loc_lam(int num, ref double[] locPhiLam, Grid grid, double time)
    {
        for (int i = 0; i < 4; i++)
        {
            locPhiLam[i] = Func.Lambda(grid.Node[grid.ElPints[1][num][i]][0], grid.Node[grid.ElPints[1][num][i]][1],
                time);
        }
    }

    private static void m_mult_v(ref double[] f, double[][] locMass)
    {
        var res = new double[9];
        for (int i = 0; i < 9; i++)
        {
            double sum = 0.0;
            for (int j = 0; j < 9; j++)
            {
                sum += f[i] * locMass[i][j];
            }

            res[i] = sum;
        }

        f = res;
    }

    private static void loc_f(ref double[] LocB, int num, Grid grid, double time,int func)
    {
        double hx, hy;
        hx = (grid.Node[grid.ElPints[1][num][1]][0] - grid.Node[grid.ElPints[1][num][0]][0]) / 2.0;
        hy = (grid.Node[grid.ElPints[1][num][2]][1] - grid.Node[grid.ElPints[1][num][0]][1]) / 2.0;
        var res = new double[9];
        for (int i = 0; i < 9; i++)
        {
            res[i] = Func.F(grid.Node[grid.ElPints[1][num][0]][0] + hx * (i % 3),
                grid.Node[grid.ElPints[1][num][0]][1] + hy * (i / 3), time,func);
        }

        LocB = res;
    }

    private static void loc_sigma(int num, ref double sigma, Grid grid, double time)
    {
        double hx;
        double hy;
        double g = 0;
        hx = (grid.Node[grid.ElPints[1][num][1]][0] - grid.Node[grid.ElPints[1][num][0]][0]) / 2.0;
        hy = (grid.Node[grid.ElPints[1][num][2]][1] - grid.Node[grid.ElPints[1][num][0]][1]) / 2.0;
        for (int i = 0; i < 9; i++)
        {
            g += Func.Sigma(grid.Node[grid.ElPints[1][num][0]][0] + hx * (i % 3),
                grid.Node[grid.ElPints[1][num][0]][1] + hy * (i / 3), time);
        }

        sigma = g / 9.0;
    }

    private static void loc_gamma(int num, ref double gamma, Grid grid, double time)
    {
        double hx;
        double hy;
        double g = 0;
        hx = (grid.Node[grid.ElPints[1][num][1]][0] - grid.Node[grid.ElPints[1][num][0]][0]) / 2.0;
        hy = (grid.Node[grid.ElPints[1][num][2]][1] - grid.Node[grid.ElPints[1][num][0]][1]) / 2.0;
        for (int i = 0; i < 9; i++)
        {
            g += Func.Gamma(grid.Node[grid.ElPints[1][num][0]][0] + hx * (i % 3),
                grid.Node[grid.ElPints[1][num][0]][1] + hy * (i / 3), time);
        }

        gamma = g / 9.0;
    }

    private static void loc_build(double rk, double hx, double hy, ref double[][] LocMass,
        ref double[][][] locStiffness)
    {
        var LocMassPsi = new double[3][][];
        // Мытрица х/у
        LocMassPsi[0] = new double[3][];
        LocMassPsi[0][0] = new double[3];
        LocMassPsi[0][1] = new double[3];
        LocMassPsi[0][2] = new double[3];
        LocMassPsi[0][0][0] = 1.0 / 60.0;
        LocMassPsi[0][0][1] = 0.0;
        LocMassPsi[0][0][2] = -1.0 / 60.0;
        LocMassPsi[0][1][0] = 0.0;
        LocMassPsi[0][1][1] = 4.0 / 15.0;
        LocMassPsi[0][1][2] = 1.0 / 15.0;
        LocMassPsi[0][2][0] = -1.0 / 60.0;
        LocMassPsi[0][2][1] = 1.0 / 15.0;
        LocMassPsi[0][2][2] = 7.0 / 60.0;

        // 1-х/1-у
        LocMassPsi[1] = new double[3][];
        LocMassPsi[1][0] = new double[3];
        LocMassPsi[1][1] = new double[3];
        LocMassPsi[1][2] = new double[3];
        LocMassPsi[1][0][0] =  7.0 / 60.0;
        LocMassPsi[1][0][1] =  1.0 / 15.0;
        LocMassPsi[1][0][2] =  -1.0 / 60.0;
        LocMassPsi[1][1][0] =  1.0 / 15.0;
        LocMassPsi[1][1][1] =  4.0 / 15.0;
        LocMassPsi[1][1][2] =  0.0;
        LocMassPsi[1][2][0] =  -1.0 / 60.0;
        LocMassPsi[1][2][1] =  0.0;
        LocMassPsi[1][2][2] =  1.0 / 60.0;

        //Стандарт
        LocMassPsi[2] = new double[3][];
        LocMassPsi[2][0] = new double[3];
        LocMassPsi[2][1] = new double[3];
        LocMassPsi[2][2] = new double[3];
        LocMassPsi[2][0][0] = 2.0 / 15.0;
        LocMassPsi[2][0][1] = 1.0 / 15.0;
        LocMassPsi[2][0][2] = -1.0 / 30.0;
        LocMassPsi[2][1][0] = 1.0 / 15.0;
        LocMassPsi[2][1][1] = 8.0 / 15.0;
        LocMassPsi[2][1][2] = 1.0 / 15.0;
        LocMassPsi[2][2][0] = -1.0 / 30.0;
        LocMassPsi[2][2][1] = 1.0 / 15.0;
        LocMassPsi[2][2][2] = 2.0 / 15.0;

        var LocGPsi = new double[3][][];

        // х/у
        LocGPsi[0] = new double[3][];
        LocGPsi[0][0] = new double[3];
        LocGPsi[0][1] = new double[3];
        LocGPsi[0][2] = new double[3];
        LocGPsi[0][0][0] = 1.0 / 2.0;
        LocGPsi[0][0][1] = -2.0 / 3.0;
        LocGPsi[0][0][2] = 1.0 / 6.0;
        LocGPsi[0][1][0] = -2.0 / 3.0;
        LocGPsi[0][1][1] = 8.0 / 3.0;
        LocGPsi[0][1][2] = -2.0;
        LocGPsi[0][2][0] = 1.0 / 6.0;
        LocGPsi[0][2][1] = -2.0;
        LocGPsi[0][2][2] = 11.0 / 6.0;
        //1-х\1-у
        LocGPsi[1] = new double[3][];
        LocGPsi[1][0] = new double[3];
        LocGPsi[1][1] = new double[3];
        LocGPsi[1][2] = new double[3];
        LocGPsi[1][0][0] = 11.0 / 6.0;
        LocGPsi[1][0][1] = -2.0;
        LocGPsi[1][0][2] = 1.0 / 6.0;
        LocGPsi[1][1][0] = -2.0;
        LocGPsi[1][1][1] = 8.0 / 3.0;
        LocGPsi[1][1][2] = -2.0 / 3.0;
        LocGPsi[1][2][0] = 1.0 / 6.0;
        LocGPsi[1][2][1] = -2.0 / 3.0;
        LocGPsi[1][2][2] = 1.0 / 2.0;
        //Новая 1-x
        LocGPsi[2] = new double[3][];
        LocGPsi[2][0] = new double[3];
        LocGPsi[2][1] = new double[3];
        LocGPsi[2][2] = new double[3];
        LocGPsi[2][0][0] = 7.0 / 3.0;
        LocGPsi[2][0][1] = -8.0 / 3.0;
        LocGPsi[2][0][2] = 1.0 / 3.0;
        LocGPsi[2][1][0] = -8.0 / 3.0;
        LocGPsi[2][1][1] = 16.0 / 3.0;
        LocGPsi[2][1][2] = -8.0 / 3.0;
        LocGPsi[2][2][0] = 1.0 / 3.0;
        LocGPsi[2][2][1] = -8.0 / 3.0;
        LocGPsi[2][2][2] = 7.0 / 3.0;

        LocMass = new double[9][];
        for (int i = 0; i < 9; i++)
        {
            LocMass[i] = new double[9];
            for (int j = 0; j < 9; j++)
            {
                LocMass[i][j] = hx * hy * LocMassPsi[2][i / 3][j / 3] * LocMassPsi[2][i % 3][j % 3];
            }
        }

        locStiffness = new double[5][][];
        for (int k = 0; k < 5; k++)
        {
            locStiffness[k] = new double[9][];
        }

        for (int i = 0; i < 9; i++)
        {
            locStiffness[0][i] = new double[9];
            locStiffness[1][i] = new double[9];
            locStiffness[2][i] = new double[9];
            locStiffness[3][i] = new double[9];
            locStiffness[4][i] = new double[9];
            for (int j = 0; j < 9; j++)
            { 
                locStiffness[0][i][j] =hy * LocGPsi[0][i % 3][j % 3] * LocMassPsi[0][i / 3][j / 3] / hx + hx * LocGPsi[0][i / 3][j / 3] * LocMassPsi[0][i % 3][j % 3] / hy;
                locStiffness[1][i][j] =hy * LocGPsi[1][i % 3][j % 3] * LocMassPsi[0][i / 3][j / 3] / hx + hx * LocGPsi[0][i / 3][j / 3] * LocMassPsi[1][i % 3][j % 3] / hy;
                locStiffness[2][i][j] = hy * LocGPsi[0][i % 3][j % 3] * LocMassPsi[1][i / 3][j / 3] / hx + hx * LocGPsi[1][i / 3][j / 3] * LocMassPsi[0][i % 3][j % 3] / hy;
                locStiffness[3][i][j] = hy * LocGPsi[1][i % 3][j % 3] * LocMassPsi[1][i / 3][j / 3] / hx + hx * LocGPsi[1][i / 3][j / 3] * LocMassPsi[1][i % 3][j % 3] / hy;
            }
        }
    }

    public void LUsq(double[] diF, double[] gglF, double[] gguF)
    {
        var n = A.Di.Length;
        A.Di.AsSpan().CopyTo(diF);
        A.Al.AsSpan().CopyTo(gglF);
        A.Au.AsSpan().CopyTo(gguF);

        for (var i = 0; i < n; i++)
        {
            var sumdi = 0.0;
            var i0 = A.Ig[i];
            var i1 = A.Ig[i + 1];

            for (var k = i0; k < i1; k++)
            {
                var j = A.Jg[k];
                var j0 = A.Ig[j];
                var j1 = A.Ig[j + 1];
                var ik = i0;
                var kj = j0;
                var sml = 0.0;
                var sum = 0.0;

                while (ik < k)
                {
                    if (A.Jg[ik] == A.Jg[kj])
                    {
                        sml += gglF[ik] * gguF[kj];
                        sum += gguF[ik] * gglF[kj];
                        ik++;
                        kj++;
                    }
                    else
                    {
                        if (A.Jg[ik] > A.Jg[kj])
                            kj++;
                        else
                            ik++;
                    }
                }

                gglF[k] = (gglF[k] - sml) / diF[j];
                gguF[k] = (gguF[k] - sum) / diF[j];
                sumdi += gglF[k] * gguF[k];
            }

            diF[i] = Math.Sqrt(diF[i] - sumdi);
        }
    }

    public void LoSPrecond(double[] diF, double[] gglF, double[] gguF, ref double[] Q, ref int k, ref double error)
    {
        int n = diF.Length;
        var buf = Matrix.Mult(A, Q);
        for (int i = 0; i < n; i++)
        {
            buf[i] = B[i] - buf[i];
        }

        double[] r = LuDirect(buf, diF, gglF);
        error = scalar_prod(r, r);
        double errorPred = 0;
        double[] z = LuReverse(r, diF, gguF, n);
        buf = Matrix.Mult(A, z);
        double[] p = LuDirect(buf, diF, gglF);
        while (Math.Abs(error) > 1e-22 && k < 1000 && Math.Abs(errorPred - error) >= 1e-22)
        {
            double pp = scalar_prod(p, p);
            double pr = scalar_prod(p, r);
            double alpha = pr / pp;
            errorPred = error;
            error -= alpha * alpha * pp;
            for (int i = 0; i < n; i++)
            {
                Q[i] += alpha * z[i];
                r[i] -= alpha * p[i];
            }

            double[] ur = LuReverse(r, diF, gguF, n);
            buf = Matrix.Mult(A, ur);
            buf = LuDirect(buf, diF, gglF);
            double betta = -(scalar_prod(p, buf) / pp);
            for (int i = 0; i < n; i++)
            {
                z[i] = ur[i] + betta * z[i];
                p[i] = buf[i] + betta * p[i];
            }

            k++;
        }

        print_(diF, gguF, gglF, A, B, n);
    }

    public static void print_(double[] di, double[] ggu, double[] ggl, Matrix A, double[] B, int N)
    {
        var mat = new double[N][];
        for (int i = 0; i < mat.Length; i++)
        {
            mat[i] = new double[N];
        }

        for (int i = 0; i < mat.Length; i++)
        {
            mat[i][i] = di[i];
            for (int j = A.Ig[i]; j < A.Ig[i + 1]; j++)
            {
                mat[i][A.Jg[j]] = ggl[j];
                mat[A.Jg[j]][i] = ggu[j];
            }
        }

        using (System.IO.StreamWriter sw = new System.IO.StreamWriter("matrix.txt", true))
        {
            sw.Write("____________");
            for (int i = 0; i < mat.Length; i++)
            {
                sw.Write("___________");
            }

            sw.Write("_____________");
            sw.Write("\n|     (z,r)");
            sw.Write("|{0,10:}|", 0);
            for (int i = 1; i < mat.Length; i++)
            {
                sw.Write("{0,10:}|", i);
            }

            sw.Write("$|         B|");
            sw.WriteLine();
            for (int i = 0; i < mat.Length + 1; i++)
            {
                sw.Write("|__________");
            }

            sw.Write("|$|__________|");
            sw.WriteLine();
            for (int i = 0; i < mat.Length; i++)
            {
                sw.Write("|{0,10:}|", i);
                for (int j = 0; j < mat[i].Length; j++)
                {
                    if (mat[i][j] != 0)
                        sw.Write("{0,10:0.00000}|", mat[i][j]);
                    else
                        sw.Write("{0,10}|", 0);
                }

                sw.Write($"$|{B[i],10:0.00000}|\n");
            }

            sw.Close();
        }
    }

    private double[] LuReverse(double[] p0, double[] diF, double[] gguF, int n)
    {
        double[] res = new double[p0.Length];
        p0.AsSpan().CopyTo(res);

        for (int i = n - 1; i >= 0; i--)
        {
            res[i] /= diF[i];
            for (int j = A.Ig[i]; j < A.Ig[i + 1]; j++)
                res[A.Jg[j]] -= gguF[j] * res[i];
        }

        return res;
    }

    public static double scalar_prod(double[] p0, double[] p1)
    {
        var res = 0.0;
        if (p0.Length == p1.Length)
        {
            for (int i = 0; i < p1.Length; i++)
            {
                res += p0[i] * p1[i];
            }

            return res;
        }
        else
        {
            throw new Exception("Scalar prod");
            return res;
        }
    }

    private double[] LuDirect(double[] buf, double[] diF, double[] gglF)
    {
        var res = new double[buf.Length];
        buf.AsSpan().CopyTo(res);

        for (int i = 0; i < res.Length; i++)
        {
            double sum = 0.0;
            for (int j = A.Ig[i]; j < A.Ig[i + 1]; j++)
                sum += gglF[j] * res[A.Jg[j]];
            res[i] -= sum;
            res[i] /= diF[i];
        }

        return res;
    }
}