namespace RecipeHelper.SDK
{
public enum Unit
{
    Gram,
    Ounce,
    Pound,
}

public interface IIngredient
{
    int Quantity { get; set; }
    Unit Unit { get; set; }
    string Name { get; set; }
}
}
