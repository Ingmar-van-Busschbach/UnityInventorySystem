using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    //Player's inventory for weapons and ammo ammount
    protected Dictionary<string, int> weaponInventory = new Dictionary<string, int>
    {
        {"Rifle", 10 },
        {"SMG", 10 },
        {"Pistol", 10 },
        {"Sniper", 10 },
        {"Minigun", 10 },
        {"Revolver", 10 },
        {"LMG", 10 },
        {"Rocket Launcher", 10 }
    };

    //This array's order determines what key is bound to what gun. weaponsSelected[index] is called when index+1 is pressed. as such, the maximum length of this array is 9.
    private List<string> weaponsSelected = new List<string>() { "Rifle", "LMG", "SMG", "Pistol", "Rocket Launcher", "Sniper" };
    [SerializeField] private string weapon = "Rifle"; //Default selected weapon = "Rifle"
    [SerializeField] private string shootButton = "space";
    [SerializeField] private bool writeDebug = true; //Whether to write all actions to console
    
    void Update()
    {
        if (Input.GetKeyDown(shootButton))
        {
            Shoot(weapon);
        }
        if (Input.GetKeyDown("r"))
        {
            Pickup("Shotgun", 5, true);
        }
        for (int i = 1; i <= Math.Min(weaponsSelected.Count, 9); i++) //Forces the highest loop to be <=9, to make sure there never is any index out of bounds for the array
        {
            string input = i.ToString();
            if (Input.GetKeyDown(input))
            {
                SwitchWeapon(weaponsSelected[i-1]);
            }
        }
    }

    public void Pickup(string weapon, int ammo = 0, bool addToSelection = false)
    {
        try
        {
            weaponInventory.Add(weapon, ammo);
            WriteDebug(weapon + " added");
            WriteDebug(weapon + " has " + weaponInventory[weapon] + " ammo left");
            if (addToSelection && !weaponsSelected.Contains(weapon))
            {
                weaponsSelected.Add(weapon);
            }
        }
        catch (ArgumentException)
        {
            weaponInventory[weapon] += ammo;
            WriteDebug(ammo + " ammo picked up for " + weapon);
            WriteDebug(weapon + " has " + weaponInventory[weapon] + " ammo left");
        }
    }

    private void Shoot(string weapon)
    {
        if(weaponInventory[weapon] <= 0)
        {
            WriteDebug("Out of ammo!");
            return;
        }
        weaponInventory[weapon] -= 1;
        OnShoot(weapon);
    }

    protected virtual void OnShoot(string weapon)
    {
        WriteDebug("Pew");
        WriteDebug(weaponInventory[weapon] + " ammo left for " + weapon);
    }

    protected virtual void SwitchWeapon(string inWeapon)
    {
        weapon = inWeapon;
        WriteDebug("weapon has been switched to " + weapon);
        WriteDebug(weaponInventory[weapon] + " ammo left for " + weapon);
    }

    //Debug Functions
    protected void WriteDebug(string DebugText, bool forceDebug = false) //Force debug can let you debug a single action
    {
        if (!writeDebug && !forceDebug)
        {
            return;
        }
        Debug.Log(DebugText);
    }
}