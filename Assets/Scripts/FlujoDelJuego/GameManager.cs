using System;
using System.Collections;
using System.Collections.Generic;
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
        updateGameState(GameState.menuPrincipal);
    }

    private void OnValidate() //Esto sirve para poder cambiar el estado desde el inspector, comentar si no es necesario.
    {
        updateGameState(state);
    }

    public void updateGameState(GameState newState){
        state = newState;

        switch(newState){
            case GameState.menuPrincipal:
                // Aquí puedes agregar lógica específica para el menú principal
                break;
            case GameState.encenderLaringoscopio:
                // Lógica para encender el laringoscopio
                break;
            case GameState.inflarBalonTuboOrotraqueal:
                // Lógica para inflar el balón del tubo orotraqueal
                break;
            case GameState.posicionarCabezaPaciente:
                // Lógica para posicionar la cabeza del paciente
                break;
            case GameState.introducirCanulaGirada:
                // Lógica para introducir la cánula girada
                break;
            case GameState.girarCanula:
                // Lógica para girar la cánula
                break;
            case GameState.oxigenarPaciente:
                // Lógica para oxigenar al paciente
                break;
            case GameState.introducirLaringoscopio:
                // Lógica para introducir el laringoscopio
                break;
            case GameState.introducirTuboOrotraqueal:
                // Lógica para introducir el tubo orotraqueal
                break;
            case GameState.sacarLaringoscopio:
                // Lógica para sacar el laringoscopio
                break;
            case GameState.inflarBalon:
                // Lógica para inflar el balón
                break;
            case GameState.asegurarTuboEnBoca:
                // Lógica para asegurar el tubo en la boca
                break;
            case GameState.insuflar:
                // Lógica para insuflar aire
                break;
            case GameState.comprobarTuboIntroducidoCorrectamente:
                // Lógica para comprobar si el tubo está introducido correctamente
                break;
            case GameState.simulacionTerminada:
                // Lógica para finalizar la simulación
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