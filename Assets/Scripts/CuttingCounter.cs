using System.Linq;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOs;

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
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectScriptableObject()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectScriptableObject()))
        {
            KitchenObjectSO outputSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectScriptableObject());

            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKithenObject(outputSO, this);
        }
    }

    // そもそもカットできる料理なのかのチェック
    private bool HasRecipeWithInput(KitchenObjectSO kitchenObjectSO)
    {
        return cuttingRecipeSOs.FirstOrDefault(a => a.input == kitchenObjectSO) != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO kitchenObjectSO)
    {
        return cuttingRecipeSOs.FirstOrDefault(a => a.input == kitchenObjectSO).output;
    }
}
