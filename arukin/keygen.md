I am thankful to [Jimmy-Baby](https://github.com/Jimmy-Baby) for the valuable assistance.

# Knight's Tour Solver (keygen)

## Object-Oriented Design Overview:

### Main Class (`KnightTour`):
- Represents the main functionality of the Knight's Tour problem.
- Manages the chessboard, knight movements, and finding a closed tour.

### Helper Class (`Cell`):
- Represents a cell on the chessboard with `x` and `y` coordinates.
- Provides a simple structure to store and manipulate cell coordinates.

## Core Logic:

### Chessboard Representation:
- The chessboard is represented as a one-dimensional array of integers (`int[] a`).
- Each element in the array corresponds to a cell on the chessboard.
- The size of the chessboard is defined as a constant (`N = 8`), and the total number of cells is calculated as `BOARD_SIZE = N * N`.

### Knight Movements:
- The knight's possible moves are defined by two arrays (`cx` and `cy`) representing the horizontal and vertical components of the moves.
- The `Cell` class is used to represent the knight's current position on the chessboard.

### Helper Functions:
- Several helper functions are defined to check board limits, check if a position is empty, calculate the degree of a position, and determine if two positions are neighbors.

### Next Move Calculation (`nextMove` Function):
- Implements Warnsdorff's rule to determine the next move of the knight based on the degree of each potential move.
- Randomization is introduced to the starting index for added variety in the search process.

### Printing Chessboard (`print` Function):
- Displays the chessboard after finding a closed tour.

### Closed Tour Search (`findClosedTour` Function):
- Initializes the chessboard with all positions set to -1 (unvisited).
- Iteratively finds the next move until a closed tour is formed.
- Checks if the last position is a neighbor of the starting position to ensure a closed tour.

### Main Execution (`Main` Method):
- Creates an instance of the `KnightTour` class.
- Continuously searches for closed tours until a solution is found.
- Prints the solution chessboard.

## How to Use:

1. **Clone the Repository:**
   - Clone the GitHub repository to your local machine.

2. **Compile and Run:**
   - Use a C# compiler (e.g., Visual Studio, dotnet CLI) to compile and run the `KnightTour` class.
   - The program will find and print closed tours of the knight on the chessboard.

3. **Explore and Experiment:**
   - Understand the code structure and logic by reviewing comments.
   - Experiment with different chessboard sizes (`N`) or try modifying the starting position.

## Additional Notes:

- **Randomization:**
  - Randomization is introduced to the search process, providing variations in tour paths.

- **Object-Oriented Principles:**
  - The code follows basic OOP principles by encapsulating related functionalities within classes (`KnightTour` and `Cell`).

- **Readability:**
  - Comments have been added to explain each part of the code, making it easier for others to understand and modify.
