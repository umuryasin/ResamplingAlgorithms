using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResamplingTest
{
    class Program
    {
        private static Random rand;

        static void Main(string[] args)
        {
            int seed = (int)DateTime.Now.Ticks;
            rand = new Random(seed);
            double[] Weights = new double[] { 2, 5, 7, 4, 9, 5, 6, 3, 1, 8 };

            int[] resamplingIndex = wheelResampling(Weights);

            int NumSamples = Weights.Length;
            int newIndex;
            for(int i = 0; i<NumSamples; i++)
            {
                newIndex = resamplingIndex[i];
                Console.WriteLine(" Weights = " + Weights[newIndex] + " index = " + newIndex);
            }

            Console.Read();
        }

        private static int[] wheelResampling(double[] weights)
        {
            double beta = 0.0;
            int NumSamples = weights.Length;
            double[] newSamples = new double[NumSamples];
            int[] newIndex = new int[NumSamples];

            double maxWeight = findMax(weights);
            int index = (int)Math.Round(rand.NextDouble() * NumSamples);
            for (int i = 0; i<NumSamples; i++)
            {
                beta += rand.NextDouble() * 2 * maxWeight;
                while(beta > weights[index])
                {
                    beta -= weights[index];
                    index = (index + 1) % NumSamples;
                }
                newSamples[i] = weights[index];
                newIndex[i] = index;
            }

            return newIndex;

        }

        private static double findMax(double[] array)
        {
            double len = array.Length;
            double maxVal = -1;
            double val = -1;
            for(int i = 0; i<len; i++)
            {
                val = array[i];
                if(val > maxVal)
                {
                    maxVal = val;
                }
            }
            return maxVal;
        }

    }
}
