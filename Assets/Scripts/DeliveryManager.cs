using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    [SerializeField] private RecipeListSO recipeList;
    public static DeliveryManager Instance { get; set; }

    private List<RecipeSO> waitingRecipeSOList = new List<RecipeSO>();

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;

    private int waitingRecipeMax = 5;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (waitingRecipeSOList.Count < waitingRecipeMax)
            {
                RecipeSO recipeSO = recipeList.RecipeSOList[UnityEngine.Random.Range(0, recipeList.RecipeSOList.Count)];
                Debug.Log(recipeSO.Name);
                waitingRecipeSOList.Add(recipeSO);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        foreach (var item_waitingRecipe in waitingRecipeSOList)
        {
            // 皿に乗っているのと、配給待ちの料理の材料の数が同じかチェック
            if (item_waitingRecipe.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                bool allIngredientMathed = true;

                // 皿の上にあるやつが
                foreach (var item_plate in plateKitchenObject.GetKitchenObjectSOList())
                {
                    bool sameIngredientFound = false;

                    // 材料と同じかチェック
                    foreach (var item_waiting in item_waitingRecipe.kitchenObjectSOList)
                    {
                        if (item_plate.Name == item_waiting.Name)
                        {
                            sameIngredientFound = true;
                            break;
                        }
                    }

                    //　同じのなかった
                    if (!sameIngredientFound)
                    {
                        allIngredientMathed = false;
                    }
                }

                if (allIngredientMathed)
                {
                    Debug.Log("CORRECT");
                    return;
                }
            }           
        }
        Debug.Log("WRONG");
    }
}
