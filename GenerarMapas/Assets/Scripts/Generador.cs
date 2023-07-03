using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum Algoritmo{
    perlinNoise,
    dispersion
}
public enum Orientacion{
    arriba = 1,
    abajo = 2,
    izquierda = 3,
    derecha = 4,
    shuffle = 5
}
public class Generador : MonoBehaviour{
    [Header("recursos")]
    public Tilemap canvas;
    public TileBase tile;

    [Header("matriz")]
    public int filas = 100;
    public int columnas = 100;

    [Header("puntos")]
    public float semilla = 111;
    public bool generar_semilla;

    [Header("parametros")]
    public int cantidad_puntos = 1;
    public float factor_reduccion = 0.900f;
    public Orientacion orientacion_dispersion = Orientacion.shuffle;
    public int[] rango_valores = {100,1000};
    public int[] rango_aparicion_fila = {50,50};
    public int[] rango_aparicion_columna = {50,50};
    public int[] rango_aceptacion = { 3, 100 };

    [Header("Algoritmos")]
    public Algoritmo algoritmo = Algoritmo.perlinNoise;
    public bool llenar;
    
    public void generar_mapa(){
        int[,] matriz;
        if(generar_semilla){
            semilla = Random.Range(0,100);
        }
        switch(algoritmo){
            case Algoritmo.perlinNoise:
                matriz = Metodos.generarMatriz(filas, columnas, llenar);
                matriz = Metodos.mapaPerlin(matriz, semilla);
                Metodos.generarMapa(matriz, canvas, tile, rango_aceptacion);
            break;
            case Algoritmo.dispersion:
                Debug.Log("first");
                matriz = Dispersion.generarMatriz(filas, columnas);
                Debug.Log("matriz " + matriz.GetLength(0) + "" + matriz.GetLength(1));
                int mitad_fila = matriz.GetLength(0) / 2;
                int mitad_columna = matriz.GetLength(1) / 2;
                rango_aparicion_fila = new int[] {mitad_fila, mitad_fila};
                rango_aparicion_columna = new int[] {mitad_columna, mitad_columna};
                List<int[]> puntos = Dispersion.generarPuntos(cantidad_puntos, rango_valores, rango_aparicion_fila, rango_aparicion_columna);
                Debug.Log("puntos " + puntos.Count);
                Debug.Log("orientacion " + (int)orientacion_dispersion);
                Dispersion.dispersionPila(matriz, puntos, factor_reduccion, (int)orientacion_dispersion);
                Metodos.generarMapa(matriz, canvas, tile, rango_aceptacion);
            break;
        }
    }

    public void limpiar_mapa(){
        canvas.ClearAllTiles();
        Debug.Log("limpiando mapa!");
    }
}
