using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using Numerics;
using System.IO;

namespace Генератор_вариантов
{
    using Ex = Microsoft.Office.Interop.Excel;

    //Класс, хранящий текст заданий варианта и ответы к заданиям
    class TestVersion
    {
        private decimal _versionNum; //Номер варианта
        private string[] _tasks; //Тексты заданий
        private List<double>[] _solutions;
        private string[] _stringSolutions;

        private string _versionText;
        private string _answersText;

        private double[][] _binomialCoefs;

        public string VersionText
        {
            get 
            {
                return _versionText;
            }
        }

        public string AnswersText
        {
            get { return _answersText; }
        }

        public decimal VersionNum
        {
            get { return _versionNum; }
        }

        public TestVersion(decimal numOfVersion)
        {
            _versionNum = numOfVersion;
            _tasks = new string[8];
            _solutions = new List<double>[8];
            _stringSolutions = new string[8];
            _binomialCoefs = new double[201][];
            for (int i = 0; i < 200; ++i)
            {
                _binomialCoefs[i] = new double[201];
                for (int j = 0; j < 200; ++j)
                {
                    _binomialCoefs[i][j] = 0;
                }
            }
        }

        //Метод, генерирующий тексты заданий и ответы к ним
        public void generateTasks()
        {
            //Генерируем каждое задание в отдельном методе
            generateFirstTask();
            generateSecondTask();
            generateThirdTask();
            generateFourthTask();
            generateFifthTask();
            generateSixthTask();
            generateSeventhTask();
            generateEighthTask();
            /*generateNinthTask();
            generateTenthTask();
            generateEleventhTask();
            generateTwelfthTask();
            generateThirteenthTask();
            generateFourteenthTask();
            generateFifteenthTask();
            generateSixsteenthTask();
            generateSeventeenthTask();
            generateEighteenthTask();*/

            //Собираем весь текст в одну переменную
            _versionText = string.Empty;
            for (int i = 0; i < 8; ++i)
                _versionText += _tasks[i];
            _versionText += "\n\n\n";
            //Собираем ответы в одну переменную
            _answersText = string.Empty;
            _answersText = _versionNum + " ВАРИАНТ\n";
            for (int i = 0; i < 8; ++i)
            {
                if (_stringSolutions[i] == null)
                    for (int j = 0; j < _solutions.ElementAt(i).Count; ++j)
                    {
                        if (_solutions.ElementAt(i).Count == 1)
                            _answersText += (i + 1) + ". " + Math.Round(_solutions.ElementAt(i).ElementAt(j), 6) + "\n\n";
                        else //Делим ответ на подпункты, если это необходимо
                        {
                            _answersText += (i + 1) + "." + (char)(97 + j) + ". " + Math.Round(_solutions.ElementAt(i).ElementAt(j), 6)
                                + "\n\n";
                        }
                    }
                else
                    _answersText += (i + 1) + ". " + _stringSolutions.ElementAt(i) + "\n\n";
            }
        }


//----------------------------------Генерация заданий----------------------------------------------
        private int randChoiceMission()
        {
            Random rand_choice = new Random();
            int randC = 0;
            for (int i = 0; i < rand_choice.Next(10, 500); i++)
            {
                randC = rand_choice.Next(0, 2);
            }
            return randC;
        }
        private void generateFirstTask()
        {
            int[] int_params = new int[4];

            int randC = randChoiceMission();

            _tasks[0] = _versionNum + " ВАРИАНТ ";

            if (randC <= 0)
            {
                ///В группе 12 человек, 4 из которых неуспевающих.По списку вызывают сразу пять человек " +
                ///"Найти вероятность того, что два из них будут неуспевающими.
                Random rand_generator = new Random();
                //Первое задание
              
                _tasks[0] += "\n\n" + _versionNum + ".1. В группе ";
                int_params[0] = rand_generator.Next(10, 30);
                int_params[0] -= int_params[0] % 10;
                _tasks[0] += int_params[0] + " человек, ";

                int_params[1] = rand_generator.Next(2, int_params[0] / 3);
                _tasks[0] += int_params[1] + " из которых неуспевающих. ";

                _tasks[0] += " По списку вызывают сразу ";
                int_params[2] = rand_generator.Next(4, 4 + int_params[0] / 4);
                _tasks[0] += int_params[2] + " человек. Найти вероятность того, что ";

                int_params[3] += rand_generator.Next(1, int_params[2] - 1);
                _tasks[0] += int_params[3] + " из них будут неуспевающими.";

                _solutions[0] = firstSolution(int_params[0], int_params[1], int_params[2], int_params[3],null, randC);
            }
            else
            {
                ///Bероятности выполнить норму для каждого из трех спортсменов соответственно равны 0,7; 0,8  " +
                ///"0,9. Найти вероятность того, что ее выполнят только два из них.
                double[] double_params = new double[3];
                Random rand_generator = new Random();
                //Первое задание
                _tasks[0] += "\n\n" + _versionNum + ".1. Bероятность выполнить норму для каждого из трех спортсменов соответственно равны ";
                for(int i=0;i<3;i++)
                {
                    double_params[i] = rand_generator.Next(1, 10) * 0.1;
                    _tasks[0] += double_params[i] +"; ";
                }
                _tasks[0] += "соответственно. Найти вероятность того, что ее выполнят только два из них. ";
                _solutions[0] = firstSolution(0,0,0,0,double_params, randC);
            }
            
        }

        private void generateSecondTask()
        {
            int[] int_params = new int[3];

            int randC = randChoiceMission();
            ///.Трое рабочих изготавливают однотипные изделия. Первый изготовил 40 изделий, 15 – второй и 25 - третий. " +
            ///   "Вероятности брака у каждого рабочего соответственно равны 0,05, 0,01, 0,02. Найти вероятность того, " +
            ///    "что наудачу взятая бракованная деталь изготовлена третьим рабочим.

            if (randC==0)
            {
                Random rand_generator = new Random();
                double[] double_params = new double[3];

                _tasks[1] = "\n\n" + _versionNum + ".2. Трое рабочих изготавливают однотипные изделия. Первый изготовил ";
                int_params[0] = rand_generator.Next(20, 100);
                _tasks[1] += int_params[0] + " изделий, ";
                int_params[1] = rand_generator.Next(15, 60);
                _tasks[1] += int_params[1] + " – второй и ";
                int_params[2] = rand_generator.Next(20, 80);
                _tasks[1] += int_params[2] + " - третий. Вероятности брака у каждого рабочего соответственно равны ";

                for (int i = 0; i < 3; i++)
                {
                    double_params[i] = rand_generator.Next(1, 10) * 0.01;
                    if(i!=2)
                        _tasks[1] += double_params[i] + ", ";
                    else
                        _tasks[1] += double_params[i] + ". ";
                }
                _tasks[1] += "Найти вероятность того, что наудачу взятая бракованная деталь изготовлена третьим рабочим.";
                
                _solutions[1] = secondSolution(int_params[0], int_params[1], int_params[2], double_params, randC);

            }
            else
            {
                /// В группе спортсменов 20 лыжников, 6 велосипедистов и 4 бегуна.Вероятности выполнить " +
                /// "квалификационную норму соответственно равны 0,9; 0,8; 0,75. Найти вероятность того, что выбранный  " +
                /// "наудачу спортсмен выполнит норму.
                Random rand_generator = new Random();
                double[] double_params = new double[3];

                _tasks[1] = "\n\n" + _versionNum + ".2. В группе спортсменов ";
                int_params[0] = rand_generator.Next(5, 30);
                _tasks[1] += int_params[0] + " лыжников, ";
                int_params[1] = rand_generator.Next(1, 8);
                _tasks[1] += int_params[1] + " – велосипедистов и ";
                int_params[2] = rand_generator.Next(1, 5);
                _tasks[1] += int_params[2] + " - бегуна. Вероятности выполнить квалификационную норму соответственно равны ";

                for (int i = 0; i < 3; i++)
                {
                    double rand = rand_generator.Next(1, 99);
                    if(rand < 10)
                    {
                        double_params[i] = rand * 0.1;
                    }
                    else
                        double_params[i] = rand * 0.01;

                    if (i != 2)
                        _tasks[1] += double_params[i] + ", ";
                    else
                        _tasks[1] += double_params[i] + ". ";
                }
                _tasks[1] += "Найти вероятность того, что выбранный наудачу спортсмен выполнит норму";

                _solutions[1] = secondSolution(int_params[0], int_params[1], int_params[2], double_params, randC);


            }
 
           
        }

