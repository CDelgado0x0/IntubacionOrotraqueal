using Oculus.Interaction;
using UnityEngine;

public class ChangeGrabTransforms : MonoBehaviour
{
    [SerializeField] private Grabbable myGrab;

    [Header("Transformers")]
    [SerializeField] private GrabFreeTransformer basicGrab;
    [SerializeField] private OneGrabTranslateTransformer justMovement;
    [SerializeField] private OneGrabRotateTransformer justRotation;

    private void Start()
    {
        justMovement?.Initialize(myGrab);
        justRotation?.Initialize(myGrab);
    }

    /*
    private void Update()
    {
        Debug.Log("GrabPoints count: " + myGrab.GrabPoints.Count);

        if (Input.GetKeyDown(KeyCode.A))
        {
            justMovement?.Initialize(myGrab);
            myGrab.InjectOptionalOneGrabTransformer(justMovement);
        }
        
        if (Input.GetKeyDown(KeyCode.S))
            myGrab.InjectOptionalOneGrabTransformer(justRotation);

        if (Input.GetKeyDown(KeyCode.D))
            myGrab.InjectOptionalOneGrabTransformer(basicGrab);
    }
    */

    public void BasicMovement()
    {
        myGrab.InjectOptionalOneGrabTransformer(basicGrab);
    }

    public void JustMovement()
    {
        justMovement?.Initialize(myGrab);
        myGrab.InjectOptionalOneGrabTransformer(justMovement);
    }

    public void JustRotation()
    {
        justRotation?.Initialize(myGrab);
        myGrab.InjectOptionalOneGrabTransformer(justRotation);
    }


    /*
    public enum GrabMode { Basic, Movement, Rotation }

    public void SetGrabMode(GrabMode mode)
    {
        ITransformer selected = mode switch
        {
            GrabMode.Basic => basicGrab,
            GrabMode.Movement => justMovement,
            GrabMode.Rotation => justRotation,
            _ => basicGrab
        };

        if (selected == null || myGrab == null) return;

        

        // Inyectar el transformer en el Grabbable
        myGrab.InjectOptionalOneGrabTransformer(selected);

    }
    */
}
