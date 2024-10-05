using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounters
{
    public event EventHandler OnPlateSpawn;
    public event EventHandler OnPlateRemove;

    [SerializeField] private KitchenObjectSO plateObjectSO;

    private float spawnPlayTimer;
    private float spawnPlayTimerMax= 5f;
    private int plateSpawnAmount;
    private int plateSpawnAmountMax= 4;

    private void Update()
    {
        spawnPlayTimer += Time.deltaTime;
        if (spawnPlayTimer > spawnPlayTimerMax)
        {
            KitchenObject.SpawnkitchenObject(plateObjectSO,this);

            spawnPlayTimer = 0;
            if (plateSpawnAmount < spawnPlayTimerMax)
            {
                plateSpawnAmount++;

                OnPlateSpawn?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if (plateSpawnAmount > 0)
            {
                plateSpawnAmount--;
                KitchenObject.SpawnkitchenObject(plateObjectSO, player);
                OnPlateRemove?.Invoke(this, EventArgs.Empty);

            }
        }
    }
}
