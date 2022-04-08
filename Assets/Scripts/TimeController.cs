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

    public TimeSpan CalcularDiferen�aDeTempo(TimeSpan tempoInicial, TimeSpan tempoFinal) {
        TimeSpan diferen�a = tempoFinal - tempoInicial;
        
        if (diferen�a.TotalSeconds < 0) {
            diferen�a += TimeSpan.FromHours(24);
        }

        return diferen�a;
    }
}