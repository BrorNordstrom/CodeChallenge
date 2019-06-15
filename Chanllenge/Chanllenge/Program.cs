using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chanllenge
{
    class Program
    {
        static void Main(string[] args)
        {
            int cnt = 0;
            int limit = 10000;
            int[][] a = new int[limit][];
            string[] lines = System.IO.File.ReadAllLines("1.in");

            // Display the file contents by using a foreach loop.
            foreach (string line in lines)
            {
                if (line.Length == 0)
                {
                    break;
                }
                string[] split = line.Split(' ');
                int length = split.Length;
                a[cnt] = new int[length];
                for (int i = 0; i < length; i++)
                {
                    try
                    {
                        a[cnt][i] = Int32.Parse(split[i]);
                    }
                    catch (FormatException e)
                    {
                        a[cnt][i] = 0;
                    }
                }
                cnt++;
            }

            int[][] f = new int[cnt][];
            int[][] g = new int[cnt][];
            for (int i = 0; i < cnt; i++)
            {
                f[i] = new int[i + 1];
                g[i] = new int[i + 1];
            }
            f[0][0] = a[0][0];
            g[0][0] = 0;
            for (int i = 1; i < cnt; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    f[i][j] = 0;
                    g[i][j] = -1;
                    if (j < i)
                    {
                        if (a[i][j] % 2 != a[i - 1][j] % 2 && g[i - 1][j] != -1)
                        {
                            int val = f[i - 1][j] + a[i][j];
                            if (val > f[i][j])
                            {
                                f[i][j] = val;
                                g[i][j] = (i - 1) * cnt + j;
                            }
                        }
                    }

                    if (j > 0)
                    {
                        if (a[i][j] % 2 != a[i - 1][j - 1] % 2 && g[i - 1][j - 1] != -1)
                        {
                            int val = f[i - 1][j - 1] + a[i][j];
                            if (val > f[i][j])
                            {
                                f[i][j] = val;
                                g[i][j] = (i - 1) * cnt + j - 1;
                            }
                        }
                    }
                }
            }
            int max = 0;
            int max_id = -1;
            for (int i = 0; i < cnt; i++)
            {
                if (g[cnt - 1][i] != -1)
                {
                    if (f[cnt - 1][i] > max)
                    {
                        max = f[cnt - 1][i];
                        max_id = i;
                    }
                }
            }
            Console.WriteLine(String.Format("Max sum: {0}", max));
            Console.Write("Path: ");
            int[] path = new int[cnt];
            int num = 0;
            
            int lastX = cnt - 1;
            int lastY = max_id;
            
            while(true) {
                path[num] = a[lastX][lastY];
                num ++;
                if(lastX == 0) {
                    break;
                }
                int par = g[lastX][lastY];
                lastX = par / cnt;
                lastY = par % cnt;
            }
            for(int i = 0; i < num; i ++) {
                if(i > 0) {
                    Console.Write(",");
                }
                Console.Write(path[num - 1 - i]);
            }
            Console.WriteLine();
        }
    }
}
