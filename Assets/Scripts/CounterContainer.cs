using System;
using UnityEngine;

public class CounterContainer : BaseCounter, IKitchenObjectParent
{
    [SerializeField] protected KitchenObjectScriptableObject kitchenObjectSO;
    public event EventHandler OnPlayerGrabbedObject;

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            return;
        }

        KitchenObject.SpawnKithenObject(kitchenObjectSO, player);

        OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
    }
}
