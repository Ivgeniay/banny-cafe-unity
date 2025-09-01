using BannyCafe.Core.Entities;
using UnityEngine;
using System;

namespace BannyCafe.Infrastructure.UnityIntegration
{
    [CreateAssetMenu(fileName = "Product", menuName = "GamePlay/ProductSO", order = 0)]
    public class ProductSO : ScriptableObject, IProduct
    {
        [SerializeField] public readonly string _id = Guid.NewGuid().ToString();
        [SerializeField] public readonly string _name = "ProductName";
        [SerializeField] public readonly string _description = "";
        [SerializeField] private readonly RecipeSO Recipe;

        public string Name { get => _name; }
        public string Description { get => _description; }
        public string GetGuid() => _id;
        public IRecipe GetRecipe() => Recipe;
    }
}