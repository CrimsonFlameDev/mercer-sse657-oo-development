using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Microsoft.Win32;

namespace RecipeHelper.GUI
{
    /// <summary>
    /// Interaction logic for MenuPreviewDialog.xaml
    /// </summary>
public partial class MenuPreviewDialog
{
    public MenuPreviewDialog()
    {
        InitializeComponent();
    }

    private void ButtonPrintGroceryList_Click(object sender, RoutedEventArgs e)
    {
        var printDlg = new PrintDialog();

        var userCancelled = !printDlg.ShowDialog().Value;
        if (userCancelled) return;

        IDocumentPaginatorSource doc = richTxtBoxGroceryList.Document;
        printDlg.PrintDocument(doc.DocumentPaginator,
            "Recipe Helper - Print Grocery List");
    }

    private void ButtonSaveMenu_OnClick(object sender, RoutedEventArgs e)
    {
        var saveDlg = new SaveFileDialog()
        {
            FileName =
                (!string.IsNullOrWhiteSpace(textBoxMenuName.Text)
                    ? textBoxMenuName.Text
                    : "My Menu"),
            DefaultExt = ".txt",
            Filter = "Menus (.txt)|*.txt",
        };

        var saveDialogShown = saveDlg.ShowDialog();

        var userCancelled = (saveDialogShown.HasValue) && (!saveDialogShown.Value);
        if (userCancelled) return;

        Save(saveDlg.FileName);
    }

    private void Save(string fileName)
    {
        var range = new TextRange(richTextBoxMenu.Document.ContentStart,
            richTextBoxMenu.Document.ContentEnd);
        var fStream = new FileStream(fileName, FileMode.Create);
        range.Save(fStream, DataFormats.Text);
        fStream.Close();
    }
}
}
