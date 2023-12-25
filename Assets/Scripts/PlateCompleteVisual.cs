using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static PlateKitchenObject;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public class KitchenObjectSO_GameObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }

    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSO_GameObjects;

    void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
        foreach (var item in kitchenObjectSO_GameObjects)
        {
            item.gameObject.SetActive(false);
        }
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, OnIngredientAddedEventArgs e)
    {
        var ingredient = kitchenObjectSO_GameObjects.FirstOrDefault(a => a.kitchenObjectSO.Name == e.Ingredient.Name);
        if (ingredient != null)
        {
            ingredient.gameObject.SetActive(true);
        }
    }

}
