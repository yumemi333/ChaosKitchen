using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] selectedGameObjects;

    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Instance_OnSelectedCounterChanged;
    }

    private void Instance_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if(e.SelectedCounter == baseCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        foreach (var item in selectedGameObjects)
        {
            item.SetActive(true);
        }
    }

    private void Hide()
    {
        foreach (var item in selectedGameObjects)
        {
            item.SetActive(false);
        }
    }
}
