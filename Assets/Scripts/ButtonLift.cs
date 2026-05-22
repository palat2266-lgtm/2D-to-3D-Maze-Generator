using UnityEngine;

public class ButtonLift : MonoBehaviour
{
    public LiftScript lift;

    public bool up;

    void OnMouseDown()
    {
        if (up) lift.GoUp();
        else lift.GoDown();
    }
}
