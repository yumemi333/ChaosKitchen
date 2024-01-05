using System;
using System.Linq;
using UnityEngine;
using static IHasProgress;

public class CuttingCounter : BaseCounter, IHasProgress
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOs;
    private int cuttingProgress = 0;


    public event EventHandler OnCut;
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;

    public override void Interact(Player player)
    {
        if (HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }               
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
        else
        {
            cuttingProgress = 0;

            if (player.HasKitchenObject())
            {
                if (!HasRecipeWithInput(player.GetKitchenObject().GetKitchenSO()))
                {
                    return;
                }

                player.GetKitchenObject().SetKitchenObjectParent(this);
                CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenSO());

                OnProgressChanged?.Invoke(this,
                    new OnProgressChangedEventArgs
                    {
                        progressNomalized = cuttingProgress / (float)cuttingRecipeSO.cuttingProgressMax
                    }
                    );
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenSO()))
        {
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenSO());
            cuttingProgress++;
            OnCut?.Invoke(this, EventArgs.Empty);
            OnProgressChanged?.Invoke(this,
                new OnProgressChangedEventArgs
                {
                    progressNomalized = cuttingProgress / (float)cuttingRecipeSO.cuttingProgressMax
                }
             );

            if (cuttingProgress == cuttingRecipeSO.cuttingProgressMax)
            {

                KitchenObjectSO outputSO = GetOutputForInput(GetKitchenObject().GetKitchenSO());

                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKithenObject(outputSO, this);
            }
        }
    }

    /// <summary>
    ///  そもそもカットできる料理なのかのチェック
    /// </summary>
    /// <param name="kitchenObjectSO"></param>
    /// <returns></returns>
    private bool HasRecipeWithInput(KitchenObjectSO kitchenObjectSO)
    {
        return GetCuttingRecipeSOWithInput(kitchenObjectSO) != null;
    }


    /// <summary>
    /// カット後のSOを取得
    /// </summary>
    /// <param name="kitchenObjectSO"></param>
    /// <returns></returns>
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO kitchenObjectSO)
    {
        return cuttingRecipeSOs.FirstOrDefault(a => a.input == kitchenObjectSO).output;
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO kitchenObjectSO)
    {
        return cuttingRecipeSOs.FirstOrDefault(a => a.input == kitchenObjectSO);
    }
}
