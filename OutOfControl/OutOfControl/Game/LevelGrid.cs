using Microsoft.Xna.Framework;
using MonoCake;
using MonoCake.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pellicalo
{
    public class LevelGrid : BasicObject
    {
        public int gridW = 6;
        public int gridH = 6;
        public int cellH = 36;
        public int cellW = 36;

        public double offsetX;
        public double offsetY;

        public List<List<Entity>> EntitiesGrid = new List<List<Entity>>();

        public List<Entity> Entities = new List<Entity>();

        
        public void DrawGrid()
        {
            offsetX = (gridW / 2.0) * cellW;
            offsetY = (gridH / 2.0) * cellH;
            for (int i = 0; i < gridW; i++)
            {
                for (int j = 0; j < gridH; j++)
                {
                    GameObject c = new GameObject(GlobalContent.LoadImg("GridCell",true));
                    c.
                       // Scale(3).
                       // SetCenter(5).
                        SetXY(i*(cellW) -offsetX, j* (cellH) - offsetY).
                        AddRender(this);
                    c.Alpha = 0.25;
                }
            }
        }

        public void SetRealEntityCoords(Entity p)
        {
            if (isInGrid(p.pX,p.pY))
            {
            //p.SetXY(v1*cellW - offsetX, v2 * cellH - offsetX);
            p.GoalX = p.pX * cellW - offsetX;
            p.GoalY = p.pY * cellH - offsetY;
            }
        }

        public LevelGrid(int w = 6, int h= 6)
        {
            gridW = w;
            gridH = h;
            for (int i=0;i<w;i++)
            {
                EntitiesGrid.Add(new List<Entity>());
                for (int j = 0; j < h; j++)
                {
                    EntitiesGrid[i].Add(null);
                }
            }

            DrawGrid();
        }

        public int Enemies = 0;
        public int Friends = 0;

        public void CountEntities()
        {
            Enemies = 0;
            Friends = 0;

            foreach (Entity e in this.Entities)
            {
                if (e != null && e.type != Entity.EType.Stone)
                {
                    if (e.isEnemy)
                    { 
                        Enemies++;
                    }
                    else
                    {
                        Friends++;
                    }
                }
            }
        }

        public Point FindEmptySpace(bool forEnemy)
        {
            Point p = new Point(0, 0);
            Entity e;
            do
            {
                p.X = Gameplay.RNG.Next(0, gridW);
                if (forEnemy)
                {
                    p.Y = Gameplay.RNG.Next(0, gridH / 2);
                }
                else
                {
                    p.Y = Gameplay.RNG.Next(gridH / 2, gridH);
                }
                e = GetEntity(p.X, p.Y);
            } while (e != null);

            return p;
        }

        public void SetEntity(Entity e)
        {
            if (isInGrid(e.pX, e.pY) && EntitiesGrid[e.pX][e.pY] == null)
            {
                EntitiesGrid[e.pX][e.pY] = e;
                if (!Entities.Contains(e))
                {
                    Entities.Add(e);
                }
            }
              

        }

        public void MoveEntity(Entity e, int newX, int newY)
        {
            if (isInGrid(newX,newY) && GetEntity(newX,newY) == null)
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

        public Entity LookfromEntity(int x, int y, direction d)
        {
            int dx, dy, cx, cy;
            dx = 0;
            dy = 0;

            if (d == direction.down) dy = 1;
            if (d == direction.up) dy = -1;
            if (d == direction.left) dx = 1;
            if (d == direction.right) dx = -1;

            cx = x;
            cy = y;

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
            return !(x < 0 || x >= gridW || y < 0 || y >= gridH);
        }


    }
}


 