        private void generateThirdTask()
        {
            int[] int_params = new int[4];

            int randC = randChoiceMission();

            if (randC==0)
            {
                Random rand_generator = new Random();
                _tasks[2] = "\n\n" + _versionNum + ".3. Вероятность выиграть по лотерейному билету равна ";
                double possib = rand_generator.Next(1, 9999);
                if (possib >= 1000)
                {
                    possib = possib*0.0001;
                }
                else
                    if(possib > 100)
                {
                    possib = possib * 0.001;
                }
                else
                    if(possib < 100)
                {
                    possib = possib * 0.01;
                }else
                    if(possib<10)
                        possib = possib * 0.1;

                int_params[1] = rand_generator.Next(4, 20);
                _tasks[2] += possib + " Найти вероятность выиграть по ";
                for(int i=0;i<int_params[1];i++)
                    int_params[0] = rand_generator.Next(1, 1 + int_params[1] / 3);
                if (int_params[0] == 1)
                    _tasks[2] += int_params[0] + " билету из " + int_params[1] + ".";
                else
                    _tasks[2] += int_params[0] + " билетам из " + int_params[1] + ".";


                _solutions[2] = thirdSolution(int_params[1], int_params[0],possib);
            }
            else
            {
                /// Устройство, состоящее из пяти независимо работающих элементов, включается на время Т. " +
                /// "Вероятность отказа каждого из элементов за это время равна 0,2. Найти вероятность того, что за время Т откажут три элемента.

                Random rand_generator = new Random();
                _tasks[2] = "\n\n" + _versionNum + ".3. Устройство, состоящее из";
                
                int_params[0] = rand_generator.Next(4, 10);
                _tasks[2] += int_params[0] + " независимо работающих элементов, включается на время Т." +
                    " Вероятность отказа каждого из элементов за это время равна ";
                double possib = rand_generator.Next(1, 10) * 0.1 ;
                _tasks[2] += possib + ". Найти вероятность того, что за время Т откажут";
               int_params[1] = rand_generator.Next(1, 1 + int_params[0]/3);
                _tasks[2] += int_params[1]+ "элемента.";

                 _solutions[2] = thirdSolution(int_params[0], int_params[1], possib);
            }
            
            
        }

        private void generateFourthTask()
        {
            int[] int_params = new int[4];
            int randC = randChoiceMission();
            if(randC==0)
            {
                /// На станциях отправления поездов находится 1000 автоматов для продажи билетов. Вероятность
                ///выхода из строя одного автомата в течение часа равна 0,005.Найти вероятность того, что в течение
                ///часа выйдут из строя: а) 5 автоматов; б) от 2 до 12 автоматов.Найти наиболее вероятное число
                ///вышедших из строя автоматов.
                ///
                double possib = 0;
                Random rand_generator = new Random();
                _tasks[3] += "\n\n" + _versionNum + ".4. На станциях отправления поездов находится ";
                int_params[0] = rand_generator.Next(500, 2000);
                _tasks[3] += int_params[0] + " автоматов для продажи билетов. Вероятность выхода из строя одного автомата в течение часа равна ";
                possib = rand_generator.Next(1, 9)*0.001;
                _tasks[3] += possib + ". Найти вероятность того, что в течение часа выйдут из строя: а) ";

                int_params[1] = rand_generator.Next(2,2+ int_params[0]/200);
                _tasks[3] += int_params[1] + " автоматов; б) от ";

                int_params[2] = rand_generator.Next(2, 2 + int_params[0] / 180);
                _tasks[3] += int_params[2] + " до ";

                int_params[3] = rand_generator.Next(int_params[2], int_params[2] + int_params[0] / 3);
                _tasks[3] += int_params[3] + " автоматов. Найти наиболее вероятное число вышедших из строя автоматов. ";

                _stringSolutions[3] = fourthSolution(int_params, possib, randC);

            }
            else
            {
                ///Вероятность поражения мишени стрелком при одном выстреле 0,75. Найти вероятность того, что
                /// при 10 выстрелах стрелок поразит мишень: а) 8 раз; б) от 2 до 5 раз.Найти наиболее вероятное число попаданий.

                double possib = 0;
                Random rand_generator = new Random();
                _tasks[3] += "\n\n" + _versionNum + ".4. Вероятность поражения мишени стрелком при одном выстреле ";
                possib = rand_generator.Next(1, 99);
                if (possib >= 10 && possib <= 99)
                {
                    possib = possib * 0.01;
                }
                else
                    if (possib < 10)
                    possib = possib * 0.1;
                _tasks[3] += possib + ". Найти вероятность того, что при ";
                int_params[0] = rand_generator.Next(5, 20);
                _tasks[3] += int_params[0] + " выстрелах стрелок поразит мишень: а) ";
                int_params[1] = rand_generator.Next(2, 2 + int_params[0] / 2);
                if (int_params[1] >= 2 || int_params[1] <= 4)
                {
                    _tasks[3] += int_params[1] + " раза б) от ";
                }
                else
                    _tasks[3] += int_params[1] + " раз б) от) ";
                int_params[2] = rand_generator.Next(1, 1 + int_params[0] / 3);
                int_params[3] = rand_generator.Next(int_params[2], int_params[2] + int_params[0] / 3);
                _tasks[3] += int_params[2] + " до " + int_params[3] + " раз. Найти наиболее вероятное число попаданий. ";
                _stringSolutions[3] = fourthSolution(int_params, possib, randC);
            }
   
        }

        private void generateFifthTask()
        {
            int int_param;
            double[] double_params = new double[7];
            int randC = randChoiceMission();
            ///Студент купил 4 билета новогодней лотереи. Вероятность выигрыша по одному билету равна 0,6.
            ///Составить закон распределения, вычислить математическое ожидание и дисперсию числа выигрышей
            ///среди купленных билетов
            if(randC==0)
            {
                Random rand_generator = new Random();

                _tasks[4] = "\n\n" + _versionNum + ".5.  Студент купил ";
                int_param = rand_generator.Next(2, 5);
                if(int_param == 5)
                    _tasks[4] += int_param + " билетов новогодней лотереи. Вероятность выигрыша по одному билету равна ";
                else
                    _tasks[4] += int_param + " билета новогодней лотереи. Вероятность выигрыша по одному билету равна ";
                double possib = rand_generator.Next(1, 9)*0.1;
                _tasks[4] += possib + ". Составить закон распределения, вычислить математическое ожидание и дисперсию числа выигрышей среди купленных билетов.";
  
                _stringSolutions[4] = fifthSolution(int_param, possib,randC);
            }else
            {
                ///Вероятность того, что необходимая студенту книга свободна в библиотеке, равна 0,3.Составить
                ///закон распределения числа библиотек, которые посетит студент, если в городе 4 библиотеки.
                ///Вычислить математическое ожидание и дисперсию этой случайной величины.
                Random rand_generator = new Random();

                _tasks[4] = "\n\n" + _versionNum + ".5.  Вероятность того, что необходимая студенту книга свободна в библиотеке, равна ";
                double possib = rand_generator.Next(1, 9) * 0.1;
                _tasks[4] += possib + ". Составить закон распределения числа библиотек, которые посетит студент, если в городе ";
                int_param = rand_generator.Next(2, 10);
                if (int_param >= 5)
                    _tasks[4] += int_param + "  библиотек. Вычислить математическое ожидание и дисперсию этой случайной величины. ";
                else
                    _tasks[4] += int_param + "  библиотеки. Вычислить математическое ожидание и дисперсию этой случайной величины.";

                _stringSolutions[4] = fifthSolution(int_param, possib, randC);
            }
           
        }

        private void generateSixthTask()
        {
            ///Задана плотность распределения случайной величины Х: f(x) = { [Ax, 0 < x <1], [0, (x<=0)|(x>1)] } " +
            /// "Найти параметр А, интегральную функцию распределения, математическое ожидание, дисперсию и среднееквадратическое отклонение. "
            /// 
            int[] int_params = new int[3];
            int randC = randChoiceMission();
            if(randC==0)
            {
                Random rand_generator = new Random();

                _tasks[5] = "\n\n" + _versionNum + ".6.  Задана плотность распределения случайной величины \nХ: f(x) = { [Ax^";
                int_params[0] = rand_generator.Next(2, 8);
                _tasks[5] += int_params[0] + ", 0 < x < ";
                int_params[1] = rand_generator.Next(1, 15);
                _tasks[5] += int_params[1] + "], [0, (x<=0)|(x>"+ int_params[1] + ")] }.\n Найти параметр А, интегральную функцию распределения, математическое ожидание, дисперсию и среднеквадратическое отклонение.";

                _stringSolutions[5] = sixthSolution(int_params[0],int_params[1],0,randC);
            }
            else
            {
                /// Задана плотность распределения случайной величины Х: f(x) = { [2Ax-A, 1 < x <= 2], [0, (x <= 1)|(x >2)] } " +
                ///"Найти параметр А, интегральную функцию распределения, математическое ожидание, дисперсию и среднее квадратическое отклонение.
                ///
                Random rand_generator = new Random();

                _tasks[5] = "\n\n" + _versionNum + ".6.  Задана плотность распределения случайной величины \nХ: f(x) = { [";
                int_params[0] = rand_generator.Next(2, 16);
                _tasks[5] += int_params[0] + "Ax^ ";
                int_params[1] = rand_generator.Next(1, 10);
                int_params[2] = rand_generator.Next(1, 15);
                _tasks[5] += int_params[1] + "-A, 1 < x <= "+ int_params[2] + "], [0, (x<=1)|(x>" + int_params[2] + ")] }.\n Найти параметр А, интегральную функцию распределения, математическое ожидание, дисперсию и среднеквадратическое отклонение.  ";

                _stringSolutions[5] = sixthSolution(int_params[0], int_params[1], int_params[2] , randC);
            }
            
        }

