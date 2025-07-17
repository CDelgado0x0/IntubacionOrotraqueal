using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class AmbuContraints : MonoBehaviour
{
    [SerializeField] private Grabbable myGrab;
    [SerializeField] private GrabFreeTransformer movement;
    [SerializeField] private GrabFreeTransformer noMovement;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            lockGrabMovement();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            unlockGrabMovement();
        }
    }

    public void lockGrabMovement()
    {
        //movement.EndTransform();
        myGrab.InjectOptionalOneGrabTransformer(noMovement);
        //noMovement.BeginTransform();
    }

    public void unlockGrabMovement()
    {
        noMovement.EndTransform();
        myGrab.InjectOptionalOneGrabTransformer(movement);
        //movement.BeginTransform();
    }
}
