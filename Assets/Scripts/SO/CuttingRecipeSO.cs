using UnityEngine;


[CreateAssetMenu]
public class CuttingRecipeSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public string Name;

    private void OnValidate()
    {
        if (string.IsNullOrEmpty(Name))
            Name = this.name;
    }
}
