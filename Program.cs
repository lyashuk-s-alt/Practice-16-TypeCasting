using System;

namespace SafeTypeCasting
{
    // Базовый абстрактний клас
    abstract class Shape
    {
        public abstract string Name { get; }
        public abstract double GetArea();
    }

    // Похідний клас Коло зі специфічним методом ChangeRadius
    class Circle : Shape
    {
        public double Radius { get; private set; }
        public override string Name { get { return "Коло"; } }

        public Circle(double radius)
        {
            Radius = radius;
        }

        // Специфічний метод, якого немає в базовому класі Shape
        public void ChangeRadius(double newRadius)
        {
            if (newRadius > 0)
            {
                Radius = newRadius;
                Console.WriteLine("   -> Радіус кола успішно змінено на " + Radius + ".");
            }
        }

        public override double GetArea()
        {
            return Math.PI * Radius * Radius;
        }
    }

    // Похідний клас Прямокутник
    class Rectangle : Shape
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public override string Name { get { return "Прямокутник"; } }

        public Rectangle(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public override double GetArea()
        {
            return Width * Height;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Масив базового типу Shape[]
            Shape[] shapes = new Shape[]
            {
                new Circle(5),
                new Rectangle(4, 6),
                new Circle(10),
                new Rectangle(2, 8)
            };

            Console.WriteLine("=== ЗАГАЛЬНЕ ЗАВДАННЯ (Використання операторів is та as) ===\n");

            // Варіант 1: Класичне використання оператора 'is' + приведення типу
            Console.WriteLine("--- Перевірка через оператор 'is' ---");
            foreach (Shape shape in shapes)
            {
                Console.WriteLine(string.Format("Фігура: {0}, поточна площа: {1:F2}", shape.Name, shape.GetArea()));

                if (shape is Circle)
                {
                    Circle circle = (Circle)shape;
                    circle.ChangeRadius(circle.Radius + 2);
                    Console.WriteLine(string.Format("   -> Нова площа кола: {0:F2}", circle.GetArea()));
                }
            }

            // Варіант 2: Використання оператора 'as' із перевіркою на null
            Console.WriteLine("\n--- Перевірка через оператор 'as' ---");
            foreach (Shape shape in shapes)
            {
                Circle circleAs = shape as Circle;
                if (circleAs != null)
                {
                    Console.WriteLine("Знайдено коло (радіус " + circleAs.Radius + "). Зменшуємо радіус у 2 рази:");
                    circleAs.ChangeRadius(circleAs.Radius / 2);
                }
            }

            Console.WriteLine("\n=== ДОМАШНЄ ЗАВДАННЯ (Визначення класу через object) ===\n");

            // Перевірка роботи методу на різних типах об'єктів
            IdentifyObject(shapes[0]);       // Circle
            IdentifyObject(shapes[1]);       // Rectangle
            IdentifyObject("Привіт, C#!");   // string
            IdentifyObject(42);              // int
            IdentifyObject(null);            // null
        }

        // Метод для домашнього завдання: сумісний зі старими версіями C# (Mono)
        static void IdentifyObject(object obj)
        {
            if (obj == null)
            {
                Console.WriteLine("Передано порожнє посилання (null).");
                return;
            }

            if (obj is Circle)
            {
                Circle c = (Circle)obj;
                Console.WriteLine("Об'єкт є екземпляром класу Circle (радіус: " + c.Radius + ").");
            }
            else if (obj is Rectangle)
            {
                Rectangle r = (Rectangle)obj;
                Console.WriteLine("Об'єкт є екземпляром класу Rectangle (" + r.Width + "x" + r.Height + ").");
            }
            else if (obj is string)
            {
                string s = (string)obj;
                Console.WriteLine("Об'єкт є екземпляром класу String (текст: \"" + s + "\").");
            }
            else
            {
                Console.WriteLine("Об'єкт є екземпляром типу " + obj.GetType().Name + ".");
            }
        }
    }
}
