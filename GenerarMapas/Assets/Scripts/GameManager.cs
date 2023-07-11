using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BoardManager board;

    private void Awake() {
        board = GetComponent<BoardManager>();
    }

    private void Start() {
        iniciarJuego();
    }

    private void iniciarJuego() { 
        board.componerEscena();
    }
}
