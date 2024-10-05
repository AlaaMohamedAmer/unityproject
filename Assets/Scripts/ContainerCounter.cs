using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ContainerCounter : BaseCounters,IkhitchenObjectparent
{
    public event EventHandler OnPlayerGrabbedobject;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;


    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            KitchenObject.SpawnkitchenObject(kitchenObjectSO, player);

            OnPlayerGrabbedobject?.Invoke(this, EventArgs.Empty);
        }
    }  
}
