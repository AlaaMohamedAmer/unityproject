using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CuttingCounter : BaseCounters,IHasProgress
{
    public event EventHandler OnCut;
    public event EventHandler<IHasProgress.OnProgressChangeEventArges> OnProgressChange;


    [SerializeField] private CuttingObjectSO[] CutObjectSOArray;

    private int cuttingProgress ;
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasRecipeInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;


                    CuttingObjectSO cuttingObjectSO = GetCuttingRecipewithInput(GetKitchenObject().GetKitchenObjectSO());
                    OnProgressChange.Invoke(this, new IHasProgress.OnProgressChangeEventArges
                    {
                        progressNormalized = (float)cuttingProgress /cuttingObjectSO.CuttingProgressMax
                    });
                }
            }
            else
            {
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngradians(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if(HasKitchenObject()&& HasRecipeInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            cuttingProgress++;

            OnCut?.Invoke(this,EventArgs.Empty);

            CuttingObjectSO cuttingObjectSO = GetCuttingRecipewithInput(GetKitchenObject().GetKitchenObjectSO());
            OnProgressChange.Invoke(this, new IHasProgress.OnProgressChangeEventArges
            {
                progressNormalized = (float)cuttingProgress / cuttingObjectSO.CuttingProgressMax
            });


            if (cuttingProgress >= cuttingObjectSO.CuttingProgressMax)
            {
                KitchenObjectSO kitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnkitchenObject(kitchenObjectSO, this);
            }
           
        }
    }

    private bool HasRecipeInput(KitchenObjectSO inputkitchenObjectSO)
    {
        CuttingObjectSO cuttingObjectSO = GetCuttingRecipewithInput(inputkitchenObjectSO);
        return cuttingObjectSO != null;
    }

    private KitchenObjectSO GetOutputForInput (KitchenObjectSO inputkitchenObjectSO)
    {
        CuttingObjectSO cuttingObjectSO = GetCuttingRecipewithInput(inputkitchenObjectSO);
        if(cuttingObjectSO != null)
        {
            return cuttingObjectSO.Output;
        }
        else
        {
            return null;
        }
    }

    private CuttingObjectSO GetCuttingRecipewithInput(KitchenObjectSO inputkitchenObjectSO) 
    {
        foreach (CuttingObjectSO cuttingObjectSO in CutObjectSOArray)
        {
            if (cuttingObjectSO.Input == inputkitchenObjectSO)
            {
                return cuttingObjectSO;
            }
        }
        return null;
    }

}
