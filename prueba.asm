;Archivo: prueba.cpp
;Fecha y hora: 04/03/2025 11:46:51 a. m.
segment .text
global main
main:
    ;Asignacion de x26
     MOV EAX,200
     PUSH EAX
     POP EAX
     MOV DWORD[x26],EAX
; Incremento termino (++)
     INC DWORD[x26]
; Incremento termino (++)
     INC DWORD[x26]
     MOV EAX,10
     PUSH EAX
     POP EAX
; Incremento Factor (+=)
     ADD DWORD[x26], EAX
     MOV EAX,10
     PUSH EAX
     POP EAX
; Incremento Factor (-=)
     SUB DWORD[x26], EAX
     MOV EAX,2
     PUSH EAX
     POP EAX
; Incremento Factor (*=)
     MUL DWORD[x26], EAX
     MOV EAX,2
     PUSH EAX
     POP EAX
; Incremento Factor (/=)
     DIV DWORD[x26], EAX
; Incremento termino (--)
     DEC DWORD[x26]
	RET
section .data
    x26 DD 0
