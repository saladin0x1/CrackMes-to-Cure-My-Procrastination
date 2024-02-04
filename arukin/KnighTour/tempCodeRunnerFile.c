#include <stdio.h>
#include <stdbool.h>

#define SIZE 8

int movesX[] = {2, 1, -1, -2, -2, -1, 1, 2};
int movesY[] = {1, 2, 2, 1, -1, -2, -2, -1};

bool isSafe(int x, int y, int chessboard[SIZE][SIZE]) {
    return (x >= 0 && x < SIZE && y >= 0 && y < SIZE && chessboard[x][y] == -1);
}

bool knightTour(int x, int y, int moveIndex, int chessboard[SIZE][SIZE]) {
    if (moveIndex == SIZE * SIZE) {
        return true;  // Tour complete
    }

    for (int i = 0; i < 8; ++i) {
        int nextX = x + movesX[i];
        int nextY = y + movesY[i];

        if (isSafe(nextX, nextY, chessboard)) {
            chessboard[nextX][nextY] = moveIndex;

            if (knightTour(nextX, nextY, moveIndex + 1, chessboard)) {
                return true;  // If the next move leads to a complete tour
            }

            chessboard[nextX][nextY] = -1;  // Backtrack
        }
    }

    return false;
}

void printChessboard(int chessboard[SIZE][SIZE]) {
    for (int i = 0; i < SIZE; ++i) {
        for (int j = 0; j < SIZE; ++j) {
            printf("%02d ", chessboard[i][j]);
        }
        printf("\n");
    }
}

int main() {
    int chessboard[SIZE][SIZE];
    for (int i = 0; i < SIZE; ++i) {
        for (int j = 0; j < SIZE; ++j) {
            chessboard[i][j] = -1;  // Initialize chessboard with -1
        }
    }

    int startX = 0;
    int startY = 0;

    chessboard[startX][startY] = 0;  // Starting point

    if (knightTour(startX, startY, 1, chessboard)) {
        printf("Knight's Tour:\n");
        printChessboard(chessboard);
    } else {
        printf("No solution found.\n");
    }

    return 0;
}
