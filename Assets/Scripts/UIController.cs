using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour {
    [SerializeField] private TMP_Text txtTempo;
    private TimeController timeController;

    private void Start() {
        timeController = FindObjectOfType<TimeController>();
    }

    public void AtualizarTexto() {
        txtTempo.text = timeController.tempoAtual.ToString("HH:mm");
    }
}