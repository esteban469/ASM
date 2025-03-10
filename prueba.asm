;Archivo: Prueba.cpp
;Fecha y hora: 10/03/2025 11:52:07 a. m.
segment .text
global main
extern printf
main:
    ;Asignacion de i
     MOV EAX, 2
     PUSH EAX
     POP EAX
     MOV DWORD[i],EAX
    ;Asignacion de a
     MOV EAX, 5
     PUSH EAX
     POP EAX
     MOV DWORD[a],EAX
     ; Console.WriteLine CADENA
     PUSH cadena_1
     PUSH format_Str
     CALL printf
     ADD ESP, 8
     ; Console.WriteLine CADENA
     PUSH cadena_2
     PUSH format_Str
     CALL printf
     ADD ESP, 8
     ; Console.WriteLine CADENA
     PUSH cadena_3
     PUSH format_Str
     CALL printf
     ADD ESP, 8
	RET
section .data
    i DD 0
    a DD 0
    format_Str db "%s" , 10, 0
    cadena_1 db "Hola Mundo", 0
    cadena_2 db "Hola Mundo: 5", 0
    cadena_3 db "Hola Mundo: 5 valor i: 2", 0
