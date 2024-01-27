**Aryan123's 100HealthGame**
<https://crackmes.one/crackme/65ae9d45eef082e477ff5f98>

```
Hi guys this is my first Crackme it may have some bugs ignore them.
This is a simple game, your health is 100 when you press the enter key your health decreases by 10
reverse the logic so the health increases by 10 when ever the enter key is pressed!! enjoy :)
```

# Challenge
Prompt:
```
>100HealthGame.exe
Author - aryan_not_ethical(Instagram)
Health: 100
Press Enter to decrease health >>>
Health: 90
Press Enter to decrease health >>>
Health: 80
Press Enter to decrease health >>>
Health: 70
Press Enter to decrease health >>>
Health: 60
Press Enter to decrease health >>>
Health: 50
Press Enter to decrease health >>>
Health: 40
Press Enter to decrease health >>>
Health: 30
Press Enter to decrease health >>>
Health: 20
Press Enter to decrease health >>>
Health: 10
Press Enter to decrease health >>>
Health: 0
Game Over! Health depleted
```

# Going up

This is a patching challenge.

We can open the target in IDA and see what's going on:
```
.text:00401496 loc_401496:
.text:00401496 mov     dword ptr [esp], offset aPressEnterToDe
.text:0040149D call    _printf
.text:004014A2 lea     eax, [esp+1Ah]
.text:004014A6 mov     [esp+4], eax
.text:004014AA mov     dword ptr [esp], offset aC ; "%c"
.text:004014B1 call    _scanf
.text:004014B6 lea     eax, [esp+1Ah]
.text:004014BA cmp     eax, offset asc_4050BF ; "\n"
.text:004014BF jz      short loc_401496
```

This loop prints the prompt, reads in a character, and presumably intends to compare it to a newline but it's implemented incorrectly.

No worries.  We were told there may be some bugs.

Let's look at where the health is actually updated:
```
.text:004014C1 sub     dword ptr [esp+1Ch], 0Ah
.text:004014C6 mov     eax, [esp+1Ch]
.text:004014CA mov     [esp+4], eax
.text:004014CE mov     dword ptr [esp], offset aHealthD_0 ; "Health: %d\n"
.text:004014D5 call    _printf
.text:004014DA cmp     dword ptr [esp+1Ch], 0
.text:004014DF jnz     short loc_401496
```

We can see that 10 is subtracted each round.  We need to change the **sub** instruction to and **add**.

We can plug in the current instruction somewhere like <https://shell-storm.org/online/Online-Assembler-and-Disassembler/> to make our lives a bit easier. Sometimes we may have to reword it a bit and correct opcodes ourselves as it isn't perfect, but this time it seems to work fairly well.

Compare the difference between these instructions:
```
sub dword ptr [esp+1Ch], 0Ah    ; 83 6c 24 1c 0a
add dword ptr [esp+1Ch], 0Ah    ; 83 44 24 1c 0a
```

So we just need to patch one byte in the binary.

Then we can complete the patch by using any number of PE editors (such as [PE Tools](https://petoolse.github.io/petools/)) to correct the checksum to the new value ```126F3```

Let's give it a go:
```
Author - aryan_not_ethical(Instagram)
Health: 100
Press Enter to decrease health >>>
Health: 110
Press Enter to decrease health >>>
Health: 120
```

And that works!

# Bug fixing

Let's see if we can fix the bug mentioned earlier.

The problem lies here:
```
.text:004014A6 mov     [esp+4], eax
.text:004014AA mov     dword ptr [esp], offset aC ; "%c"
.text:004014B1 call    _scanf
.text:004014B6 lea     eax, [esp+1Ah]
.text:004014BA cmp     eax, offset asc_4050BF ; "\n"
.text:004014BF jz      short loc_401496
```

Presumably this is supposed to loop until a newline is received. Instead it compares pointers and loops if they match.

This will (thankfully) always fail and pass through, but it leads to some strange effects:
```
Author - aryan_not_ethical(Instagram)
Health: 100
Press Enter to decrease health >>> 123456
Health: 110
Press Enter to decrease health >>> Health: 120
Press Enter to decrease health >>> Health: 130
Press Enter to decrease health >>> Health: 140
Press Enter to decrease health >>> Health: 150
Press Enter to decrease health >>> Health: 160
Press Enter to decrease health >>> Health: 170
Press Enter to decrease health >>>
```

We can fix this fairly easily:
```
.text:00401496 C7 04 24 98 50 40 00    mov     dword ptr [esp], offset aPressEnterToDe ; "Press Enter to decrease health >>> "
.text:0040149D E8 3E 26 00 00          call    _printf
.text:004014A2                         loc_4014A2:
.text:004014A2 8D 44 24 1A             lea     eax, [esp+1Ah]
.text:004014A6 89 44 24 04             mov     [esp+4], eax
.text:004014AA C7 04 24 BC 50 40 00    mov     dword ptr [esp], offset aC ; "%c"
.text:004014B1 E8 12 26 00 00          call    _scanf
.text:004014B6 80 7C 24 1A 0A          cmp     byte ptr [esp+1Ah], 0Ah
.text:004014BB 75 E5                   jnz     short loc_4014A2
```

Fixing up the checksum again, we get a final program that works a bit more as expected:
```
Author - aryan_not_ethical(Instagram)
Health: 100
Press Enter to decrease health >>>
Health: 110
Press Enter to decrease health >>> 1234
Health: 120
Press Enter to decrease health >>>
```

Nice!

QED