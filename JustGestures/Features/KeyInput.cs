using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Windows.Forms;

namespace JustGestures.Features
{
    class KeyInput 
    {

        public static Win32.INPUT[] CreateInput(MyKey key, MyKey.Action action)
        {
            List<Win32.INPUT> inputRange;
            Win32.INPUT[] input = null;
            ushort mouseValue;
            bool swapedButtons = Win32.GetSystemMetrics(Win32.SM_SWAPBUTTON) == 0 ? false : true;

            switch (action)
            {
                case MyKey.Action.KeyDown:
                    input = new Win32.INPUT[1];                    
                    input[0].type = Win32.INPUT_KEYBOARD;
                    input[0].ki = Win32.CreateKeybdInput(key.Value, 0);
                    break;
                case MyKey.Action.KeyUp:
                    input = new Win32.INPUT[1];
                    input[0].type = Win32.INPUT_KEYBOARD;
                    input[0].ki = Win32.CreateKeybdInput(key.Value, Win32.KEYEVENTF_KEYUP);
                    break;
                case MyKey.Action.KeyClick:
                    input = new Win32.INPUT[2];
                    input[0].type = Win32.INPUT_KEYBOARD;
                    input[0].ki = Win32.CreateKeybdInput(key.Value, 0);
                    input[1].type = Win32.INPUT_KEYBOARD;
                    input[1].ki = Win32.CreateKeybdInput(key.Value, Win32.KEYEVENTF_KEYUP);
                    break;
                case MyKey.Action.MouseClick:
                    input = new Win32.INPUT[1];
                    input[0].type = Win32.INPUT_MOUSE;
                    mouseValue = key.Value;
                    if (swapedButtons)
                    {
                        if (key.Value == AllKeys.MOUSE_LBUTTON_CLICK.Value)
                            mouseValue = AllKeys.MOUSE_RBUTTON_CLICK.Value;
                        else if (key.Value == AllKeys.MOUSE_RBUTTON_CLICK.Value)
                            mouseValue = AllKeys.MOUSE_LBUTTON_CLICK.Value;
                    }
                    input[0].mi = Win32.CreateMouseInput(0, 0, 0, 0, mouseValue);
                    break;
                case MyKey.Action.MouseDblClick:
                    inputRange = new List<Win32.INPUT>();
                    inputRange.AddRange(CreateInput(key, MyKey.Action.MouseClick));
                    inputRange.AddRange(CreateInput(key, MyKey.Action.MouseClick));
                    input = inputRange.ToArray();
                    break;
                case MyKey.Action.MouseX1Click:
                    input = new Win32.INPUT[1];
                    input[0].type = Win32.INPUT_MOUSE;
                    input[0].mi = Win32.CreateMouseInput(0, 0, Win32.XBUTTON1, 0, key.Value);
                    break;
                case MyKey.Action.MouseX1DblClick:
                    inputRange = new List<Win32.INPUT>();
                    inputRange.AddRange(CreateInput(key, MyKey.Action.MouseX1Click));
                    inputRange.AddRange(CreateInput(key, MyKey.Action.MouseX1Click));
                    input = inputRange.ToArray();
                    break;
                case MyKey.Action.MouseX2Click:
                    input = new Win32.INPUT[1];
                    input[0].type = Win32.INPUT_MOUSE;
                    input[0].mi = Win32.CreateMouseInput(0, 0, Win32.XBUTTON2, 0, key.Value);
                    break;
                case MyKey.Action.MouseX2DblClick:
                    inputRange = new List<Win32.INPUT>();
                    inputRange.AddRange(CreateInput(key, MyKey.Action.MouseX2Click));
                    inputRange.AddRange(CreateInput(key, MyKey.Action.MouseX2Click));
                    input = inputRange.ToArray();
                    break;
                case MyKey.Action.MouseWheelDown:
                    input = new Win32.INPUT[1];
                    input[0].type = Win32.INPUT_MOUSE;
                    input[0].mi = Win32.CreateMouseInput(0, 0, 120, 0, key.Value);
                    break;
                case MyKey.Action.MouseWheelUp:
                    input = new Win32.INPUT[1];
                    input[0].type = Win32.INPUT_MOUSE;
                    input[0].mi = Win32.CreateMouseInput(0, 0, -120, 0, key.Value);
                    break;
                   
            }
            return input; 
        }

        public static Win32.INPUT[][] CreateInput(List<MyKey> keyScript)
        {
            List<Win32.INPUT[]> finalInput = new List<Win32.INPUT[]>();
            int i;
            int type = -1;
            for (i = 0; i < keyScript.Count; i++)
            {                
                List<Win32.INPUT> partInput = new List<Win32.INPUT>();
                do
                {
                    Win32.INPUT[] input = CreateInput(keyScript[i], keyScript[i].KeyAction);
                    if (type == -1)
                        type = input[0].type;
                    if (type == input[0].type)
                    {
                        foreach (Win32.INPUT one_input in input)
                            partInput.Add(one_input);
                        i++;
                    }
                    else
                    {
                        type = input[0].type;
                        i--;
                        break;
                    }
                } while (i < keyScript.Count);
                finalInput.Add(partInput.ToArray());
            }
            return finalInput.ToArray();
        }
        
        public static void ExecuteKeyInput(MyKey key)
        {
            Win32.INPUT[] input = CreateInput(key, key.KeyAction);
            Win32.SendInput((uint)input.Length, input, (int)System.Runtime.InteropServices.Marshal.SizeOf(input[0].GetType()));
        }
    }
}