        private void generateSeventhTask()
        {
            int[] int_params = new int[5];
            Random rand_generator = new Random();
            _tasks[6] = "\n\n" + _versionNum + ".7.  Заданы математическое ожидание m и среднеквадратическое отклонение нормально" +
            " распределенной случайной величины х. Найти: 1) вероятность того, что х примет значение, " +
            "принадлежащее интервалу (α; β); 2) вероятность того, что абсолютная величина отклонения | x − m | окажется меньше δ.";
            _tasks[6] += " \nm = ";
            int_params[0] = rand_generator.Next(5, 31);
            int_params[1] = rand_generator.Next(1, 5);
            int_params[2] = rand_generator.Next(1, 40);
            int_params[3] = rand_generator.Next(int_params[2], int_params[2] + 20);
            int_params[4] = rand_generator.Next(1, 18);
            _tasks[6] += int_params[0] + " \t  σ = " + int_params[1] + " \tα =" + int_params[2] + "\tβ = " + int_params[3] +"  \tδ = " + int_params[4] + "\n";
        
            _solutions[6] = seventhSolution(int_params);

        }

        private void generateEighthTask()
        {
            int[] paramXi = new int[6];
            int[] paramNi = new int[6];
            Random rand_generator = new Random();
            ///Дано статистическое распределение выборки: в первой строке указаны выборочные варианты Xi " +
            ///"а во второй строке – соответственные частоты Ni количественного признака Х). Требуется найти: " +
            ///"\n1. Методом произведений: а) выборочную среднюю; б) выборочное среднее квадратическое отклонение; " +
            ///"\n2. Доверительные интервалы для оценки неизвестного математического ожидания α с заданной надежностью ϒ = 0,95. " +
            ///"\n3. Пользуясь критерием Пирсона, при уровне значимости α=0,05, установить, согласуется ли гипотеза " +
            ///"о нормальном распределении генеральной совокупности с данными выборки объема n = 100." +
            ///"\nXi(Ni)\t |110 (5) | 115 (10) | 120 (30) |\n| 125 (25) | 130 (15) | 135 (10) | 140 (5) ";
            double reliability = rand_generator.Next(1, 40) * 0.01;
            reliability = reliabilityRand(reliability);
            
            _tasks[7] = "\n\n" + _versionNum + ".8.  Дано статистическое распределение выборки: в первой строке указаны выборочные варианты Xi" +
                " а во второй строке – соответственные частоты Ni количественного признака Х). Требуется найти:\n" +
                "1. Методом произведений: а) выборочную среднюю; б) выборочное среднеквадратическое; \n" +
            "2. Доверительные интервалы для оценки неизвестного математического ожидания α с заданной надежностью ϒ = " + reliability + "\n";

            paramXi[0] = rand_generator.Next(2, 150);
            int step = rand_generator.Next(2, 20);
            paramXi[1] = paramXi[0] + step;
            paramXi[2] = paramXi[1] + step;
            paramXi[3] = paramXi[2] + step;
            paramXi[4] = paramXi[3] + step;
            paramXi[5] = paramXi[4] + step;
            
            paramNi[0] = rand_generator.Next(1, 30);
            paramNi[1] = rand_generator.Next(paramNi[0] + 1, paramNi[0]+5);
            paramNi[2] = rand_generator.Next(2, 25);
            paramNi[3] = rand_generator.Next(3, 30);
            paramNi[4] = rand_generator.Next(15, 29);
            paramNi[5] = rand_generator.Next(1, 20);
            for(int i=paramNi.Length -1;i>=1;i--)
            {
                int j = rand_generator.Next(i + 1);
                int tmp = paramNi[j];
                paramNi[j] = paramNi[i];
                paramNi[i] = tmp;
            }

            double sumN = paramNi[0] + paramNi[1] + paramNi[2] + paramNi[3] + paramNi[4] + paramNi[5];
            double significanceLevel = 0;
            if (sumN < 100)
            {
                significanceLevel = 0.05;
            }
            else
                significanceLevel = 0.01;
            _tasks[7] += "3. Пользуясь критерием Пирсона, при уровне значимости α= " + significanceLevel + ", установить, согласуется ли гипотеза о нормальном распределении генеральной совокупности с данными выборки объема n=";
            _tasks[7] += sumN + "\n";
            _tasks[7] += " Xi: " + paramXi[0] + "\t" + paramXi[1] + "\t" + paramXi[2] + "\t" + paramXi[3] + "\t" + paramXi[4] + "\t" + paramXi[5] + "\n";
            _tasks[7] += " Ni: "+ paramNi[0] + "\t" + paramNi[1] + "\t" + paramNi[2] + "\t" + paramNi[3] + "\t" + paramNi[4] + "\t" + paramNi[5] + "\n";

            _stringSolutions[7] = eighthSolution(paramXi, paramNi, reliability, significanceLevel);
      
        }
        private double reliabilityRand(double reliability)
        {
            if ((reliability * 100) >= 1 && (reliability * 100) < 5)
                return 0.99;
            if ((reliability * 100) >= 5 && (reliability * 100) < 10)
                return 0.95;
            if ((reliability * 100) >= 10 && (reliability * 100) < 15)
                return 0.9;
            if ((reliability * 100) >= 15 && (reliability * 100) < 20)
                return 0.85;
            if ((reliability * 100) >= 20 && (reliability * 100) < 25)
                return 0.8;
            if ((reliability * 100) >= 25 && (reliability * 100) < 30)
                return 0.75;
            if ((reliability * 100) >= 30)
                return 0.7;
            return 0.95;
        }
        private void generateNinthTask()
        {
            int[] int_params = new int[3];
            double[] double_params = new double[3];
            Random rand_generator = new Random();
            //Девятое задание
            _tasks[8] = "\n\n" + _versionNum + ".9. В больницу поступает в среднем ";
            int_params[0] = rand_generator.Next(1, 6) * 10;
            _tasks[8] += int_params[0] + "% больных с заболеванием А, ";
            int_params[1] = rand_generator.Next(1, 5) * 10;
            _tasks[8] += int_params[1] + "% с заболеванием В, ";
            int_params[2] = 100 - int_params[0] - int_params[1];
            _tasks[8] += int_params[2] + "% с заболеванием С.  Вероятность полного выздоровления для каждого заболевания соответственно " +
                "равны ";
            double_params[0] = rand_generator.Next(5, 9) * 0.1;
            double_params[1] = rand_generator.Next(5, 9) * 0.1;
            double_params[2] = rand_generator.Next(5, 9) * 0.1;
            _tasks[8] += double_params[0] + "; " + double_params[1] + "; " + +double_params[2] + ". Больной был выписан из больницы " +
                "здоровым. Найти вероятность того, что он страдал заболеванием А. ";

            _solutions[8] = ninthSolution(int_params[0], int_params[1], int_params[2], double_params[0], double_params[1],
                double_params[2]);
        }

        private void generateTenthTask()
        {
            int[] int_params = new int[2];
            double double_param;
            Random rand_generator = new Random();
            //Десятое задание
            _tasks[9] = "\n\n" + _versionNum + ".10. В семье ";
            int_params[0] = rand_generator.Next(4, 10);
            _tasks[9] += int_params[0] + " детей. Найти вероятность того, что среди них ";
            int_params[1] = rand_generator.Next(1, int_params[0]);
            _tasks[9] += int_params[1]; 
            if (int_params[1] == 1) _tasks[9] += " девочка. ";
            else if (int_params[1] > 1 && int_params[1] < 5) _tasks[9] += " девочки. ";
            else _tasks[9] += " девочек. ";
            _tasks[9] += "Вероятность рождения девочки равна ";
            double_param = rand_generator.Next(20, 60) * 0.01;
            _tasks[9] += double_param + ".";

            _solutions[9] = tenthSolution(int_params[0], int_params[1], double_param);
        }

