using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Markup;
using RecipeHelper.SDK;

namespace RecipeHelper.GUI
{
    public abstract class ConverterBase : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }

    [ValueConversion(typeof (int), typeof (string))]
    public class IntToTimeConverter : ConverterBase, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof (string))
                throw new InvalidOperationException("The target must be a String");

            var val = (int) value;
            var hours = (val/60).ToString("D2");
            var mins = (val%60).ToString("D2");
            return string.Format("{0}:{1}", hours, mins);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof (IList<IRecipe>), typeof (string))]
    public class RecipeListToStringConverter : ConverterBase, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(string))
                throw new InvalidOperationException("The target must be a String");
            
            var recipeList = value as IList;

            var sb = new StringBuilder();
            foreach (var obj in recipeList)
            {
                var recipe = obj as IRecipe;
                sb.AppendFormat("{0}........{1:C2}", recipe.Name, recipe.Price)
                    .AppendLine()
                    .AppendLine();
            }
            return sb.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(ICollection<IIngredient>), typeof(string))]
    public class GroceryListToStringConverter : ConverterBase, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(string))
                throw new InvalidOperationException("The target must be a String");

            var ingredients = (value as ICollection).Cast<IIngredient>().ToList();

            var ingredientGroups = new List<string>();
            foreach (var ingredient in ingredients)
            {
                if (ingredientGroups.Contains(ingredient.Name)) continue;
                ingredientGroups.Add(ingredient.Name);
            }

            var sb = new StringBuilder();
            foreach (var ingredientGroup in ingredientGroups)
            {
                sb.AppendFormat("{0} ({1})", ingredientGroup,
                    ingredients.Count(i => i.Name.Equals(ingredientGroup))).AppendLine();
            }

            return sb.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(string), typeof(bool))]
    public class GroceryListToBoolConverter : ConverterBase, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(bool))
                throw new InvalidOperationException("The target must be a String");
            return !string.IsNullOrWhiteSpace(value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}