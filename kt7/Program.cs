using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace TaskSelector
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Выберите задание (1-3):");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    IConverter<string, int> stringToIntConverter = new StringToIntConverter(); 
                    IConverter<object, string> objectToStringConverter = new ObjectToStringConverter(); 
                    Task1();
                    break;

                case "2":
                    Task2();
                    break;

                case "3":
                    Func<double, double, object> addOperation = (x, y) => (object)Calculator.Add(x, y); 
                    Task3();
                    break;

                default:
                    Console.WriteLine("Некорректный выбор.");
                    break;
            }
        }




        static void Task1()
        {

            IConverter<string, int> stringToIntConverter = new StringToIntConverter();
            IConverter<object, string> objectToStringConverter = new ObjectToStringConverter();

            string[] stringArray = { "1", "2", "3" };
            int[] intArray = ConvertArray(stringArray, stringToIntConverter);
            Console.WriteLine("Результат StringToIntConverter:");
            foreach (var num in intArray)
                Console.WriteLine(num);

            object[] objectArray = { 42, "Hello", 3.14 };
            string[] stringResults = ConvertArray(objectArray, objectToStringConverter);
            Console.WriteLine("Результат ObjectToStringConverter:");
            foreach (var str in stringResults)
                Console.WriteLine(str);
        }

        static U[] ConvertArray<T, U>(T[] array, IConverter<T, U> converter)
        {
            U[] result = new U[array.Length];
            for (int i = 0; i < array.Length; i++)
                result[i] = converter.Convert(array[i]);
            return result;
        }

        interface IConverter<T, U>
        {
            U Convert(T value);
        }

        class StringToIntConverter : IConverter<string, int>
        {
            public int Convert(string value) => int.Parse(value);
        }

        class ObjectToStringConverter : IConverter<object, string>
        {
            public string Convert(object value) => value.ToString();
        }

        static void Task2()
        {

            List<Animal> animals = new List<Animal>
            {
                new Dog("DOggg"),
                new Cat("Cattt")
            };

            Action<Animal> greet = animal => animal.SayHello();
            ProcessAnimals(animals, greet);
        }

        static void ProcessAnimals(List<Animal> animals, Action<Animal> action)
        {
            foreach (var animal in animals)
                action(animal);
        }

        abstract class Animal
        {
            public string Name { get; }
            public Animal(string name) => Name = name;
            public abstract void SayHello();
        }

        class Dog : Animal
        {
            public Dog(string name) : base(name) { }
            public override void SayHello() => Console.WriteLine($"Собака {Name} говорит: Гав!");
        }

        class Cat : Animal
        {
            public Cat(string name) : base(name) { }
            public override void SayHello() => Console.WriteLine($"Кошка {Name} говорит: Мяу!");
        }

        static void Task3()
        {

            Func<double, double, double> add = Calculator.Add;
            Func<double, double, double> subtract = Calculator.Subtract;
            Func<double, double, double> multiply = Calculator.Multiply;
            Func<double, double, double> divide = Calculator.Divide;

            Console.WriteLine($"5 + 3 = {ProcessNumbers(5, 3, add)}");
            Console.WriteLine($"5 - 3 = {ProcessNumbers(5, 3, subtract)}");
            Console.WriteLine($"5 * 3 = {ProcessNumbers(5, 3, multiply)}");
            Console.WriteLine($"5 / 3 = {ProcessNumbers(5, 3, divide)}");
        }

        static double ProcessNumbers(double x, double y, Func<double, double, double> operation)
        {
            return operation(x, y);
        }

        static class Calculator
        {
            public static double Add(double x, double y) => x + y;
            public static double Subtract(double x, double y) => x - y;
            public static double Multiply(double x, double y) => x * y;
            public static double Divide(double x, double y) => y != 0 ? x / y : throw new DivideByZeroException("На ноль делить нельзя.");
        }
    }
}
