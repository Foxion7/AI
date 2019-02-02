using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.entity;
using SteeringCS.Interfaces;

namespace SteeringCS.util
{
    class CellSpacePartition<T>  where T : IEntity

    {
        //the required number of cells in the space
        private List<Cell<T>> _cells;

        //the width and height of the world space the entities inhabit
        private double _spaceWidth;

        private double _spaceHeight;

        //the number of cells the space is going to be divided into
        private int _numCellsX;
        private int _numCellsY;
        private double _cellSizeX;
        private double _cellSizeY;

        //given a position in the game space, this method determines the
        //relevant cell's index
        private int PositionToIndex(Vector2D pos)
        {
            double idx = (_numCellsX * pos.X / _spaceWidth) +
                         ((_numCellsY) * pos.Y / _spaceHeight) * _numCellsX;

            //if the entity's position is equal to vector2d(m_dSpaceWidth, m_dSpaceHeight)
            //then the index will overshoot. We need to check for this and adjust
            if (idx > (int)_cells.Count - 1)
                idx = (int)_cells.Count - 1;

            return (int)idx;
        }


        public CellSpacePartition(double width, double height, int cellsX, int cellsY)
        {
            _spaceWidth = width;
            _spaceHeight = height;
            _cellSizeX = cellsX;
            _cellSizeY = cellsY;
            _numCellsX = cellsX;
            _numCellsY = cellsY;

            //calculate bounds of each cell
            _cellSizeX = width / cellsX;
            _cellSizeY = height / cellsY;


            //create the cells
            _cells = new List<Cell<T>>();
            for (int y = 0; y < _numCellsY; ++y)
            {
                for (int x = 0; x < _numCellsX; ++x)
                {
                    double left = x * _cellSizeX;
                    double right = left + _cellSizeX;
                    double top = y * _cellSizeY;
                    double bot = top + _cellSizeY;

                    _cells.Add(new Cell<T>(new Vector2D(left, top), new Vector2D(right, bot)));
                }
            }
        }

        //adds entities to the class by allocating them to the appropriate cell
        public void Add(T ent)
        {
            int sz = _cells.Count;
            int i = PositionToIndex(ent.Pos);

            _cells[i].Members.Add(ent);
        }

        //update an entity's cell by calling this from your entity's Update method
        public void UpdateEntity(T ent, Vector2D OldPos)
        {
            //if the index for the old pos and the new pos are not equal then
            //the entity has moved to another cell.
            int oldI = PositionToIndex(OldPos);
            int newI = PositionToIndex(ent.Pos);

            if (newI == oldI) return;

            //the entity has moved into another cell so delete from current cell
            //and add to new one
            _cells[oldI].Members.Remove(ent);
            _cells[newI].Members.Add(ent);
        }

        //this method calculates all a target's neighbors and stores them in
        //the neighbor vector. After you have called this method use the begin,
        //next, and end methods to iterate through the vector.
        public IEnumerable<T> CalculateNeighbors(Vector2D searcherPos, double QueryRadius)
        {
            //create the query box that is the bounding box of the target's query
            //area
            var topLeft = searcherPos - new Vector2D(QueryRadius, QueryRadius);
            var bottomRight = searcherPos + new Vector2D(QueryRadius, QueryRadius);
            var queryBox = new Box(topLeft, bottomRight);

            //iterate through each cell and test to see if its bounding box overlaps
            //with the query box. If it does and it also contains entities then
            //make further proximity tests.
            foreach (Cell<T> curCell in _cells)
            {
                //test to see if this cell contains members and if it overlaps the
                //query box
                if (curCell.boundingBox.Overlap(queryBox) && curCell.Members.Any())
                {
                    //add any entities found within query radius to the neighbor list
                    foreach (var target in curCell.Members)
                    {
                        if ((target.Pos - searcherPos).LengthSquared() < QueryRadius * QueryRadius)
                        {
                            yield return target;
                        }
                    }
                }
            }
        }

        //empties the cells of entities
        public void EmptyCells()
        {
            _cells = new List<Cell<T>>();
            for (int y = 0; y < _numCellsY; ++y)
            {
                for (int x = 0; x < _numCellsX; ++x)
                {
                    double left = x * _cellSizeX;
                    double right = left + _cellSizeX;
                    double top = y * _cellSizeY;
                    double bot = top + _cellSizeY;

                    _cells.Add(new Cell<T>(new Vector2D(left, top), new Vector2D(right, bot)));
                }
            }
        }
    };
}

