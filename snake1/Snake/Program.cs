using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;


namespace Snake
{
    [Serializable]
    public class System
    {
        public Wall wall;
        public Worm worm;

        public System(Wall wall, Worm worm)
        {
            this.wall = wall;
            this.worm = worm;


        }
    }
    public class Global
    {
        public static int level = 1;
        public static int points = 0;

    }
    class Program
    {


        static void Serialize(System system)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (var fStream = new FileStream(@"C:\Users\Lenovo\Desktop\Новая папка\dat.dat", FileMode.Create, FileAccess.Write))
            {
                formatter.Serialize(fStream, system);
            }
        }
        static System Deserialize()
        {
            using (var fStream = File.OpenRead(@"C:\Users\Lenovo\Desktop\Новая папка\dat.dat"))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                System newsystem = (System)formatter.Deserialize(fStream);
                return newsystem;
            }

        }


        static void Keys(Wall wall, Worm worm, Food food)
        {
            System system = new System(wall, worm);


            while (worm.isAlive)
            {

                Console.Clear();
                worm.Draw();


                food.Draw();Console.ForegroundColor = ConsoleColor.Yellow;
                wall.Draw();
                Console.WriteLine(" Your points:");
                Console.WriteLine(Global.points);
                Console.WriteLine(" Your level:");
                Console.WriteLine(Global.level);
                ConsoleKeyInfo pressedKey = Console.ReadKey();

                switch (pressedKey.Key)
                {
                    case ConsoleKey.UpArrow:

                        worm.Move(0, -1);
                        break;
                    case ConsoleKey.DownArrow:

                        worm.Move(0, 1);
                        break;
                    case ConsoleKey.LeftArrow:

                        worm.Move(-1, 0);
                        break;
                    case ConsoleKey.RightArrow:

                        worm.Move(1, 0);
                        break;
                    case ConsoleKey.Escape:
                        Serialize(system);
                        worm.isAlive = false;
                        break;
                    case ConsoleKey.Backspace:
                        Console.Clear();
                        Console.WriteLine(Global.points);
                        break;
                    case ConsoleKey.Spacebar:
                        Serialize(system);

                        break;
                    case ConsoleKey.F2:
                        Console.Clear();
                        System system2 = Deserialize();
                        food.WhereisFood(system2.wall, system2.worm);
                        Keys(system.wall, system.worm, food);
                        break;

                }

                if (worm.IsDead(wall))
                    worm.isAlive = false;
                if (worm.body.Count > 3)
                {
                    Global.level++;
                    Global.points = +50;
                    Worm newworm = new Worm();
                    Console.Clear();
                    Wall newwall = new Wall(Global.level);
                    newwall.Draw();
                    Food newfood = new Food();
                    newfood.WhereisFood(newwall, newworm);

                    worm.isAlive = true;
                    Keys(newwall, newworm, newfood);
                }
                if (worm.CanEat(food))
                {
                    food = new Food();
                    food.WhereisFood(wall, worm);
                    Global.points += 10;
                }

                Serialize(system);


                if (worm.IsDead(wall))
                {
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine("GAME OVER");
                    Console.WriteLine("Your points:");
                    Console.WriteLine(Global.points);
                    Console.WriteLine("max level:");
                    Console.WriteLine(Global.level);
                }
            }
        }

        static void Main(string[] args)
        {


            Console.SetWindowSize(40, 50);
            Console.SetBufferSize(40, 50);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Continue - 1");
            Console.WriteLine("New game - 2");
            Console.WriteLine("Pause - space");
            string option = Console.ReadLine();
            Food food1 = new Food();
            Console.Clear();
            if (option == "1")
            {
                System system = Deserialize();
                food1.WhereisFood(system.wall, system.worm);
                Keys(system.wall, system.worm, food1);
                if (!system.worm.isAlive)
                {
                    Console.WriteLine("Start new game?");
                    Console.WriteLine("2-yes");
                    string option1 = Console.ReadLine();
                    if (option1 == "2")
                    {
                        Console.Clear();
                        Console.SetWindowSize(40, 40);
                        Worm worm = new Worm();
                        Console.Clear();
                        Global.level = 1;
                        Wall wall = new Wall(Global.level);
                        wall.Draw();
                        Food food = new Food();
                        food.WhereisFood(wall, worm);

                        ///  System system = new System(wall, worm);
                        Keys(wall, worm, food);

                    }

                }
            }
            else
            {
           
                Console.SetWindowSize(40, 40);
                Worm worm = new Worm();

                Wall wall = new Wall(Global.level);
                wall.Draw();
                Food food = new Food();
                food.WhereisFood(wall, worm);

                ///  System system = new System(wall, worm);
                Keys(wall, worm, food);

            }

        }


    }
}
    
