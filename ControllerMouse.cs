using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.DirectInput;
namespace H2Memory_class
{
    public class ControllerMouse
    {
        /// <summary>
        /// The Main Struct for Controller Device.
        /// </summary>
        public Controller CController;
        /// <summary>
        /// The Main Struct for Mouse and Keyboard Devices.
        /// </summary>
        public MK MouseKeyBoard;
        /// <summary>
        /// Starts The Device Hooks.
        /// </summary>
        public ControllerMouse()
        {
            initDevices();
        }
        #region structs
        /// <summary>
        /// Main Controller Struct
        /// </summary>
        public struct Controller
        {
            public bool Connected;
            public ControllerInput CInput;
            public Device CDevice;
            public JoystickState CState;
        }
        /// <summary>
        /// Main MouseKeyboardStruct
        /// </summary>
        public struct MK
        {
            public bool MConnected;
            public bool KConnected;
            public KeyBoardInput KInput;
            public MouseInput Minput;
            public KeyboardState KState;
            public MouseState MState;
            public Device MDevice;
            public Device KDevice;
        }
        /// <summary>
        /// main structure for capturing Keyboard input
        /// </summary>
        public struct KeyBoardInput
        {
            public bool Connected;
            public bool Wkey;
            public bool Akey;
            public bool Skey;
            public bool Dkey;
            public bool Number1;
            public bool Number2;
            public bool Number3;
            public bool Number4;
            public bool Number5;
            public bool Number6;
            public bool Number7;
            public bool ArrowKeyUp;
            public bool ArrowKeyRight;
            public bool ArrowKeyLeft;
            public bool ArrowKeyDown;
            public bool CtrlKey;
            public bool ReturnKey;
            public bool AltKey;
            public bool SpaceKey;
            public bool F3Key;
            public bool F4Key;
            public bool F5Key;
            public bool F6Key;
            public bool F7Key;
            public bool F8Key;
            public bool F9Key;
            public bool F10Key;
            public bool InsertKey;
            public bool DeleteKey;
            public bool PageUpKey;
            public bool PageDownKey;
            public bool VKey;
        }
        /// <summary>
        /// Main structure for capturing mouse input
        /// </summary>
        public struct MouseInput
        {
            public int Xincrease;
            public int Yincrease;
            public int Zincrease;
            public int lastX;
            public int lastY;
            public int lastZ;
            public int X;
            public int Y;
            public int Z;
            public bool LB;
            public bool RB;
            public bool MB;
            public bool Connected;
        }
        /// <summary>
        /// Main Structure for capturing controller input
        /// </summary>
        public struct ControllerInput
        {
            public bool LeftAnalogUp;
            public bool LeftAnalogDown;
            public bool LeftAnalogRight;
            public bool LeftAnalogLeft;
            public bool RightAnalogUp;
            public bool RightAnalogDown;
            public bool RightAnalogRight;
            public bool RightAnalogLeft;
            public bool AButton;
            public bool BButton;
            public bool XButton;
            public bool YButton;
            public bool StartButton;
            public bool BackButton;
            public bool LeftBumpButton;
            public bool RightBumpButton;
            public bool LeftAnalogClickButton;
            public bool RightAnalogClickButton;
            public bool DPadUp;
            public bool DPadDown;
            public bool DPadLeft;
            public bool DPadRight;
            public bool RTrigger;
            public bool LTrigger;
            public float RTriggerAmount;
            public float LTriggerAmount;
            public float LeftAnalogX;
            public float LeftAnalogY;
            public float RightAnalogX;
            public float RightAnalogY;
        }
        #endregion
        #region Updates
        /// <summary>
        /// Updates the Devices.
        /// </summary>
        public void Update()
        {
            UpdateMouse();
            UpdateKeyboard();
            UpdateController();
        }
        #region Mouse
        /// <summary>
        /// updates the mouse
        /// </summary>
        private void UpdateMouse()
        {
            try { MouseKeyBoard.MState = MouseKeyBoard.MDevice.CurrentMouseState; }
            catch (Exception) { MouseKeyBoard.MConnected = false; }
            MouseKeyBoard.Minput.lastX = MouseKeyBoard.Minput.X;
            MouseKeyBoard.Minput.lastY = MouseKeyBoard.Minput.Y;
            MouseKeyBoard.Minput.lastZ = MouseKeyBoard.Minput.Z;
            MouseKeyBoard.Minput.X = MouseKeyBoard.MState.X;
            MouseKeyBoard.Minput.Y = MouseKeyBoard.MState.Y;
            MouseKeyBoard.Minput.Z = MouseKeyBoard.MState.Z;
            MouseKeyBoard.Minput.Xincrease = MouseKeyBoard.Minput.X - MouseKeyBoard.Minput.lastX;
            MouseKeyBoard.Minput.Yincrease = MouseKeyBoard.Minput.Y - MouseKeyBoard.Minput.lastY;
            MouseKeyBoard.Minput.Zincrease = MouseKeyBoard.Minput.Z - MouseKeyBoard.Minput.lastZ;
            byte[] buttons = MouseKeyBoard.MState.GetMouseButtons();
            MouseKeyBoard.Minput.LB = (buttons[0] == 0) ? false : true;
            MouseKeyBoard.Minput.RB = (buttons[1] == 0) ? false : true;
            MouseKeyBoard.Minput.MB = (buttons[2] == 0) ? false : true;
        }
        #endregion
        #region Keyboard
        /// <summary>
        /// Updates the Keyboard
        /// </summary>
        private void UpdateKeyboard()
        {
            MouseKeyBoard.KInput.Wkey = false;
            MouseKeyBoard.KInput.Akey = false;
            MouseKeyBoard.KInput.Skey = false;
            MouseKeyBoard.KInput.Dkey = false;
            MouseKeyBoard.KInput.Number1 = false;
            MouseKeyBoard.KInput.Number2 = false;
            MouseKeyBoard.KInput.Number3 = false;
            MouseKeyBoard.KInput.Number4 = false;
            MouseKeyBoard.KInput.Number5 = false;
            MouseKeyBoard.KInput.Number6 = false;
            MouseKeyBoard.KInput.Number7 = false;
            MouseKeyBoard.KInput.ArrowKeyUp = false;
            MouseKeyBoard.KInput.ArrowKeyRight = false;
            MouseKeyBoard.KInput.ArrowKeyDown = false;
            MouseKeyBoard.KInput.ArrowKeyLeft = false;
            MouseKeyBoard.KInput.CtrlKey = false;
            MouseKeyBoard.KInput.ReturnKey = false;
            MouseKeyBoard.KInput.AltKey = false;
            MouseKeyBoard.KInput.SpaceKey = false;
            MouseKeyBoard.KInput.F3Key = false;
            MouseKeyBoard.KInput.F4Key = false;
            MouseKeyBoard.KInput.F5Key = false;
            MouseKeyBoard.KInput.InsertKey = false;
            MouseKeyBoard.KInput.DeleteKey = false;
            MouseKeyBoard.KInput.PageUpKey = false;
            MouseKeyBoard.KInput.PageDownKey = false;
            MouseKeyBoard.KInput.VKey = false;
            try
            {
                foreach (Key k in MouseKeyBoard.KDevice.GetPressedKeys())
                {
                    if (k == Key.W) MouseKeyBoard.KInput.Wkey = true;
                    if (k == Key.A) MouseKeyBoard.KInput.Akey = true;
                    if (k == Key.S) MouseKeyBoard.KInput.Skey = true;
                    if (k == Key.D) MouseKeyBoard.KInput.Dkey = true;
                    if (k == Key.D1) MouseKeyBoard.KInput.Number1 = true;
                    if (k == Key.D2) MouseKeyBoard.KInput.Number2 = true;
                    if (k == Key.D3) MouseKeyBoard.KInput.Number3 = true;
                    if (k == Key.D4) MouseKeyBoard.KInput.Number4 = true;
                    if (k == Key.D5) MouseKeyBoard.KInput.Number5 = true;
                    if (k == Key.D6) MouseKeyBoard.KInput.Number6 = true;
                    if (k == Key.D7) MouseKeyBoard.KInput.Number7 = true;
                    if (k == Key.Up) MouseKeyBoard.KInput.ArrowKeyUp = true;
                    if (k == Key.Right) MouseKeyBoard.KInput.ArrowKeyRight = true;
                    if (k == Key.DownArrow) MouseKeyBoard.KInput.ArrowKeyDown = true;
                    if (k == Key.LeftArrow) MouseKeyBoard.KInput.ArrowKeyLeft = true;
                    if (k == Key.LeftControl) MouseKeyBoard.KInput.CtrlKey = true;
                    if (k == Key.RightControl) MouseKeyBoard.KInput.CtrlKey = true;
                    if (k == Key.Return) MouseKeyBoard.KInput.ReturnKey = true;
                    if (k == Key.LeftMenu) MouseKeyBoard.KInput.AltKey = true;
                    if (k == Key.RightMenu) MouseKeyBoard.KInput.AltKey = true;
                    if (k == Key.Space) MouseKeyBoard.KInput.SpaceKey = true;
                    if (k == Key.F3) MouseKeyBoard.KInput.F3Key = true;
                    if (k == Key.F4) MouseKeyBoard.KInput.F4Key = true;
                    if (k == Key.F5) MouseKeyBoard.KInput.F5Key = true;
                    if (k == Key.Insert) MouseKeyBoard.KInput.InsertKey = true;
                    if (k == Key.Delete) MouseKeyBoard.KInput.DeleteKey = true;
                    if (k == Key.PageUp) MouseKeyBoard.KInput.PageUpKey = true;
                    if (k == Key.PageDown) MouseKeyBoard.KInput.PageDownKey = true;
                    if (k == Key.V) MouseKeyBoard.KInput.VKey = true;
                }
            }
            catch (Exception) { MouseKeyBoard.KConnected = false; }
        }
        #endregion
        #region Controller
        /// <summary>
        /// Updates the controller
        /// </summary>
        private void UpdateController()
        {
            CController.Connected = true;
            CController.CInput.LeftAnalogLeft = false;
            CController.CInput.LeftAnalogRight = false;
            CController.CInput.LeftAnalogUp = false;
            CController.CInput.LeftAnalogDown = false;
            CController.CInput.RightAnalogLeft = false;
            CController.CInput.RightAnalogRight = false;
            CController.CInput.RightAnalogUp = false;
            CController.CInput.RightAnalogDown = false;
            CController.CInput.RTrigger = false;
            CController.CInput.LTrigger = false;
            CController.CInput.DPadRight = false;
            CController.CInput.DPadLeft = false;
            CController.CInput.DPadDown = false;
            CController.CInput.DPadUp = false;
            CController.CInput.AButton = false;
            CController.CInput.BButton = false;
            CController.CInput.XButton = false;
            CController.CInput.YButton = false;
            CController.CInput.LeftBumpButton = false;
            CController.CInput.RightBumpButton = false;
            CController.CInput.StartButton = false;
            CController.CInput.BackButton = false;
            CController.CInput.LeftAnalogClickButton = false;
            CController.CInput.RightAnalogClickButton = false;
            try
            {
                if (CController.Connected)
                {
                    CController.CDevice.Acquire();
                    CController.CState = CController.CDevice.CurrentJoystickState;
                }
            }
            catch (Exception) { CController.Connected = false; }
            if (CController.Connected)
            {
                if (CController.CState.X < -3500) CController.CInput.LeftAnalogLeft = true;
                if (CController.CState.X > 0xdac) CController.CInput.LeftAnalogRight = true;
                if (CController.CState.Y < -3500) CController.CInput.LeftAnalogUp = true;
                if (CController.CState.Y > 0xdac) CController.CInput.LeftAnalogDown = true;
                if (CController.CState.Rx < -3500) CController.CInput.RightAnalogLeft = true;
                if (CController.CState.Rx > 0xdac) CController.CInput.RightAnalogRight = true;
                if (CController.CState.Ry < -3500) CController.CInput.RightAnalogUp = true;
                if (CController.CState.Ry > 0xdac) CController.CInput.RightAnalogDown = true;
                if (CController.CState.Z < -2500) CController.CInput.RTrigger = true;
                if (CController.CState.Z > 0x9c4) CController.CInput.LTrigger = true;
                CController.CInput.LTriggerAmount = ((CController.CState.Z > 200) ? CController.CState.Z : 0);
                CController.CInput.RTriggerAmount = ((CController.CState.Z < 200) ? CController.CState.Z : 0);
                CController.CInput.LeftAnalogX = CController.CState.X;
                CController.CInput.LeftAnalogY = CController.CState.Y;
                CController.CInput.RightAnalogX = CController.CState.Rx;
                CController.CInput.RightAnalogY = CController.CState.Ry;
                if (CController.CDevice.CurrentJoystickState.GetPointOfView()[0] != -1)
                {
                    if (((CController.CState.GetPointOfView()[0] / 0x3e8) > 0) && ((CController.CState.GetPointOfView()[0] / 0x3e8) < 0x12)) CController.CInput.DPadRight = true;
                    if (((CController.CState.GetPointOfView()[0] / 0x3e8) > 0x12) && ((CController.CState.GetPointOfView()[0] / 0x3e8) < 0x24)) CController.CInput.DPadLeft = true;
                    if (((CController.CState.GetPointOfView()[0] / 0x3e8) > 9) && ((CController.CState.GetPointOfView()[0] / 0x3e8) < 0x1b)) CController.CInput.DPadDown = true;
                    if (((CController.CState.GetPointOfView()[0] / 0x3e8) > 0x1b) || ((CController.CState.GetPointOfView()[0] / 0x3e8) < 9)) CController.CInput.DPadUp = true;
                }
                if (CController.CState.GetButtons()[0] != 0) CController.CInput.AButton = true;
                if (CController.CState.GetButtons()[1] != 0) CController.CInput.BButton = true;
                if (CController.CState.GetButtons()[2] != 0) CController.CInput.XButton = true;
                if (CController.CState.GetButtons()[3] != 0) CController.CInput.YButton = true;
                if (CController.CState.GetButtons()[4] != 0) CController.CInput.LeftBumpButton = true;
                if (CController.CState.GetButtons()[5] != 0) CController.CInput.RightBumpButton = true;
                if (CController.CState.GetButtons()[6] != 0) CController.CInput.BackButton = true;
                if (CController.CState.GetButtons()[7] != 0) CController.CInput.StartButton = true;
                if (CController.CState.GetButtons()[8] != 0) CController.CInput.LeftAnalogClickButton = true;
                if (CController.CState.GetButtons()[9] != 0) CController.CInput.RightAnalogClickButton = true;
            }
        }
        #endregion
        #endregion
        #region Functions
        /// <summary>
        /// Refresh's and reloads the devices and there status.
        /// </summary>
        public void Refresh()
        {
            initDevices();
        }
        /// <summary>
        /// Initiates the Devices
        /// </summary>
        private void initDevices()
        {
            #region initMK
            MouseKeyBoard = new MK();
            MouseKeyBoard.MDevice = new Device(SystemGuid.Mouse);
            if (MouseKeyBoard.MDevice == null)
            {
               MouseKeyBoard.MConnected = false;
            }
            MouseKeyBoard.MDevice.Properties.AxisModeAbsolute = true;
            MouseKeyBoard.MDevice.Acquire();
            MouseKeyBoard.MConnected = true;
            MouseKeyBoard.KDevice = new Device(SystemGuid.Keyboard);
            if (MouseKeyBoard.KDevice == null)
            {
               MouseKeyBoard.KConnected = false;
            }
            MouseKeyBoard.KDevice.Acquire();
            MouseKeyBoard.KConnected = true;
            #endregion
            #region initController
            CController = new Controller();
            DeviceList devices = Manager.GetDevices(DeviceClass.GameControl, EnumDevicesFlags.AllDevices);
            if (devices.Count > 0)
            {
                devices.MoveNext();
                DeviceInstance current = (DeviceInstance)devices.Current;
                CController.CDevice = new Device(current.InstanceGuid);
                foreach (DeviceObjectInstance instance2 in CController.CDevice.Objects)
                {
                    if ((instance2.ObjectId & 3) != 0)
                    {
                        CController.CDevice.Properties.SetRange(ParameterHow.ById, instance2.ObjectId, new InputRange(-5000, 0x1388));
                    }
                }
                CController.CDevice.Properties.AxisModeAbsolute = true;
                CController.CDevice.Acquire();
            }
            #endregion
            #region Call Start Update
            UpdateController();
            UpdateKeyboard();
            UpdateMouse();
            #endregion
        }
        #endregion
    }
}
