**Nox's Crackme0x02**  
https://crackmes.one/crackme/653d880f0f4238b24302b0dc

```
This compilation is a creation of the user Nox and consists of a series of five challenging crackme exercises that offer valuable learning opportunities. The list spans a wide range of difficulty levels, ensuring that there is a challenge suitable for every skill level. (3/5)
```

# Challenge

This is the second of Nox's MacOS x64 challenges.

As before, we need a modified version of **Maloader** <https://github.com/shinh/maloader> to run this on Linux:
```
$ sudo apt install git uuid uuid-dev
$ git clone https://github.com/charlesnathansmith/maloader-no-sysctl
$ cd maloader-no-sysctl
$ make release
```

And we have a familiar style of prompt:
```
$ ./ld-mac crackme0x02
Crackme Level 0x02 (created by Nox)

Enter the passphrase: passphrase

Invalid password.
```

# Diving in

We can take a look in IDA to find our passphrase prompt:
```
100003DBD lea     rdi, aEnterThePassph ; "\nEnter the passphrase: "
100003DC4 mov     al, 0
100003DC6 call    _printf
100003DCB lea     rdi, a99d       ; "%99d"
100003DD2 lea     rsi, [rbp+password]
100003DD6 mov     al, 0
100003DD8 call    _scanf
100003DDD mov     edi, [rbp+password]
100003DE0 mov     esi, 388Eh
100003DE5 call    _check
```

Where check just appears to compare its numerical arguments and select a response to decode and print:
```
100003E00 public _check
...
100003E08 mov     [rbp+var_4], edi
100003E0B mov     [rbp+var_8], esi
100003E0E mov     eax, [rbp+var_4]
100003E11 cmp     eax, [rbp+var_8]
100003E14 jnz     loc_100003E2B     ; failure branch, presumably
```

If our assumptions are correct, then the passphrase needs to be read in as a number that matches ```0x388e```, which is ```14478``` in decimal.

Trying this out:
```
$ ./ld-mac crackme0x02
Crackme Level 0x02 (created by Nox)

Enter the passphrase: 14478

The password is correct!!
```

And that looks good! On to the next one in the series.

QED
