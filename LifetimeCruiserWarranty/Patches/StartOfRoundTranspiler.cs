﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HarmonyLib;
using MonoMod;
using MonoMod.Cil;
using TMPro;
using UnityEngine;
using GameNetcodeStuff;

namespace LifetimeCruiserWarranty.Patches
{
    [HarmonyPatch]
    public static class StartOfRoundTranspiler
    {
        // Specify the method to patch using reflection
        public static MethodBase TargetMethod()
        {
            // Locate the state machine method generated by the compiler
            var nestedType = typeof(StartOfRound).GetNestedType("<EndOfGame>d__278", BindingFlags.NonPublic);
            return AccessTools.Method(nestedType, "MoveNext");
        }

        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> TranspileEndOfGame(IEnumerable<CodeInstruction> instructions)
        {
            Plugin.Logger.LogInfo("Running transpiler");
            var codes = new List<CodeInstruction>(instructions);
            // Locate the sequence of instructions to insert before
            for (int i = 0; i < codes.Count - 3; i++)
            {
                if (codes[i].opcode == OpCodes.Ldloc_1 &&
                    codes[i + 1].opcode == OpCodes.Ldarg_0 &&
                    codes[i + 2].opcode == OpCodes.Ldfld &&
                    codes[i + 2].operand.ToString().Contains("connectedPlayersOnServer") &&
                    codes[i + 3].opcode == OpCodes.Call &&
                    codes[i + 3].operand.ToString().Contains("PassTimeToNextDay"))
                {
                    // Replace the matched instructions with a call to DisplayPenalty method
                    List<CodeInstruction> newInstructions = new List<CodeInstruction>
                    {
                        // Call DisplayPenalty method
                        new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(StartOfRoundTranspiler), nameof(DisplayPenaltyWrapper))),
                    };

                    // Insert the new instructions at the identified position
                    codes.InsertRange(i, newInstructions);

                    for (int j = i; j < codes.Count - 3; j++)
                    {
                        Plugin.Logger.LogInfo(codes[j]);
                    }
                    break;
                }
            }

            return codes.AsEnumerable();
        }


        private static void DisplayPenaltyWrapper()
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
