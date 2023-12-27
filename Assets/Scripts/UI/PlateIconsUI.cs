using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private SingleIconUI icon;

    List<SingleIconUI> icons = new List<SingleIconUI>();
    void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (var item in icons)
        {
            item.gameObject.SetActive(false);
        }

        for(int i_kitchenSO = 0; i_kitchenSO < plateKitchenObject.GetKitchenObjectSOList().Count; i_kitchenSO++)
        {
            SingleIconUI icon;
            if (i_kitchenSO < icons.Count)
            {
                icon = icons[i_kitchenSO];
            }
            else
            {
                icon = Instantiate(this.icon, this.icon.transform.parent);
                icons.Add(icon);
            }
            icon.SetKitchenObjectSO(plateKitchenObject.GetKitchenObjectSOList()[i_kitchenSO]);
        }
    }
}
