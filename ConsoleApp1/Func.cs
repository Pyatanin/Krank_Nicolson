namespace ConsoleApp1;

public class Func
{
    public static double Beta()
    {
        return 0.5;
    }

    public static double Lambda(double x, double y, double t)
    {
        return 1.0;
    }

    public static double Gamma(double x, double y, double t)
    {
        return 1.0;
    }

    public static double Sigma(double x, double y, double t)
    {
        return 1.0;
    }

    public static double RealF(double x, double y, double t,double function) // Реальная функция
    {
        switch (function)
        {
            case 1: return 5; //u=5
            case 2: return 5 + Math.Pow(t, 1); //u=5+t
            case 3: return 5 + Math.Pow(t, 2); //u=5+t^2
            case 4: return 5 + Math.Pow(t, 3); //u=5+t^3
            case 5: return 5 + Math.Pow(t, 4); //u=5+t^4
            case 6: return 5 + Math.Pow(t, 5); //u=5+t^5
            case 7: return 5 + Math.Sin(t); //u=5+sin(t)
            case 8: return 5 + Math.Exp(t); //u=5+e^t

            case 9: return Math.Pow(x, 1) + Math.Pow(y, 1); //u=x+z
            case 10: return Math.Pow(x, 1) + Math.Pow(y, 1) + Math.Pow(t, 1); //u=x+y+t
            case 11: return Math.Pow(x, 1) + Math.Pow(y, 1) + Math.Pow(t, 2); //u=x+y+t^2
            case 12: return Math.Pow(x, 1) + Math.Pow(y, 1) + Math.Pow(t, 3); //u=x+y+t^3
            case 13: return Math.Pow(x, 1) + Math.Pow(y, 1) + Math.Pow(t, 4); //u=x+y+t^4
            case 14: return Math.Pow(x, 1) + Math.Pow(y, 1) + Math.Pow(t, 5); //u=x+y+t^5
            case 15: return Math.Pow(x, 1) + Math.Pow(y, 1) + Math.Sin(t); //u=x+y+sin(t)
            case 16: return Math.Pow(x, 1) + Math.Pow(y, 1) + Math.Exp(t); //u=x+y+e^t

            case 17: return Math.Pow(x, 2) + Math.Pow(y, 2); //u=x^2+z^2
            case 18: return Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(t, 1); //u=x^2+y^2+t
            case 19: return Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(t, 2); //u=x^2+y^2+t^2
            case 20: return Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(t, 3); //u=x^2+y^2+t^3
            case 21: return Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(t, 4); //u=x^2+y^2+t^4
            case 22: return Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(t, 5); //u=x^2+y^2+t^5
            case 23: return Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Sin(t); //u=x^2+y^2+sin(t)
            case 24: return Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Exp(t); //u=x^2+y^2+e^t

            case 25: return Math.Pow(x, 3) + Math.Pow(y, 3);                  //u=x^3+y^3
            case 26: return Math.Pow(x, 3) + Math.Pow(y, 3) + Math.Pow(t, 1); //u=x^3+y^3+t
            case 27: return Math.Pow(x, 3) + Math.Pow(y, 3) + Math.Pow(t, 2); //u=x^3+y^3+t^2
            case 28: return Math.Pow(x, 3) + Math.Pow(y, 3) + Math.Pow(t, 3); //u=x^3+y^3+t^3
            case 29: return Math.Pow(x, 3) + Math.Pow(y, 3) + Math.Pow(t, 4); //u=x^3+y^3+t^4
            case 30: return Math.Pow(x, 3) + Math.Pow(y, 3) + Math.Pow(t, 5); //u=x^3+y^3+t^5
            case 31: return Math.Pow(x, 3) + Math.Pow(y, 3) + Math.Sin(t); //   u=x^3+y^3+sin(t)
            case 32: return Math.Pow(x, 3) + Math.Pow(y, 3) + Math.Exp(t); //   u=x^3+y^3+e^t

            case 33: return Math.Pow(x, 4) + Math.Pow(y, 4); //                 u=x^4+y^4
            case 34: return Math.Pow(x, 4) + Math.Pow(y, 4) + Math.Pow(t, 1); //u=x^4+y^4+t
            case 35: return Math.Pow(x, 4) + Math.Pow(y, 4) + Math.Pow(t, 2); //u=x^4+y^4+t^2
            case 36: return Math.Pow(x, 4) + Math.Pow(y, 4) + Math.Pow(t, 3); //u=x^4+y^4+t^3
            case 37: return Math.Pow(x, 4) + Math.Pow(y, 4) + Math.Pow(t, 4); //u=x^4+y^4+t^4
            case 38: return Math.Pow(x, 4) + Math.Pow(y, 4) + Math.Pow(t, 5); //u=x^4+y^4+t^5
            case 39: return Math.Pow(x, 4) + Math.Pow(y, 4) + Math.Sin(t); //   u=x^4+y^4+sin(t)
            case 40: return Math.Pow(x, 4) + Math.Pow(y, 4) + Math.Exp(t); //   u=x^4+y^4+e^t

            case 41: return Math.Pow(x, 5) + Math.Pow(y, 5); //                 u=x^5+y^5
            case 42: return Math.Pow(x, 5) + Math.Pow(y, 5) + Math.Pow(t, 1); //u=x^5+y^5+t
            case 43: return Math.Pow(x, 5) + Math.Pow(y, 5) + Math.Pow(t, 2); //u=x^5+y^5+t^2
            case 44: return Math.Pow(x, 5) + Math.Pow(y, 5) + Math.Pow(t, 3); //u=x^5+y^5+t^3
            case 45: return Math.Pow(x, 5) + Math.Pow(y, 5) + Math.Pow(t, 4); //u=x^5+y^5+t^4
            case 46: return Math.Pow(x, 5) + Math.Pow(y, 5) + Math.Pow(t, 5); //u=x^5+y^5+t^5
            case 47: return Math.Pow(x, 5) + Math.Pow(y, 5) + Math.Sin(t); //   u=x^5+y^5+sin(t)
            case 48: return Math.Pow(x, 5) + Math.Pow(y, 5) + Math.Exp(t); //   u=x^5+y^5+e^t

            case 49: return Math.Sin(x) + Math.Sin(y); //                       u=sin(x)+sin(y)
            case 50: return Math.Sin(x) + Math.Sin(y) + Math.Pow(t, 1); //      u=sin(x)+sin(y)+t
            case 51: return Math.Sin(x) + Math.Sin(y) + Math.Pow(t, 2); //      u=sin(x)+sin(y)+t^2
            case 52: return Math.Sin(x) + Math.Sin(y) + Math.Pow(t, 3); //      u=sin(x)+sin(y)+t^3
            case 53: return Math.Sin(x) + Math.Sin(y) + Math.Pow(t, 4); //      u=sin(x)+sin(y)+t^4
            case 54: return Math.Sin(x) + Math.Sin(y) + Math.Pow(t, 5); //      u=sin(x)+sin(y)+t^5
            case 55: return Math.Sin(x) + Math.Sin(y) + Math.Sin(t); //         u=sin(x)+sin(y)+sin(t)
            case 56: return Math.Sin(x) + Math.Sin(y) + Math.Exp(t); //         u=sin(x)+sin(y)+e^t

            case 57: return Math.Exp(x) + Math.Exp(y); //                       u=exp(x)+exp(y)
            case 58: return Math.Exp(x) + Math.Exp(y) + Math.Pow(t, 1); //      u=exp(x)+exp(y)+t
            case 59: return Math.Exp(x) + Math.Exp(y) + Math.Pow(t, 2); //      u=exp(x)+exp(y)+t^2
            case 60: return Math.Exp(x) + Math.Exp(y) + Math.Pow(t, 3); //      u=exp(x)+exp(y)+t^3
            case 61: return Math.Exp(x) + Math.Exp(y) + Math.Pow(t, 4); //      u=exp(x)+exp(y)+t^4
            case 62: return Math.Exp(x) + Math.Exp(y) + Math.Pow(t, 5); //      u=exp(x)+exp(y)+t^5
            case 63: return Math.Exp(x) + Math.Exp(y) + Math.Sin(t); //         u=exp(x)+exp(y)+sin(t)
            case 64: return Math.Exp(x) + Math.Exp(y) + Math.Exp(t); //         u=exp(x)+exp(y)+e^t
            
            case 65: return Math.Pow(x, 3) + Math.Pow(y, 3) + Math.Pow(t, 10);

        }

        return 0;
    }

