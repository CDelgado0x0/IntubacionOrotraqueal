using UnityEngine;

public class TubeBehaviour : MonoBehaviour
{

    [SerializeField] private TubeConstraints myMovement;
    [SerializeField] private ControladorAnimacion animationController;

    private Rigidbody myRb;

    void Start()
    {
        myRb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("OrotraquealDetectable"))
        {
            myMovement.changeGrabMovement();
            myRb.isKinematic = true;
            transform.position = other.transform.position;
            transform.rotation = other.transform.rotation;
            other.gameObject.SetActive(false);
            animationController.setPositions();
        }
    }
}
