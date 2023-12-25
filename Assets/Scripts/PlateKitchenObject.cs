using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    private List<KitchenObjectSO> kitchenObjects = new List<KitchenObjectSO>();
    [SerializeField] private List<KitchenObjectSO> validKitchenObject = new List<KitchenObjectSO>();

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (kitchenObjects.Any(e=> e.name == kitchenObjectSO.name))
        {
            return false;
        }
        if(!validKitchenObject.Any(e=> e.name == kitchenObjectSO.name))
        {
            return false;
        }
        kitchenObjects.Add(kitchenObjectSO);
        return true;
    }
}
