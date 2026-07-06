using System;
using System.Collections;
using System.Collections.Generic;

namespace SmartCollections
{
    public class SmartStack<T> : IEnumerable<T>
    {
        private T[] _items;
        private int _count;

        public SmartStack()
        {
            _items = new T[4];
            _count = 0;
        }

        public SmartStack(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity));
            }

            _items = new T[capacity];
            _count = 0;
        }

        public SmartStack(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            int size = 0;
            foreach (var _ in collection)
            {
                size++;
            }

            _items = new T[size];
            _count = 0;

            foreach (var item in collection)
            {
                _items[_count++] = item;
            }
        }

        public int Count
        {
            get { return _count; }
        }

        public int Capacity
        {
            get { return _items.Length; }
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= _count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }

                return _items[_count - 1 - index];
            }
        }

        public void Push(T item)
        {
            if (_count == _items.Length)
            {
                int newCapacity = _items.Length == 0 ? 4 : _items.Length * 2;
                T[] newArray = new T[newCapacity];
                Array.Copy(_items, 0, newArray, 0, _count);
                _items = newArray;
            }

            _items[_count++] = item;
        }

        public void PushRange(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            foreach (var item in collection)
            {
                Push(item);
            }
        }

        public T Pop()
        {
            if (_count == 0)
            {
                throw new InvalidOperationException("Стек пуст.");
            }

            T item = _items[--_count];
            _items[_count] = default!;
            return item;
        }

        public T Peek()
        {
            if (_count == 0)
            {
                throw new InvalidOperationException("Стек пуст.");
            }

            return _items[_count - 1];
        }

        public bool Contains(T item)
        {
            var comparer = EqualityComparer<T>.Default;
            for (int i = 0; i < _count; i++)
            {
                if (comparer.Equals(_items[i], item))
                {
                    return true;
                }
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = _count - 1; i >= 0; i--)
            {
                yield return _items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class Program
    {
        public static void Main()
        {
            var stack = new SmartStack<string>();
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\n--- УМНЫЙ СТЕК ---");
                Console.WriteLine("1. Push (Добавить элемент)");
                Console.WriteLine("2. PushRange (Добавить массив)");
                Console.WriteLine("3. Pop (Удалить и вернуть)");
                Console.WriteLine("4. Peek (Посмотреть вершину)");
                Console.WriteLine("5. Get Count (Количество элементов)");
                Console.WriteLine("6. Get Capacity (Ёмкость массива)");
                Console.WriteLine("7. Contains (Поиск элемента)");
                Console.WriteLine("8. Indexer (Элемент по глубине)");
                Console.WriteLine("9. View All (Обход IEnumerable)");
                Console.WriteLine("0. Exit (Выход)");
                Console.Write("\nДействие: ");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            Console.Write("Элемент: ");
                            stack.Push(Console.ReadLine() ?? "");
                            break;

                        case "2":
                            Console.Write("Введите элементы через запятую: ");
                            string input = Console.ReadLine() ?? "";
                            string[] elements = input.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            stack.PushRange(elements);
                            break;

                        case "3":
                            Console.WriteLine($"Удален: {stack.Pop()}");
                            break;

                        case "4":
                            Console.WriteLine($"На вершине: {stack.Peek()}");
                            break;

                        case "5":
                            Console.WriteLine($"Количество (Count): {stack.Count}");
                            break;

                        case "6":
                            Console.WriteLine($"Ёмкость (Capacity): {stack.Capacity}");
                            break;

                        case "7":
                            Console.Write("Что найти: ");
                            string search = Console.ReadLine() ?? "";
                            Console.WriteLine(stack.Contains(search) ? "Найден" : "Не найден");
                            break;

                        case "8":
                            Console.Write("Глубина (0 - вершина): ");
                            int index = int.Parse(Console.ReadLine() ?? "0");
                            Console.WriteLine($"Элемент: {stack[index]}");
                            break;

                        case "9":
                            Console.Write("Все элементы: ");
                            foreach (var item in stack) Console.Write($"[{item}] ");
                            Console.WriteLine();
                            break;

                        case "0":
                            isRunning = false;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }
    }
}