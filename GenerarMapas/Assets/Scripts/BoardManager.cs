using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public int rows = 8;
    public int cols = 8;
    public GameObject[] tiles_piso, outer_walls;

    private Transform transform_tablero;

    public void componerEscena() {
        montarBoard();   
    }

    public void montarBoard() {
        transform_tablero = new GameObject("tablero").transform;
        int total_tiles_piso = tiles_piso.Length;
        int total_outer_walls = outer_walls.Length;
        for (int x = -1; x < rows + 1; x++) {
            for (int y = -1; y < cols + 1; y++) {
                GameObject por_instanciar = obtenerRandomTile(tiles_piso, 0, total_tiles_piso);
                if (x == -1 || y == -1 || x == rows || y == cols)
                {
                    por_instanciar = obtenerRandomTile(outer_walls, 0, total_outer_walls);
                }
                GameObject nueva_instancia = Instantiate(por_instanciar, new Vector2(x,y), Quaternion.identity);
                nueva_instancia.transform.SetParent(transform_tablero);
            }
        }
    }

    public GameObject obtenerRandomTile(GameObject[] tiles, int min, int max) {
        return tiles[Random.Range(min, max)];
    }
}
