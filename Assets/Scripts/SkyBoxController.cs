using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SkyBoxController : MonoBehaviour {

    [SerializeField] private Material misturarSkybox;

    [Header("Valores")]
    [SerializeField, Range(0f, 360f)] private float rotacao;
    [Range(0f, 1)] public float mistura;

    private void Update() {
        AtualizarParametrosDoMaterial();
    }

    private void AtualizarParametrosDoMaterial() {
        misturarSkybox.SetFloat("_Rotation", rotacao);
        misturarSkybox.SetFloat("_Blend", mistura);
    }
}
