using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSwitcher : MonoBehaviour
{
    public void SwitchControl()
    {
        if(PlayerController.instance == null)
        {
            return;
        }

        if(PlayerController.instance.movementInputType == GameMenuManager.PlayerMovementInputType.ButtonControl)
        {
            PlayerController.instance.movementInputType = GameMenuManager.PlayerMovementInputType.PointerControl;
        }
        else if(PlayerController.instance.movementInputType == GameMenuManager.PlayerMovementInputType.PointerControl)
        {
            PlayerController.instance.movementInputType = GameMenuManager.PlayerMovementInputType.TiltControl;
        }
        else
        {
            PlayerController.instance.movementInputType = GameMenuManager.PlayerMovementInputType.ButtonControl;
        }
    }
}
