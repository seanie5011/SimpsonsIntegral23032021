using System;

namespace SimpsonsIntegral23032021
{
    class Simpson
    {   // initialising most variables
        double r, l, h, simp, addodd, addeven, error, Integral1, Integral2;
        int N, n;

        public void SetParameters(double a, double b) // to set the parameters of the function
        {
            if (b < a) // ensures no error arises from b and a
            {
                Console.WriteLine("Error, b < a");
                return;
            }
            else // reference parameters assign variables
            {
                r = b;
                l = a;
            }
        }

        public void SetNumIntervals(int numint) // the number of intervals used
        {
            if (numint % 2 != 0) // N must be even
            {
                Console.WriteLine("Error, numint is odd");
                return;
            }
            else if (numint < 10) // too low and wont be able to see changes or get a very accurate answer
            {
                Console.WriteLine("Error, numint < 10");
                return;
            }
            else // reference assigns a variable
            {
                N = numint;
            }
        }

        public double CalcIntegral() // actually finding the value of the simpson integral
        {
            h = (r - l) / N; // finding interval length
            addodd = 0;
            addeven = 0; // ensures they arent null
            n = 1;
            while (n <= N) // doesnt pass N
            {
                addodd = addodd + f(l + (n * h)); //x1 = l + 1*h, so finds function at xn
                n = n + 2; // ensures only odd as n starts at 1
            }
            n = 2;
            while (n <= N - 1) // doesnt include N
            {
                addeven = addeven + f(l + (n * h));
                n = n + 2; // ensures only even as n starts at 2
            }
            simp = (h / 3.0) * (f(l) + 4 * (addodd) + 2 * (addeven) + f(l + (N * h))); // simpsons formula
            return simp;
        }

        private double f(double x)
        {
            return (Math.Pow(x, 4) / Math.Pow(Math.Pow(x, 4) + 2 * Math.Pow(x, 2) + 3, 3/2));  // function in question
        }

        public double GetError()
        {
            SetNumIntervals(N);
            Integral1 = CalcIntegral();
            SetNumIntervals(2 * N);
            Integral2 = CalcIntegral();
            error = Integral2 - Integral1; // finding Integral at N intervals and subtracting from 2N intervals to get approximate error
            return error;
        }

        public static void Main(string[] args)
        {
            Simpson obj = new Simpson(); // creating sobject to call functions

            // test function, not user set
            obj.SetParameters(0, 100);
            Console.WriteLine("Function Simpsons Integral with parameters ({0},{1}) and to 100 intervals.", obj.l, obj.r);

            Console.WriteLine("-----------------------------------------------------------------");
            Console.WriteLine("-----------    Simpsons Method for this function    -------------"); //formatting
            Console.WriteLine("-----------------------------------------------------------------");
            Console.WriteLine("|{0,11}  |{1,11}   |{2,11}      |{3,11}     |", "Intervals", "Integral", "Error", "Error%");

            for (int countest = 10; countest <= 1000; countest = countest + 10) // creates table of results up to 100 intervals using numints variable countest
            {
                obj.SetNumIntervals(countest);
                Console.WriteLine("|{0,8}     |{1,12:0.0000000}  |{2,15:E}  |{3,12:0.0000000}%   |", countest, obj.CalcIntegral(), obj.GetError(), Math.Abs((obj.error / obj.simp) * 100));
            }
        }
    }
}
