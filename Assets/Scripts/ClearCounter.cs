using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectScriptableObject kitchenObject;
    [SerializeField] private Transform counterTopPoint;

    public void Interact()
    {
        Transform clone =  Instantiate(kitchenObject.Prefab, counterTopPoint);
        clone.transform.localPosition = Vector3.zero;

        Debug.Log(kitchenObject.KitchenObjectType.ToString());
    }
}
