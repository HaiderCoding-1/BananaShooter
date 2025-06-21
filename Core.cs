using MelonLoader;
using UnityEngine;
using System.Reflection;
using System;

[assembly: MelonInfo(typeof(BananaShooterMod.Core), "Banana Shooter IMGUI Base", "1.0.0", "Haider x Moonlight", null)]
[assembly: MelonGame("Daniel", "Banana Shooter")]

namespace BananaShooterMod
{
    public enum MenuPage
    {
        Main,
        Movement,
        Player,
        Weapon,
        Debug
    }

    public class Core : MelonMod
    {
        // --- IMGUI Settings ---
        private bool _showMenu = true;
        private Rect _windowRect = new Rect(100, 190, 350, 400);
        private GUISkin _customSkin;
        private PlayerMovementHandler _movementHandler;
        private Weapons _weaponsHandler; 
        private MenuPage _currentPage = MenuPage.Main;

        private float _jumpSliderValue = 10f;
        private float _MovementSpeedValue = 1f;
        private int _jumpCountValue = 2;
        private int _bulletCountValue = 30;
        private float _reloadTimeValue = 1.0f;
        private float _fireRateValue = 0.5f;



        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("IMGUI Mod Initialized! Press F4 to toggle the menu.");
            SetupGUISkin();
            _movementHandler = new PlayerMovementHandler();
            _weaponsHandler = new Weapons();

            float initialJump = _movementHandler.GetCurrentJumpFactor();
            if (initialJump != -1f) { _jumpSliderValue = initialJump;}
            int initialJumpCount = _movementHandler.GetCurrentJumpCount();
            if (initialJumpCount != -1) { _jumpCountValue = initialJumpCount; }
            int initialBullets = _weaponsHandler.GetCurrentBulletCount();
            if (initialBullets != -1) { _bulletCountValue = initialBullets; }
            float initialReload = _weaponsHandler.GetCurrentReloadTime();
            if (initialReload != -1f) { _reloadTimeValue = initialReload; }
            float initialFireRate = _weaponsHandler.GetCurrentFireRate();
            if (initialFireRate != -1f) { _fireRateValue = initialFireRate; }
        }

        public override void OnUpdate()
        {
            if (_weaponsHandler != null && _showMenu && _currentPage == MenuPage.Weapon)
            {
                int currentBullets = _weaponsHandler.GetCurrentBulletCount();
                if (currentBullets != -1) { _bulletCountValue = currentBullets; }
            }

            if (Input.GetKeyDown(KeyCode.F4))
            {
                _showMenu = !_showMenu;
            }
        }

