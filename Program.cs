using System;
using System.Globalization;
using System.Linq;

namespace CircleApp
{
    class Circle
    {
        private double _radius;
        private double _centerX;
        private double _centerY;

        public Circle(double radius, double centerX, double centerY)
        {
            Radius = radius;
            _centerX = centerX;
            _centerY = centerY;
        }

        public double Radius
        {
            get { return _radius; }
            set 
            { 
                if (value <= 0)
                {
                    throw new ArgumentException("Радіус повинен бути більше 0");
                }
                _radius = value;
            }
        }

        public double CenterX
        {
            get { return _centerX; }
            set { _centerX = value; }
        }

        public double CenterY
        {
            get { return _centerY; }
            set { _centerY = value; }
        }

        public double GetArea()
        {
            return Math.PI * _radius * _radius;
        }
        public void DisplayInfo()
        {
            Console.WriteLine($"Круг: центр ({_centerX:F2}, {_centerY:F2}), радіус = {_radius:F2}, площа = {GetArea():F2}");
        }

        public override string ToString()
        {
            return $"Круг: центр ({_centerX:F2}, {_centerY:F2}), радіус = {_radius:F2}, площа = {GetArea():F2}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            int n = ReadPositiveInteger("Введіть кількість кругів (n): ");

            Circle[] circles = CreateCircles(n);

            DisplayAllCircles(circles);

            DisplayCircleWithMaxArea(circles);

            Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }

        static int ReadPositiveInteger(string prompt)
        {
            int value;
            do
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (input == null)
                {
                    Console.WriteLine("Помилка читання вводу. Спробуйте ще раз.");
                    continue;
                }

                if (int.TryParse(input, NumberStyles.Integer, CultureInfo.InvariantCulture, out value))
                {
                    if (value > 0)
                    {
                        return value;
                    }
                    else
                    {
                        Console.WriteLine("Помилка: число повинно бути більше 0. Спробуйте ще раз.");
                    }
                }
                else
                {
                    Console.WriteLine("Помилка: введіть коректне ціле число. Спробуйте ще раз.");
                }
            } while (true);
        }


        static double ReadDouble(string prompt, bool mustBePositive = false)
        {
            double value;
            do
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (input == null)
                {
                    Console.WriteLine("Помилка читання вводу. Спробуйте ще раз.");
                    continue;
                }

                if (double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out value))
                {
                    if (!mustBePositive || value > 0)
                    {
                        return value;
                    }
                    else
                    {
                        Console.WriteLine("Помилка: число повинно бути більше 0. Спробуйте ще раз.");
                    }
                }
                else
                {
                    Console.WriteLine("Помилка: введіть коректне дійсне число. Спробуйте ще раз.");
                }
            } while (true);
        }

        static Circle[] CreateCircles(int n)
        {
            Circle[] circles = new Circle[n];

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"\n=== Круг №{i + 1} ===");

                double radius = ReadDouble("Введіть радіус (> 0): ", mustBePositive: true);
                double x = ReadDouble("Введіть X-координату центру: ");
                double y = ReadDouble("Введіть Y-координату центру: ");

                try
                {
                    circles[i] = new Circle(radius, x, y);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Помилка створення круга: {ex.Message}");
                    i--;
                }
            }

            return circles;
        }

        static void DisplayAllCircles(Circle[] circles)
        {
            Console.WriteLine("\n========== Всі круги ==========");
            for (int i = 0; i < circles.Length; i++)
            {
                Console.Write($"{i + 1}. ");
                circles[i].DisplayInfo();
            }
        }
        static void DisplayCircleWithMaxArea(Circle[] circles)
        {
            Circle maxCircle = circles.OrderByDescending(c => c.GetArea()).First();
            int maxIndex = Array.IndexOf(circles, maxCircle);

            Console.WriteLine("\n========== Круг з найбільшою площею ==========");
            Console.WriteLine($"Позиція в масиві: {maxIndex + 1}");
            maxCircle.DisplayInfo();
        }

        static Circle FindCircleWithMaxArea(Circle[] circles)
        {
            if (circles == null || circles.Length == 0)
            {
                throw new ArgumentException("Масив кругів не може бути порожнім");
            }

            Circle maxCircle = circles[0];

            for (int i = 1; i < circles.Length; i++)
            {
                if (circles[i].GetArea() > maxCircle.GetArea())
                {
                    maxCircle = circles[i];
                }
            }

            return maxCircle;
        }
    }
}
