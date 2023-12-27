using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private RecipeTemplateUI[] receipeTemplates;

    private void Start()
    {
        DeliveryHandler.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
        DeliveryHandler.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
    }

    private void DeliveryManager_OnRecipeSpawned(object sender, EventArgs e)
    {
        SpawnNewRecipe(DeliveryHandler.Instance.GetWaitingRecipeListSOs());
    }

    private void DeliveryManager_OnRecipeCompleted(object sender, EventArgs e)
    {
        SpawnNewRecipe(DeliveryHandler.Instance.GetWaitingRecipeListSOs());
    }

    private void SpawnNewRecipe(List<RecipeSO> recipeSOs)
    {
        for (int i = 0; i < recipeSOs.Count; i++)
        {
            receipeTemplates[i].SetNewRecipe(recipeSOs[i]);
        }
    }
}
