using MelonLoader;
using UnityEngine;
using System.Reflection;

[assembly: MelonInfo(typeof(BananaShooterMod.Core), "Banana Shooter IMGUI Base", "1.0.0", "Haider x Moonlight", null)]

[assembly: MelonGame("Daniel", "Banana Shooter")]

namespace BananaShooterMod
{
    public class Core : MelonMod
    {
        // --- IMGUI Settings ---
        private bool _showMenu = true;
        private Rect _windowRect = new Rect(20, 20, 250, 200); 

     
        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("IMGUI Mod Initialized! Press F4 to toggle the menu.");
        }
        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.F4))
            {
                _showMenu = !_showMenu;
            }
        }

        
        public override void OnGUI()
        {
            if (!_showMenu)
            {
                return;
            }

            GUI.skin = GUI.skin;

            _windowRect = GUI.Window(0, _windowRect, (GUI.WindowFunction)MyWindow, "Banana Mod Menu");
        }

        /// <param name="windowID"></param>
        private void MyWindow(int windowID)
        {
            // --- Window Content Goes Here ---

            GUI.Label(new Rect(10, 20, 230, 20), "Welcome to the Mod Menu!");

            if (GUI.Button(new Rect(10, 50, 230, 30), "Click Me!"))
            {
                LoggerInstance.Msg("Button was clicked!");
            }
            GUI.DragWindow();
        }
    }
}
