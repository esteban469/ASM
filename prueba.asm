;Archivo: prueba.cpp
;Fecha y hora: 06/03/2025 03:28:02 p. m.
segment .text
global main
extern printf
main:
    ;Asignacion de x26
     MOV EAX,200
     PUSH EAX
     POP EAX
     MOV DWORD[x26],EAX
     MOV EAX,2
     PUSH EAX
     POP EAX
; (*=)
     MUL DWORD[x26]
     MOV DWORD[x26], EAX
     MOV EAX,2
     PUSH EAX
     POP EBX
; (/=)
     MOV EAX,DWORD[x26]
     DIV EBX
     MOV DWORD[x26],EAX
     ; Console.WriteLine
     PUSH DWORD 200
     PUSH format
     CALL printf
     ADD ESP, 8
    ;Asignacion de a con Console.ReadLine
     MOV DWORD[a],EAX
     ; Console.WriteLine
     PUSH DWORD 2
     PUSH format
     CALL printf
     ADD ESP, 8
	RET
section .data
    x26 DD 0
    a DD 0
    format db "%d" , 10, 0
