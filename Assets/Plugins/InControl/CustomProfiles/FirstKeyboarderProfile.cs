using UnityEngine;
using InControl;

namespace CustomControllerProfile
{
    public class FirstKeyboarderProfile : UnityInputDeviceProfile
    {
        public FirstKeyboarderProfile()
        {
            Name = "FirstKeyboarder";
            Meta = "A keyboard setup appropriate for the first player on keyboard controls.";

            SupportedPlatforms = new[]
            {
                "Windows",
                "Mac",
                "Linux"
            };

            Sensitivity = 1.0f;
            LowerDeadZone = 0.0f;
            UpperDeadZone = 1.0f;

            if (Application.systemLanguage != SystemLanguage.French)
            {
                ButtonMappings = new[]
                {
                    new InputControlMapping
                    {
                        Handle = "Start",
                        Target = InputControlType.Start,
                        Source = KeyCodeButton (KeyCode.Escape)
                    },
                    new InputControlMapping
                    {
                        Handle = "Jump",
                        Target = InputControlType.Action1,
                        Source = KeyCodeButton( KeyCode.E )
                    },
                    new InputControlMapping
                    {
                        Handle = "Dash",
                        Target = InputControlType.Action2,
                        Source = KeyCodeButton( KeyCode.Q )
                    },
                    new InputControlMapping
                    {
                        Handle = "Dash",
                        Target = InputControlType.Action3,
                        Source = KeyCodeButton( KeyCode.C )
                    }
                };

                AnalogMappings = new[]
                {
                    new InputControlMapping
                    {
                        Handle = "Move X",
                        Target = InputControlType.LeftStickX,
                        Source = KeyCodeAxis( KeyCode.A, KeyCode.D )
                    },
                    new InputControlMapping
                    {
                        Handle = "Move Z",
                        Target = InputControlType.LeftStickY,
                        Source = KeyCodeAxis( KeyCode.S, KeyCode.W )
                    }
                };
            }
            else
            {
                ButtonMappings = new[]
                {
                    new InputControlMapping
                    {
                        Handle = "Start",
                        Target = InputControlType.Start,
                        Source = KeyCodeButton (KeyCode.Escape)
                    },
                    new InputControlMapping
                    {
                        Handle = "Jump",
                        Target = InputControlType.Action1,
                        Source = KeyCodeButton( KeyCode.E )
                    },
                    new InputControlMapping
                    {
                        Handle = "Dash",
                        Target = InputControlType.Action2,
                        Source = KeyCodeButton( KeyCode.A )
                    },
                    new InputControlMapping
                    {
                        Handle = "Dash",
                        Target = InputControlType.Action3,
                        Source = KeyCodeButton( KeyCode.C )
                    }
                };

                AnalogMappings = new[]
                {
                    new InputControlMapping
                    {
                        Handle = "Move X",
                        Target = InputControlType.LeftStickX,
                        Source = KeyCodeAxis( KeyCode.Q, KeyCode.D )
                    },
                    new InputControlMapping
                    {
                        Handle = "Move Z",
                        Target = InputControlType.LeftStickY,
                        Source = KeyCodeAxis( KeyCode.S, KeyCode.Z )
                    }
                };
            }
        }
    }
}