using System.Windows;
using System.Windows.Controls;
using RecipeHelper.ViewModel;

namespace RecipeHelper.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void ButtonAddRecipe_OnClick(object sender, RoutedEventArgs e)
    {
        var editRecipeDlg = new EditRecipeDialog
        {
            Owner = this,
        };
        editRecipeDlg.ShowDialog();
    }

    private void ButtonCreateMenu_Click(object sender, RoutedEventArgs e)
    {
        var menuPreviewDlg = new MenuPreviewDialog()
        {
            Owner = this,
            DataContext = new MenuPreviewViewModel(listBoxRecipes.SelectedItems),
        };
        menuPreviewDlg.ShowDialog();
    }

    private void ListBoxRecipes_OnSelectionChanged(object sender,
        SelectionChangedEventArgs e)
    {
        btnCreateMenu.IsEnabled = (listBoxRecipes.SelectedItems.Count > 0);
    }
}
}
