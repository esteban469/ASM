;Archivo: Prueba.cpp
;Fecha y hora: 09/03/2025 05:32:18 p. m.
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
	; Entrando al IF
     MOV EAX, DWORD[i]
     PUSH EAX
     MOV EAX, DWORD[a]
     PUSH EAX
     POP EBX
     POP EAX
     CMP EAX, EBX
     JNA jmp_Else_1
     ; Console.WriteLine CADENA
     PUSH cadena_1
     PUSH format_Str
     CALL printf
     ADD ESP, 8
    JMP jmp_Continue_If_1
jmp_Else_1:
     ; Console.WriteLine CADENA
     PUSH cadena_2
     PUSH format_Str
     CALL printf
     ADD ESP, 8
jmp_Continue_If_1:
	RET
section .data
    i DD 0
    a DD 0
    format_Str db "%s" , 10, 0
    cadena_1 db "La condicion es verdadera.", 0
    cadena_2 db "La condicion es falsa.", 0
