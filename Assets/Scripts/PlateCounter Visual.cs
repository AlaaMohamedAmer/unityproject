using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform CounterTopPoint;
    [SerializeField] private Transform platesVisualPrevab;

    private List<GameObject> plateVisualGameObjectList;

    private void Awake()
    {
        plateVisualGameObjectList= new List<GameObject>();
    }

    public void Start()
    {
        platesCounter.OnPlateSpawn += PlatesCounter_OnPlateSpawn;
        platesCounter.OnPlateRemove += PlatesCounter_OnPlateRemove;
    }

    private void PlatesCounter_OnPlateRemove(object sender, System.EventArgs e)
    {
        GameObject plateGameopject = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
        plateVisualGameObjectList.Remove(plateGameopject);
        Destroy(plateGameopject);
    }

    private void PlatesCounter_OnPlateSpawn(object sender, System.EventArgs e)
    {
        Transform PlatVisualTransform = Instantiate(platesVisualPrevab, CounterTopPoint);

        float plateOffsetY = .1f;
        PlatVisualTransform.localPosition= new Vector3(0, plateOffsetY* plateVisualGameObjectList.Count, 0);

        plateVisualGameObjectList.Add(PlatVisualTransform.gameObject);
    }
}
