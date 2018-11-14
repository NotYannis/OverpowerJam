using UnityEngine;
using UnityEngine.UI;

namespace Utilities
{
    //Class for storing a Text component variables without a component and allowing those variables to be reused later
    public class ButtonProperties
    {
        bool enabled;
        ColorBlock colors; 
        AnimationTriggers animationTriggers;
        Navigation navigation;
        SpriteState spriteState;
        Graphic targetGraphic;
        Selectable.Transition transition;

        //Transform Varaibles
        Vector3 localPosition;
        Quaternion localRotation;
        Vector3 localScale;

        public ButtonProperties(Button buttonComponent)
        {
            enabled = buttonComponent.enabled;
            colors = buttonComponent.colors;
            animationTriggers = buttonComponent.animationTriggers;
            navigation = buttonComponent.navigation;
            spriteState = buttonComponent.spriteState;
            targetGraphic = buttonComponent.targetGraphic;
            transition = buttonComponent.transition;

            localPosition = buttonComponent.transform.localPosition;
            localRotation = buttonComponent.transform.localRotation;
            localScale = buttonComponent.transform.localScale;
        }

        public void CopyToButtonComponent(Button buttonComponent)
        {

            buttonComponent.enabled = enabled;
            buttonComponent.colors = colors;
            buttonComponent.animationTriggers = animationTriggers;
            buttonComponent.navigation = navigation;
            buttonComponent.spriteState = spriteState;
            buttonComponent.targetGraphic = targetGraphic;
            buttonComponent.transition = transition;

            buttonComponent.transform.localPosition = localPosition;
            buttonComponent.transform.localRotation = localRotation;
            buttonComponent.transform.localScale = localScale;
        }
    }
}