        private void generateEleventhTask()
        {
            double[] double_params = new double[5];
            Random rand_generator = new Random();
            //Одиннадцатое задание
            _tasks[10] = "\n\n" + _versionNum + ".11. Случайная величина ξ имеет распределения вероятностей, представленное таблицей:"
                + "\nξ     | 0,1 | 0,2  | 0,3  | 0,4  | 0,5 |" + "\nР(х) | ";
            double_params[4] = 1;
            for (int i = 0; i < 4; ++i)
            {
                double_params[i] = rand_generator.Next(1, 26);
                double_params[i] -= double_params[i] % 5;
                double_params[i] *= 0.01;
                _tasks[10] += double_params[i] + " | ";
                double_params[4] -= double_params[i];
            }
            _tasks[10] += double_params[4] + " | " + "\nПостроить многоугольник распределения и найти функцию распределения F(x). ";

            _stringSolutions[10] = eleventhSolution(double_params[0], double_params[1], double_params[2], double_params[3],
                double_params[4]);
            _solutions[11] = twelfthSolution(double_params[0], double_params[1], double_params[2], double_params[3], double_params[4]);
        }

        private void generateTwelfthTask()
        {
            //Двенадцатое задание
            _tasks[11] = "\n\n" + _versionNum + ".12. Найти М(ξ), D(ξ), σ(ξ) случайной величины ξ примера 11.";
        }

        private void generateThirteenthTask()
        {
            int[] int_params = new int[3];
            Random rand_generator = new Random();
            //Тринадцатое задание
            _tasks[12] = "\n\n" + _versionNum + ".13. Задана плотность распределения непрерывной случайной величины:"
                    + "\n φ(х) = Ax^";
            int_params[0] = rand_generator.Next(2, 7);
            _tasks[12] += int_params[0] + ", ∀x ∈ (0;1]\n φ(х) = 0, ∀x ∉ (0;1]. \nНайти А и функцию распределения F(x).";
            _stringSolutions[12] = thirteenthSolution(int_params[0]);
        }

        private void generateFourteenthTask()
        {
            //Четырнадцатое задание
            _tasks[13] = "\n\n" + _versionNum + ".14.  ξ - непрерывная случайная величина примера 13. Найти М(ξ), D(ξ), σ(ξ) ";
        }

        private void generateFifteenthTask()
        {
            int[] int_params = new int[2];
            double double_param;
            Random rand_generator = new Random();

            //Пятнадцатое задание
            _tasks[14] = "\n\n" + _versionNum + ".15.  Вероятность наступления события А в каждом опыте равна ";
            double_param = rand_generator.Next(1, 91) * 0.01;
            _tasks[14] += double_param + ". Найти вероятность того, что событие А в ";
            int_params[0] = rand_generator.Next(200, 3200);
            int_params[0] -= int_params[0] % 100;
            int_params[1] = rand_generator.Next(100, 100 + (int)(0.4 * int_params[0]));
            _tasks[14] += int_params[0] + " опытах произойдет " + int_params[1];
            if (int_params[1] % 10 > 1 && int_params[1] % 10 < 5) _tasks[14] += " раза.";
            else _tasks[14] += " раз.";

            _solutions[14] = fifteenthSolution(double_param, int_params[0], int_params[1]);
        }

        private void generateSixsteenthTask()
        {
            double[] double_params = new double[4];
            Random rand_generator = new Random();
            //Шестнадцатое задание
            _tasks[15] = "\n\n" + _versionNum + ".16. ξ - нормально распределенная случайная величина с параметрами а = ";
            double_params[0] = rand_generator.Next(5, 51) * 0.1;
            _tasks[15] += double_params[0] + "; σ = ";
            double_params[1] = rand_generator.Next(2, 6) * 0.1;
            _tasks[15] += double_params[1] + ". Найти Р(|ξ-";
            double_params[2] = rand_generator.Next(3, 7) * 0.5;
            _tasks[15] += double_params[2] + "| < ";
            double_params[3] = rand_generator.Next(1, 6) * 0.1;
            _tasks[15] += double_params[3] + ").";

            _solutions[15] = sixteenthSolution(double_params[0], double_params[1], double_params[2], double_params[3]);
        }

        private void generateSeventeenthTask()
        {
            int[] int_params = new int[2];
            double double_param;
            Random rand_generator = new Random();
            //Семнадцатое задание
            _tasks[16] = "\n\n" + _versionNum + ".17. Вероятность появления события в каждом из ";
            int_params[0] = rand_generator.Next(4, 41) * 25;
            _tasks[16] += int_params[0] + " независимых испытаний постоянна и равна Р = ";
            double_param = rand_generator.Next(7, 9) * 0.1;
            _tasks[16] += double_param + ". Найти вероятность того, что событие появится не более ";
            int_params[1] = rand_generator.Next(int_params[0] / 2, 3 * int_params[0] / 4);
            if (int_params[1] % 10 == 1)
                _tasks[16] += int_params[1] + " раза.";
            else _tasks[16] += int_params[1] + " раз.";

            _solutions[16] = sevententhSolution(int_params[0], int_params[1], double_param);
        }

        private void generateEighteenthTask()
        {
            double[] double_params = new double[6];
            Random rand_generator = new Random();
            //Восемнадцатое задание
            _tasks[17] = "\n\n" + _versionNum + ".18. Дана таблица распределения вероятностей двумерной случайной величины (ξ,η)"
                + "\nξ \\ η |  -1 |  0  | 1\n0      | ";
            int zero_generated = 0; //Флаг, указывающий, был ли сгененрирован ноль (ноль нужно сгененрировать не более одного раза)
            double_params[0] = rand_generator.Next(0, 4) * 0.1;
            if (double_params[0] == 0) zero_generated = 1;

            double_params[1] = rand_generator.Next(zero_generated, 4) * 0.1;
            if (double_params[1] == 0) zero_generated = 1;
            int max_value = ((int)(10 * (1 - double_params[0] - double_params[1])) < 3) ? (int)(10 * (1 - double_params[0] -
                double_params[1])) + 1 : 4;

            double_params[2] = rand_generator.Next(zero_generated, max_value) * 0.1;
            if (double_params[2] == 0) zero_generated = 1;
            max_value = ((int)(10 * (1 - double_params[0] - double_params[1] - double_params[2])) < 3) ?
                (int)(10 * (1 - double_params[0] - double_params[1] - double_params[2])) + 1 : 4;

            double_params[3] = rand_generator.Next(zero_generated, max_value) * 0.1;
            if (double_params[3] == 0) zero_generated = 1;

            max_value = ((int)(10 * (1 - double_params[0] - double_params[1] - double_params[2] - double_params[3])) < 3) ?
               (int)(10 * (1 - double_params[0] - double_params[1] - double_params[2] - double_params[3])) + 1 : 4;
            double_params[4] = rand_generator.Next(zero_generated, max_value) * 0.1;
            if (double_params[4] == 0) zero_generated = 1;

            double_params[5] = 1;
            for (int i = 0; i < 5; ++i)
                double_params[5] -= double_params[i];

            _tasks[17] += double_params[0] + "| " + double_params[1] + " | " + double_params[2] + "\n1      |  "
                + double_params[3] + "| " + double_params[4] + " | " + double_params[5] + "\nНайти М(ξ), М(η), М(ξη), D(ξ), D(η), " +
                "D(ξη).";

            _solutions[17] = eighteenthSolution(double_params);
        }


//----------------------------------Решения заданий----------------------------------------------
        private List<double> firstSolution(int bearingNum, int defBearings, int takenBearings, int fitTakenBearings, double[] probabilities, int choice) // _solutions[0] = firstSol  (int_params[0]12, int_params[1]4, int_params[2]5,         int_params[3]2,     null, randC);
        {
            if(choice <= 0)
            {
 
                double C1 = C(bearingNum, takenBearings);
                double C2_C3 = C(defBearings, fitTakenBearings) * C((bearingNum- defBearings),(takenBearings- fitTakenBearings));
                double answer = C2_C3/C1;

                List<double> resultList = new List<double>();
                resultList.Add(answer);

                return resultList;
            }
            else
            {
                double ans = 0;
                double[] notprobabilities = new double[3];
                for (int i=0;i< probabilities.Length;i++)
                {
                    notprobabilities[i] = 1 - probabilities[i];
                }

                ans = probabilities[0] * probabilities[1] * notprobabilities[2] +
                    probabilities[0] * probabilities[2] * notprobabilities[1] + probabilities[1] * probabilities[2] * notprobabilities[0];
                List<double> resultList = new List<double>();
                resultList.Add(ans);

                return resultList;
            }
           
        }

