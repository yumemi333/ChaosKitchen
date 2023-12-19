using UnityEngine;


[CreateAssetMenu]
public class CuttingRecipeSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public string Name;

    public int cuttingProgressMax;

    private void OnValidate()
    {
        if (string.IsNullOrEmpty(Name))
            Name = this.name;
    }
}
