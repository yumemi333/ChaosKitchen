using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeUI : MonoBehaviour
{
    [SerializeField] private GameObject correct = null;
    [SerializeField] private GameObject wrong = null;

    private float showTimer = 0f;

    private float showTimeMax = 1f;

    private bool showing = false;

    private void Start()
    {
        DeliveryHandler.Instance.OnRecipeCompleted += Instance_OnRecipeCompleted;
        DeliveryHandler.Instance.OnRecipeWrong += Instance_OnRecipeWrong; ;
    }

    private void Instance_OnRecipeWrong(object sender, System.EventArgs e)
    {
        SetWrong(true);
    }

    private void Instance_OnRecipeCompleted(object sender, System.EventArgs e)
    {
        SetCorrect(true);
    }

    private void Update()
    {
        if (showing)
        {
            showTimer += Time.deltaTime;
            if (showTimer >= showTimeMax)
            {
                showTimer = 0;
                wrong.SetActive(false);
                correct.SetActive(false);
                showing = false;
            }
        }
    }

    private void SetCorrect(bool active)
    {
        wrong.SetActive(false);

        correct.SetActive(active);
        showing = active;
    }

    private void SetWrong(bool active)
    {
        correct.SetActive(false);

        wrong.SetActive(active);
        showing = active;
    }
}
