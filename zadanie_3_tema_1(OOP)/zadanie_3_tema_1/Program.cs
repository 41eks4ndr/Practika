using System;

namespace GeometryApp.Shapes
{
    public class Rectangle
    {
        private double _topLeftX;
        private double _topLeftY;
        private double _width;
        private double _height;

        public Rectangle(double x, double y, double width, double height)
        {
            if (width < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width), "Ширина не может быть отрицательной.");
            }

            if (height < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(height), "Высота не может быть отрицательной.");
            }

            _topLeftX = x;
            _topLeftY = y;
            _width = width;
            _height = height;
        }

        public double Area => _width * _height;

        public double Perimeter => 2 * (_width + _height);

        public string GetInfo()
        {
            return $"Прямоугольник [X: {_topLeftX}, Y: {_topLeftY}, W: {_width}, H: {_height}]";
        }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {
                Console.WriteLine("--- Создание прямоугольника ---");

                Console.Write("Введите X левого верхнего угла: ");
                var x = Convert.ToDouble(Console.ReadLine());

                Console.Write("Введите Y левого верхнего угла: ");
                var y = Convert.ToDouble(Console.ReadLine());

                Console.Write("Введите ширину: ");
                var width = Convert.ToDouble(Console.ReadLine());

                Console.Write("Введите высоту: ");
                var height = Convert.ToDouble(Console.ReadLine());

                var rect = new Rectangle(x, y, width, height);

                Console.WriteLine("\nОбъект успешно создан в корректном состоянии.");
                Console.WriteLine(rect.GetInfo());
                Console.WriteLine($"Площадь: {rect.Area}");
                Console.WriteLine($"Периметр: {rect.Perimeter}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: Введено не число.");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Ошибка валидации: {ex.ParamName}. {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}"); Console.WriteLine(ex.ToString());
            }

            Console.WriteLine("\nНажмите любую клавишу для завершения...");
            Console.ReadKey();
        }
    }
}