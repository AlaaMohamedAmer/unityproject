using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTemplet;

    private void Awake()
    {
        iconTemplet.gameObject.SetActive(false);
    }

    private void Start()
    {
        plateKitchenObject.OnIngradientAdded += PlateKitchenObject_OnIngradientAdded;
    }

    private void PlateKitchenObject_OnIngradientAdded(object sender, PlateKitchenObject.OnIngradientAddedEventArges e)
    {
        UpdateVisual();
    }

    public void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            if (child == iconTemplet) continue;
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO kitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
        {
            // Instantiate the icon template and set it active
            Transform iconTransform = Instantiate(iconTemplet, transform);
            iconTransform.gameObject.SetActive(true);

            // Set the icon to represent the correct ingredient
            iconTransform.GetComponent<PlateIconsSingleUI>().SetKitchenObjectSO(kitchenObjectSO);
        }
    }
}

