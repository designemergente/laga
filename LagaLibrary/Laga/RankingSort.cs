using Laga.Numbers;

namespace Laga
{
    /// <summary>
    /// To sort chromosomes in populations according to fitness evaluation.
    /// </summary>
    public class RankingSort
    {
        //LagaCasting lc = new LagaCasting();
        /*
        private object[][] objPopulation;
        private double[][] dblPopulation;
        private float[][] fltPopulation;
        private int[][] intPopulation;
        private char[][] srtPopulation;
        private float[] fltArrResults;
        private int[] intArrResults;

        /// <summary>
        /// constructor
        /// </summary>
        public RankingSort()
        {

        }

        /// <summary>
        /// Sort the individuals in the population by fitness value.
        /// </summary>
        /// <param name="population">population to sort</param>
        /// <param name="arrResults">Array of fitness in the population. Only two flavors are supported: int[] and float[]</param>
        /// <param name="minmax">if is true the sort is by min to max, else max to min</param>
        public void BidirectionalBubbleSort(object[][] population, float[] arrResults, bool minmax)
        {
            int j;
            int st = -1;
            int n = arrResults.Length;

            while (st < n)
            {
                st++;
                n--;

                for (j = st; j < n; j++)
                {
                    if (arrResults[j] > arrResults[j + 1])
                    {
                        float T = arrResults[j];
                        object[] temp = population[j];
                        arrResults[j] = arrResults[j + 1];
                        population[j] = population[j + 1];
                        arrResults[j + 1] = T;
                        population[j + 1] = temp;
                    }
                }
                for (j = n; --j >= st;)
                {
                    if (arrResults[j] > arrResults[j + 1])
                    {
                        float T = arrResults[j];
                        object[] temp = population[j];
                        arrResults[j] = arrResults[j + 1];
                        population[j] = population[j + 1];
                        arrResults[j + 1] = T;
                        population[j + 1] = temp;
                    }
                }
            }

            if (minmax)
            {
                Tools.Reverse(arrResults);
                Tools.ReversePopulation(population);
            }

            objPopulation = population;
            fltArrResults = arrResults;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="population"></param>
        /// <param name="arrResults"></param>
        /// <param name="minmax"></param>
        public void BidirectionalBubbleSort(object[][] population, int[] arrResults, bool minmax)
        {
            int j;
            int st = -1;
            int n = arrResults.Length;

            while (st < n)
            {
                st++;
                n--;

                for (j = st; j < n; j++)
                {
                    if (arrResults[j] > arrResults[j + 1])
                    {
                        int T = arrResults[j];
                        object[] temp = population[j];
                        arrResults[j] = arrResults[j + 1];
                        population[j] = population[j + 1];
                        arrResults[j + 1] = T;
                        population[j + 1] = temp;
                    }
                }
                for (j = n; --j >= st;)
                {
                    if (arrResults[j] > arrResults[j + 1])
                    {
                        int T = arrResults[j];
                        object[] temp = population[j];
                        arrResults[j] = arrResults[j + 1];
                        population[j] = population[j + 1];
                        arrResults[j + 1] = T;
                        population[j + 1] = temp;
                    }
                }
            }

            if (minmax)
            {
                Tools.Reverse(arrResults);
                Tools.ReversePopulation(population);
            }

            objPopulation = population;
            intArrResults = arrResults;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="population"></param>
        /// <param name="arrResults"></param>
        /// <param name="minmax"></param>
        public void BidirectionalBubbleSort(double[][] population, float[] arrResults, bool minmax)
        {
            int j;
            int st = -1;
            int n = arrResults.Length;

            while (st < n)
            {
                st++;
                n--;

                for (j = st; j < n; j++)

                {
                    if (arrResults[j] > arrResults[j + 1])
                    {
                        float T = arrResults[j];
                        double[] temp = population[j];
                        arrResults[j] = arrResults[j + 1];
                        population[j] = population[j + 1];
                        arrResults[j + 1] = T;
                        population[j + 1] = temp;
                    }
                }
                for (j = n; --j >= st;)
                {
                    if (arrResults[j] > arrResults[j + 1])
                    {
                        float T = arrResults[j];
                        double[] temp = population[j];
                        arrResults[j] = arrResults[j + 1];
                        population[j] = population[j + 1];
                        arrResults[j + 1] = T;
                        population[j + 1] = temp;
                    }
                }
            }

            if (minmax)
            {
                Tools.Reverse(arrResults);
                Tools.ReversePopulation(population);
            }

            dblPopulation = population;
            fltArrResults = arrResults;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="population"></param>
        /// <param name="arrResults"></param>
        /// <param name="minmax"></param>
        public void BidirectionalBubbleSort(double[][] population, int[] arrResults, bool minmax)
        {
            int j;
            int st = -1;
            int n = arrResults.Length;


            while (st < n)
            {
                st++;
                n--;

                for (j = st; j < n; j++)
                {
                    if (arrResults[j] > arrResults[j + 1])
                    {
                        int T = arrResults[j];
                        double[] temp = population[j];
                        arrResults[j] = arrResults[j + 1];
                        population[j] = population[j + 1];
                        arrResults[j + 1] = T;
                        population[j + 1] = temp;
                    }
                }
                for (j = n; --j >= st;)
                {
                    if (arrResults[j] > arrResults[j + 1])
                    {
                        int T = arrResults[j];
                        double[] temp = population[j];
                        arrResults[j] = arrResults[j + 1];
                        population[j] = population[j + 1];
                        arrResults[j + 1] = T;
                        population[j + 1] = temp;
                    }
                }
            }

            if (minmax)
            {
                Tools.Reverse(arrResults);
                Tools.ReversePopulation(population);
            }

            dblPopulation = population;
            intArrResults = arrResults;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="population"></param>
        /// <param name="arrResults"></param>
        /// <param name="minmax"></param>
        public void BidirectionalBubbleSort(float[][] population, float[] arrResults, bool minmax)
        {
            int j;
            int st = -1;
            int n = arrResults.Length;

            while (st < n)
            {
                st++;
                n--;

                for (j = st; j < n; j++)
                {
                    if (arrResults[j] > arrResults[j + 1])
                    {
                        float T = arrResults[j];
                        float[] temp = population[j];
                        arrResults[j] = arrResults[j + 1];
                        population[j] = population[j + 1];
                        arrResults[j + 1] = T;
                        population[j + 1] = temp;
                    }
                }
                for (j = n; --j >= st;)
                {
                    if (arrResults[j] > arrResults[j + 1])
                    {
                        float T = arrResults[j];
                        float[] temp = population[j];
                        arrResults[j] = arrResults[j + 1];
                        population[j] = population[j + 1];
                        arrResults[j + 1] = T;
                        population[j + 1] = temp;
                    }
                }
            }

            if (minmax)
            {
                Tools.Reverse(arrResults);
                Tools.ReversePopulation(population);
            }

            fltPopulation = population;
            fltArrResults = arrResults;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="population"></param>
        /// <param name="arrResults"></param>
        /// <param name="minmax"></param>
        public void BidirectionalBubbleSort(float[][] population, int[] arrResults, bool minmax)
        {
            int j;
            int st = -1;
            int n = arrResults.Length;

            while (st < n)
            {
                st++;
                n--;

                for (j = st; j < n; j++)
                {
                    if (arrResults[j] > arrResults[j + 1])
                    {
                        int T = arrResults[j];
                        float[] temp = population[j];
                        arrResults[j] = arrResults[j + 1];
                        population[j] = population[j + 1];
                        arrResults[j + 1] = T;
                        population[j + 1] = temp;
                    }
                }
                for (j = n; --j >= st;)
                {
                    if (arrResults[j] > arrResults[j + 1])
                    {
                        int T = arrResults[j];
                        float[] temp = population[j];
                        arrResults[j] = arrResults[j + 1];
                        population[j] = population[j + 1];
                        arrResults[j + 1] = T;
                        population[j + 1] = temp;
                    }
                }
            }

            if (minmax)
            {
                Tools.Reverse(arrResults);
                Tools.ReversePopulation(population);
            }

            fltPopulation = population;
            intArrResults = arrResults;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="population"></param>
        /// <param name="arrResults"></param>
        /// <param name="minmax"></param>
        public void BidirectionalBubbleSort(int[][] population, float[] arrResults, bool minmax)
        {
            int j;
            int st = -1;
            int n = arrResults.Length;

            while (st < n)
            {
                st++;
                n--;

                for (j = st; j < n; j++)
                {
                    if (arrResults[j] > arrResults[j + 1])
                    {
                        float T = arrResults[j];
                        int[] temp = population[j];
                        arrResults[j] = arrResults[j + 1];
                        population[j] = population[j + 1];
                        arrResults[j + 1] = T;
                        population[j + 1] = temp;
                    }
                }
                for (j = n; --j >= st;)
                {
                    if (arrResults[j] > arrResults[j + 1])
                    {
                        float T = arrResults[j];
                        int[] temp = population[j];
                        arrResults[j] = arrResults[j + 1];
                        population[j] = population[j + 1];
                        arrResults[j + 1] = T;
                        population[j + 1] = temp;
                    }
                }
            }

            if (minmax)
            {
                Tools.Reverse(arrResults);
                Tools.ReversePopulation(population);
            }

            intPopulation = population;
            fltArrResults = arrResults;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="population"></param>
        /// <param name="arrResults"></param>
        /// <param name="minmax"></param>
        public void BidirectionalBubbleSort(int[][] population, int[] arrResults, bool minmax)
        {
            int j;
            int st = -1;
            int n = arrResults.Length;

            while (st < n)
            {
                st++;
                n--;

                for (j = st; j < n; j++)
                {
                    if (arrResults[j] > arrResults[j + 1])
                    {
                        int T = arrResults[j];
                        int[] temp = population[j];
                        arrResults[j] = arrResults[j + 1];
                        population[j] = population[j + 1];
                        arrResults[j + 1] = T;
                        population[j + 1] = temp;
                    }
                }
                for (j = n; --j >= st;)
                {
                    if (arrResults[j] > arrResults[j + 1])
                    {
                        int T = arrResults[j];
                        int[] temp = population[j];
                        arrResults[j] = arrResults[j + 1];
                        population[j] = population[j + 1];
                        arrResults[j + 1] = T;
                        population[j + 1] = temp;
                    }
                }
            }

            if (minmax)
            {
                Tools.Reverse(arrResults);
                Tools.ReversePopulation(population);
            }

            intPopulation = population;
            intArrResults = arrResults;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="population"></param>
        /// <param name="arrResults"></param>
        /// <param name="minmax"></param>
        public void BidirectionalBubbleSort(char[][] population, float[] arrResults, bool minmax)
        {
            int j;
            int st = -1;
            int n = arrResults.Length;

            while (st < n)
            {
                st++;
                n--;

                for (j = st; j < n; j++)
                {
                    if (arrResults[j] > arrResults[j + 1])
                    {
                        float T = arrResults[j];
                        char[] temp = population[j];
                        arrResults[j] = arrResults[j + 1];
                        population[j] = population[j + 1];
                        arrResults[j + 1] = T;
                        population[j + 1] = temp;
                    }
                }
                for (j = n; --j >= st;)
                {
                    if (arrResults[j] > arrResults[j + 1])
                    {
                        float T = arrResults[j];
                        char[] temp = population[j];
                        arrResults[j] = arrResults[j + 1];
                        population[j] = population[j + 1];
                        arrResults[j + 1] = T;
                        population[j + 1] = temp;
                    }
                }
            }

            if (minmax)
            {
                Tools.Reverse(arrResults);
                Tools.ReversePopulation(population);
            }
            srtPopulation = population;
            fltArrResults = arrResults;
        }

        /// <summary>
        /// An optimised BidirectionalBubbleSort method. Sort the individuals in the population by fitness value.
        /// </summary>
        /// <param name="population">population to sort</param>
        /// <param name="arrResults">Array of fitness in the population. Only 2 flavors are supported: int[] and float[]</param>
        /// <param name="minmax">if is true the sort is by min to max, else max to min</param>
        /// <returns>automatically the population is sorted. the original population will be modified.</returns>
        public void BidirectionalBubbleSort(char[][] population, int[] arrResults, bool minmax)
        {
            int j;
            int st = -1;
            int n = arrResults.Length;

            while (st < n)
            {
                st++;
                n--;

                for (j = st; j < n; j++)
                {
                    if (arrResults[j] > arrResults[j + 1])
                    {
                        int T = arrResults[j];
                        char[] temp = population[j];
                        arrResults[j] = arrResults[j + 1];
                        population[j] = population[j + 1];
                        arrResults[j + 1] = T;
                        population[j + 1] = temp;
                    }
                }
                for (j = n; --j >= st;)
                {
                    if (arrResults[j] > arrResults[j + 1])
                    {
                        int T = arrResults[j];
                        char[] temp = population[j];
                        arrResults[j] = arrResults[j + 1];
                        population[j] = population[j + 1];
                        arrResults[j + 1] = T;
                        population[j + 1] = temp;
                    }
                }
            }

            if (minmax)
            {
                Tools.Reverse(arrResults);
                Tools.ReversePopulation(population);
            }

            srtPopulation = population;
            intArrResults = arrResults;
        }
        */
    }
}
