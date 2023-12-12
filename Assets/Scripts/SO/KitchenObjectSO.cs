using UnityEngine;

[CreateAssetMenu()]
public class KitchenObjectSO : ScriptableObject
{
    public Transform Prefab;
    public Sprite Sprite;
    public string Name;

    private void OnValidate()
    {
        if (string.IsNullOrEmpty(Name))
            Name = this.name;
    }
}
