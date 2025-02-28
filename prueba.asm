;Archivo: prueba.cpp
;Fecha y hora: 28/02/2025 11:52:40 a. m.
segment .text
global main
main:
    ;Asignacion de x26
     MOV EAX,200
     PUSH EAX
     POP EAX
     MOV DWORD[x26],EAX
	; do
jmp_DO_ 1 :
    ;Asignacion de x26
     MOV EAX,x26
     PUSH EAX
     MOV EAX,1
     PUSH EAX
     POP EBX
     POP EAX
     ADD EAX, EBX
     PUSH EAX
     POP EAX
     MOV DWORD[x26],EAX
     MOV EAX,x26
     PUSH EAX
     MOV EAX,211
     PUSH EAX
     POP EBX
     POP EAX
     CMP EAX, EBX
     JB jmp_DO_ 1 
	RET
section .data
    x26 DB 0
