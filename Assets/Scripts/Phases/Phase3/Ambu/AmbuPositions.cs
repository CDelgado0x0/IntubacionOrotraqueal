using UnityEngine;

public class AmbuPositions : MonoBehaviour
{
    [SerializeField] private AmbuContraints Ambu;

    private Rigidbody myRb;

    private void Start()
    {
        myRb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AmbuDetectable"))
        {
            Ambu.changeGrabMovement();
            myRb.isKinematic = true;
            transform.position = other.transform.position;
            transform.rotation = other.transform.rotation;
            GameManager.applicationController.updateGameState(GameState.oxigenarPaciente);
        }
    }
}
