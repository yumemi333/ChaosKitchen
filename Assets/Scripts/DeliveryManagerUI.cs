using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private RecipeTemplateUI[] receipeTemplates;

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeWrong += DeliveryManager_OnRecipeWrong;
    }

    private void DeliveryManager_OnRecipeWrong(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void DeliveryManager_OnRecipeSpawned(object sender, EventArgs e)
    {
        SpawnNewRecipe(DeliveryManager.Instance.GetWaitingRecipeListSOs());
    }

    private void DeliveryManager_OnRecipeCompleted(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void SpawnNewRecipe(List<RecipeSO> recipeSOs)
    {
        for (int i = 0; i < recipeSOs.Count; i++)
        {
            receipeTemplates[i].SetNewRecipe(recipeSOs[i]);
        }
    }
}
