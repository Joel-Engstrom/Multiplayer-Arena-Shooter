using InControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputController : PlayerActionSet
{
    public PlayerAction Left;
    public PlayerAction Right;
    public PlayerOneAxisAction Move;

    public PlayerAction Jump;
    public PlayerAction Shoot;

    public PlayerAction MouseNegativeX;
    public PlayerAction MousePositiveX;
    public PlayerOneAxisAction MouseX;

    public PlayerAction MouseNegativeY;
    public PlayerAction MousePositiveY;
    public PlayerOneAxisAction MouseY;
    

    public CharacterInputController()
    {
        Left = CreatePlayerAction("Move Left");
        Right = CreatePlayerAction("Move Right");
        Move = CreateOneAxisPlayerAction(Left, Right);
        
        Jump = CreatePlayerAction("Jump");
        Shoot = CreatePlayerAction("Shoot");

        MouseNegativeX = CreatePlayerAction("Mouse Left");
        MousePositiveX = CreatePlayerAction("Mouse Right");
        MouseX = CreateOneAxisPlayerAction(MouseNegativeX, MousePositiveX);

        MouseNegativeY = CreatePlayerAction("Mouse Down");
        MousePositiveY = CreatePlayerAction("Mouse Up");
        MouseY = CreateOneAxisPlayerAction(MouseNegativeY, MousePositiveY);
    }

}
