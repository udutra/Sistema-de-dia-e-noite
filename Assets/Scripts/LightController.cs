using System;
using UnityEngine;

public class LightController : MonoBehaviour {

    private TimeController timeController;
    private SkyBoxController skyBoxController;
    [SerializeField] private Light luzSol, luzLua, luzConstrucao, luzLanterna;
    [SerializeField] private Color luzAmbienteDia, luzAmbienteNoite;
    [SerializeField] private AnimationCurve curvaLuzAmbiente, curvaTransicaoSkybox, curvaTrasicaoCenario;
    [SerializeField] private float intensidadeMaxLuzSol, intensidadeMaxLuzLua, intensidadeMaxLuzConstrucao, intensidadeMaxLuzLanterna;

    private void Start() {
        Inicializacao();
    }

    private void Update() {
        RotacionarSol();
        AtualizaConfiguracaoDeLuzESkybox();
    }

    private void Inicializacao() {
        timeController = FindObjectOfType<TimeController>();
        skyBoxController = FindObjectOfType<SkyBoxController>();
        luzSol.color = luzAmbienteDia;
        luzLua.color = luzAmbienteNoite;
    }

    private void RotacionarSol() {
        float rotacaoLuzSol;

        if (timeController.tempoAtual.TimeOfDay > timeController.tempoNascerDoSol && timeController.tempoAtual.TimeOfDay < timeController.tempoPorDoSol) {
            //Dia
            TimeSpan duracaoParaONascimentoAoPorDoSol = timeController.CalcularDiferençaDeTempo(timeController.tempoNascerDoSol, timeController.tempoPorDoSol);
            TimeSpan duracaoDesdeONascimento = timeController.CalcularDiferençaDeTempo(timeController.tempoNascerDoSol, timeController.tempoAtual.TimeOfDay);

            double porcentagem = duracaoDesdeONascimento.TotalMinutes / duracaoParaONascimentoAoPorDoSol.TotalMinutes;
            rotacaoLuzSol = Mathf.Lerp(0f, 180f, (float)porcentagem);
        }

        else {
            //Noite
            TimeSpan duracaoParaOPorDoSolAoNascimento = timeController.CalcularDiferençaDeTempo(timeController.tempoPorDoSol, timeController.tempoNascerDoSol);
            TimeSpan duracaoDesdeOPorDoSol = timeController.CalcularDiferençaDeTempo(timeController.tempoPorDoSol, timeController.tempoAtual.TimeOfDay);

            double porcentagem = duracaoDesdeOPorDoSol.TotalMinutes / duracaoParaOPorDoSolAoNascimento.TotalMinutes;
            rotacaoLuzSol = Mathf.Lerp(180f, 360f, (float)porcentagem);
        }

        luzSol.transform.rotation = Quaternion.AngleAxis(rotacaoLuzSol, Vector3.right);
    }

    private void AtualizaConfiguracaoDeLuzESkybox() {
        float pontoProducao = Vector3.Dot(luzSol.transform.forward, Vector3.down);

        TrocarIntencidadeDaLuz(luzSol, 0f, intensidadeMaxLuzSol, pontoProducao, curvaLuzAmbiente);
        TrocarIntencidadeDaLuz(luzLua, intensidadeMaxLuzLua, 0f, pontoProducao, curvaLuzAmbiente);
        TrocarIntencidadeDaLuz(luzConstrucao, intensidadeMaxLuzConstrucao, 0f, pontoProducao, curvaTrasicaoCenario);
        TrocarIntencidadeDaLuz(luzLanterna, intensidadeMaxLuzLanterna, 0f, pontoProducao, curvaLuzAmbiente);

        RenderSettings.ambientLight = Color.Lerp(luzAmbienteNoite, luzAmbienteDia, curvaLuzAmbiente.Evaluate(pontoProducao));
        skyBoxController.mistura = Mathf.Lerp(1f, 0f, curvaTransicaoSkybox.Evaluate(pontoProducao));
    }


    private void TrocarIntencidadeDaLuz(Light luzAlvo, float lerpA, float lerpB, float pontoProducao, AnimationCurve curvaTempo) {
        luzAlvo.intensity = Mathf.Lerp(lerpA, lerpB, curvaTempo.Evaluate(pontoProducao));
    }
}
