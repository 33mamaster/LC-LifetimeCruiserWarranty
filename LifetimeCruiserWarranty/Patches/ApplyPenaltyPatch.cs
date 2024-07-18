using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

using UnityEngine;
using Object = UnityEngine.Object;

namespace LifetimeCruiserWarranty.Patches
{
    [HarmonyPatch(typeof(RoundManager))]
    internal class RoundManagerPatch
    {
        [HarmonyPatch("DespawnPropsAtEndOfRound")]
        [HarmonyPrefix]
        private static void DespawnPropsAtEndOfRoundPatch()
        {
            if (Config.applyPenalty.Value)
            {
                Terminal terminal = Object.FindObjectOfType<Terminal>();
                int groupCredits = terminal.groupCredits;
                Plugin.isVehicleLeftBehind = false;
                Plugin.due = 0;
                Plugin.Logger.LogInfo("Attempting Lookup of Vehicles");
                try
                {
                    VehicleController[] array3 = UnityEngine.Object.FindObjectsByType<VehicleController>(FindObjectsSortMode.None);
                    for (int k = 0; k < array3.Length; k++)
                    {
                        if (!array3[k].magnetedToShip && array3[k].NetworkObject != null)
                        {
                            Plugin.Logger.LogInfo($"Found Left Behind Vehicle: {array3[k].name}");
                            Plugin.isVehicleLeftBehind = true;

                            // Deduct social credit score
                            Plugin.Logger.LogInfo($"Credits: {terminal.groupCredits}");
                            terminal.groupCredits -= (int)((float)groupCredits * 0.2f);
                            Plugin.Logger.LogInfo($"Credits after: {terminal.groupCredits}");
                            Plugin.due = groupCredits - terminal.groupCredits;
                        }
                    }
                }
                catch (Exception arg)
                {
                    Plugin.Logger.LogError($"Error Searching for vehicle: {arg}");
                }
            }
        }
    }
}
