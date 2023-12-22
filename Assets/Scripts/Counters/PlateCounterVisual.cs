using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{
    [SerializeField] private PlateCounter plateCounter;
    [SerializeField] private Transform counterTop;
    [SerializeField] private Transform plateVisualPrefab;

    private Stack<GameObject> plateVisualGameObjectList = new Stack<GameObject>();

    public void Start()
    {
        plateCounter.OnSpawnEvent += PlatesCounter_OnPlateSpawned;
        plateCounter.OnPlateRemovedEvent += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateRemoved(object sender, EventArgs e)
    {
       var prefab =  plateVisualGameObjectList.Pop();
        Destroy(prefab);
    }

    private void PlatesCounter_OnPlateSpawned(object sender, System.EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTop);
        float plateOffsetY = 0.1f;
        plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * plateVisualGameObjectList.Count, 0);
        plateVisualGameObjectList.Push(plateVisualTransform.gameObject);
    }

}
