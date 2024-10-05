using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static CuttingCounter;

public class StoveCounter : BaseCounters,IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangeEventArges> OnProgressChange;

    public event EventHandler<OnStateChangedEventArges> OnStateChanged;
    public class OnStateChangedEventArges : EventArgs
    {
        public State state;
    }
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned,
    }

    [SerializeField] FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] BurningRecipeSO[] burningingRecipeSOArray;


    private float fryingTimer;
    private float burnedTimer;
    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;

    private State state;

    private void Start()
    {
        state = State.Idle;
    }

    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;

                    OnProgressChange?.Invoke(this, new IHasProgress.OnProgressChangeEventArges
                    {
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingtimerMax
                    });

                    if (fryingTimer > fryingRecipeSO.fryingtimerMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnkitchenObject(fryingRecipeSO.Output, this);

                        state = State.Fried;
                        burnedTimer = 0f;
                        burningRecipeSO = GetBurningRecipewithInput(GetKitchenObject().GetKitchenObjectSO());

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArges { state = state });
                    }
                    break;
                case State.Fried:
                    burnedTimer += Time.deltaTime;

                    OnProgressChange?.Invoke(this, new IHasProgress.OnProgressChangeEventArges
                    {
                        progressNormalized = burnedTimer / burningRecipeSO.burningtimerMax
                    });

                    if (burnedTimer > burningRecipeSO.burningtimerMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnkitchenObject(burningRecipeSO.Output, this);

                        state = State.Burned;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArges { state = state });

                        OnProgressChange?.Invoke(this, new IHasProgress.OnProgressChangeEventArges
                        {
                            progressNormalized = 0f
                        });
                    }
                    break;
                case State.Burned:
                    break;

            }
        }
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasRecipeInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    fryingRecipeSO = GetFryingRecipewithInput(GetKitchenObject().GetKitchenObjectSO());

                    state = State.Frying;
                    fryingTimer = 0f;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArges { state = state });

                    OnProgressChange?.Invoke(this, new IHasProgress.OnProgressChangeEventArges { 
                        progressNormalized = fryingTimer/fryingRecipeSO.fryingtimerMax });
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

                        state = State.Idle;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArges { state = state });

                        OnProgressChange?.Invoke(this, new IHasProgress.OnProgressChangeEventArges
                        {
                            progressNormalized = 0f
                        });
                    }
                }
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);

                state = State.Idle;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArges { state = state });

                OnProgressChange?.Invoke(this, new IHasProgress.OnProgressChangeEventArges
                {
                    progressNormalized = 0f
                });
            }
        }
    }

    private bool HasRecipeInput(KitchenObjectSO inputkitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipewithInput(inputkitchenObjectSO);
        return fryingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputkitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipewithInput(inputkitchenObjectSO);
        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.Output;
        }
        else
        {
            return null;
        }
    }

    private FryingRecipeSO GetFryingRecipewithInput(KitchenObjectSO inputkitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.Input == inputkitchenObjectSO)
            {
                return fryingRecipeSO;
            }
        }
        return null;
    }

    private BurningRecipeSO GetBurningRecipewithInput(KitchenObjectSO inputkitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSO in burningingRecipeSOArray)
        {
            if (burningRecipeSO.Input == inputkitchenObjectSO)
            {
                return burningRecipeSO;
            }
        }
        return null;
    }

}
