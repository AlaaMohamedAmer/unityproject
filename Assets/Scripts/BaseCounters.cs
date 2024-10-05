using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounters : MonoBehaviour,IkhitchenObjectparent
{

    [SerializeField] private Transform CounterTopPoint;

    private KitchenObject KitchenObject;

    public virtual void Interact(Player player)
    {
        Debug.LogError("Base Counter.Interact();");
    }
    public virtual void InteractAlternate(Player player)
    {
        //Debug.LogError("Base Counter.Interact();");
    }

    public Transform GetKitchenObjectfollowtransform()
    {
        return CounterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.KitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return KitchenObject;
    }

    public void ClearKitchenObject()
    {
        KitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return KitchenObject != null;
    }
}
