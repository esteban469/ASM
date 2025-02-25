;Archivo: prueba.cpp
;Fecha y hora: 25/02/2025 11:51:23 a. m.
SEGMENT .TEXT
GLOBAL MAIN
MAIN:
     MOV EAX,3
     PUSH AX
     MOV EAX,5
     PUSH AX
     POP EBX
     POP EAX
     ADD EAX, EBX
     PUSH EAX
     MOV EAX,8
     PUSH AX
     POP EBX
     POP EAX
     MUL EBX
     PUSH AX
     MOV EAX,10
     PUSH AX
     MOV EAX,4
     PUSH AX
     POP EBX
     POP EAX
     SUB EAX, EBX
     PUSH EAX
     MOV EAX,2
     PUSH AX
     POP EBX
     POP EAX
     DIV EBX
     PUSH EAX
     POP EBX
     POP EAX
     SUB EAX, EBX
     PUSH EAX
     POP
.DATA
    x26 DW 0
