using System.Collections.Generic;
using BannyCafe.Core.Entities;
using UnityEngine;
using System;

namespace BannyCafe.Infrastructure.UnityIntegration
{
    [CreateAssetMenu(fileName = "Recipes", menuName = "GamePlay/ReciepsSO", order = 0)]
    public class RecipeSO : ScriptableObject, IRecipe
    {
        [SerializeField] private readonly string _id = Guid.NewGuid().ToString();
        [SerializeField] private readonly string _name = "RecipeName";
        [SerializeField] private readonly string _description = "";
        [SerializeField] private readonly List<IngredientSO> ingredients;
        public string Name { get => _name; }
        public string Description { get => _description; }

        public IEnumerable<IIngredient> GetIngredients() => this.ingredients;
        public string GetGuid() => this._id;
    }

}