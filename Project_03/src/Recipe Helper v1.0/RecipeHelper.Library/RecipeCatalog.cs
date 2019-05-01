using System.Collections.ObjectModel;
using RecipeHelper.SDK;
using RecipeHelper.Utils;

namespace RecipeHelper.Library
{
public class RecipeCatalog
{
    public ObservableCollection<IRecipe> Recipes { get; set; }

    public RecipeCatalog()
    {
        Recipes = Utilities.LoadRecipes();
    }
}
}
