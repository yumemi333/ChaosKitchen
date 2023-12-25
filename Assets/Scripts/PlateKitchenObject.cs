using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs: EventArgs
    {
        public KitchenObjectSO Ingredient;
    }

    private List<KitchenObjectSO> kitchenObjectSOList = new List<KitchenObjectSO>();
    [SerializeField] private List<KitchenObjectSO> validKitchenObject = new List<KitchenObjectSO>();

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        // もう所持している
        if (kitchenObjectSOList.Any(e=> e.name == kitchenObjectSO.name))
        {
            return false;
        }

        // 正しいキッチンオブジェクト
        if(!validKitchenObject.Any(e=> e.name == kitchenObjectSO.name))
        {
            return false;
        }
        
        kitchenObjectSOList.Add(kitchenObjectSO);

        OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs { Ingredient = kitchenObjectSO});

        return true;
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }
}
