using Bhaptics.SDK2;
using Bhaptics.SDK2.Glove;
using UnityEngine;

public class Ala : MonoBehaviour
{
    [SerializeField] private int fingerIndex;
    [SerializeField] private bool isLeft;

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Collision detected on finger " + fingerIndex + " with: " + collision.gameObject.name);
        // We typically use collision.relativeVelocity, but depending on how your hand physics are designed, you can use other overload functions.
        BhapticsPhysicsGlove.Instance.SendEnterHaptic(isLeft ? PositionType.GloveL : PositionType.GloveR, fingerIndex);
    }

    private void OnTriggerStay(Collider collision)
    {
        BhapticsPhysicsGlove.Instance.SendStayHaptic(isLeft ? PositionType.GloveL : PositionType.GloveR, fingerIndex);
    }

    private void OnTriggerExit(Collider collision)
    {
        BhapticsPhysicsGlove.Instance.SendExitHaptic(isLeft ? PositionType.GloveL : PositionType.GloveR, fingerIndex);
    }

}
