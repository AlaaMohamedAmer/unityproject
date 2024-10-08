using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter :BaseCounters,IkhitchenObjectparent
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
  

    public override void Interact(Player player)
    {
        if(!HasKitchenObject())
        {
            if(player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }else 
            { 

            }
        }
        else
        {
            if(player.HasKitchenObject()) {
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) 
                { 
                    if (plateKitchenObject.TryAddIngradians(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else
                {
                    if(GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        if (plateKitchenObject.TryAddIngradians(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf() ;
                        }
                    }
                }
                
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

  
}
