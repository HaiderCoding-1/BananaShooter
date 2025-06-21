using MelonLoader;
using UnityEngine;
using System.Reflection;
using System;
using Il2CppWeapon;
// You may need a 'using' statement for the namespace containing 'firearms'
// using Il2CppWeaponList; 

namespace BananaShooterMod
{
    public class Weapons
    {
        public int GetCurrentBulletCount()
        {
            try
            {
                // We assume the property is on the '' class
                Type targetType = typeof(Firearms);
                PropertyInfo bulletCountProperty = targetType.GetProperty("bulletCount",
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                if (bulletCountProperty == null)
                {
                    MelonLogger.Error("Could not find 'bulletCount' property on firearms class.");
                    return -1;
                }

                // FindObjectOfType will find the currently active firearm component
                Firearms weaponInstance = UnityEngine.Object.FindObjectOfType<Firearms>();
                if (weaponInstance == null)
                {
                    // This is normal if you don't have a weapon out
                    return -1;
                }

                return (int)bulletCountProperty.GetValue(weaponInstance);
            }
            catch (Exception e)
            {
                MelonLogger.Error($"Error in GetCurrentBulletCount: {e.Message}");
                return -1;
            }
        }

        public void SetBulletCount(int newCount)
        {
            try
            {
                Type targetType = typeof(Firearms);
                PropertyInfo bulletCountProperty = targetType.GetProperty("bulletCount",
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                if (bulletCountProperty == null) return;
                Firearms weaponInstance = UnityEngine.Object.FindObjectOfType<Firearms>();
                if (weaponInstance == null) return;

                bulletCountProperty.SetValue(weaponInstance, newCount);
            }
            catch (Exception e)
            {
                MelonLogger.Error($"Error in SetBulletCount: {e.Message}");
            }
        }
    }
}