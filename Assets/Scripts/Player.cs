using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour,IkhitchenObjectparent
{
    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterEventChangedArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterEventChangedArgs : EventArgs
    {
        public BaseCounters SelectedCounter;
    }

    [SerializeField] private float spead = 7f;
    [SerializeField] private GameInput GameInput;
    [SerializeField] private LayerMask CounterLayerMask;
    [SerializeField] private Transform KitchenObjectHoldPoint;


    private bool isWalking;
    private Vector3 lastInteractionDir;
    private BaseCounters SelectedCounter;
    private KitchenObject KitchenObject;


    public void Awake()
    {
        Instance = this;
        
    }
    public void Start()
    {
        GameInput.OnInteractAcion += GameInput_OnInteraction;
        GameInput.OnInteractAlternateAcion += GameInput_OnInteractAlternateAcion;
    }

    private void GameInput_OnInteractAlternateAcion(object sender, EventArgs e)
    {
        if (SelectedCounter != null)
        {
            SelectedCounter.InteractAlternate(this);
        }
    }

    private void GameInput_OnInteraction(object sender, System.EventArgs e)
    {
        if (SelectedCounter != null)
        {
            SelectedCounter.Interact(this);
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteraction();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleInteraction()
    {
        Vector2 InputVector = GameInput.GetMovementVectorNormalize();
        Vector3 moveDir = new Vector3(InputVector.x, 0f, InputVector.y);

        if(moveDir != Vector3.zero)
        {
            lastInteractionDir = moveDir;
        }

        float interactDistance = 2f;

        if( Physics.Raycast(transform.position,lastInteractionDir, out RaycastHit raycastHit, interactDistance,CounterLayerMask))
        {
            if(raycastHit.transform.TryGetComponent(out BaseCounters baseCounter))
            {
                if (baseCounter != SelectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }
    private void HandleMovement()
    {
        Vector2 InputVector = GameInput.GetMovementVectorNormalize();
        Vector3 moveDir = new Vector3(InputVector.x, 0f, InputVector.y);

        float moveDistance = spead * Time.deltaTime;
        float playerRedius = .7f;
        float playerhight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerhight, playerRedius, moveDir, moveDistance);

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove =moveDir.x !=0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerhight, playerRedius, moveDirX, moveDistance);

            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(moveDir.z, 0, 0).normalized;
                canMove =moveDir.z !=0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerhight, playerRedius, moveDirZ, moveDistance);

                if (canMove)
                {
                    moveDir = moveDirZ;
                }
                else
                {

                }
            }
        }
        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }
        float rotspead = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotspead);

        isWalking = moveDir != Vector3.zero;
    }

    public void SetSelectedCounter(BaseCounters SelectedCounter)
    {
        this.SelectedCounter = SelectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterEventChangedArgs { SelectedCounter = SelectedCounter }) ;
    }

    public Transform GetKitchenObjectfollowtransform()
    {
        return KitchenObjectHoldPoint;
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
