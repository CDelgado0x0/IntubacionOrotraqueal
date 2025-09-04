using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class AmbuContraints : MonoBehaviour
{
    [SerializeField] private GameObject ManosAmbu;
    [SerializeField] private GameObject ManosCompresor;

    private void Start()
    {
        ManosAmbu.SetActive(true);
        ManosCompresor.SetActive(false);
    }

    public void changeGrabMovement()
    {
        ManosAmbu.SetActive(!ManosAmbu.activeSelf);
        ManosCompresor.SetActive(!ManosCompresor.activeSelf);
    }
}