        private List<double> secondSolution(int first, int second, int third,double[] possib,int choice)
        { ////                          int_params[0]20  int_params[1]6 int_params[2]4
            if (choice == 0)
            {
                double all = first + second + third;
                double[] worker = { first / all, second / all, third / all };
                double sum = 0;
                for(int i=0;i<possib.Length;i++)
                {
                    sum += possib[i] * worker[i];
                }
                double ans = (possib[2] * worker[2] )/ sum;

                List<double> resultList = new List<double>();
                resultList.Add(ans);
                return resultList;

            }
            else
            {
                /// В группе спортсменов 20 лыжников, 6 велосипедистов и 4 бегуна.Вероятности выполнить " +
                /// "квалификационную норму соответственно равны 0,9; 0,8; 0,75. Найти вероятность того, что выбранный  " +
                /// "наудачу спортсмен выполнит норму.
                double all = first + second + third;
                double[] worker = { first / all, second / all, third / all };
                double sum = 0;

                for (int i = 0; i < possib.Length; i++)
                {
                    sum += possib[i] * worker[i];
                }

                List<double> resultList = new List<double>();
                resultList.Add(sum);
                return resultList;
            }
            
        }

        private List<double> thirdSolution(int all, int byM,double possib)
        {                                     ///5        ///2        ///1/7
            List<double> resultList = new List<double>();
            double combination = C(all, byM);
            double powP = 1, powQ = 1;
            for(int i=0;i< byM; i++)
            {
                powP *= possib;
            }
            for(int i=0;i<(all- byM);i++)
            {
                powQ *= (1 - possib);
            }
            resultList.Add(combination*powP*powQ);

            return resultList;
        }

        private string fourthSolution(int[] param, double possib, int choice)
        {
            string resStr = "";
            if (choice == 0 )
            {
                /// На станциях отправления поездов находится 1000 автоматов для продажи билетов. Вероятность
                ///выхода из строя одного автомата в течение часа равна 0,005.Найти вероятность того, что в течение
                ///часа выйдут из строя: а) 5 автоматов; б) от 2 до 12 автоматов.Найти наиболее вероятное число
                ///вышедших из строя автоматов.
                ///
                double result = 0;
                if ((param[0]*possib)<10)
                {
                    result = (Math.Pow((param[0] * possib), param[1]) * Math.Exp(-((param[0] * possib)))) / factorial(param[1], 1);
                    resStr += "a) " + result +"\t";
                }else
                {
                    result = 1 / (Math.Sqrt(param[0] * possib * (1 - possib)));
                    double x = (param[1] - param[0] * possib) / (Math.Sqrt(param[0] * possib * (1 - possib)));
                    double f = phiSmallLaplass(x);
                    resStr += "а) ϕ(" + x + ")\t";
                    result *= f;
                    resStr += "" + result +"\t";
                }
                
                double result2 = 1;
                if ((param[0] * possib * (1 - possib)) >= 20 || param[2]>180 || param[3]>180)
                {
                    int k1 = param[2], k2 = param[3];
                    double x1 = (k1 - param[0] * possib) / (Math.Sqrt(param[0] * possib * (1 - possib)));
                    double x2 = (k2 - param[0] * possib) / (Math.Sqrt(param[0] * possib * (1 - possib)));
                    resStr += "\nб) Ф(" + x2 + ") - Ф(" + x1 + ") = \t";
                    result2 = resultMoivreLaplace(x1, x2);
                    resStr += "" + result2 +"\t";
                }
                else
                {
                    int k1 = param[2], k2 = param[3];
                    double sum = 0;
                    for (; k1 <= k2; k1++)
                    {
                        sum += C(k2, k1) * Math.Pow(possib, k1) * Math.Pow((1 - possib), k2 - k1);
                    }
                    result2 = sum;
                    resStr += "\n б) " + result2 +"\t";
                }

                resStr += "\n";
                ///Наивероятнейшее
                double[] result3 = { 0, 0 };
                double multNP = param[0] * possib;
                if (multNP % 1 == 0)
                {
                    result3[0] = multNP;
                }
                if ((multNP - (1 - possib)) % 1 == 0)
                {
                    result3[0] = multNP - (1 - possib);
                    result3[1] = multNP + possib;
                }
                else
                {
                    result3[0] = Math.Round(multNP - (1 - possib));
                }
                
                if (result3[1] != 0)
                {
                    resStr += result3[0] + " <= k0 <= " + result3[1];
                }
                else
                {
                    resStr += "k0 = " + result3[0];
                }

                
            }
            else
            {
                double result = 0;
                if ((param[0] * possib) < 10)
                {
                    result = (Math.Pow((param[0] * possib), param[1]) * Math.Exp(-((param[0] * possib)))) / factorial(param[1], 1);
                    resStr += "a) " + result + "\t";
                }
                else
                {
                    result = 1 / (Math.Sqrt(param[0] * possib * (1 - possib)));
                    double x = (param[1] - param[0] * possib) / (Math.Sqrt(param[0] * possib * (1 - possib)));
                    double f = phiSmallLaplass(x);
                    resStr += "а) ϕ(" + x + ")\t";
                    result *= f;
                    resStr += "" + result + "\t";
                }
                

                ////Интегральная теорема Лапласа
                double result2 = 1;
                if ((param[0] * possib * (1 - possib)) >= 20 || param[2] > 180 || param[3] > 180)
                {
                    int k1 = param[2], k2 = param[3];
                    double x1 = (k1 - param[0] * possib) / (Math.Sqrt(param[0] * possib * (1 - possib)));
                    double x2 = (k2 - param[0] * possib) / (Math.Sqrt(param[0] * possib * (1 - possib)));
                    resStr += "\nб) Ф(" + x2 + ") - Ф(" + x1 + ")=";
                    result2 = resultMoivreLaplace(x1, x2);
                    resStr += "" + result2 + "\t";
                }
                else
                {
                    int k1 = param[2], k2 = param[3];
                    double sum = 0;
                    for (; k1 <= k2; k1++)
                    {
                        sum += C(k2, k1) * Math.Pow(possib, k1) * Math.Pow((1 - possib), k2 - k1);
                    }
                    result2 = sum;
                    resStr += "\n б) " + result2 + "\t";
                }
                resStr += "\n";
                ///Наивероятнейшее
                double[] result3 = { 0, 0 };
                double multNP = param[0] * possib;
                if(multNP %1 == 0)
                {
                    result3[0] = multNP;
                }else
                {
                    if((multNP-(1-possib)) % 1 == 0)
                    {
                        result3[0] = multNP - (1 - possib);
                        result3[1] = multNP + possib;
                    }else
                    {
                        result3[0] = Math.Round(multNP - (1 - possib));
                    }
                }
                if (result3[1]!=0)
                {
                    resStr += result3[0] + " <= k0 <= " + result3[1];
                }
                else
                {
                    resStr += "k0 = " + result3[0];
                } 
            }

            return resStr;
        }

        private double resultMoivreLaplace(double x1, double x2)
        {
            double phi1 = 0, phi2 = 0;
            if (Math.Abs(x1) > 4)
                phi1 = 1;
            if (Math.Abs(x2) > 4)
                phi2 = 1;

            if (phi1 == 0 && phi2 == 0)
            {
                if (x1 < 0 && x2 < 0)
                {
                    return -Moivre_LaplacePhi(Math.Abs(x2)) + Moivre_LaplacePhi(Math.Abs(x1));
                }
                else
                {
                    if (x1 < 0)
                    {
                        return Moivre_LaplacePhi(Math.Abs(x2)) + Moivre_LaplacePhi(Math.Abs(x1));
                    }
                    else
                    {
                        if (x2 < 0)
                        {
                            return -Moivre_LaplacePhi(Math.Abs(x2)) - Moivre_LaplacePhi(Math.Abs(x1));
                        }
                        else
                        {
                            return Moivre_LaplacePhi(Math.Abs(x2)) - Moivre_LaplacePhi(Math.Abs(x1));
                        }
                    }
                }
            }
            else
            if (phi1 == 0)
            {
                if (x1 < 0 && x2 < 0)
                {
                    return -1 + Moivre_LaplacePhi(Math.Abs(x1));
                }
                else
                {
                    if (x1 < 0)
                    {
                        return 1 + Moivre_LaplacePhi(Math.Abs(x1));
                    }
                    else
                    {
                        if (x2 < 0)
                        {
                            return -1 - Moivre_LaplacePhi(Math.Abs(x1));
                        }
                        else
                        {
                            return 1 - Moivre_LaplacePhi(Math.Abs(x1));
                        }
                    }
                }
            }
            else
            if (phi2 == 0)
            {
                if (x1 < 0 && x2 < 0)
                {
                    return -Moivre_LaplacePhi(Math.Abs(x2)) + 1;
                }
                else
                {
                    if (x1 < 0)
                    {
                        return Moivre_LaplacePhi(Math.Abs(x2)) + 1;
                    }
                    else
                    {
                        if (x2 < 0)
                        {
                            return -Moivre_LaplacePhi(Math.Abs(x2)) - 1;
                        }
                        else
                        {
                            return Moivre_LaplacePhi(Math.Abs(x2)) - 1;
                        }
                    }
                }
            }
            else
            {
                if (x1 < 0)
                    return 2;
                else
                    if (x2 < 0)
                    return -2;
                else
                    return 0;
              
            }
        }

