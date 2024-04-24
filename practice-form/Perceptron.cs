using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticAl;

namespace practice_form
{
    class Perceptron
    {
        // Инициализация весов сети
        public static int[,] weights { get; private set; }

        // Порог функции активации
        public static int bias { get; set; }

        public static int trainings { get; set; }

        private static Random random = new Random(Guid.NewGuid().GetHashCode());

        public static void training()
        {
            weights = new int[10, 25];
            // Цифры (Обучающая выборка)

            var let0 = "1111110001111111000110001".ToCharArray();//A
            var let1 = "1111110001111111000111111".ToCharArray();//B
            var let2 = "1111110000100001000011111".ToCharArray();//C
            var let3 = "1111010001100011000111110".ToCharArray();//D
            var let4 = "1111110000111111000011111".ToCharArray();//E
            var let5 = "1111110000111111000010000".ToCharArray();//F
            var let6 = "1111110000100111000111111".ToCharArray();//G
            var let7 = "1000110001111111000110001".ToCharArray();//H
            var let8 = "0111000100001000010001110".ToCharArray();//I
            var let9 = "0111000010000100101000100".ToCharArray();//J

            // Список всех вышеуказанных цифр
            char[][] letters = { let0, let1, let2, let3, let4, let5, let6, let7, let8, let9 };
            /*
            // Виды буквы F (Тестовая выборка)
            var num51 = "1111010000111001000010000".ToCharArray();
            var num52 = "1111110000111011000010000".ToCharArray();
            var num53 = "1110110000111111000010000".ToCharArray();
            var num54 = "1111110000111111110010000".ToCharArray();
            var num55 = "1111110000111000000010000".ToCharArray();
            var num56 = "1111110000111111000011000".ToCharArray();
            var num10 = "1111110001111111100110001".ToCharArray();//A

            // Тренировка сети
            for (int i = 0; i < trainings; i++)
            {
                // Генерируем случайное число от 0 до 9
                var option = random.Next(0, 9 + 1);

                for (int j = 0; j < 10; j++)
                {
                    if (option != j)
                    {
                        if (proceed(letters[option], j)) decrease(letters[option], j);
                    }
                    else
                    {
                        if (!proceed(letters[j], j)) increase(letters[j], j);
                    }
                }
            }*/
            EvolutionManager manager = new EvolutionManager();
            manager.InitPipolation(300);
            Func<int[][], int> det = determByWaights;
            for (int i = 0; i < trainings; i++)
            {
                manager.GetNewGeneration(det);
            }
            for (int i = 0; i < weights.GetLength(0); i++)
            {
                for (int j = 0; j < weights.GetLength(1); j++)
                {
                    weights[i, j] = manager.BestW[i][j];
                }
            }
        }

        public static bool proceed(char[] letter, int right)
        {
            // Рассчитываем взвешенную сумму
            var net = 0;
            for (int i = 0; i < 25; i++)
            {
                net += int.Parse(letter[i].ToString()) * weights[right, i];
            }

            // Превышен ли порог? (Да - сеть думает, что это 5. Нет - сеть думает, что это другая цифра)
            return net >= bias;
        }

        // Уменьшение значений весов, если сеть ошиблась и выдала 1
        private static void decrease(char[] letter, int right)
        {
            for (int i = 0; i < 25; i++)
            {
                // Возбужденный ли вход
                if (int.Parse(letter[i].ToString()) == 1)
                {
                    // Уменьшаем связанный с ним вес на единицу
                    weights[right, i]--;
                }
            }
        }

        // Увеличение значений весов, если сеть ошиблась и выдала 0
        private void increase(char[] number, int right)
        {
            for (int i = 0; i < 25; i++)
            {
                // Возбужденный ли вход
                if (int.Parse(number[i].ToString()) == 1)
                {
                    // Увеличиваем связанный с ним вес на единицу
                    weights[right, i]++;
                }
            }
        }
        public static bool proceed(char[] letter, int right, int[][] _weights)
        {
            // Рассчитываем взвешенную сумму
            var net = 0;
            for (int i = 0; i < 25; i++)
            {
                net += int.Parse(letter[i].ToString()) * _weights[right][i];
            }

            // Превышен ли порог? (Да - сеть думает, что это 5. Нет - сеть думает, что это другая цифра)
            return net >= bias;
        }
        public static int determByWaights(int[][] _weights)
        {
            var let0 = "1111110001111111000110001".ToCharArray();//A
            var let1 = "1111110001111111000111111".ToCharArray();//B
            var let2 = "1111110000100001000011111".ToCharArray();//C
            var let3 = "1111010001100011000111110".ToCharArray();//D
            var let4 = "1111110000111111000011111".ToCharArray();//E
            var let5 = "1111110000111111000010000".ToCharArray();//F
            var let6 = "1111110000100111000111111".ToCharArray();//G
            var let7 = "1000110001111111000110001".ToCharArray();//H
            var let8 = "0111000100001000010001110".ToCharArray();//I
            var let9 = "0111000010000100101000100".ToCharArray();//J
            int right = 0;
            // Список всех вышеуказанных цифр
            char[][] letters = { let0, let1, let2, let3, let4, let5, let6, let7, let8, let9 };
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (proceed(letters[j], i, _weights))
                    {
                        if (i == j)
                        {
                            right++;
                        }
                        else
                        {
                            right -= 5;
                        }
                    }
                }
            }
            return right;
        }
    }
}
