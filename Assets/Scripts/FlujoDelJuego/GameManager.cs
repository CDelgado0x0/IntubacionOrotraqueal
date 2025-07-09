using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager applicationController;

    public GameState state;

    public static event Action<GameState> onGameStateChanged;

    void Awake(){
        applicationController = this;
    }

    void Start(){

    }

    

    public void updateGameState(GameState newState){
        State = newState;

        switch(newState){
            //Insert all cases
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

    //Fase 3, oxigenación
    introducirCanulaGirada,
    girarCanula,
    oxigenarPaciente,

    //Fase 4, intubación
    introducirLaringoscopio,
    introducirTuboOrotraqueal,
    sacarLaringoscopio,
    inflarBalon,
    asegurarTuboEnBoca,
    insuflar,
    comprobarTuboIntroducidoCorrectamente,

    simulacionTerminada
}