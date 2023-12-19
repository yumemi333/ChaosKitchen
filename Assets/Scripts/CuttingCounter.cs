using System;
using System.Linq;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOs;
    private int cuttingProgress = 0;

    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;

    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNomalized;
    }

    public event EventHandler OnCut;

    public override void Interact(Player player)
    {
        if (HasKitchenObject())
        {
            if (this.HasKitchenObject() && !player.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
        else
        {
            cuttingProgress = 0;

            if (player.HasKitchenObject())
            {
                if (!HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectScriptableObject()))
                {
                    return;
                }

                player.GetKitchenObject().SetKitchenObjectParent(this);
                CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectScriptableObject());

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
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectScriptableObject()))
        {
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectScriptableObject());
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

                KitchenObjectSO outputSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectScriptableObject());

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
