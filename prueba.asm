;Archivo: prueba.cpp
;Fecha y hora: 26/02/2025 11:53:39 a. m.
SEGMENT .TEXT
GLOBAL MAIN
MAIN:
     MOV EAX,3
     PUSH EAX
     MOV EAX,5
     PUSH EAX
     POP EBX
     POP EAX
     ADD EAX, EBX
     PUSH EAX
     MOV EAX,8
     PUSH EAX
     POP EBX
     POP EAX
     MUL EBX
     PUSH EAX
     MOV EAX,10
     PUSH EAX
     MOV EAX,4
     PUSH EAX
     POP EBX
     POP EAX
     SUB EAX, EBX
     PUSH EAX
     MOV EAX,2
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
     MOV DWORD[x26], EAX
SECTION .DATA
    x26 DB 0
