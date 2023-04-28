using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSAalgorithm
{
    class Program
    {
        public const string alphabet = "abcdefghigklmnopqrstuvwxyz_";
        public static readonly int[] numbers = { 11, 20, 17, 22, 15, 14, 23, 9, 19, 18, 26, 32, 12, 30, 7, 21, 46, 38, 43, 10, 43, 16, 50, 24, 25, 28, 27 };
        private static int gcd(int a, int b)
        {
            if (a == 0)
                return b;
            return gcd(b % a, a);
        }

        private static int f(int p, int q)
        {
            return (p - 1) * (q - 1);
        }
        
        private static int phi(int n)
        {
            int result = 1;
            for (int i = 2; i < n; i++)
                if (gcd(i, n) == 1)
                    result++;
            return result;
        }

        private static long ModPow(long x, long y, long p)
        {
            if (y == 1)
                return x;
            else
                return (((long)Math.Pow(x, y)) % p);
        }

        private static void Encryption()
        {
            Console.WriteLine("Enter p:");
            var p = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter q:");
            var q = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter PK:");
            var PK = int.Parse(Console.ReadLine());
            var N = p * q;
            Console.WriteLine("N:" + N);
            var SK = ModPow(PK, phi(f(p, q)) - 1, f(p, q));
            Console.WriteLine("SK:" + SK);
            Console.WriteLine("Enter the plain text:");
            var plain = Console.ReadLine();
            plain = plain.ToLower().Replace(" ", "_");
            var cipherText = "";
            foreach(var s in plain)
            {
                var index = ModPow(numbers[alphabet.IndexOf(s)], PK, N);
                cipherText += index + " ";
            }
            Console.WriteLine("Cipher is: ");
            Console.WriteLine(cipherText);
        }

        private static void Decryption()
        {
            Console.WriteLine("Enter p:");
            var p = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter q:");
            var q = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter PK:");
            var PK = int.Parse(Console.ReadLine());
            var N = p * q;
            Console.WriteLine("N: " + N);
            var SK = ModPow(PK, phi(f(p, q)) - 1, f(p, q));
            Console.WriteLine("SK: " + SK);
            Console.WriteLine("Enter cipher:");
            var cipherText = Console.ReadLine();
            var cipher= cipherText.Split(' ').Select(int.Parse).ToList();
            var plainText = "";
            foreach(var s in cipher)
            {
                var index = ModPow(s, SK, N);         
                plainText += alphabet[Array.IndexOf(numbers,(int)index)];
            }
            Console.WriteLine("Plain text is: ");
            Console.WriteLine(plainText);
        }

        public static void Main()
        {
            bool choose = true;
            while (choose)
            {
                Console.WriteLine("Press 1 for encryption or 2 for decryption:");
                int choice = int.Parse(Console.ReadLine());
                choose = false;
                switch (choice)
                {
                    case 1:            
                        Encryption();
                        break;
                    case 2:                  
                        Decryption();
                        break;
                    default:
                        Console.WriteLine("Invalid choice!");
                        choose = true;
                        break;
                }
            }
            Console.ReadKey();
        }
    }
}
