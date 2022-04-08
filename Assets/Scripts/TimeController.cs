using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeController : MonoBehaviour {

    private UIController uiController;

    [SerializeField] private float multiplicadorTempo, horaInicial, horaNascerDoSol, horaPorDoSol;
    public DateTime tempoAtual;
    public TimeSpan tempoNascerDoSol, tempoPorDoSol;

    private void Start() {
        Inicializacao();
    }

    private void Update() {
        AtualizarHoraDoDia();
    }

    private void Inicializacao() {
        uiController = FindObjectOfType<UIController>();
        tempoAtual = DateTime.Now.Date + TimeSpan.FromHours(horaInicial);
        tempoNascerDoSol = TimeSpan.FromHours(horaNascerDoSol);
        tempoPorDoSol = TimeSpan.FromHours(horaPorDoSol);
    }

    private void AtualizarHoraDoDia() {
        tempoAtual = tempoAtual.AddSeconds(Time.deltaTime * multiplicadorTempo);
        uiController.AtualizarTexto();
    }

    public TimeSpan CalcularDiferençaDeTempo(TimeSpan tempoInicial, TimeSpan tempoFinal) {
        TimeSpan diferença = tempoFinal - tempoInicial;
        
        if (diferença.TotalSeconds < 0) {
            diferença += TimeSpan.FromHours(24);
        }

        return diferença;
    }
}