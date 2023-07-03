using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEditor;

public class Dispersion {
    public static System.Random rd = new System.Random();

    public static int[,] generarMatriz(int filas, int columnas){
        int[,] matriz = new int[filas,columnas];
        for(int f = 0; f < filas; f++){
            for(int c = 0; c < columnas; c++){
                matriz[f,c] = 0;
            }
        }
        return matriz;
    }

    public static List<int[]> generarPuntos(int cantidad, int[] valor, int[] fila, int[] columna){
        List<int[]> puntos = new List<int[]>();
        for(int p = 0; p < cantidad; p++){
            puntos.Add(new int[3]{ UnityEngine.Random.Range(valor[0], valor[1]),UnityEngine.Random.Range(fila[0],fila[1]),UnityEngine.Random.Range(columna[0],columna[1])});
        }
        return puntos;
    }

    public static void dispersionPila(int[,] matriz, List<int[]> puntos, float factor_reduccion, int orientacion_dispersion){
        Stack<int[]> pila = new Stack<int[]>();
        foreach (int[] punto in puntos){
            pila.Push(punto);
        }
        int[] punto_actual;
        while(pila.Count > 0){
            punto_actual = pila.Pop();
            List<int[]> nuevos = evaluar(matriz, punto_actual, factor_reduccion, orientacion_dispersion);
            if(nuevos != null){
                if (nuevos.Count > 0) {
                    foreach (int[] punto in nuevos) {
                        pila.Push(punto);
                    }
                }
            }
        }
    }

    public static List<int[]> evaluar(int[,] matriz, int[] punto, float factor_reduccion, int orientacion_dispersion){
        if(punto[0] <= 0){
            return null;
        }
        List<int[]> disponibles = espaciosDisponibles(matriz, punto);
        if (disponibles != null) {
            if (disponibles.Count > 0) {
                disponibles = acomodarPuntos(disponibles, orientacion_dispersion);
                List<int[]> nuevos_puntos = new List<int[]>();
                int nuevo_valor_punto = (int)(punto[0] * factor_reduccion);
                if (nuevo_valor_punto > 0) {
                    foreach (int[] espacio in disponibles) {
                        matriz[espacio[0], espacio[1]] = nuevo_valor_punto;
                        nuevos_puntos.Add(new int[] { nuevo_valor_punto, espacio[0], espacio[1] });
                    }
                    return nuevos_puntos;
                }
                return null;
            }
        }
        return null;
    }

    public static List<int[]> acomodarPuntos(List<int[]> puntos, int orientacion_dispersion){
        switch (orientacion_dispersion) {
            case 2:
                puntos.Reverse(); 
                break;
            case 3: 
                puntos = puntos.OrderBy(punto => rd.Next()).ToList();
                break;
        }
        return puntos;
    }


    public static List<int[]> espaciosDisponibles(int[,] matriz, int[] punto){
        int fila = punto[1];
        int columna = punto[2];
        int maxfila = matriz.GetLength(0);
        int maxcol = matriz.GetLength(1);
        if(verificarBordes(fila, columna, maxfila, maxcol)){
            List<int[]> espacios = new List<int[]>{
                new int[]{fila - 1, columna, 1} //espacio arriba
                ,new int[]{fila, columna + 1, 2} //derecha
                ,new int[]{fila + 1, columna, 3} //abajo
                ,new int[]{fila, columna - 1, 4} //izquierda
            };
            List<int[]> disponibles = new List<int[]>();
            foreach(int[] espacio in espacios){
                if(matriz[espacio[0],espacio[1]] == 0){
                    disponibles.Add(espacio);
                }
            }
            return disponibles;
        }
        return null;
    }

    public static bool verificarBordes(int fila, int columna, int maxfila, int maxcol){
        if(fila - 1 <= 0 || columna - 1 <= 0){
            return false;
        }
        if(fila + 1 >= maxfila || columna + 1 >= maxcol){
            return false;
        }
        return true;
    }
}
