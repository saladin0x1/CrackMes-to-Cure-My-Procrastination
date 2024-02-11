using System;
using System.Linq;

// The main class representing the Knight's Tour problem
public class KnightTour
{
    // Constants defining the size of the chessboard
    public static int N = 8;
    public static int BOARD_SIZE = N * N;

    // Arrays representing possible moves of the knight
    public int[] cx = new int[] { 1, 1, 2, 2, -1, -1, -2, -2 };
    public int[] cy = new int[] { 2, -2, 1, -1, 2, -2, 1, -1 };

    // Function to check if a position is within the board limits
    bool limits(int x, int y)
    {
        return ((x >= 0 && y >= 0) && (x < N && y < N));
    }

    // Function to check if a position is empty on the chessboard
    bool isempty(int[] a, int x, int y)
    {
        return ((limits(x, y)) && (a[y * N + x] < 0));
    }

    // Function to calculate the degree of a position, i.e., the number of possible moves
    int getDegree(int[] a, int x, int y)
    {
        int count = 0;
        for (int i = 0; i < N; ++i)
            if (isempty(a, (x + cx[i]), (y + cy[i])))
                count++;

        return count;
    }

    // Function to determine the next move of the knight based on Warnsdorff's rule
    Cell nextMove(int[] a, Cell cell)
    {
        int min_deg_idx = -1, c, min_deg = (N + 1), nx, ny;
        Random random = new Random(Guid.NewGuid().GetHashCode());
        int start = random.Next(0, 1000);

        for (int count = 0; count < N; ++count)
        {
            int i = (start + count) % N;
            nx = cell.x + cx[i];
            ny = cell.y + cy[i];

            if ((isempty(a, nx, ny)) && (c = getDegree(a, nx, ny)) < min_deg)
            {
                min_deg_idx = i;
                min_deg = c;
            }
        }

        if (min_deg_idx == -1)
            return null;

        nx = cell.x + cx[min_deg_idx];
        ny = cell.y + cy[min_deg_idx];

        // Mark the position on the chessboard with the current step count
        a[ny * N + nx] = a[cell.y * N + cell.x] + 1;

        // Update the current cell to the new position
        cell.x = nx;
        cell.y = ny;

        return cell;
    }

    // Function to print the chessboard
    void print(int[] a)
    {
        for (int i = 0; i < N; ++i)
        {
            for (int j = 0; j < N; ++j)
            {
                // Print the chessboard values without spaces
                Console.Write($"{(a[j * N + i]):D2}");
            }
        }
        Console.WriteLine(); // Move to the next line after printing the entire board
    }

    // Function to check if two positions are neighbors on the chessboard
    bool neighbour(int x, int y, int xx, int yy)
    {
        for (int i = 0; i < N; ++i)
            if (((x + cx[i]) == xx) && ((y + cy[i]) == yy))
                return true;

        return false;
    }

    // Function to find a closed tour of the knight on the chessboard
    int[] findClosedTour()
    {
        // Initialize the chessboard with all positions set to -1 (unvisited)
        int[] a = new int[BOARD_SIZE];
        Array.Fill(a, -1);

        int sx = 0;
        int sy = 0;
        Cell cell = new Cell(sx, sy);
        a[cell.y * N + cell.x] = 0; // Start from 0

        Cell ret = null;
        // Iterate through all positions on the chessboard
        for (int i = 0; i < BOARD_SIZE - 1; ++i)
        {
            ret = nextMove(a, cell);
            if (ret == null)
                return null;
        }

        // Check if the last position is a neighbor of the starting position to form a closed tour
        if (!neighbour(ret.x, ret.y, sx, sy))
            return null;

        // Return the chessboard with the closed tour
        return a;
    }

    // Main method to execute the Knight's Tour algorithm
    static void Main()
    {
        KnightTour knightTour = new KnightTour();

        // Keep finding closed tours until a solution is found
        while (true)
        {
            int[] solution = knightTour.findClosedTour();

            if (solution != null)
            {
                // Print the solution chessboard
                knightTour.print(solution);
                break;
            }
        }
    }
}

// Class representing a cell on the chessboard with x and y coordinates
class Cell
{
    public int x;
    public int y;

    // Constructor to initialize the cell with coordinates
    public Cell(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}
