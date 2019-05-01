using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GameFactory.Library;
using GameFactory.SDK;
using GameFactory.ViewModel.Annotations;

namespace GameFactory.ViewModel
{
public class ViewModel : INotifyPropertyChanged
{
    public ObservableCollection<IGame> Games { get; private set; }

    public GameLibrary Model { get; private set; }

    public DelegateCommand PlayGameCommand { get; private set; }

    public ViewModel()
    {
        Model = new GameLibrary();
        Games = Model.Games;
        SelectedGame = null;
        PlayGameCommand = new DelegateCommand(OnPlayGame);
    }

    private void OnPlayGame()
    {
        Model.PlayGame(SelectedGame.Title);
    }

    private IGame _selectedGame;

    public IGame SelectedGame
    {
        get { return _selectedGame; }
        set { _selectedGame = value; }
    }


    public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void
        OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        var handler = PropertyChanged;
        if (handler != null)
            handler(this, new PropertyChangedEventArgs(propertyName));
    }
}
}