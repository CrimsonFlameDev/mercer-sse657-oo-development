using System.Windows.Controls;

namespace GameFactory.GUI
{
    using ViewModel;

    /// <summary>
    /// Interaction logic for View.xaml
    /// </summary>
public partial class View
{
    public View()
    {
        InitializeComponent();

        DataContext = new ViewModel();
    }

    private void GameSelector_OnSelectionChanged(object sender,
                                                    SelectionChangedEventArgs e)
    {
        var selectedGame = comboBoxGames.SelectedItem.ToString();

        buttonPlay.IsEnabled = !string.IsNullOrWhiteSpace(selectedGame);
    }
}
}