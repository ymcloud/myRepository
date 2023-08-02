using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JingChess : MonoBehaviour
{
    private static int player;
    private static int count;
    private int winner;
    private int[,] chessBoard = new int[3,3];
    // Start is called before the first frame update
    void Start()
    {
        print("井字棋游戏开始!");
        Restart();
    }

    void OnGUI(){
        GUI.Box(new Rect(500, 25, 200, 350), "井字棋");
        if (GUI.Button(new Rect(562, 310, 75, 25), "退出游戏")) OnExitGame();
        if (winner == 0){
            if (GUI.Button(new Rect(562, 275, 75, 25), "重新开始")) Restart();
        }
        else{
            if (GUI.Button(new Rect(562, 275, 75, 25), "再来一局")) Restart();
        }
        if (winner == 0){
            for (int i = 0; i < 3; i++){
                for (int j = 0; j < 3; j++){
                    if (chessBoard[i,j] == 0 && GUI.Button(new Rect(525 + j * 50, 60 + i * 50, 50, 50), "")){
                        if (player == 1){
                            chessBoard[i,j] = player;
                            count++;
                            Judge();
                            player = 2;
                            if (winner == 0){
                                aiTurn();
                            }
                        }
                    }
                    else if (chessBoard[i,j] == 1){
                        GUI.Box(new Rect(525 + j * 50, 60 + i * 50, 50, 50), "O");
                    }
                    else if (chessBoard[i,j] == 2){
                        GUI.Box(new Rect(525 + j * 50, 60 + i * 50, 50, 50), "X");
                    }
                }
            }
        }
        else{
            for (int i = 0; i < 3; i++){
                for (int j = 0; j < 3; j++){
                    if (chessBoard[i,j] == 0){
                        GUI.Box(new Rect(525 + j * 50, 60 + i * 50, 50, 50), "");
                    }
                    else if (chessBoard[i,j] == 1){
                        GUI.Box(new Rect(525 + j * 50, 60 + i * 50, 50, 50), "O");
                    }
                    else if (chessBoard[i,j] == 2){
                        GUI.Box(new Rect(525 + j * 50, 60 + i * 50, 50, 50), "X");
                    }
                }
            }
            if (winner == 1){
                GUI.Box(new Rect(562, 225, 75, 25), "you win!");
            }
            else if (winner == 2){
                GUI.Box(new Rect(562, 225, 75, 25), "you lose!");
            }
            else if (winner == 3){
                GUI.Box(new Rect(562, 225, 75, 25), "draw!");
            }
        }
    }

    void Judge(){
        for (int i = 0; i < 3; i++){
            if (chessBoard[i,0] != 0 && chessBoard[i,0] == chessBoard[i,1] && chessBoard[i,0] == chessBoard[i,2]){
                winner = chessBoard[i,0];
            }
            if (chessBoard[0,i] != 0 && chessBoard[0,i] == chessBoard[1,i] && chessBoard[0,i] == chessBoard[2,i]){
                winner = chessBoard[0,i];
            }
        }
        if (chessBoard[0,0] != 0 && chessBoard[0,0] == chessBoard[1,1] && chessBoard[0,0] == chessBoard[2,2]){
                winner = chessBoard[0,0];
            }
        if (chessBoard[2,0] != 0 && chessBoard[2,0] == chessBoard[1,1] && chessBoard[2,0] == chessBoard[0,2]){
                winner = chessBoard[0,0];
            }
        if (winner == 0 && count == 9){
            winner = 3;
        }
    }

    void aiTurn(){
        int score = 0;
        int x = 0;
        int y = 0;
        for (int i = 0; i < 3; i++){
            for (int j = 0; j < 3; j++){
                if (score < countScore(i,j) && chessBoard[i,j] == 0){
                    score = countScore(i,j);
                    x = i;
                    y = j;
                }
            }
        }
        chessBoard[x,y] = player;
        count++;
        Judge();
        player = 1;
    }

    int countScore(int i, int j){
        int score = 0;
        if (chessBoard[0,j] + chessBoard[1,j] + chessBoard[2,j] == 4){
            score = 5;
        }
        else if (chessBoard[0,j] + chessBoard[1,j] + chessBoard[2,j] == 2 && chessBoard[0,j] != 2 && chessBoard[1,j] != 2 && chessBoard[2,j] != 2){
            if (score < 4){
                score = 4;
            }
        }
        else if (chessBoard[0,j] + chessBoard[1,j] + chessBoard[2,j] == 2 && chessBoard[0,j] != 1 && chessBoard[1,j] != 1 && chessBoard[2,j] != 1){
            if (score < 3){
                score = 3;
            }
        }
        else if (chessBoard[0,j] + chessBoard[1,j] + chessBoard[2,j] == 1){
            if (score < 2){
                score = 2;
            }
        }
        else if (chessBoard[0,j] + chessBoard[1,j] + chessBoard[2,j] == 0){
            if (score < 1){
                score = 1;
            }
        }
        if (chessBoard[i,j] + chessBoard[i,j] + chessBoard[i,j] == 4){
            score = 5;
        }
        else if (chessBoard[i,0] + chessBoard[i,1] + chessBoard[i,2] == 2 && chessBoard[i,0] != 2 && chessBoard[i,1] != 2 && chessBoard[i,2] != 2){
            if (score < 4){
                score = 4;
            }
        }
        else if (chessBoard[i,0] + chessBoard[i,1] + chessBoard[i,2] == 2 && chessBoard[i,0] != 1 && chessBoard[i,1] != 1 && chessBoard[i,2] != 1){
            if (score < 3){
                score = 3;
            }
        }
        else if (chessBoard[i,0] + chessBoard[i,1] + chessBoard[i,2] == 1){
            if (score < 2){
                score = 2;
            }
        }
        else if (chessBoard[i,0] + chessBoard[i,1] + chessBoard[i,2] == 0){
            if (score < 1){
                score = 1;
            }
        }
        if(i == j){
            if (chessBoard[0,0] + chessBoard[1,1] + chessBoard[2,2] == 4){
                score = 5;
            }
            else if (chessBoard[0,0] + chessBoard[1,1] + chessBoard[2,2] == 2 && chessBoard[0,0] != 2 && chessBoard[1,1] != 2 && chessBoard[2,2] != 2){
                if (score < 4){
                    score = 4;
                }
            }
            else if (chessBoard[0,0] + chessBoard[1,1] + chessBoard[2,2] == 2 && chessBoard[0,0] != 1 && chessBoard[1,1] != 1 && chessBoard[2,2] != 1){
                if (score < 3){
                    score = 3;
                }
            }
            else if (chessBoard[0,0] + chessBoard[1,1] + chessBoard[2,2] == 1){
                if (score < 2){
                    score = 2;
                }
            }
            else if (chessBoard[0,0] + chessBoard[1,1] + chessBoard[2,2] == 0){
                if (score < 1){
                    score = 1;
                }
            }
        }
        if(i + j == 2){
            if (chessBoard[0,2] + chessBoard[1,1] + chessBoard[2,0] == 4){
                score = 5;
            }
            else if (chessBoard[0,2] + chessBoard[1,1] + chessBoard[2,0] == 2 && chessBoard[0,2] != 2 && chessBoard[1,1] != 2 && chessBoard[2,0] != 2){
                if (score < 4){
                    score = 4;
                }
            }
            else if (chessBoard[0,2] + chessBoard[1,1] + chessBoard[2,0] == 2 && chessBoard[0,2] != 1 && chessBoard[1,1] != 1 && chessBoard[2,0] != 1){
                if (score < 3){
                    score = 3;
                }
            }
            else if (chessBoard[0,2] + chessBoard[1,1] + chessBoard[2,0] == 1){
                if (score < 2){
                    score = 2;
                }
            }
            else if (chessBoard[0,2] + chessBoard[1,1] + chessBoard[2,0] == 0){
                if (score < 1){
                    score = 1;
                }
            }
        }
        return score;
    }

    void Restart(){
        player = 1;
        winner = 0;
        count = 0;
        for (int i = 0; i < 3; i++){
            for (int j = 0; j < 3; j++){
                chessBoard[i,j] = 0;
            }
        }
    }

    void OnExitGame(){
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
