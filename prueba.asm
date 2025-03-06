;Archivo: prueba.cpp
;Fecha y hora: 06/03/2025 01:34:46 p. m.
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
	RET
section .data
    x26 DD 0
    format db "x26 = %d" , 10, 0
