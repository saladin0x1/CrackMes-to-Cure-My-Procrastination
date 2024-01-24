#!/usr/bin/python3

import sys

min_lowercase = 97      # 0x61
max_lowercase = 122     # 0x7a
min_uppercase = 65      # 0x41    
max_uppercase = 90      # 0x5A

# crypt_str = "geiwevgmtliv"

# If it's the case, we should create
# a decrypt_uppercase functionality as well:

def decrypt_lowercase(crypt_str):
    for idx in range(0, len(crypt_str)):
        dec_char = ord(crypt_str[idx])      # dec = decimal
        if (dec_char - 4) < min_lowercase:
            remain = min_lowercase - (dec_char - 3)
            actual_char = max_lowercase
            actual_char -= (remain)
            print(chr(actual_char), end="")
            continue
        dec_char -= 4
        print(chr(dec_char), end="")
    print("")

def main():
    if len(sys.argv) != 2:
        print(f"Usage: {sys.argv[0]} <encrypted_string>")
        sys.exit(0)
    decrypt_lowercase(sys.argv[1])

if __name__ == "__main__":
    main()
