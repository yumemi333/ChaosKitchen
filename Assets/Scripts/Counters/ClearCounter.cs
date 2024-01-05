using UnityEngine;

public class ClearCounter : BaseCounter
{

    public override void Interact(Player player)
    {
        if (HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                // プレイヤーが皿を持っているかチェック
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else
                {
                    // カウンターの上に皿があるかチェック
                    if(GetKitchenObject().TryGetPlate(out plateKitchenObject)){
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
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
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
    }
}
