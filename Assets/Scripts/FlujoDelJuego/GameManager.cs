using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static OVRPlugin;

public class GameManager : MonoBehaviour
{
    public static GameManager applicationController;

    public GameState state;
    public static event Action<GameState> onGameStateChanged;

    public Material LEDPantallas;

    [SerializeField] private GameObject pasosCanula;
    [SerializeField] private GameObject pasoColocarPrimerAmbu;
    [SerializeField] private GameObject pasoInsertarTubo;
    [SerializeField] private GameObject pasoInsertarLaringo;
    [SerializeField] private GameObject pasoInsertarCapnografo;
    [SerializeField] private GameObject pasoPosicionFinalAmbu;
    [SerializeField] private GameObject pasoColocarFijador;
    [SerializeField] private PatientAnimations controladorBoca;

    [SerializeField] private GameObject meshColliderClosedMouth;
    [SerializeField] private GameObject meshColliderOpenedMouth;

    [SerializeField] private GameObject oxygenConectionCollider;

    [SerializeField] private TextMeshProUGUI usernameText;

    public SupabaseClient myDatabase;

    [Header("MenuLogIn")]
    [SerializeField] private GameObject logInMenu;

    [Header("MainMenu")]
    [SerializeField] private GameObject mainMenu;

    [Header("MenuInGame")]
    [SerializeField] private GameObject inGameMenu;
    [SerializeField] private GameObject spanishInGame;
    [SerializeField] private GameObject englishInGame;

    [Header("MenuEndGame")]
    [SerializeField] private GameObject endGameMenu;
    [SerializeField] private GameObject spanishEndGame;
    [SerializeField] private GameObject englishEndGame;

    [Header("Timer")]
    public TextMeshProUGUI timerText;

    [Header("Spanish Textures")]
    [SerializeField] private Texture[] instruccionesES;

    [Header("English Textures")]
    [SerializeField] private Texture[] instruccionesEN;

    [Header("Sounds")]
    [SerializeField] private AudioSource correctSound;
    [SerializeField] private AudioSource wrongSound;

    private Dictionary<Language, Texture[]> instruccionesPorIdioma;
    private Language idiomaActual = Language.English;
    private float elapsedTime;


    [ContextMenu("Reproducir Animación Paciente")] //Dar click derecho al componente desde el inspector para ejecutarlo
    public void EjecutarAnimacionPaciente()
    {
        StartCoroutine(controladorBoca.mouthController());
    }

    void Awake(){

        applicationController = this;

        if (instruccionesPorIdioma == null)
        {
            instruccionesPorIdioma = new Dictionary<Language, Texture[]>
        {
            { Language.Spanish, instruccionesES },
            { Language.English, instruccionesEN }
        };
            }
        }

    void Start(){
        updateGameState(GameState.menuPrincipal);
        pasosCanula.SetActive(false);
        pasoColocarPrimerAmbu.SetActive(false);
        pasoInsertarTubo.SetActive(false);

        meshColliderClosedMouth.SetActive(true);
        meshColliderOpenedMouth.SetActive(false);
        oxygenConectionCollider.SetActive(false);
        elapsedTime = 0;

        inGameMenu.SetActive(false);
        endGameMenu.SetActive(false);
        spanishInGame.SetActive(false);
        englishInGame.SetActive(false);
        spanishEndGame.SetActive(false);
        englishEndGame.SetActive(false);

        if (PlayerPrefs.HasKey("user_uid"))
        {
            logInMenu.SetActive(false);
            mainMenu.SetActive(true);
            usernameText.text = PlayerPrefs.GetString("user_al", "Guest");
        }
        else
        {
            logInMenu.SetActive(true);
        }

        instruccionesPorIdioma = new Dictionary<Language, Texture[]>
        {
            { Language.Spanish, instruccionesES },
            { Language.English, instruccionesEN }
        };
    }

