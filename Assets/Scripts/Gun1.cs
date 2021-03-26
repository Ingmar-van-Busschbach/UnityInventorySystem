using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun1 : WeaponInventory
{
    protected override void OnShoot(string weapon)
    {
        Debug.Log("yeet");
        WriteDebug(weaponInventory[weapon] + " ammo left for " + weapon);
    }
}
