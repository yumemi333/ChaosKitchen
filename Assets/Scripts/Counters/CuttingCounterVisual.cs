using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    [SerializeField] private Animator animator = null;
    [SerializeField] private CuttingCounter cuttingCounter = null;

    private void Reset()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        cuttingCounter.OnCut += CounterContainer_OnPlayerGrabbedObject;
    }

    private void CounterContainer_OnPlayerGrabbedObject(object sender, EventArgs e)
    {
        animator.SetTrigger("Cut");
    }

}
