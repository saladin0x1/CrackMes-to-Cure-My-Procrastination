# Just Another Crackme
*Original Author (Found on crackmes.one): Masarushi*
*Enhancer (Ghidra Usage): Saladin0x1*

## Masarushi's Steps (Using readelf):
1. **Identify native functions** (utilized `readelf`).
2. **Enumerate and analyze** the functionality of identified functions, variables, and the program.
3. **Develop an attack plan** based on the insights gained.
4. **Create scripts** to effectively tackle the challenge.

### Functions Discovered:
- `<check>`:
  - Considered useless as it is not called.
- `<fn_142418>`:
  - Deemed useless as it is not called.

---

### `<fn_142437>` (Renamed to: "BP_verify")
- Verifies if a Breakpoint (BP) was set inside `<fn_142420>`.
- Arguments:
  - RDI: Address of `<fn_142420>`
  - RSI: 299 (0x12b)
- Suggests setting a BP after "BP_verify" finishes or setting it on the instruction CALL which invokes it.

---

### `<fn_142420>`
- Invokes `scanf()` to read input (password).
- Calls `<fn_142419>`.

---

### `<fn_142419>` (Renamed to 'encrypt')
- Arguments:
  - RDI: Address of our password string
  - RSI: A "magic" value
- Calls `strlen()` to determine the input size.
- Utilizes a loop to shift the input with the 'magic' value passed as an argument in RSI.

---

### `<fn_142420>` (Renamed to "cmp_encrypt")
- Post 'encrypt' function execution:
  - Transfers our encrypted input into RDI.
  - Moves some encrypted input ("geiwevgmtliv") into RSI.
  - Calls `strcmp()`:
    1. If not equal, prints "nope".
    2. If equal, prints "Correct" and exits.

---

## Saladin0x1's Steps (Using Ghidra):
1. **Identify native functions** (utilized Ghidra).
2. **Enumerate and analyze** the functionality of identified functions, variables, and the program.
3. **Develop an attack plan** based on the insights gained.
4. **Create scripts** to effectively tackle the challenge.

---

## Attack Plan
- Inject a string that, when encrypted, matches the value of "geiwevgmtliv" (the encrypted password).
- Once the logic is understood, retrieve the encrypted password and apply the reverse logic.

    Example:
    ```
    "Z" - 4 = "D"
    ```

## Decryption Script@
```python

#!/usr/bin/python3

import sys

min_lowercase = 97      # ASCII code for 'a'
max_lowercase = 122     # ASCII code for 'z'
min_uppercase = 65      # ASCII code for 'A'
max_uppercase = 90      # ASCII code for 'Z'
# crypt_str = "geiwevgmtliv"

# If needed, create a decrypt_uppercase functionality:
def decrypt_lowercase(crypt_str):
    for idx in range(0, len(crypt_str)):
        dec_char = ord(crypt_str[idx])      # Get the ASCII code of the encrypted character
        if (dec_char - 4) < min_lowercase:
            # Handle wrapping around the alphabet for lowercase characters
            remain = min_lowercase - (dec_char - 3)
            actual_char = max_lowercase
            actual_char -= (remain)
            print(chr(actual_char), end="")
            continue
        dec_char -= 4
        print(chr(dec_char), end="")
    print("")

def main():
    # Check if the correct number of command-line arguments is provided
    if len(sys.argv) != 2:
        print(f"Usage: {sys.argv[0]} <encrypted_string>")
        #./a.out Enter the password: caesarcipher Correct!

        sys.exit(0)
    
    # Decrypt the provided encrypted string and print the result
    decrypt_lowercase(sys.argv[1])

if __name__ == "__main__":
    main()

