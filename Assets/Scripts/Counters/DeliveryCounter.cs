using System;

public class DeliveryCounter : BaseCounter
{
    private event EventHandler<OnDeliveredEventArgs> OnDelivered;
    public class OnDeliveredEventArgs
    {
        public PlateKitchenObject plateKitchenObject;
    }
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                player.GetKitchenObject().DestroySelf();

                DeliveryHandler.Instance.DeliverRecipe(plateKitchenObject);
                SetKitchenObject(plateKitchenObject);
            }
        }

    }
}
