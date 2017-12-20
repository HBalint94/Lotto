using LottoWPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoWPF.ViewModel
{
    public class LottoViewModel : ViewModelBase
    {

        private LottoGameModel _model;
        private Int32 mapSizeX;
        private Int32 mapSizeY;
        private Int32 guessNumber; // tippek száma ( Majd a Command paramétertől függ) 
        private Int32 successFullScore; // helyes találatok száma


        public Int32 MapSizeX { get { return mapSizeX; } set { mapSizeX = value; } }
        public Int32 MapSizeY { get { return mapSizeY; } set { mapSizeY = value; } }
        public Int32 GuessNumber { get { return guessNumber; } set { guessNumber = value; } }
        public Int32 SuccessFullScore { get { return successFullScore; } set { successFullScore = value; } }

        
        public ObservableCollection<GameField> Fields { get; set; }
        public ObservableCollection<String> WinnerNumbers { get; set; }


        public DelegateCommand FiveMapSizeCommand { get; set; }
        public DelegateCommand SixMapSizeCommand { get; set; }
        public DelegateCommand SevenMapSizeCommand { get; set; }

        public LottoViewModel()
        {
            FiveMapSizeCommand = new DelegateCommand(param => GenerateTable(90));
            SixMapSizeCommand = new DelegateCommand(param => GenerateTable(45));
            SevenMapSizeCommand = new DelegateCommand(param => GenerateTable(35));

            WinnerNumbers = new ObservableCollection<string>();

            _model = new LottoGameModel();
            _model.GuessAction += new EventHandler<LottoEventArgs>(Model_GuessAction);
            _model.RandomWinnerNumber += new EventHandler<RandomEventArgs>(Model_GenerateRandomWinnerNumber);
            successFullScore = 0;
            
        }

        public void GenerateTable(Int32 TypeOfLotto)
        {
            if(TypeOfLotto == 90)
            {
                mapSizeX = 10;
                _model.MapSizeX = 10;
                mapSizeY = 9;
                _model.MapSizeY = 9;
                guessNumber = 5;
                _model.GuessNumber = 5;
                _model.GuessesNumberHistory = new Int32[guessNumber];
                _model.WinnerNumberHistory = new Int32[guessNumber];
            }else if(TypeOfLotto == 45)
            {
                mapSizeX = 9;
                _model.MapSizeX = 9;
                mapSizeY = 5;
                _model.MapSizeY = 5;
                guessNumber = 6;
                _model.GuessNumber = 6;

                _model.GuessesNumberHistory = new Int32[guessNumber];
                _model.WinnerNumberHistory = new Int32[guessNumber];
            }
            else if(TypeOfLotto == 35)
            {
                mapSizeX = 7;
                _model.MapSizeX = 7;
                mapSizeY = 5;
                _model.MapSizeY = 5;
                guessNumber = 7;
                _model.GuessNumber = 7;

                _model.GuessesNumberHistory = new Int32[guessNumber];
                _model.WinnerNumberHistory = new Int32[guessNumber];
            }
            Fields = new ObservableCollection<GameField>();
            for(Int32 i = 0; i< mapSizeX; i++)
            {
                for(Int32 j = 0; j< mapSizeY; j++)
                {
                    Fields.Add(new GameField
                    {
                        FieldValue = i * mapSizeY + j,
                        X = i,
                        Y = j,
                        Signed = false,
                        SignCommand = new DelegateCommand(param => StepGame(Convert.ToInt32(param)))
                    });
                }
            }
            _model.GameTimer.Start();
            OnPropertyChanged("MapSizeX");
            OnPropertyChanged("MapSizeY");
            OnPropertyChanged("Fields");
            OnPropertyChanged("Signed");
        }

    
        
        public void Model_GenerateRandomWinnerNumber(object sender, RandomEventArgs e)
        {
            if (e.Bingo) successFullScore++;
            String N = e.Number.ToString();
            App.Current.Dispatcher.Invoke((Action)delegate
            {

                WinnerNumbers.Add(N);
            });
            OnPropertyChanged("WinnerNumbers");
            OnPropertyChanged("SuccessFullScore");
        }

        public void Model_GuessAction(object sender, LottoEventArgs e)
        {
            Int32 ActFieldPosition = e.Y * MapSizeY + e.X;
            if (!Fields[ActFieldPosition].Signed)
            {
                Fields[ActFieldPosition].Signed = true;
                OnPropertyChanged("Signed");
            }
            OnPropertyChanged("Fields");
        }

        private void StepGame(Int32 index)
        {
            Int32 Y = index / mapSizeX;
            Int32 X = index % mapSizeX;

            _model.Step(X,Y,index);

            Fields[index].Signed = true;
            OnPropertyChanged("Fields");
           
        }
    }
}
