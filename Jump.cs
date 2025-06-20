using MelonLoader;
using UnityEngine;
using System.Reflection;
using System;
using Il2CppMovement;

namespace BananaShooterMod
{
    public class Jump
    {
        public void GetAndSetMaxJump()
        {
            try
            {
                Type playerMovementType = typeof(PlayerMovement);

                // --- THE FIX ---
                // We are now using GetProperty instead of GetField.
                PropertyInfo jumpFactorProperty = playerMovementType.GetProperty("jumpFactor",
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                if (jumpFactorProperty == null)
                {
                    // This error shouldn't happen now, but it's good practice to keep it.
                    MelonLogger.Error("Could not find the 'jumpFactor' property in PlayerMovement!");
                    return;
                }

                PlayerMovement playerInstance = UnityEngine.Object.FindObjectOfType<PlayerMovement>();
                if (playerInstance == null)
                {
                    MelonLogger.Error("Could not find a live instance of PlayerMovement!");
                    return;
                }

                // Get the current value from the Property
                float currentValue = (float)jumpFactorProperty.GetValue(playerInstance);
                MelonLogger.Msg($"Current jump factor is: {currentValue}");

                // Set the new value on the Property
                jumpFactorProperty.SetValue(playerInstance, 20f);
                MelonLogger.Msg("Jump factor set to 20!");
            }
            catch (Exception e)
            {
                MelonLogger.Error($"An error occurred during reflection: {e.Message}");
            }
        }
    

        public void DumpPlayerMovementInfo()
        {
            try
            {
                MelonLogger.Msg("==========================================");
                MelonLogger.Msg("Dumping members of PlayerMovement class...");

                Type playerMovementType = typeof(PlayerMovement);
                var flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;

                // Get all Fields
                FieldInfo[] fields = playerMovementType.GetFields(flags);
                MelonLogger.Msg($"--- Fields ({fields.Length}) ---");
                foreach (FieldInfo field in fields)
                {
                    MelonLogger.Msg($"Found Field: {field.Name} (Type: {field.FieldType.Name})");
                }

                // Get all Properties
                PropertyInfo[] properties = playerMovementType.GetProperties(flags);
                MelonLogger.Msg($"--- Properties ({properties.Length}) ---");
                foreach (PropertyInfo property in properties)
                {
                    MelonLogger.Msg($"Found Property: {property.Name} (Type: {property.PropertyType.Name})");
                }

                MelonLogger.Msg("==========================================");
            }
            catch (Exception e)
            {
                MelonLogger.Error($"An error occurred during dumper: {e.Message}");
            }
        }
    }
}