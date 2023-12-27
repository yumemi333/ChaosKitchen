using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryHandler : MonoBehaviour
{
    [SerializeField] private RecipeListSO recipeList;
    public static DeliveryHandler Instance { get; set; }

    private List<RecipeSO> waitingRecipeSOList = new List<RecipeSO>();

    private float spawnRecipeTimer = 0f;
    private float spawnRecipeTimerMax = 4f;

    private int waitingRecipeMax = 5;

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeWrong;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (!KitchenGameManager.Instance.IsGamePlaying)
        {
            return;
        }
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (waitingRecipeSOList.Count < waitingRecipeMax)
            {
                RecipeSO recipeSO = recipeList.RecipeSOList[UnityEngine.Random.Range(0, recipeList.RecipeSOList.Count)];
                waitingRecipeSOList.Add(recipeSO);
                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i_waiting = 0; i_waiting < waitingRecipeSOList.Count; i_waiting++)
        {
            // 皿に乗っているのと、配給待ちの料理の材料の数が同じかチェック
            if (waitingRecipeSOList[i_waiting].kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                bool allIngredientMathed = true;

                // 材料と同じかチェック
                foreach (var item_waiting in waitingRecipeSOList[i_waiting].kitchenObjectSOList)
                {
                    bool sameIngredientFound = false;
                    // 皿の上にあるやつが
                    foreach (var item_plate in plateKitchenObject.GetKitchenObjectSOList())
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
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    waitingRecipeSOList.RemoveAt(i_waiting);
                    return;
                }
            }
        }
        OnRecipeWrong.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipeListSOs()
    {
        return waitingRecipeSOList;
    }
}
