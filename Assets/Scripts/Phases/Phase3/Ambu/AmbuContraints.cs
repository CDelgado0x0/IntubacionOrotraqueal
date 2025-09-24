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

    public void AmbuMovement()
    {
        ManosAmbu.SetActive(true);
        ManosCompresor.SetActive(false);
    }

    public void AmbuCompression()
    {
        ManosAmbu.SetActive(false);
        ManosCompresor.SetActive(true);
    }
}
