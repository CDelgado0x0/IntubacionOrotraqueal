using UnityEngine;

public class TubeBehaviour : MonoBehaviour
{

    [SerializeField] private ControladorAnimacion animationController;
    [SerializeField] private GameObject ManosTubo;
    [SerializeField] private GameObject ManosAnimacion;

    private Rigidbody myRb;

    void Start()
    {
        myRb = GetComponent<Rigidbody>();
        ManosTubo.SetActive(true);
        ManosAnimacion.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("OrotraquealDetectable"))
        {
            ManosTubo.SetActive(false);
            ManosAnimacion.SetActive(true);
            myRb.isKinematic = true;
            transform.position = other.transform.position;
            transform.rotation = other.transform.rotation;
            other.gameObject.SetActive(false);
            animationController.setPositions();
        }
    }
}
