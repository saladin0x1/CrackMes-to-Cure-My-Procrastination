**Nox's Crackme0x03**  
https://crackmes.one/crackme/653d88460f4238b24302b0e4

```
This compilation is a creation of the user Nox and consists of a series of five challenging crackme exercises that offer valuable learning opportunities. The list spans a wide range of difficulty levels, ensuring that there is a challenge suitable for every skill level. (4/5)
```

# Challenge

Here we have challenge 0x03 from this MacOS challenge series.

If you haven't read the others, we need a modified version of **Maloader** <https://github.com/shinh/maloader> to run this on Linux:
```
$ sudo apt install git uuid uuid-dev
$ git clone https://github.com/charlesnathansmith/maloader-no-sysctl
$ cd maloader-no-sysctl
$ make release
```

Our prompt has changed marginally, but it seems to follow the same idea:
```
$ ./ld-mac crackme0x03
Crackme Level 0x03 (created by Nox)

Enter the passcode: 1234567
Invalid passcode.
```

# Diving in

Let's use Ghidra this time just to make the verification loop a bit more readable.

Here is where our passphrase is read in **main**:
```
  _printf("\nEnter the passcode: ");
  _scanf("%99s",password);
  check_pass(password)
```

After giving the variables some sensible names, here is what check_pass looks like:
```
void check_pass(char *password_arg)
...
  counter = 0;
  sum = 0;
  password = password_arg;
  passlen = _strlen(password_arg);
  for (; counter < (int)passlen; counter = counter + 1) {
    cur_char = password[counter];
    _sscanf(&cur_char,"%d",&cur_num);
    sum = cur_num + sum;
  }
  if (sum == 0xf) {
    print_valid();
    _exit(0);
  }
  _puts("Invalid passcode.\n");
```

So each character of the passcode is treated as an individual decimal digit, and these must sum together to equal 15 (0xf).

This means there are several valid passcodes, which we can calculate fairly easily.

# Keygen

We are going to ignore '0' digits.  These can clearly be inserted anywhere in a valid passcode to generate another valid passcode since they don't affect the sum.

Digits can also be rearranged in any order without affecting the sum, so we only need to find one version of each.  (Ie. if '96' is valid, then so is '69')

This is a combination sum problem <https://www.geeksforgeeks.org/combinational-sum/>

We can just borrow their example and plug in our values to create **keygen0x03.py**:
```
$ ./keygen0x03.py
111111111111111
11111111111112
1111111111113
1111111111122
111111111114
111111111123
...
```

Trying these back in the target:
```
$ ./ld-mac crackme0x03
Crackme Level 0x03 (created by Nox)

Enter the passcode: 111111111123
111111111123 is a valid passcode.
```

And we can of course rearrange the digits and add any number of '0's anywhere we want:
```
$ ./ld-mac crackme0x03
Crackme Level 0x03 (created by Nox)

Enter the passcode: 10203111110011110000
10203111110011110000 is a valid passcode.
```

Looks good!

QED
