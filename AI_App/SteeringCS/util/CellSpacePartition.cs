using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SteeringCS.entity;
using SteeringCS.Interfaces;

namespace SteeringCS.util
{
    class CellSpacePartition<T>  where T : IEntity

    {
        //a lock for safety
        private readonly ReaderWriterLockSlim _writeLock = new ReaderWriterLockSlim();

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
        public void Add(int key, T ent)
        {
            int sz = _cells.Count;
            int i = PositionToIndex(ent.Pos);

            _cells[i].Members.TryAdd(key, ent);
        }

        //update an entity's cell by calling this from your entity's Update method
        public void UpdateEntity(int key, Vector2D oldPos, Vector2D newPos)
        {
            //if the index for the old pos and the new pos are not equal then
            //the entity has moved to another cell.
            int oldI = PositionToIndex(oldPos);
            int newI = PositionToIndex(newPos);

            if (newI == oldI) return;

            //the entity has moved into another cell so delete from current cell
            //and add to new one
            lock (_writeLock)
            {
                _cells[oldI].Members.TryRemove(key, out T entity);
                if(!entity.Equals(default(T)))
                    _cells[newI].Members.TryAdd(key, entity);
            }
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
            var queryBox = new InvertedBox(topLeft, bottomRight);

            //iterate through each cell and test to see if its bounding box overlaps
            //with the query box. If it does and it also contains entities then
            //make further proximity tests.
            var i = 0;
            while(i < _cells.Count)
            {

                    var members = _cells[i].Members.Values.ToList();
                    //test to see if this cell contains members and if it overlaps the
                    //query box
                    if (_cells[i].BoundingBox.Overlap(queryBox) && members.Any())
                    {
                        int j = 0;
                        //add any entities found within query radius to the neighbor list
                        while (j < members.Count)
                        {
                            var target = members[j];
                            if (target != null && (target.Pos - searcherPos).LengthSquared() < QueryRadius * QueryRadius)
                            {
                                yield return target;
                            }

                            j++;
                        }
                    }
                i++;
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

