using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shifr_7
{
    class Program
    {
        static void Main(string[] args)
        {
            int p, g, n, r, u, m, t = 1, w = 1;
            List<int> a = new List<int>();
            List<int> b = new List<int>();
            String text = "", str, s;
            Random random = new Random();
            Console.Write("Введите фразу > ");
            text = Console.ReadLine();
            text = text.ToLower();
            s = Hash(text);

            Console.Write("Введите простое p > ");
            str = Console.ReadLine();
            Int32.TryParse(str, out p);
            while (p == 0 || p == 1 || !easyNum(p))
            {
                Console.WriteLine("Ошибка ввода значения p");
                Console.Write("Введите простое p > ");
                str = Console.ReadLine();
                Int32.TryParse(str, out p);
            }

            Console.Write("Введите простое g > ");
            str = Console.ReadLine();
            Int32.TryParse(str, out g);
            while (g == 0 || g == 1 || p == g || !easyNum(g))
            {
                Console.WriteLine("Ошибка ввода значения g");
                Console.Write("Введите простое g > ");
                str = Console.ReadLine();
                Int32.TryParse(str, out g);
            }


            n = p * g;
            r = random.Next(1, n);
            u = (r * r) % n;

            m = random.Next(4, 10);
            s = s.Substring(0, m);

            for (int i = 0; i < m; i++)
            {
                a.Add(random.Next(1, 10));
                t *= (int)Math.Pow(a[i], Int32.Parse(s[i].ToString()));
            }

            t *= r;
            t %= n;
            Console.Write("Открытый ключ > {");
            foreach (int num in a)
            {
                Console.Write(num.ToString() + ", ");
            }
            Console.WriteLine(n.ToString() + "}");

            for (int i = 0; i < m; i++)
            {

                b.Add(B(a[i], n));
                w *= (int)Math.Pow(b[i], Int32.Parse(s[i].ToString()));
            }

            Console.Write("Закрытый ключ > {");
            for (int i = 0; i < b.Count; i++)
            {
                Console.Write(b[i].ToString());
                if (i != b.Count - 1)
                    Console.Write(", ");
            }
            Console.WriteLine("}");

            w *= t * t;
            w %= n;

            Console.WriteLine($"\nПодпись > ({Convert.ToInt32(s, 2)}, {t})");
            Console.WriteLine($"Проверка подписи:");
            Console.WriteLine($"u = {u}, w = {w}");
            Console.WriteLine((u == w) ? "Подпись действительна" : "Подпись не действительна");

            Console.ReadLine();
        }

        static String Hash(String str)
        {
            String bits = "";
            List<long> blocks = new List<long>();
            long sum;
            bits = string.Join("", str.ToCharArray().Select(c => Convert.ToString(c, 2)).ToArray());
            for (int i = 0; i < bits.Length; i += 16)
            {
                if (i + 15 >= bits.Length)
                {
                    blocks.Add(Convert.ToInt64(bits.Substring(i), 2));
                }
                else
                {
                    blocks.Add(Convert.ToInt64(bits.Substring(i, 16), 2));
                }
            }
            sum = blocks.First();
            for (int i = 1; i < blocks.Count(); i++)
            {
                sum ^= blocks[i];
                sum >>= 1;
            }
            return Convert.ToString(sum, 2);
        }

        static bool easyNum(int num)
        {
            for (int i = 2; i < num; i++)
            {
                if (num % i == 0)
                    return false;
            }
            return true;
        }
        static int B(int a, int n)
        {
            int num = 0;
            while ((a * a * num) % n != 1)
            {
                num++;
            }

            return num;
        }
    }
}
