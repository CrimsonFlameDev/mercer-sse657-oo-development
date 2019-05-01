using System.Collections.Generic;

namespace RecipeHelper.SDK
{
public interface IRecipe
{
    string Name { get; set; }
    List<IIngredient> Ingredients { get; set; }
    List<string> CookingSteps { get; set; }
    int PrepTime { get; set; }
    int CookTime { get; set; }
    double Price { get; set; }
}
}