    public static double Theta(double x, double y, double t, int border) // странный кружочек 2 краевые
    {
        switch (border)
        {
            case 0:
            {
                return -3;
            }
            case 1:
            {
                return 0;
            }
            case 2:
            {
                return -3;
            }
            case 3:
            {
                return 0;
            }
        }

        return 0;
    }

    public static double Ubeta(double x, double y, double t, int border) // 3 краевые коэф
    {
        switch (border)
        {
            case 0:
            {
                return x*x-4;
                break;
            }
            case 1:
            {
                return 5;
                break;
            }
            case 2:
            {
                return 5 + t * t;
                break;
            }
            case 3:
            {
                return 5 + t * t;
                break;
            }
        }

        return 0;
    }

    public static double Ug(double x, double y, double t, int border, double function) // 1 кроевые
    {
        return RealF(x,  y, t,function);
    }

    public static double F(double x, double y, double t,double function) // правая часть
    {
        switch (function)
        {
            case 1: return 0; //                                                   u=5
            case 2: return 1; //                                                   u=5+t
            case 3: return 2 * Math.Pow(t, 1); //                                  u=5+t^2
            case 4: return 3 * Math.Pow(t, 2); //                                  u=5+t^3
            case 5: return 4 * Math.Pow(t, 3); //                                  u=5+t^4
            case 6: return 5 * Math.Pow(t, 4); //                                  u=5+t^5
            case 7: return Math.Cos(t); //                                         u=5+sin(t)
            case 8: return Math.Exp(t); //                                         u=5+e^t

            case 9: return  0; //                                  u=x+y
            case 10: return 0 + 1; //                             u=x+y+t
            case 11: return 0 + 2 * Math.Pow(t, 1); //            u=x+y+t^2
            case 12: return 0 + 3 * Math.Pow(t, 2); //            u=x+y+t^3
            case 13: return 0 + 4 * Math.Pow(t, 3); //            u=x+y+t^4
            case 14: return 0 + 5 * Math.Pow(t, 4); //            u=x+y+t^5
            case 15: return 0 + Math.Cos(t); //                   u=x+y+sin(t)
            case 16: return 0 + Math.Exp(t); //                   u=x+y+e^t

            case 17: return -(2 + 2); //                        u=x^2+y^2
            case 18: return -(2 + 2) + 1; //                    u=x^2+y^2+t
            case 19: return -(2 + 2) + 2 * Math.Pow(t, 1); //   u=x^2+y^2+t^2
            case 20: return -(2 + 2) + 3 * Math.Pow(t, 2); //   u=x^2+y^2+t^3
            case 21: return -(2 + 2) + 4 * Math.Pow(t, 3); //   u=x^2+y^2+t^4
            case 22: return -(2 + 2) + 5 * Math.Pow(t, 4); //   u=x^2+y^2+t^5
            case 23: return -(2 + 2) + Math.Cos(t); //          u=x^2+y^2+sin(t)
            case 24: return -(2 + 2) + Math.Exp(t); //          u=x^2+y^2+e^t

            case 25: return -(6 * Math.Pow(x, 1) + 6 * Math.Pow(y, 1)); //                       u=x^3+y^3
            case 26: return -(6 * Math.Pow(x, 1) + 6 * Math.Pow(y, 1)) + 1; //               //  u=x^3+y^3+t
            case 27: return -(6 * Math.Pow(x, 1) + 6 * Math.Pow(y, 1)) + 2 * Math.Pow(t, 1); //  u=x^3+y^3+t^2
            case 28: return -(6 * Math.Pow(x, 1) + 6 * Math.Pow(y, 1)) + 3 * Math.Pow(t, 2); //  u=x^3+y^3+t^3
            case 29: return -(6 * Math.Pow(x, 1) + 6 * Math.Pow(y, 1)) + 4 * Math.Pow(t, 3); //  u=x^3+y^3+t^4
            case 30: return -(6 * Math.Pow(x, 1) + 6 * Math.Pow(y, 1)) + 5 * Math.Pow(t, 4); //  u=x^3+y^3+t^5
            case 31: return -(6 * Math.Pow(x, 1) + 6 * Math.Pow(y, 1)) + Math.Cos(t); //         u=x^3+y^3+sin(t)
            case 32: return -(6 * Math.Pow(x, 1) + 6 * Math.Pow(y, 1)) + Math.Exp(t); //         u=x^3+y^3+e^t

            case 33: return -(12 * Math.Pow(x, 2) + 12 * Math.Pow(y, 2)); //                     u=x^4+y^4
            case 34: return -(12 * Math.Pow(x, 2) + 12 * Math.Pow(y, 2)) + 1; //               //u=x^4+y^4+t
            case 35: return -(12 * Math.Pow(x, 2) + 12 * Math.Pow(y, 2)) + 2 * Math.Pow(t, 1); //u=x^4+y^4+t^2
            case 36: return -(12 * Math.Pow(x, 2) + 12 * Math.Pow(y, 2)) + 3 * Math.Pow(t, 2); //u=x^4+y^4+t^3
            case 37: return -(12 * Math.Pow(x, 2) + 12 * Math.Pow(y, 2)) + 4 * Math.Pow(t, 3); //u=x^4+y^4+t^4
            case 38: return -(12 * Math.Pow(x, 2) + 12 * Math.Pow(y, 2)) + 5 * Math.Pow(t, 4); //u=x^4+y^4+t^5
            case 39: return -(12 * Math.Pow(x, 2) + 12 * Math.Pow(y, 2)) + Math.Cos(t); //       u=x^4+y^4+sin(t)
            case 40: return -(12 * Math.Pow(x, 2) + 12 * Math.Pow(y, 2)) + Math.Exp(t); //       u=x^4+y^4+e^t

            case 41: return -(20 * Math.Pow(x, 3) + 20 * Math.Pow(y, 3)); //                     u=x^3+y^3
            case 42: return -(20 * Math.Pow(x, 3) + 20 * Math.Pow(y, 3)) + 1; //               //u=x^3+y^3+t
            case 43: return -(20 * Math.Pow(x, 3) + 20 * Math.Pow(y, 3)) + 2 * Math.Pow(t, 1); //u=x^3+y^3+t^2
            case 44: return -(20 * Math.Pow(x, 3) + 20 * Math.Pow(y, 3)) + 3 * Math.Pow(t, 2); //u=x^3+y^3+t^3
            case 45: return -(20 * Math.Pow(x, 3) + 20 * Math.Pow(y, 3)) + 4 * Math.Pow(t, 3); //u=x^3+y^3+t^4
            case 46: return -(20 * Math.Pow(x, 3) + 20 * Math.Pow(y, 3)) + 5 * Math.Pow(t, 4); //u=x^3+y^3+t^5
            case 47: return -(20 * Math.Pow(x, 3) + 20 * Math.Pow(y, 3)) + Math.Cos(t); //       u=x^3+y^3+sin(t)
            case 48: return -(20 * Math.Pow(x, 3) + 20 * Math.Pow(y, 3)) + Math.Exp(t); //       u=x^3+y^3+e^t

            case 49: return -( - Math.Sin(x) - Math.Sin(y)); //                           u=sin(x)+sin(y)
            case 50: return -( - Math.Sin(x) - Math.Sin(y)) + 1; //               //      u=sin(x)+sin(y)+t
            case 51: return -( - Math.Sin(x) - Math.Sin(y)) + 2 * Math.Pow(t, 1); //      u=sin(x)+sin(y)+t^2
            case 52: return -( - Math.Sin(x) - Math.Sin(y)) + 3 * Math.Pow(t, 2); //      u=sin(x)+sin(y)+t^3
            case 53: return -( - Math.Sin(x) - Math.Sin(y)) + 4 * Math.Pow(t, 3); //      u=sin(x)+sin(y)+t^4
            case 54: return -( - Math.Sin(x) - Math.Sin(y)) + 5 * Math.Pow(t, 4); //      u=sin(x)+sin(y)+t^5
            case 55: return -( - Math.Sin(x) - Math.Sin(y)) + Math.Cos(t); //             u=sin(x)+sin(y)+sin(t)
            case 56: return -( - Math.Sin(x) - Math.Sin(y)) + Math.Exp(t); //             u=sin(x)+sin(y)+e^t-r*
            
            case 57: return -(Math.Exp(x)  + Math.Exp(y)); //                           u=exp(x)+exp(y)
            case 58: return -(Math.Exp(x)  + Math.Exp(y)) + 1; //               //      u=exp(x)+exp(y)+t
            case 59: return -(Math.Exp(x)  + Math.Exp(y)) + 2 * Math.Pow(t, 1); //      u=exp(x)+exp(y)+t^2
            case 60: return -(Math.Exp(x)  + Math.Exp(y)) + 3 * Math.Pow(t, 2); //      u=exp(x)+exp(y)+t^3
            case 61: return -(Math.Exp(x)  + Math.Exp(y)) + 4 * Math.Pow(t, 3); //      u=exp(x)+exp(y)+t^4
            case 62: return -(Math.Exp(x)  + Math.Exp(y)) + 5 * Math.Pow(t, 4); //      u=exp(x)+exp(y)+t^5
            case 63: return -(Math.Exp(x)  + Math.Exp(y)) + Math.Cos(t); //             u=exp(x)+exp(y)+sin(t)
            case 64: return -(Math.Exp(x)  + Math.Exp(y)) + Math.Exp(t); //             u=exp(x)+exp(y)+e^t
            
            case 65: return -(20 * Math.Pow(x, 3) + 20 * Math.Pow(y, 3)) + 10 * Math.Pow(t, 9);
        }
        return 0;
    }
}