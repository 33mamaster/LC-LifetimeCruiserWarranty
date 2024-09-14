using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

using UnityEngine;

namespace LifetimeCruiserWarranty.Patches
{
    [HarmonyPatch(typeof(StartOfRound))]
    internal class StartOfRoundPatch
    {
        [HarmonyPatch("PassTimeToNextDay")]
        [HarmonyPostfix]
        private static void PassTimeToNextDayPatch()
        {
            Plugin.Logger.LogInfo("Display Penalty Function");
            if (Plugin.isVehicleLeftBehind)
            {
                HUDManager.Instance.StartCoroutine(DisplayAlert());
            }
        }

        private static IEnumerator DisplayAlert()
        {

            yield return (object)new WaitForSeconds((float)4f);

            // Display Punishment
            HUDManager.Instance.DisplayTip("Cruiser Lost", $"The Company Cruiser was lost.\nFINES DUE: ${Plugin.due}", true, false, "LC_Warning1");

            yield return (object)new WaitForSeconds((float)3f);
        }
    }
}
