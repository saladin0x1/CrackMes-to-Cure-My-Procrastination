// Initialize variables
let knightPosition = 0;
let inputOrder = [];
let movementLog = []; // Array to store movement positions

// Function to render the chessboard based on user input
function renderChessboard() {
  // Get the user input from the textarea
  const inputChessboard = document.getElementById('inputChessboard').value;
  console.log('Input Chessboard:', inputChessboard);

  // Convert the input into an array of numbers
  const chessboardValues = inputChessboard.split(' ').map(Number);

  // Validate the input length
  if (chessboardValues.length !== 64) {
    alert('Please provide 64 space-separated integers.');
    return;
  }

  // Save the original order for highlighting
  inputOrder = Array.from({ length: 64 }, (_, i) => i);

  // Get the chessboard container element
  const chessboardContainer = document.getElementById('chessboard');
  chessboardContainer.innerHTML = '';

  // Add column headers with numerical values 1 to 8
  for (let i = 0; i <= 8; i++) {
    const header = document.createElement('div');
    header.classList.add('header');
    header.textContent = i === 0 ? '' : i;
    chessboardContainer.appendChild(header);
  }

  // Sort the order based on the chessboard values
  inputOrder.sort((a, b) => chessboardValues[a] - chessboardValues[b]);

  // Add rows and cells to the chessboard
  for (let row = 0; row < 8; row++) {
    // Add row header with numerical values 1 to 8
    const rowHeader = document.createElement('div');
    rowHeader.classList.add('header');
    rowHeader.textContent = row + 1;
    chessboardContainer.appendChild(rowHeader);

    // Add cells
    for (let col = 0; col < 8; col++) {
      const cell = document.createElement('div');
      cell.classList.add('cell');
      const value = chessboardValues[row * 8 + col].toString().padStart(2, '0');
      cell.textContent = value;
      // Highlight the current cell based on the knight's position
      if (row * 8 + col === inputOrder[knightPosition]) {
        cell.classList.add('highlighted');
        // Log the movement position
        movementLog.push(`${row + 1}.${col + 1}`);
      }
      chessboardContainer.appendChild(cell);
    }
  }

  // Update the knight's position on the chessboard
  updateKnightPosition();
}

// Function to start the knight tour animation
function startKnightTourAnimation() {
  const knight = document.getElementById('knight');
  knight.style.opacity = 0;

  // Set up an interval for the animation
  let animationCount = 0;
  const knightInterval = setInterval(() => {
    renderChessboard(); // Update the highlighted cell during the animation

    // Move the knight to the next position (jump by 1 position)
    if (knightPosition >= 63) {
      clearInterval(knightInterval);
      knight.style.opacity = 1;
      console.log('Knight Tour Animation Completed');
      console.log('Movement Log:', movementLog);
      downloadMovementLog();
      updateKnightPosition();
      return;
    } else {
      knightPosition += 1;
    }
    console.log('Knight Position:', knightPosition);
    updateKnightPosition();
  }, 500);
}

// Function to download the movement log as a text file
function downloadMovementLog() {
  const movementLogText = movementLog.join('\n');
  const blob = new Blob([movementLogText], { type: 'text/plain' });
  const link = document.createElement('a');
  link.href = URL.createObjectURL(blob);
  link.download = 'movement_log.txt';
  link.click();
}

// Function to update the knight's position on the chessboard
function updateKnightPosition() {
  const chessboardContainer = document.getElementById('chessboard');
  const knight = document.getElementById('knight');
  // Get the cell corresponding to the knight's position
  const cell = chessboardContainer.children[inputOrder[knightPosition] + 9]; // +9 to skip headers
  const cellRect = cell.getBoundingClientRect();

  // Update the knight's position on the page
  knight.style.left = `${cellRect.left}px`;
  knight.style.top = `${cellRect.top}px`;
}