        private string fifthSolution(int details, double possib,int choice)
        {
            ///Студент купил 4 билета новогодней лотереи. Вероятность выигрыша по одному билету равна 0,6.
            ///Составить закон распределения, вычислить математическое ожидание и дисперсию числа выигрышей
            ///среди купленных билетов
            string resStr = "";
            if (choice==0)
            {
                int[] arrX = new int[details+1];
                for(int i=0;i<=details;i++)
                {
                    arrX[i] = i;
                }
                double[] arrP = new double[details + 1];
                for(int i=0;i<=details;i++)
                {
                    arrP[i] = C(details, i) * Math.Pow(possib, i) * Math.Pow((1 - possib), details - i);
                }
                double[] arrCharacteristicsOfARandomVariable = new double[3];
                arrCharacteristicsOfARandomVariable = characteristicsOfARandomVariable(arrX,arrP, details);

                 resStr = " Закон распределения \n X: ";
                for(int i=0;i<=details;i++)
                {
                    resStr += arrX[i] + "\t";
                }
                resStr += "\n P: ";
                for(int i=0;i<=details;i++)
                {
                    resStr += arrP[i] + "\t";
                }
                resStr += "\n Мат ожидание: " + arrCharacteristicsOfARandomVariable[0] + "\n Дисперсия " + arrCharacteristicsOfARandomVariable[1] + " \n Среднеквадратическое отклонение " + arrCharacteristicsOfARandomVariable[2] + "\n";
                
            }
            else
            {
                int[] arrX = new int[details + 1];
                for (int i = 0; i <= details; i++)
                {
                    arrX[i] = i;
                }
                double[] arrP = new double[details + 1];
                for (int i = 0; i <= details; i++)
                {
                    arrP[i] = C(details, i) * Math.Pow(possib, i) * Math.Pow((1 - possib), details - i);
                }
                double[] arrCharacteristicsOfARandomVariable = new double[3];
                arrCharacteristicsOfARandomVariable = characteristicsOfARandomVariable(arrX, arrP, details);

                resStr = " Закон распределения \n X: ";
                for (int i = 0; i <= details; i++)
                {
                    resStr += arrX[i] + "\t";
                }
                resStr += "\n P: ";
                for (int i = 0; i <= details; i++)
                {
                    resStr += arrP[i] + "\t";
                }
                resStr += "\n Мат ожидание: " + arrCharacteristicsOfARandomVariable[0] + "\n Дисперсия " + arrCharacteristicsOfARandomVariable[1] + "\n";
            }
            return resStr;
        }

        private string sixthSolution(int powerX, int endInterval, int coef , int choice)
        {
            string resStr = "";
            if(choice ==0)
            {
                //Коэффициент А
                double A = 1/(Math.Pow(endInterval,powerX+1)/powerX+1);
                resStr = "A = 1/ʃx^" + powerX + "dx, 0 < x < " + endInterval + ";\n" +
                    "A = "+ A;
                resStr += "\nF(x) = 0, при х ≤ 0 " +
                    "\nF(x) =" + A + "*( x ^ " + (powerX + 1) + "/(" + (powerX + 1) + ")), при 0 < x ≤ " + endInterval;
                resStr += "\nF(x) = 1, при х > " + endInterval;
                //Характеристики
                resStr += "\n M(X) = " + A * (Math.Pow(endInterval, powerX + 2) /( powerX + 2)) +"\n" ;
                double variance = (A * (Math.Pow(endInterval, powerX + 3) / (powerX + 3))) - Math.Pow(A * (Math.Pow(endInterval, powerX + 2) / (powerX + 2)), 2);
                resStr += "D(X) = M(X^2) - [M(X)]^2 = " + variance + "\n σ(Х) = " + Math.Sqrt(variance);
            }
            else
            {
                ///2Ax - A, 1 <x < y
                //Коэффициент А
                double integralEndInterval = (coef * Math.Pow(endInterval, powerX + 1) / powerX + 1) - endInterval;
                double integralBeginInterval = (coef / (powerX + 1)) - 1;
                double A = 1 / (integralEndInterval - integralBeginInterval);
                resStr = "A = 1/ʃ" + coef + "x^" + powerX + " - 1 dx, 1 < x < " + endInterval + ";\n" +
                    "A = " + A;
                resStr += "\nF(x) = 0, при х ≤ 1 \n " +
                    "\nF(x) =" + A + "*("+coef+" x ^ " + (powerX + 1) + "/(" + powerX + 1 + ")) - x, при 1 < x ≤ " + endInterval;
                resStr += "\nF(x) = 1, при х > " + endInterval;

                //Характеристики
                integralEndInterval = (coef * Math.Pow(endInterval, powerX + 2) / powerX + 2) - (Math.Pow(endInterval, 2) / 2);
                integralBeginInterval = (coef / (powerX + 2)) - 0.5;
                resStr += "\n M(X) = " + (integralEndInterval - integralBeginInterval) + "\n";

                double variance = ((coef * Math.Pow(endInterval, powerX + 3) / (powerX + 3)) - (Math.Pow(endInterval, 3) / 3) - (coef / (powerX + 3)) - 1/3) - Math.Pow((integralEndInterval - integralBeginInterval),2);
                resStr += "D(X) = M(X^2) - [M(X)]^2 = " + variance + "\n σ(Х) = " + Math.Sqrt(variance);
            }
            return resStr;
        }
 

        private List<double> seventhSolution(int[] param)
        {
            List<double> resultList = new List<double>();
             //P(|X-m|<б)=Ф(beta-m/sigma)-Ф(alfa-m/sigma)
             double x1 = ((param[3] - param[0]) / param[1]);
             double x2 = ((param[2] - param[0]) / param[1]);
             double result = resultMoivreLaplace(x1, x2);
             resultList.Add(result);
            //P(|X-m|<б)=2Ф(б/sigma)
            double x = (param[4] / param[1]);
            result = 2.0 * Moivre_LaplacePhi(x);
            resultList.Add(result);
            return resultList;
        }

