using MelonLoader;
using UnityEngine;
using System.Reflection;
using System;
using Il2CppMovement;
using Il2CppWeapon;

namespace BananaShooterMod
{
    public class PlayerMovementHandler
    {
        public void SetJumpFactor(float newFactor)
        {
            try
            {
                Type playerMovementType = typeof(PlayerMovement);
                PropertyInfo jumpFactorProperty = playerMovementType.GetProperty("jumpFactor",
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                if (jumpFactorProperty == null) return;

                PlayerMovement playerInstance = UnityEngine.Object.FindObjectOfType<PlayerMovement>();
                if (playerInstance == null) return;

                jumpFactorProperty.SetValue(playerInstance, newFactor);
            }
            catch (Exception e)
            {
                MelonLogger.Error($"Error in SetJumpFactor: {e.Message}");
            }
        }
        public float GetCurrentJumpFactor()
        {
            try
            {
                Type playerMovementType = typeof(PlayerMovement);
                PropertyInfo jumpFactorProperty = playerMovementType.GetProperty("jumpFactor",
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                if (jumpFactorProperty == null) return -1f; // Return -1 to indicate an error

                PlayerMovement playerInstance = UnityEngine.Object.FindObjectOfType<PlayerMovement>();
                if (playerInstance == null) return -1f;

                return (float)jumpFactorProperty.GetValue(playerInstance);
            }
            catch (Exception e)
            {
                MelonLogger.Error($"Error in GetCurrentJumpFactor: {e.Message}");
                return -1f;
            }
        }
        public int GetCurrentJumpCount()
        {
            try
            {
                Type playerMovementType = typeof(PlayerMovement);
                PropertyInfo maxJumpCountProperty = playerMovementType.GetProperty("maxJumpCount",
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                if (maxJumpCountProperty == null) return -1; // Return -1 on error

                PlayerMovement playerInstance = UnityEngine.Object.FindObjectOfType<PlayerMovement>();
                if (playerInstance == null) return -1;

                return (int)maxJumpCountProperty.GetValue(playerInstance);
            }
            catch (Exception e)
            {
                MelonLogger.Error($"Error in GetCurrentJumpCount: {e.Message}");
                return -1;
            }
        }

        public void SetJumpCount(int newCount)
        {
            try
            {
                Type playerMovementType = typeof(PlayerMovement);
                PropertyInfo maxJumpCountProperty = playerMovementType.GetProperty("maxJumpCount",
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                if (maxJumpCountProperty == null) return;

                PlayerMovement playerInstance = UnityEngine.Object.FindObjectOfType<PlayerMovement>();
                if (playerInstance == null) return;

                maxJumpCountProperty.SetValue(playerInstance, newCount);
            }
            catch (Exception e)
            {
                MelonLogger.Error($"Error in SetJumpCount: {e.Message}");
            }
        }

        public float GetCurrentMovementSpeed()
        {
            try
            {
                Type playerMovementType = typeof(PlayerMovement);
                PropertyInfo moveSpeedFactorProperty = playerMovementType.GetProperty("moveSpeedFactor",
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                if (moveSpeedFactorProperty == null) return -1f;

                PlayerMovement playerInstance = UnityEngine.Object.FindObjectOfType<PlayerMovement>();
                if (playerInstance == null) return -1f;

                return (float)moveSpeedFactorProperty.GetValue(playerInstance);
            }
            catch (Exception e)
            {
                // Fixed the error message here too
                MelonLogger.Error($"Error in GetCurrentMovementSpeed: {e.Message}");
                return -1f;
            }
        }

        public void SetMovementSpeed(float newSpeed)
        {
            try
            {
                Type playerMovementType = typeof(PlayerMovement);
                PropertyInfo moveSpeedFactorProperty = playerMovementType.GetProperty("moveSpeedFactor",
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                if (moveSpeedFactorProperty == null) return;

                PlayerMovement playerInstance = UnityEngine.Object.FindObjectOfType<PlayerMovement>();
                if (playerInstance == null) return;

                // The parameter is now a float, which is correct
                moveSpeedFactorProperty.SetValue(playerInstance, newSpeed);
            }
            catch (Exception e)
            {
                // Fixed the error message here too
                MelonLogger.Error($"Error in SetMovementSpeed: {e.Message}");
            }
        }

        public void GetAndSetMovementSpeedFactor()
        {
            try
            {
                Type playerMovementType = typeof(PlayerMovement);

                PropertyInfo moveSpeedFactorProperty = playerMovementType.GetProperty("moveSpeedFactor",
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                if (moveSpeedFactorProperty == null)
                {
                    MelonLogger.Error("Could not find the 'moveSpeedFactorProperty' property in PlayerMovement!");
                    return;
                }

                PlayerMovement playerInstance = UnityEngine.Object.FindObjectOfType<PlayerMovement>();
                if (playerInstance == null)
                {
                    MelonLogger.Error("Could not find a live instance of PlayerMovement!");
                    return;
                }

                float currentValue = (float)moveSpeedFactorProperty.GetValue(playerInstance);
                MelonLogger.Msg($"Current moveSpeedFactorProperty is: {currentValue}");

                moveSpeedFactorProperty.SetValue(playerInstance, 20);
                MelonLogger.Msg("Max Movement Speed set to 20!");
            }
            catch (Exception e)
            {
                MelonLogger.Error($"An error occurred during reflection: {e.Message}");
            }
        }
        public void GetAndSetPlayerName()
        {
            try
            {
                Type playerMovementType = typeof(PlayerMovement);
                PropertyInfo nameProperty = playerMovementType.GetProperty("name",
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                if (nameProperty == null)
                {
                    MelonLogger.Error("Could not find the 'name' property in PlayerMovement!");
                    return;
                }
                PlayerMovement playerInstance = UnityEngine.Object.FindObjectOfType<PlayerMovement>();
                if (playerInstance == null)
                {
                    MelonLogger.Error("Could not find a live instance of PlayerMovement!");
                    return;
                }
                string currentName = (string)nameProperty.GetValue(playerInstance);
                MelonLogger.Msg($"Current player name is: {currentName}");
                nameProperty.SetValue(playerInstance, "Modded Banana");
                MelonLogger.Msg("Player name has been changed!");
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

                FieldInfo[] fields = playerMovementType.GetFields(flags);
                MelonLogger.Msg($"--- Fields ({fields.Length}) ---");
                foreach (FieldInfo field in fields)
                {
                    MelonLogger.Msg($"Found Field: {field.Name} (Type: {field.FieldType.Name})");
                }

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
       

        public void DumpFirearmsInfo()
        {
            try
            {
                MelonLogger.Msg("==========================================");
                MelonLogger.Msg("Dumping members of firearms class...");

                // The only change is the class name inside typeof()
                Type targetType = typeof(Firearms);
                var flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;

                FieldInfo[] fields = targetType.GetFields(flags);
                MelonLogger.Msg($"--- Fields ({fields.Length}) ---");
                foreach (FieldInfo field in fields)
                {
                    MelonLogger.Msg($"Found Field: {field.Name} (Type: {field.FieldType.Name})");
                }

                PropertyInfo[] properties = targetType.GetProperties(flags);
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