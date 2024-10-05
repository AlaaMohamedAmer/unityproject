using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngradientAddedEventArges> OnIngradientAdded;
    public class OnIngradientAddedEventArges:EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }

    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;

    private List<KitchenObjectSO> KitchenObjectSOList;


    private void Awake()
    {
        KitchenObjectSOList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngradians(KitchenObjectSO kitchenObjectSO)
    {
        if (!validKitchenObjectSOList.Contains(kitchenObjectSO)) return false;

        if (KitchenObjectSOList.Contains(kitchenObjectSO)) return false;
        else
        {
            KitchenObjectSOList.Add(kitchenObjectSO);

            OnIngradientAdded?.Invoke(this, new OnIngradientAddedEventArges
            {
                kitchenObjectSO = kitchenObjectSO
            });

            return true;
        }
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return KitchenObjectSOList;
    }

}
