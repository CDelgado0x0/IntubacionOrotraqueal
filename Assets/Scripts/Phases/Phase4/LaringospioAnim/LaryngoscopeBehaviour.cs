using UnityEngine;

public class LaryngoscopeBehaviour : MonoBehaviour
{
    [SerializeField] private ControladorAnimacion LaryngoscopeControlAnimation;
    void Start()
    {
        LaryngoscopeControlAnimation.setPositions();
    }

}
