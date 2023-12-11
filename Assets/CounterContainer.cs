using System;
using UnityEngine;

public class CounterContainer : BaseCounter, IKitchenObjectParent
{
    public event EventHandler OnPlayerGrabbedObject;

    public override void Interact(Player player)
    {
        Transform clone = Instantiate(kitchenObjectSO.Prefab);
        clone.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
        OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
    }
}
