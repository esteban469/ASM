;Archivo: Prueba.cpp
;Fecha y hora: 19/03/2025 12:19:39 p. m.
%include "io.inc"
segment .text
global main
main:
    ;Asignacion de x
     MOV EAX, 0
     PUSH EAX
     POP EAX
     MOV DWORD[x],EAX
    ;Asignacion de y
     MOV EAX, 10
     PUSH EAX
     POP EAX
     MOV DWORD[y],EAX
    ;Asignacion de z
     MOV EAX, 2
     PUSH EAX
     POP EAX
     MOV DWORD[z],EAX
    ;Asignacion de c
     MOV EAX, 100
     PUSH EAX
     MOV EAX, 200
     PUSH EAX
     POP EBX
     POP EAX
     ADD EAX, EBX
     PUSH EAX
     POP EAX
     PUSH EAX
     POP EAX
     MOV DWORD[c],EAX
     ; Console.WriteLine CADENA
     PRINT_STRING cadena_0
     NEWLINE
     ; Console.WriteLine CADENA
     PRINT_STRING cadena_2
     NEWLINE
    ;Asignacion de altura con Console.ReadLine
    GET_DEC 4,EAX
    MOV DWORD[altura], EAX
    ;Asignacion de x
     MOV EAX, 3
     PUSH EAX
     MOV EAX, DWORD[altura]
     PUSH EAX
     POP EBX
     POP EAX
     ADD EAX, EBX
     PUSH EAX
     MOV EAX, 8
     PUSH EAX
     POP EBX
     POP EAX
     MUL EBX
     PUSH EAX
     MOV EAX, 10
     PUSH EAX
     MOV EAX, 4
     PUSH EAX
     POP EBX
     POP EAX
     SUB EAX, EBX
     PUSH EAX
     MOV EAX, 2
     PUSH EAX
     POP EBX
     POP EAX
     DIV EBX
     PUSH EAX
     POP EBX
     POP EAX
     SUB EAX, EBX
     PUSH EAX
     POP EAX
     MOV DWORD[x],EAX
     ; Incremento termino (--)
     DEC DWORD[x]
     MOV EAX, DWORD[altura]
     PUSH EAX
     MOV EAX, 8
     PUSH EAX
     POP EBX
     POP EAX
     MUL EBX
     PUSH EAX
     POP EAX
; (+=)
     ADD DWORD[x], EAX
     MOV EAX, 2
     PUSH EAX
     POP EAX
; (*=)
     MUL DWORD[x]
     MOV DWORD[x], EAX
     MOV EAX, DWORD[y]
     PUSH EAX
     MOV EAX, 6
     PUSH EAX
     POP EBX
     POP EAX
     SUB EAX, EBX
     PUSH EAX
     POP EBX
; /=
     MOV EAX,DWORD[x]
     MOV EDX, 0
     DIV EBX
     MOV DWORD[x],EAX
; Entrando al ciclo FOR
    ;Asignacion de i
     MOV EAX, 1
     PUSH EAX
     POP EAX
     MOV DWORD[i],EAX
For_1:
     MOV EAX, DWORD[i]
     PUSH EAX
     MOV EAX, DWORD[altura]
     PUSH EAX
     POP EBX
     POP EAX
     CMP EAX, EBX
     JA jmp_Continue_For_1
     ; Incremento termino (++)
     INC DWORD[i]
; Entrando al ciclo FOR
    ;Asignacion de j
     MOV EAX, 1
     PUSH EAX
     POP EAX
     MOV DWORD[j],EAX