        private string eighthSolution(int[] arrX, int[] arrN, double reliability,double significanceLevel)
        {
            
            string resStr = "";
            int indexMaxN = 0,max = arrN[0],C = 0;

            for(int i=0;i<arrN.Length;i++)
            {
                if(arrN[i]>max)
                {
                    indexMaxN = i;
                    max = arrN[i];
                    C = arrX[i];
                }
            }
            int[] u = new int[6];
            for(int i=0,j=1;i<u.Length;i++)
            {
                if (i <= indexMaxN)
                    u[i] = -indexMaxN + i;
                else
                {
                    u[i] = j;
                    j++;
                }
            }

            int[] mult_nu = new int[6];
            int[] mult_nuu = new int[6];
            int[] mult_n_sumu1pow2 = new int[6];
            int sumNegative = 0, sumPossitive = 0, sum_multnuu =0 , sum_mult_n_sumu1pow2 = 0,sumN = 0;
            for(int i=0;i<mult_nu.Length;i++)
            {
                mult_nu[i] = arrN[i] * u[i];
                mult_nuu[i] = arrN[i] * u[i] * u[i];
                mult_n_sumu1pow2[i] = arrN[i] * ((u[i] + 1) * (u[i] + 1));
                //Сумма столбцов
                if (mult_nu[i] < 0)
                    sumNegative += mult_nu[i];
                else
                    sumPossitive += mult_nu[i];
                sum_multnuu += mult_nuu[i];
                sum_mult_n_sumu1pow2 += mult_n_sumu1pow2[i];
                sumN += arrN[i];
                //----------------------
            }
            int diff = sumPossitive - Math.Abs(sumNegative);
            if(checkCalc8Mission(arrN,u, sum_mult_n_sumu1pow2)==false)
            {
                resStr = " Error calculation! ";
                return resStr;
            }
            double M1 = (double)(diff/sumN), M2 = (double)(sum_multnuu/sumN);
            double h = Math.Abs(arrX[0]-arrX[1]);

            double sampleAverage = M1*h+C;
            double sampleVariance = (M2 - M1 * M1) * h * h;
            double correctedVariance = (sumN / (sumN - 1)) * sampleVariance;
            double correctedStandartDeviation = Math.Sqrt(correctedVariance);
            double t = readExecelStuard(sumN, reliability,false);

            double confidencIntervalLeft = sampleAverage - (t * correctedStandartDeviation / Math.Sqrt(sumN));
            double confidencIntervalRight = sampleAverage + (t * correctedStandartDeviation / Math.Sqrt(sumN));

            for(int i=0;i<arrX.Length;i++)
            {
                if(i==0)
                {
                    resStr += "Xi\tNi\tUi\tNiUi\tNiUi^2\tNi(Ui+1)^2\n";
                    resStr += arrX[i] + "\t" + arrN[i] + "\t" + u[i] + "\t" + mult_nu[i] + "\t" + mult_nuu[i] + "\t" + mult_n_sumu1pow2[i] + "\n";
                }
                else
                    resStr += arrX[i] + "\t" + arrN[i] + "\t" + u[i] + "\t" + mult_nu[i] + "\t" + mult_nuu[i] + "\t" + mult_n_sumu1pow2[i] + "\n";
            }
            resStr += "Summa N = " + sumN + "\t Summa NiUi = " + diff + "\t Summa NiUiUi = " + sum_multnuu + "\t Summa Ni(Ui+1)^2 = " + sum_mult_n_sumu1pow2 + "\n";
            resStr += " M1 = " + M1 + "\t M2 = " + M2 + "\t h = " + h + "\n";
            resStr += "1) Выборочная средняя = " + sampleAverage + "\t Выборочная дисперсия = " + sampleVariance + " Исправленная дисперсия = " + correctedVariance + "\n";
            resStr += " Исправленная среднеквадратическое отклонение = " + correctedStandartDeviation +"\n";
            resStr += "2) При y = " + reliability + " N = " + sumN + " , t = " + t + "\n";
            resStr += " Доверительный интервал : " + confidencIntervalLeft + " < a < " + confidencIntervalRight + "\n";

            resStr += "Выдвинем гипотезу H0: распределение генеральной совокупности X подчинено нормальному закону с параметрами x_ =" + sampleAverage + "и σ = " + (Math.Round(correctedStandartDeviation,5)) + ". Проверим эту гипотезу по критерию Пирсона при уровне значимости α = " + significanceLevel + "\n";
            
            double[] U = new double[6];
            double[] phiSmallFromU = new double[6];
            double[] N0i = new double[6];
            double[] difNiN0i_pow2_divNi0 = new double[6];
            double observerValue = 0;
            for(int i=0;i<arrX.Length;i++)
            {
                U[i] = Math.Round((arrX[i] - sampleAverage) / correctedStandartDeviation,5);
                phiSmallFromU[i] = Math.Round(phiSmallLaplass(U[i]),5);
                N0i[i] = Math.Round(((sumN*h/ correctedStandartDeviation)* phiSmallFromU[i]), 5);
                difNiN0i_pow2_divNi0[i] = Math.Round((((arrN[i]-N0i[i])*(arrN[i]-N0i[i]))/N0i[i]), 5);
                observerValue += difNiN0i_pow2_divNi0[i];
                if(i==0)
                {
                    resStr += "Xi\t\tUi\\ttPhi(Ui)\t\tN0i\t\t(Ni-N0i)^2/Ni0\n";
                    resStr += arrX[i] + "\t" + U[i] + "\t" + phiSmallFromU[i] + "\t" + N0i[i] + "\t" + difNiN0i_pow2_divNi0[i] + "\n";
                }else
                    resStr += arrX[i] + "\t" + U[i] + "\t" + phiSmallFromU[i] + "\t" + N0i[i] + "\t" + difNiN0i_pow2_divNi0[i] + "\n";
            }
            resStr += "Наблюдаемое значение = " + observerValue + "\n";
            double controlValue = readExecelStuard((arrX.Length-3), (significanceLevel),true);
            if (observerValue < controlValue)
                resStr += " Контрольное значение = " + controlValue + " \n Набл меньше Контр => нулевую гипотезу о нормальном распределении можно принять при данном уровне значимости\n";
            else
                resStr += " Контрольное значение = " + controlValue + " \n Набл больше Контр => гипотезу о нормальном распределении генеральной совокупности отвергаем.\n";
 
            return resStr;
        }
        private bool checkCalc8Mission(int[] arrN,int[] arrU,int sumLastColumnn)
        {
            int sum = 0, sum1 = 0, sumN = 0;
            for(int i=0;i<arrN.Length;i++)
            {
                sum += arrN[i] * arrU[i] * arrU[i];
                sum1 += arrN[i] * arrU[i];
                sumN += arrN[i];
            }
            sum1 *= 2;
            sum = sum + sum1 + sumN;
            return sum == sumLastColumnn ? true : false;
        }
        private double readExecelStuard(int n,double reliability,bool HI)
        {
           Microsoft.Office.Interop.Excel.Application ObjExcel = new Microsoft.Office.Interop.Excel.Application();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "\\Stuard.xlsx");
            if (path != null)
                path = Directory.GetCurrentDirectory() + @"\Stuard.xlsx";
            else
                return 0;
            //Открываем книгу.                                                                                                                                                        
            Microsoft.Office.Interop.Excel.Workbook ObjWorkBook = ObjExcel.Workbooks.Open(path, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

            //Выбираем таблицу(лист).
            Microsoft.Office.Interop.Excel.Worksheet ObjWorkSheet;
            ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[1];

            // Указываем номер столбца (таблицы Excel) из которого будут считываться данные.
            int column = 1;
            reliability = (int)(reliability *100);
            if(HI == false)
            {
                switch (reliability)
                {
                    case 99:
                        column = 1;
                        break;
                    case 95:
                        column = 2;
                        break;
                    case 90:
                        column = 3;
                        break;
                    case 85:
                        column = 4;
                        break;
                    case 80:
                        column = 5;
                        break;
                    case 75:
                        column = 6;
                        break;
                    case 70:
                        column = 7;
                        break;
                    default:
                        break;
                }
            }else
            {
                switch (reliability)
                {
                    case 1:
                        column = 9;
                        break;
                    case 3:
                        column = 10;
                        break;
                    case 5:
                        column = 11;
                        break;
                    case 95:
                        column = 12;
                        break;
                    case 98:
                        column = 13;
                        break;
                    case 99:
                        column = 14;
                        break;
                    default:
                        break;
                }
            }
            var cellValue =0.0;
           if (HI==false)
            {
                if (n - 1 == 0)
                    n = 2;
                if (n - 1 == 1)
                    n = 2;
                if (n > 150)
                    n = 149;
                cellValue = (ObjWorkSheet.Cells[(n - 1), column] as Microsoft.Office.Interop.Excel.Range).Value;
            }
            else
            {
                if (n == 0)
                    n = 2;
                if (n == 1)
                    n = 2;
                if (n > 30)
                    n = 31;
                cellValue = (ObjWorkSheet.Cells[n, column] as Microsoft.Office.Interop.Excel.Range).Value;
            }

            
            /* Range usedColumn = ObjWorkSheet.UsedRange.Columns[numCol];
             System.Array myvalues = (System.Array)usedColumn.Cells.Value2;
             string[] strArray = myvalues.OfType<object>().Select(o => o.ToString()).ToArray();*/

            // Выходим из программы Excel.
            ObjExcel.Quit();
            return cellValue;
        }

        private List<double> ninthSolution(int firstDiseasePercent, int secondDiseasePercent, int thirdDiseasePercent,
            double firstDiseaseProb, double secondDiseaseProb, double thirdDiseaseProb)
        {
            double result = (firstDiseasePercent / 100.0 * firstDiseaseProb) / (firstDiseaseProb * (firstDiseasePercent/100.0) +
                secondDiseaseProb * (secondDiseasePercent / 100.0) + thirdDiseaseProb * (thirdDiseasePercent/100.0));
            List<double> resultList = new List<double>();
            resultList.Add(result);
            return resultList;
        }

        private List<double> tenthSolution(int childrenNum, int girlsNum, double girlsBirthProb)
        {
            double result = C(childrenNum, girlsNum) * Math.Pow(girlsBirthProb, girlsNum) *
                Math.Pow(1 - girlsBirthProb, childrenNum - girlsNum);
            List<double> resultList = new List<double>();
            resultList.Add(result);
            return resultList;
        }

