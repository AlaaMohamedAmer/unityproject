using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trashcounter : BaseCounters
{
    public override void Interact(Player player)
    {
        if(player.HasKitchenObject())
            player.GetKitchenObject().DestroySelf();
    }
}
