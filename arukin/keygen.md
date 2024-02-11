I am thankful to [Jimmy-Baby](https://github.com/Jimmy-Baby) for the valuable assistance.

# Knight's Tour Solver and Keygen

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

## Keygen Development:

### Reverse Engineering Insights:
- During the reverse engineering process using IDA, the Knight's movement logic was identified within the code block from address `0x4010E4` to `0x40101F`.
```

first your input is checked if it is less than 128 in length:
// esp + 0x110 is first arg for this function which receives the return result from GetDlgItemTextA from main function, it is the length of the input you provide
.text:00401006                 cmp     [esp + 0x110], 128 
.text:00401011                 push    edi
.text:00401012                 mov     edi, eax
.text:00401014                 jnb     short loc_401020
 
+ Then, the algorithm below iterates through the input and maps it to the int[64] variable:
.text:00401031                 mov     al, [edi+esi*2]
.text:00401034                 mov     cl, [edi+esi*2+1]
.text:00401038                 lea     edx, [esp+118h+String]
.text:0040103C                 push    edx             ; String
.text:0040103D                 mov     [esp+11Ch+String], al
.text:00401041                 mov     byte ptr [esp+11Ch+var_10B], cl
.text:00401045                 call    ebx ; atoi
.text:00401047                 mov     ecx, esi
.text:00401049                 mov     edx, esi
.text:0040104B                 shr     ecx, 3
.text:0040104E                 and     edx, 7
.text:00401051                 add     esi, 1
.text:00401054                 add     esp, 4
.text:00401057                 cmp     esi, 40h ; '@'
.text:0040105A                 lea     ecx, [edx+ecx*8]
.text:0040105D                 mov     [esp+ecx*4+118h+var_100], eax
.text:00401061                 jl      short loc_401031
.text:00401063                 mov     eax, 64h ; 'd'
.text:00401068                 mov     [esp+118h+var_108], eax
.text:0040106C                 mov     [esp+118h+var_104], eax
.text:00401070                 xor     edx, edx
.text:00401072                 lea     esi, [esp+118h+var_100]
+ You can recognise the Knight movement because of these decompiled blocks of code from 0x4010E4 - 0x40101F:
LABEL_20:
    v20 = v14;
    if ( v14 > v30 || (v20 = v30, v21 = v14, v14 >= v30) )
      v21 = v30;
    if ( v20 - v21 != 2 )
      goto LABEL_35;
    v22 = v15;
    if ( v15 > v31 || (v22 = v31, v23 = v15, v15 >= v31) )
      v23 = v31;
    if ( v22 - v23 != 1 )
    {
LABEL_35:
      v24 = v14;
      if ( v14 > v30 || (v24 = v30, v25 = v14, v14 >= v30) )
        v25 = v30;
      if ( v24 - v25 != 1 )
        return 0;
      v26 = v15;
      if ( v15 > v31 || (v26 = v31, v27 = v15, v15 >= v31) )
        v27 = v31;
      if ( v26 - v27 != 2 )
        return 0;
 ```

- Recognizing the specific conditions and patterns related to Knight movements, the keygen was crafted to leverage this knowledge.

### Key Generation Algorithm:
- The keygen uses the identified Knight's movement logic to generate valid keys for the crackme.
- It applies the same conditions and checks found in the decompiled code to ensure the generated key reflects a valid Knight's tour.

### Integration with Crackme:
- The developed keygen can be used to generate valid keys for the crackme after successfully reversing the Knight's movement logic.
- By understanding and replicating the logic, users can obtain valid keys to unlock the crackme.

### Technical Implementation:
- The keygen incorporates the Knight's movement conditions into its algorithm for generating valid keys.
- It follows the same sequence of operations as identified during reverse engineering to ensure compatibility with the crackme's verification process.
- The keygen can be compiled and executed alongside the crackme, providing a technical solution to unlocking its functionalities.

### How to Use the Keygen:
- Execute the keygen after compiling the code.(elf already built)
- The keygen will generate valid keys based on the Knight's movement logic identified in the reverse engineering phase.
- Use the generated keys to unlock the crackme and explore its functionalities.

By combining the Knight's Tour Solver and the keygen, this system offers both an entertaining puzzle-solving experience and a technical challenge for those interested in reverse engineering and key generation.
