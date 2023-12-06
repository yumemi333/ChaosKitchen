using UnityEngine;

[CreateAssetMenu()]
public class KitchenObjectScriptableObject : ScriptableObject
{
    public Transform Prefab;
    public Sprite Sprite;
    public KitchenObjectType KitchenObjectType;
}
