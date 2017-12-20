using HuszarBejaras.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HuszarBejaras
{
    public partial class Form1 : Form
    {
        #region VARIABLES
        private HuszarModel _model;
        private Button[,] _buttonGrid;
        private Int32 _mapSize;

        #endregion
        public Form1()
        {
            InitializeComponent();
        }

        // gombokra esemény kezelők
        private void EasyNewGameButton_Click(Object sender, EventArgs e)
        {
            _mapSize = 3;
            SetTheOptions();
        }
        private void MediumNewGameButton_Click(Object sender, EventArgs e)
        {
            _mapSize = 4;
            SetTheOptions();
        }
        private void BigNewGameButton_Click(Object sender, EventArgs e)
        {
            _mapSize = 6;
            SetTheOptions();
        }

        // állítsuk be a játékot
        private void SetTheOptions()
        {
            _model = new HuszarModel(_mapSize);
            // Modelben szereplő eseményekre való esemény kezelők itt kerüljenek majd kötésre

            GenerateTable(_mapSize);
            SetupTable();

            //_model.NewGame(_mapSize); 

        }

        private void GenerateTable(Int32 size)
        {
            _buttonGrid = new Button[size, size];
            gameTableBox.Controls.Clear();
            gameTableBox.Size = new Size(size * 35, size * 35);

            for(Int32 i = 0;i< size; i++)
            {
                for(Int32 j = 0;j< size; j++)
                {
                    _buttonGrid[i, j] = new Button();
                    _buttonGrid[i, j].Size = new Size(25,25);
                    _buttonGrid[i, j].Text = String.Empty;
                    _buttonGrid[i, j].Location = new Point(5 + 25 * j, 10 + 25 * i);
                    _buttonGrid[i, j].TabIndex = 100 + i * _mapSize + j;
                    if (_model.GetValue(i, j) == 0)
                    {
                        _buttonGrid[i, j].BackColor = Color.Black;
                    }
                    else
                    {
                        _buttonGrid[i, j].BackColor = Color.White;
                    }
                        _buttonGrid[i, j].Enabled = false;
                    _buttonGrid[i, j].MouseClick += new MouseEventHandler(ButtonGrid_Click);
                    gameTableBox.Controls.Add(_buttonGrid[i, j]);

                }
            }
        }
        private void SetupTable()
        {
            for(Int32 i = 0; i < _mapSize; i++)
            {
                for(Int32 j = 0;j< _mapSize; j++)
                {
                   // if (_model.GetValue(i,j) == 0) _buttonGrid[i, j].BackColor = Color.Black;
                    //else if (_model.GetValue(i, j) == 1) _buttonGrid[i, j].BackColor = Color.White;
                    if (_model.GetValue(i, j) == 2) _buttonGrid[i, j].BackColor = Color.Yellow; // Ez lesz a ló
                    else if (_model.GetValue(i, j) == 3) _buttonGrid[i, j].BackColor = Color.Gray; // Ha már volt rajta a ló
                    _buttonGrid[i, j].Enabled = true;
                }
            }

            scoreValueLabel.Text = _model.Score.ToString();
        }

        private void ButtonGrid_Click(Object sender, MouseEventArgs e)
        {
            Int32 x = ((sender as Button).TabIndex - 100) / _mapSize;
            Int32 y = ((sender as Button).TabIndex - 100) % _mapSize;

            _model.Step(x, y); // A ló lép

            //mezők 
            SetupTable();
        }
    }
}
