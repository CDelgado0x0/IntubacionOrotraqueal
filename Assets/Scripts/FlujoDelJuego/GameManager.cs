using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager applicationController;

    public GameState state;
    public static event Action<GameState> onGameStateChanged;

    public Material LEDPantallas;

    [SerializeField] private GameObject pasosCanula;
    [SerializeField] private GameObject pasoColocarPrimerAmbu;
    [SerializeField] private GameObject pasoInsertarTubo;
    [SerializeField] private PatientAnimations controladorBoca;

    [Header("Spanish Textures")]
    [SerializeField] private Texture[] instruccionesES;

    [Header("English Textures")]
    [SerializeField] private Texture[] instruccionesEN;

    private Dictionary<Language, Texture[]> instruccionesPorIdioma;
    private Language idiomaActual = Language.Spanish;


    [ContextMenu("Reproducir Animación Paciente")] //Dar click derecho al componente desde el inspector para ejecutarlo
    public void EjecutarAnimacionPaciente()
    {
        StartCoroutine(controladorBoca.mouthController());
    }

    void Awake(){

        applicationController = this;

    }

    void Start(){
        updateGameState(GameState.menuPrincipal);
        pasosCanula.SetActive(false);
        pasoColocarPrimerAmbu.SetActive(false);
        pasoInsertarTubo.SetActive(false);
    }

    private void OnValidate() //Esto sirve para poder cambiar el estado desde el inspector, comentar si no es necesario.
    {
        if (instruccionesPorIdioma == null)
        {
            instruccionesPorIdioma = new Dictionary<Language, Texture[]>
            {
                { Language.Spanish, instruccionesES },
                { Language.English, instruccionesEN }
            };
        }

        updateGameState(state);
    }

    public void CambiarIdioma(Language nuevoIdioma)
    {
        idiomaActual = nuevoIdioma;
        updateGameState(state); // refresca pantalla con el idioma nuevo
    }

    public void updateGameState(GameState newState){

        pasosCanula.SetActive(false);
        pasoColocarPrimerAmbu.SetActive(false);
        pasoInsertarTubo.SetActive(false);

        state = newState;

        switch (newState)
        {
            case GameState.menuPrincipal:
                SetPantalla(22);
                break;

            case GameState.encenderLaringoscopio:
                SetPantalla(0);
                break;

            case GameState.inflarBalonTuboOrotraqueal:
                SetPantalla(1);
                break;

            case GameState.posicionarCabezaPaciente:
                SetPantalla(2);
                break;

            case GameState.abrirBocaPaciente:
                SetPantalla(3);
                break;

            case GameState.introducirCanulaGirada:
                pasosCanula.SetActive(true);
                SetPantalla(4);
                break;

            case GameState.girarCanula:
                SetPantalla(5);
                break;

            case GameState.colocarAmbu:
                pasoColocarPrimerAmbu.SetActive(true);
                SetPantalla(6);
                break;

            case GameState.oxigenarPaciente:
                SetPantalla(7);
                break;

            case GameState.extraerAmbuYCanula:
                SetPantalla(8);
                break;

            case GameState.introducirLaringoscopio:
                SetPantalla(9);
                break;

            case GameState.elevarLaringoscopio:
                SetPantalla(10);
                break;

            case GameState.introducirTuboOrotraqueal:
                pasoInsertarTubo.SetActive(true);
                SetPantalla(11);
                break;

            case GameState.sacarLaringoscopio:
                SetPantalla(12);
                break;

            case GameState.conectarJeringa:
                SetPantalla(13);
                break;

            case GameState.inflarBalon:
                SetPantalla(14);
                break;

            case GameState.quitarJeringa:
                SetPantalla(15);
                break;

            case GameState.desacoplarMascarilla:
                SetPantalla(16);
                break;

            case GameState.conectarOxigeno:
                SetPantalla(17);
                break;

            case GameState.acoplarCapnografoYAmbuAlTubo:
                SetPantalla(18);
                break;

            case GameState.insuflar:
                SetPantalla(19);
                break;

            case GameState.RealizarAuscultacion:
                SetPantalla(20);
                break;

            case GameState.asegurarTuboEnBoca:
                SetPantalla(21);
                break;

            case GameState.simulacionTerminada:
                SetPantalla(22);
                break;
        }

        onGameStateChanged?.Invoke(newState);
    }

    private void SetPantalla(int index)
    {
        Texture[] instrucciones = instruccionesPorIdioma[idiomaActual];
        if (index >= 0 && index < instrucciones.Length)
        {
            LEDPantallas.SetTexture("_BaseMap", instrucciones[index]);
            LEDPantallas.SetTexture("_EmissionMap", instrucciones[index]);
        }
    }

}

public enum GameState{
    menuPrincipal,

    //Fase 1, comprobar el material
    encenderLaringoscopio,
    inflarBalonTuboOrotraqueal,

    //Fase 2, posicionar al paciente
    posicionarCabezaPaciente,
    abrirBocaPaciente,

    //Fase 3, oxigenación
    introducirCanulaGirada,
    girarCanula,
    colocarAmbu,
    oxigenarPaciente,
    extraerAmbuYCanula,

    //Fase 4, intubación
    introducirLaringoscopio,
    elevarLaringoscopio,
    introducirTuboOrotraqueal,
    sacarLaringoscopio,
    conectarJeringa,
    inflarBalon,
    quitarJeringa,
    desacoplarMascarilla,
    conectarOxigeno,
    acoplarCapnografoYAmbuAlTubo,
    insuflar,
    RealizarAuscultacion,
    asegurarTuboEnBoca,

    simulacionTerminada
}

public enum Language
{
    Spanish,
    English
}