;Archivo: Prueba.cpp
;Fecha y hora: 08/03/2025 08:15:52 p. m.
segment .text
global main
extern printf
main:
    ;Asignacion de i
     MOV EAX, 1
     PUSH EAX
     POP EAX
     MOV DWORD[i],EAX
    ;Asignacion de a
     MOV EAX, 5
     PUSH EAX
     POP EAX
     MOV DWORD[a],EAX
	; while
While_1:
     MOV EAX, DWORD[i]
     PUSH EAX
     MOV EAX, DWORD[a]
     PUSH EAX
     POP EBX
     POP EAX
     CMP EAX, EBX
     JAE jmp_While_False
     ; Incremento termino (++)
     INC DWORD[i]
     JMP While_1
jmp_While_False:
	RET
section .data
    i DD 0
    a DD 0
    format_Num db "%d" , 10, 0
    cadena db "", 0
    format_Str db "%s" , 10, 0
