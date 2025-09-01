using BannyCafe.Core.Entities;
using UnityEngine;
using System;

namespace BannyCafe.Infrastructure.UnityIntegration
{
    [CreateAssetMenu(fileName = "Ingredient", menuName = "GamePlay/IngredientSO", order = 0)]
    public class IngredientSO : ScriptableObject, IIngredient
    {
        [SerializeField] private string _id = Guid.NewGuid().ToString();
        [SerializeField] private string _name = "IngredienName";
        [SerializeField] private string _description;

        public string Name { get => _name; }
        public string Description { get => _description; }
        public string GetGuid() => _id;
    }
}