    private void Update()
    {
        if (state == GameState.menuPrincipal || state == GameState.simulacionTerminada) return;

        elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void CambiarIdioma(Language nuevoIdioma)
    {
        idiomaActual = nuevoIdioma;
        updateGameState(state); // refresca pantalla con el idioma nuevo
    }

    public void updateGameState(GameState newState){

        //if (state != GameState.menuPrincipal && state != GameState.simulacionTerminada)

        Debug.Log("Linea 146 de GameManager. Se actualiza el estado del juego");

        if ((int)newState < (int)state)
            wrongSound.Play();
        else
            CorrectSoundPlayer(newState);

        if(newState != GameState.menuPrincipal)
            myDatabase.LogAction("Paso realizado: " + state, true, "Siguiente paso: " + newState);

        Debug.Log("Linea 156 de GameManager. Se pasa de nivel.");

        pasosCanula.SetActive(false);
        Debug.Log("Linea 159 de GameManager. Se desactiva pasosCanula.");
        pasoColocarPrimerAmbu.SetActive(false);
        Debug.Log("Linea 161 de GameManager. Se desactiva pasoColocarPrimerAmbu.");
        pasoInsertarTubo.SetActive(false);
        Debug.Log("Linea 163 de GameManager. Se desactiva pasoInsertarTubo.");
        pasoInsertarLaringo.SetActive(false);
        Debug.Log("Linea 165 de GameManager. Se desactiva pasoInsertarLaringo.");
        pasoInsertarCapnografo.SetActive(false);
        Debug.Log("Linea 167 de GameManager. Se desactiva pasoInsertarCapnografo.");
        pasoPosicionFinalAmbu.SetActive(false);
        Debug.Log("Linea 169 de GameManager. Se desactiva pasoPosicionFinalAmbu.");
        pasoColocarFijador.SetActive(false);
        Debug.Log("Linea 171 de GameManager. Se desactiva pasoColocarFijador.");

        state = newState;

        switch (newState)
        {
            case GameState.menuPrincipal:
                SetPantalla(22);
                break;

            case GameState.encenderLaringoscopio:
                SetPantalla(0);
                OpenInGameMenu();
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
                meshColliderClosedMouth.SetActive(false);
                meshColliderOpenedMouth.SetActive(true);
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
                pasoInsertarLaringo.SetActive(true);
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
                oxygenConectionCollider.SetActive(true);
                SetPantalla(17);
                break;

            case GameState.acoplarCapnografoYAmbuAlTubo:
                pasoInsertarCapnografo.SetActive(true);
                pasoPosicionFinalAmbu.SetActive(true);
                SetPantalla(18);
                break;

            case GameState.insuflarRealizandoAuscultacion:
                SetPantalla(19);
                break;

            case GameState.asegurarTuboEnBoca:
                pasoColocarFijador.SetActive(true);
                SetPantalla(21);
                break;

            case GameState.simulacionTerminada:
                SetPantalla(22);
                OpenEndGameMenu();
                break;
        }

        Debug.Log("Linea 275 de GameManager. Se va a invocar el nuevo estado");
        onGameStateChanged?.Invoke(newState);
        Debug.Log("Linea 277 de GameManager. Se ha invocado el nuevo estado");
    }

    private void SetPantalla(int index)
    {
        Debug.Log("Linea 283 de GameManager. Se va a cambiar la pantalla.");
        Texture[] instrucciones = instruccionesPorIdioma[idiomaActual];
        Debug.Log("Linea 286 de GameManager. Se obtuvo el conjunto de instrucciones para el idioma actual.");
        if (index >= 0 && index < instrucciones.Length)
        {
            LEDPantallas.SetTexture("_BaseMap", instrucciones[index]);
            LEDPantallas.SetTexture("_EmissionMap", instrucciones[index]);
            Debug.Log("Linea 291 de GameManager. Pantalla cambiada al índice: " + index);
        }
        Debug.Log("Linea 296 de GameManager. Se cambió");
    }

    private void CorrectSoundPlayer(GameState step)
    {
        if (correctSound == null) return;
        int stepIndex = (int)step;

        float minPitch = 0.9f;
        float maxPitch = 1f;

        
        int totalSteps = (int)GameState.simulacionTerminada;
        float pitch = Mathf.Lerp(minPitch, maxPitch, (float)stepIndex / totalSteps);
        correctSound.pitch = pitch;
        correctSound.Play();
    }

    private void OpenInGameMenu()
    {
        Debug.Log("Linea 315 de GameManager. Se abre el menú de juego.");
        inGameMenu.SetActive(true);
        
        if(idiomaActual == Language.Spanish)
        {
            spanishInGame.SetActive(true);
            Debug.Log("Linea 320 de GameManager. Menú en español activado.");
        }
        else
        {
            englishInGame.SetActive(true);
            Debug.Log("Linea 325 de GameManager. Menú en inglés activado.");
        }
    }

    private void CloseInGameMenu()
    {
        inGameMenu.SetActive(false);
        spanishInGame.SetActive(false);
        englishInGame.SetActive(false);
    }

    public void OpenEndGameMenu()
    {
        Debug.Log("Linea 332 de GameManager. Se abre el menú de fin de juego.");
        CloseInGameMenu();

        endGameMenu.SetActive(true);

        if (idiomaActual == Language.Spanish)
        {
            spanishEndGame.SetActive(true);
        }
        else
        {
            englishEndGame.SetActive(true);
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
    insuflarRealizandoAuscultacion,
    asegurarTuboEnBoca,

    simulacionTerminada
}

public enum Language
{
    Spanish,
    English
}