using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Metodos : MonoBehaviour
{
    
    public static int[,] generarMatriz(int row, int col, bool llenar){
        int[,] mapa = new int[row,col];
        for (int fila = 0; fila < row; fila++){
            for (int columna = 0; columna < row; columna++){
                mapa[fila,columna] = llenar ? 1 : 0;
            }
        }
        return mapa;
    } 


    public static void generarMapa(int[,] matriz, Tilemap canvas, TileBase tile, int[] rango_aceptacion){
        canvas.ClearAllTiles();
        // matriz.GetLength(0) obtengo filas y j
        // matriz.GetLength(1) obtengo columnas x i
        for (int fila = 0; fila < matriz.GetLength(0); fila++) {
            for (int columna = 0; columna < matriz.GetLength(1); columna++) {
                int valor = matriz[fila, columna];
                if ( valor >= rango_aceptacion[0] && valor <= rango_aceptacion[1]){
                    canvas.SetTile(new Vector3Int(fila,columna,0), tile);
                }
            }
        }
    }  

    public static int[,] mapaPerlin(int[,] matriz, float semilla){
        int nuevo_punto;
        int altura_matriz = matriz.GetLength(1);
        float reduccion = 0.5f;
        for (int fila = 0; fila < matriz.GetLength(0); fila++){
            nuevo_punto = Mathf.FloorToInt((Mathf.PerlinNoise(fila, semilla) - reduccion ) * altura_matriz);
            nuevo_punto += altura_matriz / 2;
            for (int columna = nuevo_punto; columna >= 0; columna--){
                matriz[fila,columna] = 1;
            }
        }
        return matriz;
    }
}
