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

    public Texture[] Instrucciones;

    [SerializeField] private GameObject pasosCanula;
    [SerializeField] private GameObject pasoColocarPrimerAmbu;
    [SerializeField] private PatientAnimations controladorBoca;


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
    }

    private void OnValidate() //Esto sirve para poder cambiar el estado desde el inspector, comentar si no es necesario.
    {
        updateGameState(state);
    }

    public void updateGameState(GameState newState){

        pasosCanula.SetActive(false);
        pasoColocarPrimerAmbu.SetActive(false);

        state = newState;

        switch(newState){
            case GameState.menuPrincipal:
                LEDPantallas.SetTexture("_BaseMap", Instrucciones[21]);
                LEDPantallas.SetTexture("_EmissionMap", Instrucciones[21]);
                break;
                case GameState.encenderLaringoscopio:
                LEDPantallas.SetTexture("_BaseMap", Instrucciones[0]);
                LEDPantallas.SetTexture("_EmissionMap", Instrucciones[0]);
                break;
                case GameState.inflarBalonTuboOrotraqueal:
                LEDPantallas.SetTexture("_BaseMap", Instrucciones[1]);
                LEDPantallas.SetTexture("_EmissionMap", Instrucciones[1]);
                break;
                case GameState.posicionarCabezaPaciente:
                LEDPantallas.SetTexture("_BaseMap", Instrucciones[2]);
                LEDPantallas.SetTexture("_EmissionMap", Instrucciones[2]);
                break;
                case GameState.abrirBocaPaciente:
                LEDPantallas.SetTexture("_BaseMap", Instrucciones[3]);
                LEDPantallas.SetTexture("_EmissionMap", Instrucciones[3]);
                break;
                case GameState.introducirCanulaGirada:
                pasosCanula.SetActive(true);
                LEDPantallas.SetTexture("_BaseMap", Instrucciones[4]);
                LEDPantallas.SetTexture("_EmissionMap", Instrucciones[4]);
                break;
                case GameState.girarCanula:
                LEDPantallas.SetTexture("_BaseMap", Instrucciones[5]);
                LEDPantallas.SetTexture("_EmissionMap", Instrucciones[5]);
                break;
                case GameState.colocarAmbu:
                pasoColocarPrimerAmbu.SetActive(true);
                LEDPantallas.SetTexture("_BaseMap", Instrucciones[6]);
                LEDPantallas.SetTexture("_EmissionMap", Instrucciones[6]);
                break;
                case GameState.oxigenarPaciente:
                LEDPantallas.SetTexture("_BaseMap", Instrucciones[7]);
                LEDPantallas.SetTexture("_EmissionMap", Instrucciones[7]);
                break;
                case GameState.extraerAmbuYCanula:
                LEDPantallas.SetTexture("_BaseMap", Instrucciones[8]);
                LEDPantallas.SetTexture("_EmissionMap", Instrucciones[8]);
                break;
                case GameState.introducirLaringoscopio:
                LEDPantallas.SetTexture("_BaseMap", Instrucciones[9]);
                LEDPantallas.SetTexture("_EmissionMap", Instrucciones[9]);
                break;
                case GameState.elevarLaringoscopio:
                LEDPantallas.SetTexture("_BaseMap", Instrucciones[10]);
                LEDPantallas.SetTexture("_EmissionMap", Instrucciones[10]);
                break;
                case GameState.orientarTuboOrotraqueal:
                LEDPantallas.SetTexture("_BaseMap", Instrucciones[11]);
                LEDPantallas.SetTexture("_EmissionMap", Instrucciones[11]);
                break;
                case GameState.introducirTuboOrotraqueal:
                LEDPantallas.SetTexture("_BaseMap", Instrucciones[12]);
                LEDPantallas.SetTexture("_EmissionMap", Instrucciones[12]);
                break;
                case GameState.sacarLaringoscopio:
                LEDPantallas.SetTexture("_BaseMap", Instrucciones[13]);
                LEDPantallas.SetTexture("_EmissionMap", Instrucciones[13]);
                break;
                case GameState.conectarJeringa:
                LEDPantallas.SetTexture("_BaseMap", Instrucciones[14]);
                LEDPantallas.SetTexture("_EmissionMap", Instrucciones[14]);
                break;
                case GameState.inflarBalon:
                LEDPantallas.SetTexture("_BaseMap", Instrucciones[15]);
                LEDPantallas.SetTexture("_EmissionMap", Instrucciones[15]);
                break;
                case GameState.quitarJeringa:
                LEDPantallas.SetTexture("_BaseMap", Instrucciones[16]);
                LEDPantallas.SetTexture("_EmissionMap", Instrucciones[16]);
                break;
                case GameState.desacoplarMascarilla:
                LEDPantallas.SetTexture("_BaseMap", Instrucciones[16]);
                LEDPantallas.SetTexture("_EmissionMap", Instrucciones[16]);
                break;
                case GameState.asegurarTuboEnBoca:
                LEDPantallas.SetTexture("_BaseMap", Instrucciones[17]);
                LEDPantallas.SetTexture("_EmissionMap", Instrucciones[17]);
                break;
                case GameState.AcoplarCapnogragoYAmbu:
                LEDPantallas.SetTexture("_BaseMap", Instrucciones[18]);
                LEDPantallas.SetTexture("_EmissionMap", Instrucciones[18]);
                break;
                case GameState.insuflar:
                LEDPantallas.SetTexture("_BaseMap", Instrucciones[19]);
                LEDPantallas.SetTexture("_EmissionMap", Instrucciones[19]);
                break;
                case GameState.RealizarAuscultacion:
                LEDPantallas.SetTexture("_BaseMap", Instrucciones[20]);
                LEDPantallas.SetTexture("_EmissionMap", Instrucciones[20]);
                break;
                case GameState.simulacionTerminada:
                LEDPantallas.SetTexture("_BaseMap", Instrucciones[21]);
                LEDPantallas.SetTexture("_EmissionMap", Instrucciones[21]);
                break;
        }

        onGameStateChanged?.Invoke(newState);
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
    orientarTuboOrotraqueal,
    introducirTuboOrotraqueal,
    sacarLaringoscopio,
    conectarJeringa,
    inflarBalon,
    quitarJeringa,
    desacoplarMascarilla,
    asegurarTuboEnBoca,
    AcoplarCapnogragoYAmbu,
    insuflar,
    RealizarAuscultacion,

    simulacionTerminada
}