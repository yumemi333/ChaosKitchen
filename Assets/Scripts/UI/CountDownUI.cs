using TMPro;
using UnityEngine;

public class CountDownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countDown = null;

    private void Start()
    {
        KitchenGameManager.Instance.OnStateChanged += Instance_OnStateChanged;
    }

    private void Update()
    {
        countDown.text = $"{Mathf.Ceil(KitchenGameManager.Instance.GetCountdownToStartTimer())}";
    }

    private void Instance_OnStateChanged(object sender, System.EventArgs e)
    {
        if (KitchenGameManager.Instance.IsCountdownToStartActive)
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
        countDown.gameObject.SetActive(true);
    }

    private void Hide()
    {
        countDown.gameObject.SetActive(false);
    }
}
