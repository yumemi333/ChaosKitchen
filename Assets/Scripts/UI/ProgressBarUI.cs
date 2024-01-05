using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject ihasProgressGameObject;
    private IHasProgress iHasProgress;
    [SerializeField] private Image barImage;


    private void Start()
    {
        ihasProgressGameObject.TryGetComponent<IHasProgress>(out iHasProgress);
        if (iHasProgress == null)
        {
            Debug.LogError($"Game Object {ihasProgressGameObject} does not have IHasProgress component");
        }
        iHasProgress.OnProgressChanged += IHasProgress_OnProgressChanged;

        barImage.fillAmount = 0;
        Hide();
    }

    private void IHasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNomalized;
        if (e.progressNomalized == 0 || e.progressNomalized >= 1)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
