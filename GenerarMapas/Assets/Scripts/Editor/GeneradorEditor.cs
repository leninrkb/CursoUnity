using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Generador))]
public class GeneradorEditor : Editor {

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        Generador generador = (Generador) target;
        if (GUILayout.Button("generar mapa")) {
            generador.generar_mapa();
        }
        if (GUILayout.Button("limpiar mapa")) {
            generador.limpiar_mapa();
        }
    }
}
