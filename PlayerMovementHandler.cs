using MelonLoader;
using UnityEngine;
using System.Reflection;
using System;
using Il2CppMovement;

namespace BananaShooterMod
{
    public class PlayerMovementHandler
    {
        public void GetAndSetMaxJump()
        {
            try
            {
                Type playerMovementType = typeof(PlayerMovement);

                PropertyInfo jumpFactorProperty = playerMovementType.GetProperty("jumpFactor",
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                if (jumpFactorProperty == null)
                {
                    MelonLogger.Error("Could not find the 'jumpFactor' property in PlayerMovement!");
                    return;
                }

                PlayerMovement playerInstance = UnityEngine.Object.FindObjectOfType<PlayerMovement>();
                if (playerInstance == null)
                {
                    MelonLogger.Error("Could not find a live instance of PlayerMovement!");
                    return;
                }

                float currentValue = (float)jumpFactorProperty.GetValue(playerInstance);
                MelonLogger.Msg($"Current jump factor is: {currentValue}");

                jumpFactorProperty.SetValue(playerInstance, 20f);
                MelonLogger.Msg("Jump factor set to 20!");
            }
            catch (Exception e)
            {
                MelonLogger.Error($"An error occurred during reflection: {e.Message}");
            }
        }

        public void GetAndSetMaxJumpCount()
        {
            try
            {
                Type playerMovementType = typeof(PlayerMovement);

                PropertyInfo maxJumpCountProperty = playerMovementType.GetProperty("maxJumpCount",
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                if (maxJumpCountProperty == null)
                {
                    MelonLogger.Error("Could not find the 'maxJumpCount' property in PlayerMovement!");
                    return;
                }

                PlayerMovement playerInstance = UnityEngine.Object.FindObjectOfType<PlayerMovement>();
                if (playerInstance == null)
                {
                    MelonLogger.Error("Could not find a live instance of PlayerMovement!");
                    return;
                }

                int currentValue = (int)maxJumpCountProperty.GetValue(playerInstance);
                MelonLogger.Msg($"Current jump count number is: {currentValue}");

                maxJumpCountProperty.SetValue(playerInstance, 20);
                MelonLogger.Msg("Max Jump Count set to 20!");
            }
            catch (Exception e)
            {
                MelonLogger.Error($"An error occurred during reflection: {e.Message}");
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
    }
}