using UnityEngine;
using UnityEngine.UI;

public class PlateIconSingleUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;

    public void SetKitchenObjectSO(KitchenObjectSO kitchenObjectSO)
    {
        iconImage.sprite = kitchenObjectSO.Sprite;
        this.gameObject.SetActive(true);
    }
}

