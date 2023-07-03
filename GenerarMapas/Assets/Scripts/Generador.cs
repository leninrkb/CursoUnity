using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum Algoritmo{
    perlinNoise
}

public class Generador : MonoBehaviour{
    public Tilemap canvas;
    public TileBase tile;
    public int rows;
    public int cols;
    public float semilla;
    public bool generar_semilla;
    public Algoritmo algoritmo = Algoritmo.perlinNoise;
    public bool llenar;
    
    public void generar_mapa(){
        int[,] matriz = Metodos.generarMatriz(rows, cols, llenar);
        if(generar_semilla){
            semilla = Random.Range(0,100);
        }
        switch(algoritmo){
            case Algoritmo.perlinNoise:
                matriz = Metodos.mapaPerlin(matriz, semilla);
            break;
        }
        Metodos.generarMapa(matriz, canvas, tile);
        Debug.Log("generando mapa!");
    }

    public void limpiar_mapa(){
        canvas.ClearAllTiles();
        Debug.Log("limpiando mapa!");
    }
}
