using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectOS_GameObject
    {
        public GameObject gameObject;
        public KitchenObjectSO kitchenObjectSO;
    }

    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenObjectOS_GameObject> kitchenObjectOSGameObjectsList;
    private void Start()
    {
        plateKitchenObject.OnIngradientAdded += PlateKitchenObject_OnIngradientAdded;
        foreach (KitchenObjectOS_GameObject kitchenObjectOSGameObjects in kitchenObjectOSGameObjectsList)
        {
                kitchenObjectOSGameObjects.gameObject.SetActive(false);
        }
    }

    private void PlateKitchenObject_OnIngradientAdded(object sender, PlateKitchenObject.OnIngradientAddedEventArges e)
    {
        foreach(KitchenObjectOS_GameObject kitchenObjectOSGameObjects in kitchenObjectOSGameObjectsList)
        {
            if(kitchenObjectOSGameObjects.kitchenObjectSO == e.kitchenObjectSO)
            {
                kitchenObjectOSGameObjects.gameObject.SetActive(true);
            }
        }
    }
}
