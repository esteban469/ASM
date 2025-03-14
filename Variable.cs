using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASM
{
    public class Variable
    {
        public enum TipoDato
        {
            Char, Int, Float
        }

        private TipoDato tipo;
        private string nombre;
        private float valor;

        public Variable(TipoDato tipo, string nombre, float valor = 0)
        {
            this.tipo = tipo;
            this.nombre = nombre;
            this.valor = valor;
        }
        public float Valor
        {
            get => valor;
            set
            {
                if (valorToTipoDato(value) <= tipo)
                {
                    valor = value;
                }
                else
                {
                    throw new Exception($"Semántico: no se puede asignar un {valorToTipoDato(value)} a un {tipo} en: [{Lexico.linea},{Lexico.columna}]");
                }
            }
        }
        public string Nombre => nombre;
        public TipoDato Tipo => tipo;
        public static TipoDato valorToTipoDato(float valor)
        {
            if (!float.IsInteger(valor))
            {
                return TipoDato.Float;
            }
            else if (valor <= 255)
            {
                return TipoDato.Char;
            }
            else if (valor <= 65535)
            {
                return TipoDato.Int;
            }
            else
            {
                return TipoDato.Float;
            }
        }
    }
}