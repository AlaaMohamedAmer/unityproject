using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO KitchenObjectSO;

    private IkhitchenObjectparent KitchenObjectParent;
    public KitchenObjectSO GetKitchenObjectSO()
    { 
        return KitchenObjectSO;
    }

    public void SetKitchenObjectParent(IkhitchenObjectparent kitchenObjectParent)
    {
         if(this.KitchenObjectParent != null)
         {
            this.KitchenObjectParent.ClearKitchenObject();
         }

         if (kitchenObjectParent.HasKitchenObject()) 
         {
            Debug.Log("Error");
         }

         this.KitchenObjectParent = kitchenObjectParent;
         KitchenObjectParent.SetKitchenObject(this); 


         transform.parent= KitchenObjectParent.GetKitchenObjectfollowtransform();
         transform.localPosition = Vector3.zero;
    }

    public IkhitchenObjectparent GetKitchenObjectParent()
    { 
        return KitchenObjectParent; 
    }

    public void DestroySelf()
    {
        KitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }
    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if(this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else
        {
            plateKitchenObject=null;
            return false;
        }
    }

    public static KitchenObject SpawnkitchenObject(KitchenObjectSO kitchenObjectSO, IkhitchenObjectparent ikhitchenObjectparent)
    {
        Transform KitchenObjecttransform = Instantiate(kitchenObjectSO.prefab);

        KitchenObject kitchenObject = KitchenObjecttransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(ikhitchenObjectparent);

        return kitchenObject;
    }

}
