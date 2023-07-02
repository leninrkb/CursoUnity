using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Metodos : MonoBehaviour
{
    
    public static int[,] generarMatriz(int ancho, int alto, bool llenar){
        int[,] mapa = new int[ancho,alto];
        for (int x = 0; x < ancho; x++)
        {
            for (int y = 0; y < alto; y++)
            {
                mapa[x,y] = llenar ? 1 : 0;
            }
        }
        return mapa;
    } 


    public static void generarMapa(int[,] matriz, Tilemap canvas, TileBase tile){
        canvas.ClearAllTiles();
        // matriz.GetLength(0) obtengo filas
        // matriz.GetLength(1) obtengo columnas
        for (int row = 0; row < matriz.GetLength(0); row++)
        {
            for (int col = 0; col < matriz.GetLength(1); col++)
            {
                if(matriz[row,col] == 1){
                    canvas.SetTile(new Vector3Int(row,col,0), tile);
                }
            }
        }
    }  
}
