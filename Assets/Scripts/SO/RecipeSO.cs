using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class RecipeSO : ScriptableObject
{
    public List<KitchenObjectSO> kitchenObjectSOList;
    public string Name;
    private void OnValidate()
    {
        if (string.IsNullOrEmpty(Name))
            Name = this.name;
    }
}
