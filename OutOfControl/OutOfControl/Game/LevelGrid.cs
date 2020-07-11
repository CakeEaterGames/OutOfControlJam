using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfControl
{
    class LevelGrid
    {
        public int W = 6;
        public int H = 6;

        List<List<Entity>> EntitiesGrid = new List<List<Entity>>();

        public List<Entity> Entities = new List<Entity>();

        public LevelGrid(int w = 6, int h= 6)
        {
            W = w;
            H = h;
            for (int i=0;i<w;i++)
            {
                EntitiesGrid.Add(new List<Entity>());
                for (int j = 0; j < h; j++)
                {
                    EntitiesGrid[i].Add(null);
                }
            }
        }

        public void SetEntity(Entity e)
        {
            if (isInGrid(e.pX, e.pY) && EntitiesGrid[e.pX][e.pY]==null)
                EntitiesGrid[e.pX][e.pY] = e;
        }

        public void MoveEntity(Entity e, int newX, int newY)
        {
            if (isInGrid(newX,newY) && GetEntity(newX,newX) == null)
            {
                RemoveEntity(e);
                e.pX = newX;
                e.pY = newY;
                SetEntity(e);
            }
        }

        public void RemoveEntity(Entity e)
        {
            EntitiesGrid[e.pX][e.pY]=null;
        }


        public Entity GetEntity(int x, int y)
        {
            if (isInGrid(x, y))
                return EntitiesGrid[x][y];
            else
                return null;
        }
        public enum direction
        {
            up, down, left, right
        }

        public Entity LookfromEntity(Entity e, direction d)
        {
            int dx, dy, cx, cy;
            dx = 0;
            dy = 0;

            if (d == direction.down)    dy = 1;
            if (d == direction.up)      dy = -1;
            if (d == direction.left)    dx = 1;
            if (d == direction.right)   dx = -1;

            cx = e.pX;
            cy = e.pY;

            cx += dx;
            cy += dy;

            while (isInGrid(cx, cy))
            {
                var n = GetEntity(cx, cy);
                if (n != null)
                {
                    return n;
                }
                cx += dx;
                cy += dy;
            }

            return null;
        }

        public bool isInGrid(int x, int y)
        {
            return !(x < 0 || x >= W || y < 0 || y >= H);
        }


    }
}


 