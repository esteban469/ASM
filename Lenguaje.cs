/*
REQUERIMIENTOS AUTOMATAS I:
    1) Indicar en el error Léxico o sintáctico, el número de línea y caracter [DONE]
    2) En el log colocar el nombre del archivo a compilar, la fecha y la hora [DONE]
    3)  Agregar el resto de asignaciones [DONE]
            Asignacion -> 
            Id = Expresion
            Id++
            Id--
            Id IncrementoTermino Expresion
            Id IncrementoFactor Expresion
            Id = Console.Read()
            Id = Console.ReadLine()
    4) Emular el Console.Write() & Console.WriteLine() [DONE] 
    5) Emular el Console.Read() & Console.ReadLine() [DONE]

NUEVOS REQUERIMIENTOS AUTOMATAS I:
    1) Concatenación [DONE]
    2) Inicializar una variable desde la declaración [DONE]
    3) Evaluar las expresiones matemáticas [DONE]
    4) Levantar una excepción si en el Console.(Read | ReadLine) no ingresan números [DONE]
    5) Modificar la variable con el resto de operadores (Incremento de factor y termino) [DONE]
    6) Implementar el else [DONE]



    ***********************REQUERIMIENTOS AUTOMATAS II:********************************
    1) Implementar set y get para la clase token (listo)
    2) Implementar parametros por default en el constructor del archivo lexico (listo)
    3) Implementar linea y columna en los errores semanticos[Listo]
    4) Implementar maxTipo en la asignacion, es decir, cuando se haga v.setValor(r)
    5) Implementar el casteo en el stack


    -----------------------------REQUERIMIENTOS Parcial 2-----------------------------
    1) Declarar las variables en ensamblador con su tipo de dato [LISTO]
    2) En asignacion generar codigo en ensamblador para ++(inc) --(dec) [LISTO]
    3) En asignacion generar codigo en ensamblador para += -= *= /= [LISTO] , !!!!!FALTA %=!!!!!!!
    4) Generar codigo en ensamblador para console.Write/WriteLine [Listo]
    5) Generar codigo para Console.Read/ReadLine [LISTO]
    6) Programar el do while [LISTO]
    7) Programar el while [LISTO]
    8) Programar el for [LISTO]
    9) Condicionar todos los setValor() en asignacion {if(ejecuta)} [LISTO]
    10) Pogramar el else [LISTO]
    11) Usar set y get en variable
    12) Ajustar todos los constructores con parametros por default [LISTO]
    ***********************************************************************************
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace ASM
{
    public class Lenguaje : Sintaxis
    {
        private int ifCont, whileCont, doWhileCont, forCont, ContCadenasConsoleASM=1, Cont2CadenasConsoleASM=1
                    , continueWhileCont=1, continueForCont=1, continueIfCont=1;
        Stack<float> s;
        List<Variable> l;
        Variable.TipoDato maxTipo;
        private string concatenaciones = "";
        public static bool printNumberAsm = false;
        public static bool printStringAsm = false;
        List<string> cadenasASM = new List<string>();
        /*public Lenguaje() : base()
        {
            s = new Stack<float>();
            l = new List<Variable>();
            maxTipo = Variable.TipoDato.Char;
            log.WriteLine("Constructor lenguaje");
            ifCont = whileCont = doWhileCont = forCont = 1;
        }*/
        public Lenguaje(string nombre = "Prueba.cpp") : base(nombre)
        {
            s = new Stack<float>();
            l = new List<Variable>();
            maxTipo = Variable.TipoDato.Char;
            log.WriteLine("Constructor lenguaje");
            ifCont = whileCont = doWhileCont = forCont = 1;
        }

        private void displayStack()
        {
            Console.WriteLine("Contenido del stack: ");
            foreach (float elemento in s)
            {
                Console.WriteLine(elemento);
            }
        }

        private void displayLista()
        {
            asm.WriteLine("section .data");
            log.WriteLine("Lista de variables: ");
            foreach (Variable elemento in l)
            {
                log.WriteLine($"{elemento.getNombre()} {elemento.getTipoDato()} {elemento.getValor()}");
                asm.WriteLine($"    {elemento.getNombre()} DD 0"); //{elemento.getValor()}");
            }
            if (printNumberAsm == true)
            {
                asm.WriteLine("    format_Num db \"%d\" , 10, 0");// Formato para imprimir el valor de la variable (numero)
            }
            if (printStringAsm == true)
            {
                asm.WriteLine("    format_Str db \"%s\" , 10, 0");// Formato para imprimir el valor de la variable (cadena)
                foreach (string cadenaConsole in cadenasASM)
                {
                    asm.WriteLine($"    cadena_{ContCadenasConsoleASM++} db \"{cadenaConsole}\", 0");// Mensaje a imprimir
                }
            }
        }

        //Programa  -> Librerias? Variables? Main
        public void Programa()
        {
            if (Contenido == "using")
            {
                Librerias();
            }
            if (Clasificacion == Tipos.TipoDato)
            {
                Variables(true);
            }
            Main();
            asm.WriteLine("\tRET");
            displayLista();
        }
        //Librerias -> using ListaLibrerias; Librerias?

        private void Librerias()
        {
            match("using");
            ListaLibrerias();
            match(";");
            if (Contenido == "using")
            {
                Librerias();
            }
        }
        //Variables -> tipo_dato Lista_identificadores; Variables?

        private void Variables(bool ejecuta)
        {
            Variable.TipoDato t = Variable.TipoDato.Char;
            switch (Contenido)
            {
                case "int": t = Variable.TipoDato.Int; break;
                case "float": t = Variable.TipoDato.Float; break;
            }
            match(Tipos.TipoDato);
            ListaIdentificadores(ejecuta, t);
            match(";");
            if (Clasificacion == Tipos.TipoDato)
            {
                Variables(ejecuta);
            }
        }
        //ListaLibrerias -> identificador (.ListaLibrerias)?
        private void ListaLibrerias()
        {
            match(Tipos.Identificador);
            if (Contenido == ".")
            {
                match(".");
                ListaLibrerias();
            }
        }
        //ListaIdentificadores -> identificador (= Expresion)? (,ListaIdentificadores)?
        private void ListaIdentificadores(bool ejecuta, Variable.TipoDato t)
        {
            if (l.Find(variable => variable.getNombre() == Contenido) != null)
            {
                throw new Error($"La variable {Contenido} ya existe", log, linea, columna);
            }
            //l.Add(new Variable(t, getContenido()));
            Variable v = new Variable(t, Contenido);
            l.Add(v);

            match(Tipos.Identificador);
            if (Contenido == "=")
            {
                match("=");
                if (Contenido == "Console")
                {
                    match("Console");
                    match(".");
                    if (Contenido == "Read")
                    {
                        match("Read");
                        int r = Console.Read();
                        v.setValor(r);
                    }
                    else
                    {
                        match("ReadLine");
                        string? r = Console.ReadLine();
                        if (float.TryParse(r, out float valor))
                        {
                            v.setValor(valor);
                        }
                        else
                        {
                            throw new Error("Sintaxis. No se ingresó un número ", log, linea, columna);
                        }
                    }
                    match("(");
                    match(")");
                    asm.WriteLine($"    ;Asignacion de {v.getNombre()} desde Console");
                    asm.WriteLine($"    MOV DWORD[{v.getNombre()}],{v.getValor()}");
                }
                else
                {
                    // Como no se ingresó un número desde el Console, entonces viene de una expresión matemática
                    asm.WriteLine($"    ;Asignacion de {v.getNombre()}");
                    Expresion();
                    float resultado = s.Pop();
                    asm.WriteLine("     POP EAX");
                    asm.WriteLine($"     MOV DWORD[{v.getNombre()}],EAX");
                    l.Last().setValor(resultado);
                }
            }
            if (Contenido == ",")
            {
                match(",");
                ListaIdentificadores(ejecuta, t);
            }
        }
        //BloqueInstrucciones -> { listaIntrucciones? }
        private void BloqueInstrucciones(bool ejecuta)
        {
            match("{");
            if (Contenido != "}")
            {
                ListaInstrucciones(ejecuta);
            }
            else
            {
                match("}");
            }
        }
        //ListaInstrucciones -> Instruccion ListaInstrucciones?
        private void ListaInstrucciones(bool ejecuta)
        {
            Instruccion(ejecuta);
            if (Contenido != "}")
            {
                ListaInstrucciones(ejecuta);
            }
            else
            {
                match("}");
            }
        }

        //Instruccion -> console | If | While | do | For | Variables | Asignación
        private void Instruccion(bool ejecuta)
        {
            if (Contenido == "Console")
            {
                console(ejecuta);
            }
            else if (Contenido == "if")
            {
                If(ejecuta);
            }
            else if (Contenido == "while")
            {
                While();
            }
            else if (Contenido == "do")
            {
                Do();
            }
            else if (Contenido == "for")
            {
                For(ejecuta);
            }
            else if (Clasificacion == Tipos.TipoDato)
            {
                Variables(ejecuta);
            }
            else
            {
                Asignacion(ejecuta);
                match(";");
            }
        }
        //Asignacion -> Identificador = Expresion; (DONE)
        /*
        Id++ (DONE)
        Id-- (DONE)
        Id IncrementoTermino Expresion (DONE)
        Id IncrementoFactor Expresion (DONE)
        Id = Console.Read() (DONE)
        Id = Console.ReadLine() (DONE)
        */

        //4) Implementar maxTipo en la asignacion, es decir, cuando se haga v.setValor(r)
        private void Asignacion(bool ejecuta, Variable? v = null)
        {
            maxTipo = Variable.TipoDato.Char;
            float r;
            v = l.Find(variable => variable.getNombre() == Contenido);
            if (v == null)
            {
                throw new Error("Sintaxis: La variable " + Contenido + " no está definida", log, linea, columna);
            }
            //Console.Write(getContenido() + " = ");
            //2) En asignacion generar codigo en ensamblador para ++(inc) --(dec)
            //inc dword [a]  ; Incrementa el valor almacenado en a en 1
            match(Tipos.Identificador);
            if (Contenido == "++")
            {
                match("++");
                r = v.getValor() + 1;
                v.setValor(r);
                asm.WriteLine("     ; Incremento termino (++)");
                asm.WriteLine($"     INC DWORD[{v.getNombre()}]");
            }
            else if (Contenido == "--")
            {
                match("--");
                r = v.getValor() - 1;
                v.setValor(r);
                asm.WriteLine("     ; Incremento termino (--)");
                asm.WriteLine($"     DEC DWORD[{v.getNombre()}]");
            }
            else if (Contenido == "=")
            {
                match("=");
                if (Contenido == "Console")
                {
                    match("Console");
                    match(".");
                    if (Contenido == "Read")
                    {
                        match("Read");
                        match("(");
                        Console.Read();
                    }
                    else
                    {
                        match("ReadLine");
                        match("(");
                        Console.Write("--> ");
                        string? lineaLeida = Console.ReadLine();
                        if (!float.TryParse(lineaLeida, out float numero))
                        {
                            throw new Error("Entrada invalida: Solo se permiten numeros enteros.");
                        }
                        s.Push(numero);
                        //asm.WriteLine("     PUSH EAX");
                        if (ejecuta)
                        {
                            v?.setValor(numero, maxTipo);
                        }
                    }
                    match(")");
                    asm.WriteLine($"    ;Asignacion de {v?.getNombre()} con Console.ReadLine");
                    asm.WriteLine($"     MOV DWORD[{v?.getNombre()}],EAX");
                }
                else
                {
                    asm.WriteLine($"    ;Asignacion de {v.getNombre()}");
                    Expresion();
                    r = s.Pop();
                    asm.WriteLine("     POP EAX");
                    asm.WriteLine($"     MOV DWORD[{v.getNombre()}],EAX");
                    if (ejecuta)
                    {
                        v?.setValor(r, maxTipo);
                    }
                }
            }
            else if (Contenido == "+=")
            {
                match("+=");
                Expresion();
                r = v.getValor() + s.Pop();
                asm.WriteLine("     POP EAX");
                if (ejecuta)
                {
                    v.setValor(r);
                }
                asm.WriteLine("; (+=)");
                asm.WriteLine($"     ADD DWORD[{v.getNombre()}], EAX");
            }
            else if (Contenido == "-=")
            {
                match("-=");
                Expresion();
                r = v.getValor() - s.Pop();
                asm.WriteLine("     POP EAX");
                if (ejecuta)
                {
                    v.setValor(r);
                }
                asm.WriteLine("; (-=)");
                asm.WriteLine($"     SUB DWORD[{v.getNombre()}], EAX");
            }
            else if (Contenido == "*=")
            {
                match("*=");
                Expresion();
                r = v.getValor() * s.Pop();
                asm.WriteLine("     POP EAX");
                if (ejecuta)
                {
                    v.setValor(r);
                }
                asm.WriteLine("; (*=)");
                asm.WriteLine($"     MUL DWORD[{v.getNombre()}]");
                asm.WriteLine($"     MOV DWORD[{v.getNombre()}], EAX");
            }
            else if (Contenido == "/=")
            {
                match("/=");
                Expresion();
                r = v.getValor() / s.Pop();
                asm.WriteLine("     POP EBX");
                if (ejecuta)
                {
                    v.setValor(r);
                }
                asm.WriteLine("; (/=)");
                asm.WriteLine($"     MOV EAX,DWORD[{v.getNombre()}]");
                asm.WriteLine($"     DIV EBX");
                asm.WriteLine($"     MOV DWORD[{v.getNombre()}],EAX");
            }
            else if (Contenido == "%=")
            {
                match("%=");
                Expresion();
                r = v.getValor() % s.Pop();
                asm.WriteLine("     POP EAX");
                if (ejecuta)
                {
                    v.setValor(r);
                }
                asm.WriteLine("; (%=)");
                asm.WriteLine("      MOV EDX,0");
                //asm.WriteLine($"     MOV EAX,DWORD[{v?.getNombre()}]");
                asm.WriteLine($"     DIV EAX");
                asm.WriteLine($"     MOV DWORD[{v?.getNombre()}],EDX");
            }
            //displayStack();
        }
        /*If -> if (Condicion) bloqueInstrucciones | instruccion
        (else bloqueInstrucciones | instruccion)?*/
        private void If(bool ejecuta2)
        {
            match("if");
            match("(");
            asm.WriteLine("\t; Entrando al IF");
            string label = $"jmp_Else_{ifCont++}";
            string labelFalse = $"jmp_Continue_If_{continueIfCont++}";
            bool ejecuta = Condicion(label,false) && ejecuta2;
            //Console.WriteLine(ejecuta);
            match(")");
            if (Contenido == "{")
            {
                BloqueInstrucciones(ejecuta);
            }
            else
            {
                Instruccion(ejecuta);
            }
            asm.WriteLine($"    JMP {labelFalse}");
            asm.WriteLine($"{label}:");
            if (Contenido == "else")
            {
                match("else");
                bool ejecutarElse = !ejecuta && ejecuta2; // Solo se ejecuta el else si el if no se ejecutó
                if (Contenido == "{")
                {
                    BloqueInstrucciones(ejecutarElse);
                }
                else
                {
                    Instruccion(ejecutarElse);
                }
                asm.WriteLine($"{labelFalse}:");
            }
        }
        //Condicion -> Expresion operadorRelacional Expresion
        private bool Condicion(string label, bool isDo = false)
        {
            maxTipo = Variable.TipoDato.Char;
            Expresion();
            float valor1 = s.Pop();
            string operador = Contenido;
            match(Tipos.OperadorRelacional);
            maxTipo = Variable.TipoDato.Char;
            Expresion();
            float valor2 = s.Pop();
            asm.WriteLine("     POP EBX");
            asm.WriteLine("     POP EAX");
            asm.WriteLine("     CMP EAX, EBX");
            if (!isDo)
            {
                switch (operador)
                {
                    case ">": asm.WriteLine($"     JNA {label}"); return valor1 > valor2;//<=
                    case ">=": asm.WriteLine($"     JB {label}"); return valor1 >= valor2;//<
                    case "<": asm.WriteLine($"     JAE {label}"); return valor1 < valor2;//>=
                    case "<=": asm.WriteLine($"     JA {label}"); return valor1 <= valor2;//>
                    case "==": asm.WriteLine($"     JNE {label}"); return valor1 == valor2;// distinto
                    default: asm.WriteLine($"     JE {label}"); return valor1 != valor2;// ==
                }
            }
            else
            {
                switch (operador)
                {
                    case ">": asm.WriteLine($"     JA {label}"); return valor1 > valor2;
                    case ">=": asm.WriteLine($"     JAE {label}"); return valor1 >= valor2;
                    case "<": asm.WriteLine($"     JB {label}"); return valor1 < valor2;
                    case "<=": asm.WriteLine($"     JBE {label}"); return valor1 <= valor2;
                    case "==": asm.WriteLine($"     JE {label}"); return valor1 == valor2;
                    default: asm.WriteLine($"     JNE {label}"); return valor1 != valor2;
                }
            }
        }
        //While -> while(Condicion) bloqueInstrucciones | instruccion
        private void While()
        {
            asm.WriteLine("\t; while");
            string label = $"While_{whileCont++}";
            string labelFalse = $"jmp_Continue_While_{continueWhileCont++}";
            asm.WriteLine($"{label}:");
            match("while");
            match("(");
            Condicion(labelFalse, false);
            match(")");
            if (Contenido == "{")
            {
                BloqueInstrucciones(true);
            }
            else
            {
                Instruccion(true);
            }
            asm.WriteLine($"     JMP {label}");
            asm.WriteLine($"{labelFalse}:");
        }
        /*Do -> do bloqueInstrucciones | intruccion 
        while(Condicion);*/
        private void Do()
        {
            match("do");
            asm.WriteLine("\t; do");
            string label = $"jmp_DO_{doWhileCont++}";
            asm.WriteLine($"{label}:");
            if (Contenido == "{")
            {
                BloqueInstrucciones(true);
            }
            else
            {
                Instruccion(true);
            }
            match("while");
            match("(");
            Condicion(label, true);
            match(")");
            match(";");
        }
        /*For -> for(Asignacion; Condicion; Asignacion) 
        BloqueInstrucciones | Intruccion*/
        private void For(bool ejecuta)
        {
            asm.WriteLine("\t; for");
            string label = $"For_{forCont++}";
            string labelFalse = $"jmp_Continue_For_{continueForCont++}";
            match("for");
            match("(");
            Asignacion(ejecuta);
            match(";");
            asm.WriteLine($"{label}:");
            Condicion(labelFalse, false);
            match(";");
            Asignacion(ejecuta);
            match(")");
            if (Contenido == "{")
            {
                BloqueInstrucciones(true);
            }
            else
            {
                Instruccion(true);
            }
            asm.WriteLine($"     JMP {label}");
            asm.WriteLine($"{labelFalse}:");
        }
        //Console -> Console.(WriteLine|Write) (cadena? concatenaciones?);
        private void console(bool ejecuta)
        {
            bool isWriteLine = false;
            match("Console");
            match(".");

            if (Contenido == "WriteLine")
            {
                match("WriteLine");
                isWriteLine = true;
            }
            else
            {
                match("Write");
            }

            match("(");

            concatenaciones = "";

            if (Clasificacion == Tipos.Cadena)
            {
                printStringAsm = true;
                concatenaciones = Contenido.Trim('"');
                match(Tipos.Cadena);
                asm.WriteLine("     ; Console.WriteLine CADENA");
                asm.WriteLine($"     PUSH cadena_{Cont2CadenasConsoleASM++}"); //Pasar la cadena a imprimir
                asm.WriteLine("     PUSH format_Str"); //Pasar el formato de impresion
                asm.WriteLine("     CALL printf");
                asm.WriteLine("     ADD ESP, 8");//  Limpiar la pila
                cadenasASM.Add(concatenaciones);
            }
            else
            {
                printNumberAsm = true;
                Variable? v = l.Find(var => var.getNombre() == Contenido);
                if (v == null)
                {
                    throw new Error("Sintaxis: La variable " + Contenido + " no está definida", log, linea, columna);
                }
                else
                {
                    concatenaciones = v.getValor().ToString();
                    match(Tipos.Identificador);
                    asm.WriteLine("     ; Console.WriteLine VARIABLE");
                    asm.WriteLine($"     PUSH DWORD {concatenaciones}");
                    asm.WriteLine("     PUSH format_Num"); //Pasar el formato de impresion
                    asm.WriteLine("     CALL printf");
                    asm.WriteLine("     ADD ESP, 8");//  Limpiar la pila
                }
            }

            if (Contenido == "+")
            {
                match("+");
                concatenaciones += Concatenaciones();  // Se acumula el resultado de las concatenaciones
                //cadenasASM.Add(concatenaciones);
            }

            match(")");
            match(";");
            if (ejecuta)
            {
                if (isWriteLine)
                {
                    Console.WriteLine(concatenaciones);
                }
                else
                {
                    Console.Write(concatenaciones);
                }
            }
        }
        // Concatenaciones -> Identificador|Cadena ( + concatenaciones )?
        private string Concatenaciones()
        {
            string resultado = "";
            if (Clasificacion == Tipos.Identificador)
            {
                Variable? v = l.Find(variable => variable.getNombre() == Contenido);
                if (v != null)
                {
                    resultado = v.getValor().ToString(); // Obtener el valor de la variable y convertirla
                }
                else
                {
                    throw new Error("La variable " + Contenido + " no está definida", log, linea, columna);
                }
                match(Tipos.Identificador);
            }
            else if (Clasificacion == Tipos.Cadena)
            {
                resultado = Contenido.Trim('"');
                match(Tipos.Cadena);
            }
            if (Contenido == "+")
            {
                match("+");
                resultado += Concatenaciones();  // Acumula el siguiente fragmento de concatenación
            }
            return resultado;
        }
        //Main -> static void Main(string[] args) BloqueInstrucciones 
        private void Main()
        {
            match("static");
            match("void");
            match("Main");
            match("(");
            match("string");
            match("[");
            match("]");
            match("args");
            match(")");
            BloqueInstrucciones(true);
        }
        // Expresion -> Termino MasTermino
        private void Expresion()
        {
            Termino();
            MasTermino();
        }
        //MasTermino -> (OperadorTermino Termino)?
        private void MasTermino()
        {
            if (Clasificacion == Tipos.OperadorTermino)
            {
                string operador = Contenido;
                match(Tipos.OperadorTermino);
                Termino();
                //Console.Write(operador + " ");
                float n1 = s.Pop();
                asm.WriteLine("     POP EBX");

                float n2 = s.Pop();
                asm.WriteLine("     POP EAX");
                switch (operador)
                {
                    case "+":
                        s.Push(n2 + n1);
                        asm.WriteLine("     ADD EAX, EBX");
                        asm.WriteLine("     PUSH EAX");
                        break;
                    case "-":
                        s.Push(n2 - n1);
                        asm.WriteLine("     SUB EAX, EBX");
                        asm.WriteLine("     PUSH EAX");
                        break;
                }
            }
        }
        //Termino -> Factor PorFactor
        private void Termino()
        {
            Factor();
            PorFactor();
        }
        //PorFactor -> (OperadorFactor Factor)?
        private void PorFactor()
        {
            if (Clasificacion == Tipos.OperadorFactor)
            {
                string operador = Contenido;
                match(Tipos.OperadorFactor);
                Factor();
                //Console.Write(operador + " ");
                float n1 = s.Pop();
                asm.WriteLine("     POP EBX");
                float n2 = s.Pop();
                asm.WriteLine("     POP EAX");
                switch (operador)
                {
                    case "*":
                        s.Push(n2 * n1);
                        asm.WriteLine("     MUL EBX");
                        asm.WriteLine("     PUSH EAX");
                        break;
                    case "/":
                        s.Push(n2 / n1);
                        asm.WriteLine("     DIV EBX");
                        asm.WriteLine("     PUSH EAX");
                        break;
                    case "%":
                        s.Push(n2 % n1);
                        asm.WriteLine("     DIV EBX");
                        asm.WriteLine("     PUSH EDX");
                        break;
                }
            }
        }
        //Factor -> numero | identificador | (Expresion)

        private void Factor()
        {
            if (Clasificacion == Tipos.Numero)
            {
                // Si el tipo de dato del número es mayor al tipo de dato actual, cambiarlo
                if (maxTipo < Variable.valorToTipoDato(float.Parse(Contenido)))
                {
                    maxTipo = Variable.valorToTipoDato(float.Parse(Contenido));
                }

                s.Push(float.Parse(Contenido));
                asm.WriteLine($"     MOV EAX, " + Contenido);
                asm.WriteLine("     PUSH EAX");
                match(Tipos.Numero);
            }
            else if (Clasificacion == Tipos.Identificador)
            {
                Variable? v = l.Find(variable => variable.getNombre() == Contenido);

                if (v == null)
                {
                    throw new Error("Sintaxis: la variable " + Contenido + " no está definida ", log, linea, columna);
                }

                if (maxTipo < v.getTipoDato())
                {
                    maxTipo = v.getTipoDato();
                }

                s.Push(v.getValor());
                asm.WriteLine($"     MOV EAX, DWORD[{Contenido}]");
                asm.WriteLine("     PUSH EAX");
                match(Tipos.Identificador);
            }
            else
            {
                match("(");

                Variable.TipoDato tipoCasteo = Variable.TipoDato.Char;
                bool huboCasteo = false;

                // Verificar si hay un tipo de dato explícito (casteo)
                if (Clasificacion == Tipos.TipoDato)
                {
                    switch (Contenido)
                    {
                        case "int": tipoCasteo = Variable.TipoDato.Int; break;
                        case "float": tipoCasteo = Variable.TipoDato.Float; break;
                    }
                    match(Tipos.TipoDato);
                    match(")");
                    match("(");
                    huboCasteo = true;
                }

                Expresion();

                if (huboCasteo)
                {
                    float valor = s.Pop(); // Obtener el valor actual de la pila
                    asm.WriteLine("     POP");
                    switch (tipoCasteo)
                    {
                        case Variable.TipoDato.Int:
                            /*float valorEntero = valor - (valor % 1); // valor total - parte decimal
                            valor = valorEntero;*/
                            valor %= 65536; //  2^16
                            break;
                        case Variable.TipoDato.Char:
                            // Convertir a char
                            valor %= 256;
                            break;
                    }
                    s.Push(valor); // Regresar el valor casteado al stack
                    asm.WriteLine("     PUSH");
                    maxTipo = tipoCasteo; // Actualizar el tipo máximo
                }
                match(")");
            }
        }
    }
}