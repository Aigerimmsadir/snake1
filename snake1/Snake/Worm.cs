using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    [Serializable]
    public class Worm
    {

        public char sign = '*';

        public List<Point> body = new List<Point>();
        public bool isAlive = true;
        public Worm()
        {
            body.Add(new Point(10, 10));

        }
        public void Draw()
        {
            for (int i = 0; i < body.Count; ++i)
            {
                Console.SetCursorPosition(body[i].x, body[i].y);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(sign);

            }
        }
        public void Clear()
        {
            for (int i = 0; i < body.Count; ++i)
            {
                Console.SetCursorPosition(body[i].x, body[i].y);

                
                Console.Write(' ');

            }
        }
        public void Move(int dx, int dy)
        {
            Clear();
            if (body[0].x + dx < 0) return;
            if (body[0].y + dy < 0) return;
            if (body[0].x + dx > 40) return;
            if (body[0].y + dy > 40) return;

            for (int i = body.Count - 1; i > 0;--i)
            {
                body[i].x = body[i - 1].x;
                body[i].y = body[i - 1].y;
            }

            body[0].x = body[0].x + dx;
            body[0].y = body[0].y + dy;


        }

        public bool CanEat(Food food)
        {
            if (body[0].Equals(food.location))
            {
                body.Add(food.location);
                return true;
            }
            return false;
        }
        public bool IsDead(Wall wall)
        {
            for (int i = 0; i < wall.bricks.Count; i++)
            {
                if (body[0].Equals(wall.bricks[i]))
                    return true;
            }
            for (int i = 1; i < this.body.Count; i++)
            {
                if (body[0].Equals(this.body[i]))
                    return true;
            }


            return false;
        }


    }
}