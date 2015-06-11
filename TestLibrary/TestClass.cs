using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLibrary
{
    public class TestClass
    {
        public static void Main(string[] args)
        {
//            Console.WriteLine("{0}  Main started", DateTime.Now.ToShortTimeString());
            TestConditions(5);
            TestSwitch(2);
            TestBox(5);
            TestDateArray(5);
            TestArray(5);
            TestNew();
            TestSubProc();
        }

        public static void TestSubProc()
        {
            int i = 3;
            while (i < 5)
            {
                Console.WriteLine("{0}  SubProc started: {1}", DateTime.Now.ToShortTimeString(), i.ToString());
                Mul(i);
                i++;
            }
        }

        public static void Mul(int i)
        {
            i++;
            Console.WriteLine((i*i).ToString());
        }

        public static int Sum(int[] arr)
        {
            int result = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i]++;
                result += arr[i];

                Console.WriteLine("arr[{0}] = {1}   result = {2}", i, arr[i], result);
            }
            return result;
        }

        public static void TestBox(object i)
        {
            Console.WriteLine(((double)i * (double)i).ToString());
        }

        public static void TestNew()
        {
            DateTime d = new DateTime(2015, 6, 10);
            Console.WriteLine(d.ToLongDateString());
        }

        public static void TestArray(int x)
        {
            int[] arr = new int[x];
            for (int i = 0; i < x; i++)
            {
                arr[i] = i;
            }

            Console.WriteLine("Sum is:" +  Sum(arr).ToString());
        }


        public static void TestDateArray(int x)
        {
            DateTime[] arr = new DateTime[x];
            for (int i = 0; i < x; i++)
            {
                arr[i] = DateTime.Now.AddDays(i);
            }

            foreach (DateTime value in arr)
                Console.WriteLine(value);
        }

        public static void TestSwitch(int value)
        {
            switch (value.ToString())
            {
                case "1":
                    Console.WriteLine("one");
                    break;
                case "2":   
                    Console.WriteLine("two");
                    break;
                default:
                    Console.WriteLine("default");
                    break;
            }
        }

        public static void TestConditions(int x)
        {
            if (x > 0)
                Console.WriteLine(String.Format("{0} is more than zero", x.ToString()));
            if (x >= 5)
                Console.WriteLine(String.Format("{0} is more or equals than 5", x.ToString()));
            if (x == 5)
                Console.WriteLine(String.Format("{0} equals 5", x.ToString()));
            if (x < 0)
                Console.WriteLine(String.Format("{0} is less than zero", x.ToString()));
            if (x <= 0)
                Console.WriteLine(String.Format("{0} is less oe equals zero", x.ToString()));
        }
    }
}
