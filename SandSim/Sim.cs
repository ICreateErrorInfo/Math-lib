using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SandSim
{
    public class Sim
    {
        public Particle[,] array;
        public int scale;

        public Sim(int scale)
        {
            this.scale = scale;
            array = new Particle[scale,scale];

            Particle p = new Particle();
            p.id = "empty";
            p.color = System.Drawing.Color.Black;

            for (int y = array.GetLength(0) - 1; y >= 0; y--)
            {
                for (int x = 0; x < array.GetLength(1); x++)
                {
                    array[y, x] = p;
                }
            }
        }

        public void UpdateFrame()
        {
            for (int y = 0; y < array.GetLength(0); y++)
            {
                for (int x = 0; x < array.GetLength(1); x++)
                {
                    string partID = array[x,y].id;

                    switch (partID)
                    {
                        case "empty":
                            break;
                        case "sand":
                            updateSand(x,y);
                            break;
                        case "water":
                            updateWater(x, y);
                            break;
                    }
                }
            }
        }

        private void updateSand(int x, int y)
        {
            Particle p = new Particle();
            p.id = "empty";
            p.color = System.Drawing.Color.Black;

            Particle s = array[x,y];

            if (y - 1 >= 0  && array[x,y - 1].id == "empty")
            {
                array[x, y] = p;
                array[x,y - 1] = s;
                return;
            }
            else if(y - 1 >= 0 && x - 1 >= 0 && array[x-1,y-1].id == "empty")
            {
                array[x, y] = p;
                array[x - 1, y - 1] = s;
                return;
            }
            else if(y - 1 >= 0 && x + 1 <= scale - 1 &&  array[x + 1,y - 1].id == "empty")
            {
                array[x, y] = p;
                array[x + 1, y - 1] = s;
                return;
            }
            else
            {
                return;
            }
        }
        private void updateWater(int x, int y)
        {
            Particle p = new Particle();
            p.id = "empty";
            p.color = System.Drawing.Color.Black;

            Particle s = array[x, y];

            if (y - 1 >= 0 && array[x, y - 1].id == "empty")
            {
                array[x, y] = p;
                array[x, y - 1] = s;
                return;
            }
            else if (y - 1 >= 0 && x - 1 >= 0 && array[x - 1, y - 1].id == "empty")
            {
                array[x, y] = p;
                array[x - 1, y - 1] = s;
                return;
            }
            else if (y - 1 >= 0 && x + 1 <= scale - 1 && array[x + 1, y - 1].id == "empty")
            {
                array[x, y] = p;
                array[x + 1, y - 1] = s;
                return;
            }
            else if(x - 1 >= 0 && array[x - 1, y].id == "empty")
            {
                array[x, y] = p;
                array[x - 1, y] = s;
                return;
            }
            else if(x + 1 <= scale - 1 && array[x + 1, y].id == "empty")
            {
                array[x, y] = p;
                array[x + 1, y] = s;
                return;
            }
        }
    }
}
