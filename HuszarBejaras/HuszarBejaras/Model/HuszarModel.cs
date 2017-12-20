using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuszarBejaras.Model
{

    public class HuszarModel
    {

        private Int32[,] _table;
        private Int32 _score;
        private Int32 _mapSize;
        private Int32 HorseActualPositionX;
        private Int32 HorseActualPositionY;


        public Int32 Score { get { return _score; } set { _score = value; } }
        public Int32[,] Table { get { return _table; } set { _table = value; } }
        public Int32 MapSize { get { return _mapSize; } set { _mapSize = value; } }

        public void SetTableValue(Int32 x,Int32 y,Int32 value)
        {
            _table[x, y] = value;
        }
        public Int32 GetValue(Int32 x,Int32 y)
        {
            return _table[x, y];
        }

        // Konstruktor
        public HuszarModel(Int32 mapSize)
        {
            _mapSize = mapSize;
            _table = new Int32[mapSize, mapSize];
            FillTableWithValues();
            _score = 0;
            HorseActualPositionX = 0;
            HorseActualPositionY = 0;
        }

       
        public void Step(Int32 x,Int32 y)
        {
            if (ValidHorseStep(x,y))
            {
                if (_table[x, y] == 3)
                {
                    _score--;
                }
                else { _score = _score + 2; }
                _table[HorseActualPositionX, HorseActualPositionY] = 3;
                _table[x, y] = 2;
                HorseActualPositionX = x;
                HorseActualPositionY = y;
            }
            
        }

        private void FillTableWithValues()
        {
            for(int i = 0; i < _mapSize; i++)
            {
                for(int j = 0; j < _mapSize; j++)
                {
                    if (i + j == 0) _table[i, j] = 2;
                    else if ((i + j ) % 2 == 0) _table[i, j] = 0;
                    else if ((i + j ) % 2 == 1) _table[i, j] = 1;
                }
            }
        }
        private Boolean ValidHorseStep(Int32 x,Int32 y)
        {
            if((HorseActualPositionX + 2 == x && HorseActualPositionY + 1 == y) || (HorseActualPositionX + 2 == x && HorseActualPositionY - 1 == y ) || (HorseActualPositionX - 2 == x && HorseActualPositionY -1 == y) || 
                (HorseActualPositionX - 2 == x && HorseActualPositionY + 1 == y) || (HorseActualPositionX + 1 == x && HorseActualPositionY +2 == y) || (HorseActualPositionX + 1 == x && HorseActualPositionY -2 == y) || (HorseActualPositionX -1 == x && HorseActualPositionY +2 == y) ||
                    (HorseActualPositionX -1 == x && HorseActualPositionY -2 == y))
            {
                return true;
            }else { return false; }
        }
    }
}
