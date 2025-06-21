using MelonLoader;
using UnityEngine;
using System.Reflection;
using System;
using Il2Cpp;
using Il2CppWeapon;

namespace BananaShooterMod
{
    public class Weapons
    {
        private Firearms GetActiveFirearm()
        {
            Firearms[] allWeapons = UnityEngine.Object.FindObjectsOfType<Firearms>();

            foreach (Firearms weapon in allWeapons)
            {
                if (weapon.enabled && weapon.gameObject.activeInHierarchy)
                {
                    return weapon;
                }
            }

            return null;
        }


        public int GetCurrentBulletCount()
        {
            try
            {
                Firearms weaponInstance = GetActiveFirearm();
                if (weaponInstance == null) return -1; 

                Type targetType = typeof(Firearms);
                PropertyInfo bulletCountProperty = targetType.GetProperty("bulletCount",
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                if (bulletCountProperty == null) return -1;

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
                Firearms weaponInstance = GetActiveFirearm();
                if (weaponInstance == null) return;

                Type targetType = typeof(Firearms);
                PropertyInfo bulletCountProperty = targetType.GetProperty("bulletCount",
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                if (bulletCountProperty == null) return;

                bulletCountProperty.SetValue(weaponInstance, newCount);
            }
            catch (Exception e)
            {
                MelonLogger.Error($"Error in SetBulletCount: {e.Message}");
            }
        }

        public void RefillAndSetMaxAmmo(int amount)
        {
            try
            {
                Firearms weaponInstance = GetActiveFirearm();
                if (weaponInstance == null) return;

                Type firearmsType = typeof(Firearms);

                // Set maxAmmo
                PropertyInfo maxAmmoProperty = firearmsType.GetProperty("maxAmmo",
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                if (maxAmmoProperty != null)
                {
                    maxAmmoProperty.SetValue(weaponInstance, amount);
                }

                // Set currentAmmo
                PropertyInfo currentAmmoProperty = firearmsType.GetProperty("currentAmmo",
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                Type ammoType = typeof(HKGNDGAMPGB);
                ConstructorInfo ammoConstructor = ammoType.GetConstructor(new Type[] { typeof(int) });

                if (currentAmmoProperty != null && ammoConstructor != null)
                {
                    object newAmmoObject = ammoConstructor.Invoke(new object[] { amount });
                    currentAmmoProperty.SetValue(weaponInstance, newAmmoObject);
                }
            }
            catch (Exception e)
            {
                MelonLogger.Error($"An error occurred in RefillAndSetMaxAmmo: {e.Message}");
            }
        }
        public float GetCurrentFireRate()
        {
            try
            {
                Firearms weaponInstance = GetActiveFirearm();
                if (weaponInstance == null) return -1f;

                Type firearmsType = typeof(Firearms);
                PropertyInfo fireRateProperty = firearmsType.GetProperty("fireRate",
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                if (fireRateProperty == null) return -1f;

                return (float)fireRateProperty.GetValue(weaponInstance);
            }
            catch (Exception e)
            {
                MelonLogger.Error($"Error in GetCurrentFireRate: {e.Message}");
                return -1f;
            }
        }

        public void SetFireRate(float newRate)
        {
            try
            {
                Firearms weaponInstance = GetActiveFirearm();
                if (weaponInstance == null) return;

                Type firearmsType = typeof(Firearms);
                PropertyInfo fireRateProperty = firearmsType.GetProperty("fireRate",
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                if (fireRateProperty == null) return;

                fireRateProperty.SetValue(weaponInstance, newRate);
            }
            catch (Exception e)
            {
                MelonLogger.Error($"Error in SetFireRate: {e.Message}");
            }
        }

        public float GetCurrentReloadTime()
        {
            try
            {
                Firearms weaponInstance = GetActiveFirearm();
                if (weaponInstance == null) return -1f;

                Type firearmsType = typeof(Firearms);
                PropertyInfo reloadTimeProperty = firearmsType.GetProperty("reloadTime",
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                if (reloadTimeProperty == null) return -1f;

                return (float)reloadTimeProperty.GetValue(weaponInstance);
            }
            catch (Exception e)
            {
                MelonLogger.Error($"Error in GetCurrentReloadTime: {e.Message}");
                return -1f;
            }
        }

        public void SetReloadTime(float newTime)
        {
            try
            {
                Firearms weaponInstance = GetActiveFirearm();
                if (weaponInstance == null) return;

                Type firearmsType = typeof(Firearms);
                var flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

                PropertyInfo normalReloadTimeProperty = firearmsType.GetProperty("normalReloadTime", flags);
                if (normalReloadTimeProperty != null)
                {
                    normalReloadTimeProperty.SetValue(weaponInstance, newTime);
                }
                else
                {
                    MelonLogger.Warning("Could not find 'normalReloadTime' property to set.");
                }

                PropertyInfo reloadTimeProperty = firearmsType.GetProperty("reloadTime", flags);
                if (reloadTimeProperty != null)
                {
                    reloadTimeProperty.SetValue(weaponInstance, newTime);
                }
                else
                {
                    MelonLogger.Warning("Could not find 'reloadTime' property to set.");
                }
            }
            catch (Exception e)
            {
                MelonLogger.Error($"Error in SetReloadTime: {e.Message}");
            }
        }
    }
}