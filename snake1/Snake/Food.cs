using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{[Serializable]
    public class Food
    {
        public Point location;
        public char sign = '♠';
        public void WhereisFood(Wall wall,Worm worm)
        {
            location = new Point(new Random().Next() % 30, new Random().Next() % 30);
            for (int i = 0; i < wall.bricks.Count; i++)
            {

                if (location.Equals(wall.bricks[i]))
                {
                    WhereisFood(wall,worm);
                }
            }
            for(int i = 0; i < worm.body.Count; i++)
            {
                if (location.Equals(worm.body[i]))
                {
                    WhereisFood(wall, worm);
                }
            }

        }
        public void Draw()
        {
            Console.SetCursorPosition(location.x, location.y);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(sign);
        }
    }
}
