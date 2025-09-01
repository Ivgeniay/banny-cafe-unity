using System.Collections.Generic;
using System.Linq;
using System;

using Guid = System.Guid;

namespace BannyCafe.Core.Entities
{
    public abstract class Entity
    {
        public Guid GUID { get; private set; }

        public Entity() => GUID = new Guid();
        public Entity(Guid GUID) => this.GUID = GUID;

        public void GenerateGUID() => this.GUID = new Guid();
    }


    [Serializable]
    public class Guest : Entity
    {
        public Guest() : base() { }
        public Guest(Guid GUID) : base(GUID) { }

    }
    [Serializable]
    public class InteractiveObject : Entity
    {
        public InteractiveObject() : base() { }
        public InteractiveObject(Guid GUID) : base(GUID) { }
    }


    public interface IIngredient
    {
        public string GetGuid();
        public string Name { get; }
        public string Description { get; }
    }
    [Serializable]
    public class IngredientInfo : Entity, IIngredient
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string GetGuid() => this.GUID.ToString();
    }

    public interface IRecipe
    {
        public string Name { get; }
        public string Description { get; }
        public string GetGuid();
        public IEnumerable<IIngredient> GetIngredients();
    }
    [Serializable]
    public class RecipeInfo : Entity, IRecipe
    {
        private List<IIngredient> Ingredients;
        public string Name { get; set; }
        public string Description { get; set; }

        public string GetGuid() => this.GUID.ToString();
        public IEnumerable<IIngredient> GetIngredients() => this.Ingredients;
    }


    public interface IProduct
    {
        public string Name { get; }
        public string Description { get; }
        public string GetGuid();
        public IRecipe GetRecipe();
    }
    [Serializable]
    public class Product : Entity, IProduct
    {
        private string _name;
        private string _description;
        private RecipeInfo Recipe;

        public Product(string name, string description, RecipeInfo recipe) : base()
        {
            if (recipe == null) throw new ArgumentNullException($"Param: {recipe.GetType().Name} {nameof(recipe)} ");
            _name = string.IsNullOrEmpty(name) ? "" : name;
            _description = string.IsNullOrEmpty(description) ? "" : description;
            Recipe = recipe;
        }

        public string Name { get => _name; }
        public string Description { get => _description; }
        public string GetGuid() => this.GUID.ToString();
        public IRecipe GetRecipe() => Recipe;
    }



    public interface IPreparation
    {
        public void Appand(IIngredient ingredient);
        public IEnumerable<IIngredient> GetOrderIngredients();
    }
    [Serializable]
    public class PreparationProcess : IPreparation
    {
        private List<IIngredient> ingredients = new List<IIngredient>();
        public void Appand(IIngredient ingredient)
        {
            ingredients.Add(ingredient);
        }

        public IEnumerable<IIngredient> GetOrderIngredients()
        {
            foreach (var ingredient in ingredients)
            {
                yield return ingredient;
            }
        }
    }
    public interface IPreparationComparer
    {
        public bool Compare(IPreparation order, IProduct product);
        public bool Compare(IPreparation order, IRecipe recipeInfo);
    }
    public class OrderExactComparer : IPreparationComparer
    {
        public bool Compare(IPreparation order, IProduct product)
        {
            var recipe = product.GetRecipe();
            return this.Compare(order, recipe);
        }

        public bool Compare(IPreparation order, IRecipe recipeInfo)
        {
            var neededIngr = recipeInfo.GetIngredients();
            var preparetedIngr = order.GetOrderIngredients();

            bool countCompare = neededIngr.Count() == preparetedIngr.Count();
            if (!countCompare) return false;

            using (var enum1 = neededIngr.GetEnumerator())
            {
                using (var enum2 = preparetedIngr.GetEnumerator())
                {
                    while (enum1.MoveNext() && enum2.MoveNext())
                    {
                        var item1 = enum1.Current;
                        var item2 = enum2.Current;
                        if (item1.GetGuid() != item2.GetGuid()) return false;
                    }
                }
            }

            return true;
        }

    }
    public class OrederComparer : IPreparationComparer
    {
        public bool Compare(IPreparation order, IProduct product)
        {
            var recipe = product.GetRecipe();
            return this.Compare(order, recipe);
        }

        public bool Compare(IPreparation order, IRecipe recipeInfo)
        {
            var neededIngr = recipeInfo.GetIngredients();
            var preparetedIngr = order.GetOrderIngredients();

            bool countCompare = neededIngr.Count() == preparetedIngr.Count();
            if (!countCompare) return false;

            var neededGuids = neededIngr.Select(x => x.GetGuid());
            var preparetedGuids = preparetedIngr.Select(x => x.GetGuid());

            var result = neededGuids.OrderBy(x => x).SequenceEqual(preparetedGuids.OrderBy(x => x));
            return result;
        }
    }

}