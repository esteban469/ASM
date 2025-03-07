;Archivo: Prueba.cpp
;Fecha y hora: 07/03/2025 11:20:17 a. m.
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
     ; Console.WriteLine VARIABLE
     PUSH DWORD 200
     PUSH format_Num
     CALL printf
     ADD ESP, 8
    ;Asignacion de a con Console.ReadLine
     MOV DWORD[a],EAX
     ; Console.WriteLine VARIABLE
     PUSH DWORD 22
     PUSH format_Num
     CALL printf
     ADD ESP, 8
     ; Console.WriteLine CADENA
     PUSH cadena
     PUSH format_Str
     CALL printf
     ADD ESP, 8
	RET
section .data
    x26 DD 0
    a DD 0
    format_Num db "%d" , 10, 0
    cadena db"Hello World!", 0
    format_Str db "%s" , 10, 0
