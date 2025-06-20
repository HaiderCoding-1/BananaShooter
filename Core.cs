using MelonLoader;
using UnityEngine;
using System.Reflection;
using System;

[assembly: MelonInfo(typeof(BananaShooterMod.Core), "Banana Shooter IMGUI Base", "1.0.0", "Haider x Moonlight", null)]
[assembly: MelonGame("Daniel", "Banana Shooter")]

namespace BananaShooterMod
{
    public class Core : MelonMod
    {
        // IMGUI Settings
        private bool _showMenu = true;
        private Rect _windowRect = new Rect(100, 190, 350, 400);
        private GUISkin _customSkin;
        private Jump _jumpHandler;

        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("IMGUI Mod Initialized! Press F4 to toggle the menu.");
            SetupGUISkin();
            _jumpHandler = new Jump();
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
            GUI.skin = _customSkin;
            _windowRect = GUI.Window(0, _windowRect, (GUI.WindowFunction)MyWindow, "Banana Mod Menu");
        }

        private void MyWindow(int windowID)
        {
            float yPos = 40f;
            float width = _windowRect.width - 40f;
            float height = 30f;
            float padding = 10f;
            float xPos = 20f;

            GUI.Label(new Rect(xPos, yPos, width, height), "Welcome to the Mod Menu!", _customSkin.label);
            yPos += height + padding;

            // Jump Value
            if (GUI.Button(new Rect(xPos, yPos, width, height), "Set Max Jump"))
            {
                LoggerInstance.Msg("Set Max Jump button clicked!");
                _jumpHandler.GetAndSetMaxJump();
            }
            yPos += height + padding;

            // Dumper Tool 
            if (GUI.Button(new Rect(xPos, yPos, width, height), "Dump Player Info"))
            {
                LoggerInstance.Msg("Dump Player Info button clicked!");
                _jumpHandler.DumpPlayerMovementInfo();
            }
            yPos += height + padding;

            if (GUI.Button(new Rect(xPos, yPos, width, height), "Another Option"))
            {
                LoggerInstance.Msg("Another option was selected!");
            }
            yPos += height + padding;

            float closeButtonY = _windowRect.height - height - 20f;
            if (GUI.Button(new Rect(xPos, closeButtonY, width, height), "Close Menu"))
            {
                _showMenu = false;
            }
            GUI.DragWindow();
        }

        private void SetupGUISkin()
        {
            _customSkin = ScriptableObject.CreateInstance<GUISkin>();
            var darkGray = new Color(0.12f, 0.12f, 0.12f, 0.98f);
            var midGray = new Color(0.2f, 0.2f, 0.2f, 1f);
            var lightGray = new Color(0.3f, 0.3f, 0.3f, 1f);
            var textColor = Color.white;
            var highlightColor = new Color(1f, 0.8f, 0.4f);
            var windowBg = MakeTex(1, 1, darkGray);
            var buttonBg = MakeTex(1, 1, midGray);
            var buttonHoverBg = MakeTex(1, 1, lightGray);
            _customSkin.window.normal.background = windowBg;
            _customSkin.window.normal.textColor = textColor;
            _customSkin.window.padding = new RectOffset(15, 15, 25, 15);
            _customSkin.window.alignment = TextAnchor.UpperCenter;
            _customSkin.window.fontSize = 16;
            _customSkin.button.normal.background = buttonBg;
            _customSkin.button.normal.textColor = textColor;
            _customSkin.button.hover.background = buttonHoverBg;
            _customSkin.button.hover.textColor = highlightColor;
            _customSkin.button.active.background = buttonHoverBg;
            _customSkin.button.padding = new RectOffset(10, 10, 10, 10);
            _customSkin.button.margin = new RectOffset(0, 0, 5, 5);
            _customSkin.button.fontSize = 14;
            _customSkin.label.normal.textColor = textColor;
            _customSkin.label.alignment = TextAnchor.MiddleCenter;
            _customSkin.label.fontSize = 16;
        }

        private Texture2D MakeTex(int width, int height, Color col)
        {
            var pix = new Color[width * height];
            for (int i = 0; i < pix.Length; i++)
            {
                pix[i] = col;
            }
            var result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            UnityEngine.Object.DontDestroyOnLoad(result);
            result.hideFlags = HideFlags.HideAndDontSave;
            return result;
        }
    }
}