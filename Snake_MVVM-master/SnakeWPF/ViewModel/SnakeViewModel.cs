using SnakeWPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SnakeWPF.ViewModel
{
    /// <summary>
    /// Sudoku nézetmodell típusa.
    /// </summary>
    public class SnakeViewModel : ViewModelBase
    {
        #region Fields

        private SnakeGameModel _model; // modell
        private Int32 mapSize;
        private Boolean isGamePaused;
        private String gameEvent;
        private Int32 gameScoreView;
        private Boolean gameStarted;

        #endregion

        #region Properties
        public Int32 MapSize { get { return mapSize; } set { mapSize = value; } }

        public Boolean IsGamePaused { get { return isGamePaused; } set { isGamePaused = value; } }

        public String GameEvent { get { return gameEvent; } set { GameEvent = value; } }

        public Int32 GameScoreView { get { return gameScoreView;} set { GameScoreView = value; } }

        public SnakeGameModel GameModel { get { return _model; } }
        /// <summary>
        /// Új játék kezdése parancs lekérdezése.
        /// </summary>
        public DelegateCommand NewGameCommand { get; private set; }

        /// <summary>
        /// Játék betöltése parancs lekérdezése.
        /// </summary>
        public DelegateCommand LoadGameCommand { get; private set; }

        /// <summary>
        /// Játék mentése parancs lekérdezése.
        /// </summary>
        public DelegateCommand SaveGameCommand { get; private set; }

        /// <summary>
        /// Kilépés parancs lekérdezése.
        /// </summary>
        public DelegateCommand ExitGameCommand { get; private set; }

        /// <summary>
        /// Játékmező gyűjtemény lekérdezése.
        /// </summary>
        public ObservableCollection<GameField> Fields { get; set; }

        /// <summary>
        /// Lépések számának lekérdezése.
        /// </summary>
        public Int32 GameScore { get { return _model.GameScore; } }


        #endregion

        #region Events

        /// <summary>
        /// Új játék eseménye.
        /// </summary>
        public event EventHandler NewGame;

        /// <summary>
        /// Játék betöltésének eseménye.
        /// </summary>
        public event EventHandler LoadGame;

        /// <summary>
        /// Játék mentésének eseménye.
        /// </summary>
        public event EventHandler SaveGame;

        /// <summary>
        /// Játékból való kilépés eseménye.
        /// </summary>
        public event EventHandler ExitGame;

        public DelegateCommand NewEasyGameCommand { get; private set; }
        public DelegateCommand NewMediumGameCommand { get; private set; }
        public DelegateCommand NewHardGameCommand { get; private set; }
        public DelegateCommand MoveCommand { get; private set; }
        public DelegateCommand PauseCommand { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Sudoku nézetmodell példányosítása.
        /// </summary>
        /// <param name="model">A modell típusa.</param>
        public SnakeViewModel(Int32 size,SnakeGameModel model)
        {
            NewGameCommand = new DelegateCommand(param => { OnNewGame(); GenerateTable(); });
            LoadGameCommand = new DelegateCommand(param => { OnLoadGame(); GenerateTable(); });
            SaveGameCommand = new DelegateCommand(param => OnSaveGame());
            ExitGameCommand = new DelegateCommand(param => OnExitGame());
            NewEasyGameCommand = new DelegateCommand(param => { changeDifficulty(10); });
            NewMediumGameCommand = new DelegateCommand(param => { changeDifficulty(15); });
            NewHardGameCommand = new DelegateCommand(param => { changeDifficulty(20); });
            MoveCommand = new DelegateCommand(param => StepGame(param.ToString()));
            PauseCommand = new DelegateCommand(param => { PauseGame(); });

            // játék csatlakoztatása
            _model = model;
            
            _model.SnakeMoved += new EventHandler<SnakeEventArgs>(Model_SnakeMoved);
            _model.GameOver += new EventHandler<SnakeEventArgs>(Model_GameOver);
            _model.OnMoveChange += RefreshTable;
            gameStarted = false;

            changeDifficulty(size);

        }

        private void changeDifficulty(Int32 Size)
        {

            if (gameStarted)
            {
                _model.GameOver -= new EventHandler<SnakeEventArgs>(Model_GameOver);
                _model.SnakeMoved -= new EventHandler<SnakeEventArgs>(Model_SnakeMoved);
                
                Fields.Clear();

            }
            
            //_model = new SnakeGameModel(Size);
            //propertyk beállítása
            gameScoreView = _model.GameScore;
            mapSize = Size;
            isGamePaused = false;
            gameEvent = "Jatek beallitas";
            OnPropertyChanged("gameScoreView");
            OnPropertyChanged("gameEvent");
        }

        private void Model_SnakeMoved(object sender, SnakeEventArgs e)
        {
            if (e.isEat)
            {
                Fields[e.headPosX * e.headPosY + e.headPosY].FieldValue = _model.Table.GetValue(e.headPosX, e.headPosY);
            }
            else
            {
                Fields[e.headPosX * e.headPosY + e.headPosY].FieldValue = _model.Table.GetValue(e.headPosX, e.headPosY);
                Fields[e.tailPosX * e.tailPosY + e.tailPosY].FieldValue = _model.Table.GetValue(e.tailPosX, e.tailPosY);
            }

        }
        private void Model_GameOver(object sender, SnakeEventArgs e)
        {
            _model.GameTimer.Stop();
            gameStarted = false;
            gameEvent = "Sajnalom vege a jateknak, kezdj újat!";
            OnPropertyChanged("GameEvent");
        }

        #endregion

        #region Private methods

        private void GenerateTable() {
            // játéktábla létrehozása
            Fields = new ObservableCollection<GameField>();
            for (Int32 i = 0; i < mapSize; i++) // inicializáljuk a mezőket
            {
                for (Int32 j = 0; j < mapSize; j++)
                {
                    Fields.Add(new GameField
                    {
                        FieldValue = _model.Table.GetValue(i, j),
                        X = i,
                        Y = j,
                        Number = i * _model.GameTableSize + j,
                        fieldColor = new System.Windows.Media.SolidColorBrush()
                    });
                }
            }
            OnPropertyChanged("MapSize");
            OnPropertyChanged("Fields");

        }
        /// <summary>
        /// Tábla frissítése.
        /// </summary>
        private void RefreshTable()
        {
        
            foreach (GameField field in Fields) // inicializálni kell a mezőket is
            {
                if (_model.Table.GetValue(field.X, field.Y) == 1) field.FieldValue = 1;
                if (_model.Table.GetValue(field.X, field.Y) == 0) field.FieldValue = 0;
                if (_model.Table.GetValue(field.X, field.Y) == 2) field.FieldValue = 2;
            }
            gameScoreView = _model.GameScore;
            OnPropertyChanged("gameScoreView");

        }

        /// <summary>
        /// Játék léptetése eseménykiváltása.
        /// </summary>
        /// <param name="param">A lenyomott billentyű.</param>
        private void StepGame(String param)
        {
            switch (param)
            {
                case "W": _model.setDirection(2); break;
                case "A": _model.setDirection(1); break;
                case "S": _model.setDirection(0); break;
                case "D": _model.setDirection(3); break;
                case "P":

                    if (_model.isGamePaused)
                    {
                         gameEvent = "A játék folyik!";
                        _model.isGamePaused = false;
                    }
                    else
                    {
                         gameEvent= "A játék szünetel!";
                        _model.isGamePaused = true;
                    }
                    break;
            }
            gameScoreView = _model.GameScore;
            OnPropertyChanged("gameScoreView");
            OnPropertyChanged("GameEvent");

        }
        private void PauseGame()
        {
            isGamePaused = !isGamePaused;
            _model.isGamePaused = !_model.isGamePaused;
            OnPropertyChanged("isGamePaused");
            if (!_model.isGamePaused)
            {
                gameEvent = "A játék folyik!";
                //_model.isGamePaused = false;
            }
            else
            {
                gameEvent = "A játék szünetel!";
                //_model.isGamePaused = true;
            }
            OnPropertyChanged("GameEvent");
            

        }

        #endregion
        

        
        #region Event methods

        /// <summary>
        /// Új játék indításának eseménykiváltása.
        /// </summary>
        private void OnNewGame()
        {
            if (NewGame != null)
                NewGame(this, EventArgs.Empty);
            gameStarted = true;
            gameEvent = "A jatek folyik";
            OnPropertyChanged("GameEvent");
           

        }



        /// <summary>
        /// Játék betöltése eseménykiváltása.
        /// </summary>
        private void OnLoadGame()
        {
            if (LoadGame != null)
                LoadGame(this, EventArgs.Empty);
            gameStarted = true;
            gameEvent = "A jatek szünetel";
            OnPropertyChanged("GameEvent");
            mapSize = _model.GameTableSize;
            OnPropertyChanged("MapSize");

        }

        /// <summary>
        /// Játék mentése eseménykiváltása.
        /// </summary>
        private void OnSaveGame()
        {
            if (SaveGame != null)
                SaveGame(this, EventArgs.Empty);
        }

        /// <summary>
        /// Játékból való kilépés eseménykiváltása.
        /// </summary>
        private void OnExitGame()
        {
            if (ExitGame != null)
                ExitGame(this, EventArgs.Empty);
        }



        #endregion
    }
}