For_2:
     MOV EAX, DWORD[j]
     PUSH EAX
     MOV EAX, DWORD[i]
     PUSH EAX
     POP EBX
     POP EAX
     CMP EAX, EBX
     JAE jmp_Continue_For_2
     ; Incremento termino (++)
     INC DWORD[j]
	; Entrando al IF
     MOV EAX, DWORD[j]
     PUSH EAX
     MOV EAX, 2
     PUSH EAX
     POP EBX
     POP EAX
     MOV EDX,0
     DIV EBX
     PUSH EDX
     MOV EAX, 0
     PUSH EAX
     POP EBX
     POP EAX
     CMP EAX, EBX
     JNE jmp_Else_1
     ; Console.WriteLine CADENA
     PRINT_STRING cadena_3
    JMP jmp_EndIf_2
jmp_Else_1:
     ; Console.WriteLine CADENA
     PRINT_STRING cadena_4
jmp_EndIf_2:
     JMP For_2
jmp_Continue_For_2:
     ; Console.WriteLine CADENA
     PRINT_STRING cadena_5
     NEWLINE
     JMP For_1
jmp_Continue_For_1:
    ;Asignacion de i
     MOV EAX, 0
     PUSH EAX
     POP EAX
     MOV DWORD[i],EAX
	; do
jmp_DO_1:
     ; Console.WriteLine CADENA
     PRINT_STRING cadena_4
     ; Incremento termino (++)
     INC DWORD[i]
     MOV EAX, DWORD[i]
     PUSH EAX
     MOV EAX, DWORD[altura]
     PUSH EAX
     MOV EAX, 2
     PUSH EAX
     POP EBX
     POP EAX
     MUL EBX
     PUSH EAX
     POP EBX
     POP EAX
     CMP EAX, EBX
     JB jmp_DO_1
     ; Console.WriteLine CADENA
     PRINT_STRING cadena_5
     NEWLINE
; Entrando al ciclo FOR
    ;Asignacion de i
     MOV EAX, 1
     PUSH EAX
     POP EAX
     MOV DWORD[i],EAX
For_3:
     MOV EAX, DWORD[i]
     PUSH EAX
     MOV EAX, DWORD[altura]
     PUSH EAX
     POP EBX
     POP EAX
     CMP EAX, EBX
     JA jmp_Continue_For_3
     ; Incremento termino (++)
     INC DWORD[i]
    ;Asignacion de j
     MOV EAX, 1
     PUSH EAX
     POP EAX
     MOV DWORD[j],EAX
	; while
While_1:
     MOV EAX, DWORD[j]
     PUSH EAX
     MOV EAX, DWORD[i]
     PUSH EAX
     POP EBX
     POP EAX
     CMP EAX, EBX
     JAE jmp_Continue_While_1
     ; Console.WriteLine VARIABLE
     PRINT_DEC 4, j
     ; Incremento termino (++)
     INC DWORD[j]
     JMP While_1
jmp_Continue_While_1:
     ; Console.WriteLine CADENA
     PRINT_STRING cadena_5
     NEWLINE
     JMP For_3
jmp_Continue_For_3:
    ;Asignacion de i
     MOV EAX, 0
     PUSH EAX
     POP EAX
     MOV DWORD[i],EAX
	; do
jmp_DO_2:
     ; Console.WriteLine CADENA
     PRINT_STRING cadena_4
     ; Incremento termino (++)
     INC DWORD[i]
     MOV EAX, DWORD[i]
     PUSH EAX
     MOV EAX, DWORD[altura]
     PUSH EAX
     MOV EAX, 2
     PUSH EAX
     POP EBX
     POP EAX
     MUL EBX
     PUSH EAX
     POP EBX
     POP EAX
     CMP EAX, EBX
     JB jmp_DO_2
     ; Console.WriteLine CADENA
     PRINT_STRING cadena_5
     NEWLINE
	RET
section .data
    altura DD 0
    i DD 0
    j DD 0
    x DD 0
    y DD 0
    z DD 0
    c DD 0
    cadena_1 db "Valor de y = ", 0
    cadena_2 db "Valor de altura = ", 0
    cadena_3 db "*", 0
    cadena_4 db "-", 0
    cadena_5 db " ", 0