        private string eleventhSolution(double prob1, double prob2, double prob3, double prob4, double prob5)
        {
            string result = "F(x < 0,1) = 0" +
                "\nF(x < 0,2) = " + prob1.ToString() +
                "\nF(x < 0,3) = " + (prob1 + prob2).ToString() +
                "\nF(x < 0,4) = " + (prob1 + prob2 + prob3).ToString() +
                "\nF(x < 0,5) = " + (prob1 + prob2 + prob3 + prob4).ToString() +
                "\nF(x ≥ 0,5) = " + (prob1 + prob2 + prob3 + prob4 + prob5).ToString();

            List<double> xList = new List<double>();
            for (double i = 0.1; i <= 0.5; i += 0.1)
                xList.Add(i);
            List<double> yList = new List<double>();
            yList.Add(prob1);
            yList.Add(prob2);
            yList.Add(prob3);
            yList.Add(prob4);
            yList.Add(prob5);

            return result;
        }

        private List<double> twelfthSolution(double prob1, double prob2, double prob3, double prob4, double prob5)
        {
            double result = 0.1 * prob1 + 0.2 * prob2 + 0.3 * prob3 + 0.4 * prob4 + 0.5 * prob5;
            List<double> resultList = new List<double>();
            resultList.Add(result);
            result = 0.01 * prob1 + 0.04 * prob2 + 0.09 * prob3 + 0.16 * prob4 + 0.25 * prob5 - Math.Pow(result, 2);
            resultList.Add(result);
            result = Math.Sqrt(result);
            resultList.Add(result);

            return resultList;
        }

        private string thirteenthSolution(int power)
        {
            //Коэффициент А
            double coef = power + 1;

            string result = "A = " + coef.ToString() + "\nF(x) = 0, при х ≤ 0" +
                "\nF(x) = x ^ " + (power + 1).ToString() + ", при 0 < x ≤ 1" +
            "\nF(x) = 1, при х > 1";

            _solutions[13] = fourteenthSolution(power, coef, 0, 1);

            return result;
        }

        private List<double> fourteenthSolution(int power, double coef, int lowLimit, int highLimit)
        {
            //Мат.ожидание
            double result = coef / (power + 2);
            List<double> resultList = new List<double>();
            resultList.Add(result);
            //Дисперсия
            result = coef / (power + 3) - Math.Pow(result, 2);
            resultList.Add(result);
            //Кадратичное отклонение
            result = Math.Sqrt(result);
            resultList.Add(result);

            return resultList;
        }

        private List<double> fifteenthSolution(double prob, int totalExp, int eventExp)
        {
            double result = 1 / Math.Sqrt(prob * (1 - prob) * totalExp) *
                phiSmallLaplass((eventExp - prob * totalExp) / Math.Sqrt(prob * (1 - prob) * totalExp));
            List<double> resultList = new List<double>();
            resultList.Add(result);

            return resultList;
        }

        private List<double> sixteenthSolution(double a, double sigma, double deviation, double range)
        {
            double result = Moivre_LaplacePhi((range + deviation - a) / Math.Sqrt(sigma)) - Moivre_LaplacePhi((deviation - range - a) / Math.Sqrt(sigma));
            List<double> resultList = new List<double>();
            resultList.Add(result);

            return resultList;
        }

        private List<double> sevententhSolution(int totalExp, int requiredExp, double prob)
        {
            double result = Moivre_LaplacePhi((requiredExp - totalExp * prob) / Math.Sqrt(totalExp * prob * (1 - prob))) -
                Moivre_LaplacePhi((1 - totalExp * prob) / Math.Sqrt(totalExp * prob * (1 - prob)));

            List<double> resultList = new List<double>();
            resultList.Add(result);

            return resultList;
        }

        private List<double> eighteenthSolution(double[] tableValues)
        {
            //М(ξ)
            double expValE = 0;
            for (int i = 3; i < tableValues.Length; ++i)
                expValE += tableValues[i];
            List<double> resultList = new List<double>();
            resultList.Add(expValE);

            //M(η)
            double mathExpN = -1 * (tableValues[0] + tableValues[3]) + tableValues[2] + tableValues[5];
            resultList.Add(mathExpN);

            //M(ξη)
            double mathExpEN = -1 * tableValues[3] + tableValues[5];
            resultList.Add(mathExpEN);

            //D(ξ)
            double result = 0;
            for (int i = 3; i < tableValues.Length; ++i)
                result += tableValues[i];
            result -= Math.Pow(expValE, 2);
            resultList.Add(result);

            //D(η)
            result = tableValues[0] + tableValues[3] + tableValues[2] + tableValues[5] - Math.Pow(mathExpN, 2);
            resultList.Add(result);

            //D(ξη)
            result = tableValues[3] + tableValues[5] - Math.Pow(mathExpEN, 2);
            resultList.Add(result);

            return resultList;
        }

//-------------------------------Вспомогательные методы--------------------------------------------
        //Количество сочетаний
        private double C(int n, int m)
        {
            if (n == 0 || n == m || m == 0) return 1;
            if (n < 200 && m < 200 && _binomialCoefs[n][m] != 0) return _binomialCoefs[n][m];

            return _binomialCoefs[n][m] = C(n - 1, m - 1) + C(n - 1, m);
        }
        private int factorial(int n, int count)
        {
            if (n == 0)
                return count;
            else
            {
                int fact = count * n;
                return factorial(n - 1, fact);
            }
        }

        private double[] characteristicsOfARandomVariable(int[] arrX, double[] arrP,int length)
        {
            double[] charOfARandomVar = new double[3];
            double mathematicalExpectation = 0;
            for (int i = 0; i <= length; i++)
            {
                mathematicalExpectation += arrX[i] * arrP[i];
            }
            double variance = 0;
            double mathematicalExpectationSquareX = 0;
            for (int i = 0; i <= length; i++)
            {
                mathematicalExpectationSquareX += arrX[i] * arrX[i] * arrP[i];
            }
            variance = mathematicalExpectationSquareX - (mathematicalExpectation * mathematicalExpectation);
            double standardDeviation = Math.Sqrt(variance);
            charOfARandomVar[0] = mathematicalExpectation;
            charOfARandomVar[1] = variance;
            charOfARandomVar[2] = standardDeviation;
            return charOfARandomVar;
        }

        //Настройка графика
        private void setChart(ref Chart chart, List<double> xList, List<double> yList)
        { 
            
        }

        private double phiSmallLaplass(double arg)
        {
            return Math.Exp(-Math.Pow(arg, 2) / 2) / Math.Sqrt(2 * Math.PI);
        }

        //Функция Лапласа
        private double Moivre_LaplacePhi(double arg)
        {
            return 1 / Math.Sqrt(2 * Math.PI) * integral(func, 0, arg);
        }

        //Подынтегральная функция из функции Лапласа
        private double func(double x)
        {
            return Math.Exp(-Math.Pow(x, 2) / 2);
        }

        //Определенный интеграл, значение которого вычисляется методом Симпсона
        private double integral(Func<double, double> integrand, double lowLimit, double highLimit) 
        {
            double n = 100;//Количество отрезков, на которые разбивается [a,b]
            double h; //Шаг
            List<double> x = new List<double>();
            double previous_approx, current_approx;
            const double EPS = 1e-6;

            h = (highLimit - lowLimit) / n;
            x.Add(lowLimit);
            for (int i = 1; i < n; i++)
            {
                x.Add(lowLimit + i * h);
            }
            x.Add(highLimit);
            current_approx = S(integrand, x, h);
            do
            {
                n = n * 2;//Удваиваем количество отрезков разбиения
                h = (highLimit - lowLimit) / n;

                x.Clear();
                x.Add(lowLimit);
                for (int i = 1; i < n; i++)
                {
                    x.Add(lowLimit + i * h);
                }
                x.Add(highLimit);

                previous_approx = current_approx;
                current_approx = S(integrand, x, h);//Применяем формулу Симпсона        
            } while (Math.Abs(previous_approx - current_approx) >= EPS);//Сравниваем с точностью

            return previous_approx;
        }

        //Формула Симпсона
        private double S(Func<double, double> f, List<double> x, double h)
        {
            double evenSum = 0, oddSum = 0;
            for (int i = 2; i < x.Count - 1; i += 2)//Считаем сумму значений подынтегральной функции в узлах с четными индексами
                evenSum += f(x.ElementAt(i));

            for (int i = 1; i < x.Count - 1; i += 2)//С нечетными индексами
                oddSum += f(x.ElementAt(i));

            return (h / 3) * (f(x.ElementAt(0)) + f(x.ElementAt(x.Count - 1)) + 2 * evenSum + 4 * oddSum);
        }
    }
}
