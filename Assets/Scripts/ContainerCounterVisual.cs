using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    [SerializeField] private Animator animator = null;
    [SerializeField] private CounterContainer counterContainer = null;
    private const string OPEN_CLOSE = "OpenClose";

    private void Reset()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        counterContainer.OnPlayerGrabbedObject += CounterContainer_OnPlayerGrabbedObject;
    }

    private void CounterContainer_OnPlayerGrabbedObject(object sender, EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }



}