        public override void OnGUI()
        {
            if (!_showMenu) return;
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
            float buttonWidth = 40f;
            float labelWidth = width - (buttonWidth * 2);

            switch (_currentPage)
            {
                case MenuPage.Main:
                    GUI.Label(new Rect(xPos, yPos, width, height), "Main Menu", _customSkin.label);
                    yPos += height + padding;
                    if (GUI.Button(new Rect(xPos, yPos, width, height), "Movement Mods")) { _currentPage = MenuPage.Movement; }
                    yPos += height + padding;
                    if (GUI.Button(new Rect(xPos, yPos, width, height), "Player Mods")) { _currentPage = MenuPage.Player; }
                    yPos += height + padding;
                    if (GUI.Button(new Rect(xPos, yPos, width, height), "Weapon Mods")) { _currentPage = MenuPage.Weapon; }
                    yPos += height + padding;
                    if (GUI.Button(new Rect(xPos, yPos, width, height), "Debug Tools")) { _currentPage = MenuPage.Debug; }
                    yPos += height + padding;
                    float closeButtonY = _windowRect.height - height - 20f;
                    if (GUI.Button(new Rect(xPos, closeButtonY, width, height), "Close Menu")) { _showMenu = false; }
                    break;

                //Jump Power
                case MenuPage.Movement:
                    GUI.Label(new Rect(xPos, yPos, width, height), "Movement Mods", _customSkin.label);
                    yPos += height + padding;

                    // --- Jump Power Adjuster
                    float oldJumpPower = _jumpSliderValue;
                    if (GUI.Button(new Rect(xPos, yPos, buttonWidth, height), "-")) { _jumpSliderValue -= 0.5f; }
                    GUI.Label(new Rect(xPos + buttonWidth, yPos, labelWidth, height), $"Jump Power: {_jumpSliderValue.ToString("F1")}");
                    if (GUI.Button(new Rect(xPos + buttonWidth + labelWidth, yPos, buttonWidth, height), "+")) { _jumpSliderValue += 0.5f; }
                    _jumpSliderValue = Mathf.Clamp(_jumpSliderValue, 1f, 50f);
                    if (oldJumpPower != _jumpSliderValue) { _movementHandler.SetJumpFactor(_jumpSliderValue); }
                    yPos += height + padding;

                    // --- Max Jumps Adjuster
                    int oldJumpCount = _jumpCountValue;
                    if (GUI.Button(new Rect(xPos, yPos, buttonWidth, height), "-")) { _jumpCountValue--; }
                    GUI.Label(new Rect(xPos + buttonWidth, yPos, labelWidth, height), $"Max Jumps: {_jumpCountValue}");
                    if (GUI.Button(new Rect(xPos + buttonWidth + labelWidth, yPos, buttonWidth, height), "+")) { _jumpCountValue++; }
                    _jumpCountValue = Mathf.Clamp(_jumpCountValue, 6, 40);
                    if (oldJumpCount != _jumpCountValue) { _movementHandler.SetJumpCount(_jumpCountValue); }
                    yPos += height + padding;

                    float oldMovementSpeed = _MovementSpeedValue;
                    if (GUI.Button(new Rect(xPos, yPos, buttonWidth, height), "-")) { _MovementSpeedValue -= 0.5f; }
                    GUI.Label(new Rect(xPos + buttonWidth, yPos, labelWidth, height), $"Movement Speed: { _MovementSpeedValue}");
                    if (GUI.Button(new Rect(xPos + buttonWidth + labelWidth, yPos, buttonWidth, height), "+")) { _MovementSpeedValue += 0.5f; }
                    _MovementSpeedValue = Mathf.Clamp(_MovementSpeedValue, 1f, 30f);
                    if (oldMovementSpeed != _MovementSpeedValue) { _movementHandler.SetMovementSpeed(_MovementSpeedValue); }
                    yPos += height + padding;



                    if (GUI.Button(new Rect(xPos, _windowRect.height - height - 20f, width, height), "Back"))
                    {
                        _currentPage = MenuPage.Main;
                    }
                    break;
                case MenuPage.Player:
                    GUI.Label(new Rect(xPos, yPos, width, height), "Player Mods", _customSkin.label);
                    yPos += height + padding;
                    if (GUI.Button(new Rect(xPos, _windowRect.height - height - 20f, width, height), "Back")) { _currentPage = MenuPage.Main; }
                    break;

                case MenuPage.Weapon:
                    GUI.Label(new Rect(xPos, yPos, width, height), "Weapon Mods", _customSkin.label);
                    yPos += height + padding;

                    int oldBulletCount = _bulletCountValue;
                    GUI.Label(new Rect(xPos + buttonWidth, yPos, labelWidth, height), $"Bullet Count: {_bulletCountValue}");
                    if (GUI.Button(new Rect(xPos, yPos, buttonWidth, height), "-")) { _bulletCountValue--; }
                    if (GUI.Button(new Rect(xPos + buttonWidth + labelWidth, yPos, buttonWidth, height), "+")) { _bulletCountValue++; }
                    _bulletCountValue = Mathf.Clamp(_bulletCountValue, 0, 999);
                    if (oldBulletCount != _bulletCountValue) { _weaponsHandler.SetBulletCount(_bulletCountValue); }
                    yPos += height + padding;

                    float oldReloadTime = _reloadTimeValue;
                    GUI.Label(new Rect(xPos + buttonWidth, yPos, labelWidth, height), $"Reload Time: {_reloadTimeValue.ToString("F2")}s");
                    if (GUI.Button(new Rect(xPos, yPos, buttonWidth, height), "-")) { _reloadTimeValue -= 0.1f; }
                    if (GUI.Button(new Rect(xPos + buttonWidth + labelWidth, yPos, buttonWidth, height), "+")) { _reloadTimeValue += 0.1f; }
                    _reloadTimeValue = Mathf.Clamp(_reloadTimeValue, 0.1f, 5f);
                    if (oldReloadTime != _reloadTimeValue) { _weaponsHandler.SetReloadTime(_reloadTimeValue); }
                    yPos += height + padding;
                    float oldFireRate = _fireRateValue;
                    GUI.Label(new Rect(xPos + buttonWidth, yPos, labelWidth, height), $"Fire Rate Delay: {_fireRateValue.ToString("F2")}");

                    if (GUI.Button(new Rect(xPos, yPos, buttonWidth, height), "-")) { _fireRateValue -= 0.05f; }
                    if (GUI.Button(new Rect(xPos + buttonWidth + labelWidth, yPos, buttonWidth, height), "+")) { _fireRateValue += 0.05f; }
                    _fireRateValue = Mathf.Clamp(_fireRateValue, 0.01f, 2f); 
                    if (oldFireRate != _fireRateValue) { _weaponsHandler.SetFireRate(_fireRateValue); }
                    yPos += height + padding;

                    if (GUI.Button(new Rect(xPos, yPos, width, height), "Refill Current Ammo to 99"))
                    {
                        _weaponsHandler.RefillAndSetMaxAmmo(99);
                    }
                    yPos += height + padding;


                    if (GUI.Button(new Rect(xPos, _windowRect.height - height - 20f, width, height), "Back"))
                    {
                        _currentPage = MenuPage.Main;
                    }
                    break;
            
                case MenuPage.Debug:
                    GUI.Label(new Rect(xPos, yPos, width, height), "Debug Tools", _customSkin.label);
                    yPos += height + padding;
                    if (GUI.Button(new Rect(xPos, yPos, width, height), "Dump Player Info")) { _movementHandler.DumpPlayerMovementInfo(); }
                    yPos += height + padding;
      
                    if (GUI.Button(new Rect(xPos, yPos, width, height), "Dump Firearms Info"))
                    {
                        _movementHandler.DumpFirearmsInfo();
                    }
                    yPos += height + padding;

                    if (GUI.Button(new Rect(xPos, _windowRect.height - height - 20f, width, height), "Back"))
                    {
                        _currentPage = MenuPage.Main;
                    }
                    break;

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