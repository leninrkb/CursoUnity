using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Generador : MonoBehaviour
{
    public Tilemap canvas;
    public TileBase tile;
    public int filas;
    public int columnas;
    
    public void generar_mapa(){
        int[,] matriz = Metodos.generarMatriz(filas, columnas, true);
        Metodos.generarMapa(matriz, canvas, tile);
        Debug.Log("generando mapa!");
    }

    public void limpiar_mapa(){
        canvas.ClearAllTiles();
        Debug.Log("limpiando mapa!");
    }
}
