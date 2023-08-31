using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Tools;
using System;
using System.Linq;
using System.Reflection;

namespace FishingTreasureSpawner
{
    /// <summary>
    /// This mod spawns a fishing treasure with the press of a hotkey.
    /// This mod also doesn't give a damn about where you are in the map, be it inside or outside.
    /// Mod only triggers the event for getting a fishing treasure using players fishing rod, if they don't have one also gives a fishing rod.
    /// </summary>
    internal sealed class ModEntry : Mod
    {
        private int lastClearWaterDistance = 0;
        private FieldInfo clearWaterDistanceField;

        public override void Entry(IModHelper helper)
        {
            helper.Events.Input.ButtonPressed += this.OnButtonPressed;
        }

        private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (!Context.IsWorldReady)
                return;

            /*
             * Hotkeys are hardcoded to be Numpad 0 to 9
             * 0 being fishing zone 0, and 9 being fishing zone 9
             * According to wiki only fishing zones 0 to 5 are used for treasures, I added other zones just for fun
             */

            if (e.Button == SButton.NumPad0)
            {
                var playerFishingRod = CheckAndGetPlayerFishingRod();
                SetClearWaterDistance(playerFishingRod, 0);
                playerFishingRod.openTreasureMenuEndFunction(0);
            }
            else if (e.Button == SButton.NumPad1)
            {
                var playerFishingRod = CheckAndGetPlayerFishingRod();
                SetClearWaterDistance(playerFishingRod, 1);
                playerFishingRod.openTreasureMenuEndFunction(0);
            }
            else if (e.Button == SButton.NumPad2)
            {
                var playerFishingRod = CheckAndGetPlayerFishingRod();
                SetClearWaterDistance(playerFishingRod, 2);
                playerFishingRod.openTreasureMenuEndFunction(0);
            }
            else if (e.Button == SButton.NumPad3)
            {
                var playerFishingRod = CheckAndGetPlayerFishingRod();
                SetClearWaterDistance(playerFishingRod, 3);
                playerFishingRod.openTreasureMenuEndFunction(0);
            }
            else if (e.Button == SButton.NumPad4)
            {
                var playerFishingRod = CheckAndGetPlayerFishingRod();
                SetClearWaterDistance(playerFishingRod, 4);
                playerFishingRod.openTreasureMenuEndFunction(0);
            }
            else if (e.Button == SButton.NumPad5)
            {
                var playerFishingRod = CheckAndGetPlayerFishingRod();
                SetClearWaterDistance(playerFishingRod, 5);
                playerFishingRod.openTreasureMenuEndFunction(0);
            }
            else if (e.Button == SButton.NumPad6)
            {
                var playerFishingRod = CheckAndGetPlayerFishingRod();
                SetClearWaterDistance(playerFishingRod, 6);
                playerFishingRod.openTreasureMenuEndFunction(0);
            }
            else if (e.Button == SButton.NumPad7)
            {
                var playerFishingRod = CheckAndGetPlayerFishingRod();
                SetClearWaterDistance(playerFishingRod, 7);
                playerFishingRod.openTreasureMenuEndFunction(0);
            }
            else if (e.Button == SButton.NumPad8)
            {
                var playerFishingRod = CheckAndGetPlayerFishingRod();
                SetClearWaterDistance(playerFishingRod, 8);
                playerFishingRod.openTreasureMenuEndFunction(0);
            }
            else if (e.Button == SButton.NumPad9)
            {
                var playerFishingRod = CheckAndGetPlayerFishingRod();
                SetClearWaterDistance(playerFishingRod, 9);
                playerFishingRod.openTreasureMenuEndFunction(0);
            }
        }

        /// <summary>
        /// Player needs to have a fishing rod in order for them to get a fishing treasure chest.
        /// This functions checks if player has a fishing rod, if they don't gives them one and returns the fishing rod object reference.
        /// </summary>
        /// <returns></returns>
        public FishingRod CheckAndGetPlayerFishingRod()
        {
            var playerFishingRod = Game1.player.Items.FirstOrDefault(x => x is FishingRod);
            if (playerFishingRod == null)
            {
                Monitor.Log("Player doesn't have a fishing rod, giving one.", LogLevel.Debug);
                playerFishingRod = new FishingRod(3);
                Game1.player.addItemToInventory(playerFishingRod);
            }
            return ((FishingRod)playerFishingRod);
        }

        /// <summary>
        /// Some treasures are only available if bobber is 'x' units away from the nearest land (land includes bridges, dirt, anything player can stand on)
        /// For example iridium ores are only available in fishing zone 5, that is bobber was 5 tiles away from land when treasure was caught
        /// This function changes fishing rod fishing zone to a specific value without having to cast the line.
        /// Because this value is 'private' in game code, we use reflection to set it forcibly.
        /// </summary>
        /// <param name="rod">Fishing rod to set the value </param>
        /// <param name="distance">Fishing zone to set</param>
        public void SetClearWaterDistance(FishingRod rod, int distance)
        {
            if (clearWaterDistanceField == null) // Reflection is pricey in terms of performance. We want to minimize 'GetField' calls, so we get it and put it somewhere for later use.
                clearWaterDistanceField = typeof(FishingRod).GetField("clearWaterDistance", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (lastClearWaterDistance == distance) // If current clear water distance is same as previous one, don't set it to avoid unnecessary reflection
                return;
            clearWaterDistanceField.SetValue(rod, distance);
            lastClearWaterDistance = distance;
        }
    }
}
