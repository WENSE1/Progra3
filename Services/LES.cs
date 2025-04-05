using System;
using System.Collections.Generic;
using LESApplication.Models;

namespace LESApplication.Services
{
    public class LDES
    {
        public Nodo PrimerNodo { get; private set; }
        public Nodo UltimoNodo { get; private set; }
        public int Cantidad { get; private set; }

        public LDES()
        {
            PrimerNodo = null;
            UltimoNodo = null;
            Cantidad = 0;
        }

        #region Métodos de Agregar

        public string AgregarNodoInicio(Nodo nuevoNodo)
        {
            if (nuevoNodo == null) return "Nodo no puede ser nulo";

            if (PrimerNodo == null)
            {
                PrimerNodo = nuevoNodo;
                UltimoNodo = nuevoNodo;
            }
            else
            {
                nuevoNodo.Referencia = PrimerNodo;
                PrimerNodo.Anterior = nuevoNodo;
                PrimerNodo = nuevoNodo;
            }
            Cantidad++;
            return "Nodo agregado al inicio";
        }

        public string AgregarNodoFinal(Nodo nuevoNodo)
        {
            if (nuevoNodo == null) return "Nodo no puede ser nulo";

            if (UltimoNodo == null)
            {
                PrimerNodo = nuevoNodo;
                UltimoNodo = nuevoNodo;
            }
            else
            {
                nuevoNodo.Anterior = UltimoNodo;
                UltimoNodo.Referencia = nuevoNodo;
                UltimoNodo = nuevoNodo;
            }
            Cantidad++;
            return "Nodo agregado al final";
        }

        public string AgregarNodoAntesDe(Nodo nuevoNodo, int datoX)
        {
            if (nuevoNodo == null) return "Nodo no puede ser nulo";
            if (PrimerNodo == null) return "Lista vacía";

            if (PrimerNodo.Informacion == datoX.ToString())
                return AgregarNodoInicio(nuevoNodo);

            Nodo actual = BuscarNodoPorDato(datoX);
            if (actual == null) return "Dato no encontrado";

            nuevoNodo.Referencia = actual;
            nuevoNodo.Anterior = actual.Anterior;
            actual.Anterior.Referencia = nuevoNodo;
            actual.Anterior = nuevoNodo;
            Cantidad++;
            return "Nodo agregado antes del dato";
        }

        public string AgregarNodoDespuesDe(Nodo nuevoNodo, int datoX)
        {
            if (nuevoNodo == null) return "Nodo no puede ser nulo";
            if (PrimerNodo == null) return "Lista vacía";

            Nodo actual = BuscarNodoPorDato(datoX);
            if (actual == null) return "Dato no encontrado";

            if (actual == UltimoNodo)
                return AgregarNodoFinal(nuevoNodo);

            nuevoNodo.Referencia = actual.Referencia;
            nuevoNodo.Anterior = actual;
            actual.Referencia.Anterior = nuevoNodo;
            actual.Referencia = nuevoNodo;
            Cantidad++;
            return "Nodo agregado después del dato";
        }

        public string AgregarNodoEnPosicion(Nodo nuevoNodo, int posicion)
        {
            if (nuevoNodo == null) return "Nodo no puede ser nulo";
            if (posicion < 0 || posicion > Cantidad) return "Posición inválida";

            if (posicion == 0) return AgregarNodoInicio(nuevoNodo);
            if (posicion == Cantidad) return AgregarNodoFinal(nuevoNodo);

            Nodo actual = ObtenerNodoEnPosicion(posicion);

            nuevoNodo.Referencia = actual;
            nuevoNodo.Anterior = actual.Anterior;
            actual.Anterior.Referencia = nuevoNodo;
            actual.Anterior = nuevoNodo;
            Cantidad++;
            return $"Nodo agregado en posición {posicion}";
        }

        public string AgregarNodoAntesDePosicion(Nodo nuevoNodo, int posicion)
        {
            if (posicion <= 0 || posicion > Cantidad) return "Posición inválida";
            return AgregarNodoEnPosicion(nuevoNodo, posicion - 1);
        }

        public string AgregarNodoDespuesDePosicion(Nodo nuevoNodo, int posicion)
        {
            if (posicion < 0 || posicion >= Cantidad) return "Posición inválida";
            return AgregarNodoEnPosicion(nuevoNodo, posicion + 1);
        }

        #endregion

        #region Métodos de Eliminar

        public string EliminarNodoInicio()
        {
            if (PrimerNodo == null) return "Lista vacía";

            if (PrimerNodo == UltimoNodo)
            {
                PrimerNodo = null;
                UltimoNodo = null;
            }
            else
            {
                PrimerNodo = PrimerNodo.Referencia;
                PrimerNodo.Anterior = null;
            }
            Cantidad--;
            return "Nodo eliminado al inicio";
        }

        public string EliminarNodoFinal()
        {
            if (UltimoNodo == null) return "Lista vacía";

            if (PrimerNodo == UltimoNodo)
            {
                PrimerNodo = null;
                UltimoNodo = null;
            }
            else
            {
                UltimoNodo = UltimoNodo.Anterior;
                UltimoNodo.Referencia = null;
            }
            Cantidad--;
            return "Nodo eliminado al final";
        }

        public string EliminarNodoAntesDe(int datoX)
        {
            if (PrimerNodo == null) return "Lista vacía";

            Nodo actual = BuscarNodoPorDato(datoX);
            if (actual == null) return "Dato no encontrado";
            if (actual.Anterior == null) return "No hay nodo antes del dato";

            if (actual.Anterior == PrimerNodo)
                return EliminarNodoInicio();

            actual.Anterior.Anterior.Referencia = actual;
            actual.Anterior = actual.Anterior.Anterior;
            Cantidad--;
            return "Nodo eliminado antes del dato";
        }

        public string EliminarNodoDespuesDe(int datoX)
        {
            if (PrimerNodo == null) return "Lista vacía";

            Nodo actual = BuscarNodoPorDato(datoX);
            if (actual == null) return "Dato no encontrado";
            if (actual.Referencia == null) return "No hay nodo después del dato";

            if (actual.Referencia == UltimoNodo)
                return EliminarNodoFinal();

            actual.Referencia = actual.Referencia.Referencia;
            actual.Referencia.Anterior = actual;
            Cantidad--;
            return "Nodo eliminado después del dato";
        }

        public string EliminarNodoEnPosicion(int posicion)
        {
            if (posicion < 0 || posicion >= Cantidad) return "Posición inválida";
            if (posicion == 0) return EliminarNodoInicio();
            if (posicion == Cantidad - 1) return EliminarNodoFinal();

            Nodo actual = ObtenerNodoEnPosicion(posicion);
            actual.Anterior.Referencia = actual.Referencia;
            actual.Referencia.Anterior = actual.Anterior;
            Cantidad--;
            return $"Nodo eliminado en posición {posicion}";
        }

        #endregion

        #region Métodos de Búsqueda y Recorrido

        public Nodo BuscarElementoPorPosicion(int posicion)
        {
            if (posicion < 0 || posicion >= Cantidad) return null;
            return ObtenerNodoEnPosicion(posicion);
        }

        public List<string> RecorridoRecursivo(Nodo nodoActual, List<string> resultado)
        {
            if (nodoActual == null) return resultado;
            resultado.Add(nodoActual.Informacion);
            return RecorridoRecursivo(nodoActual.Referencia, resultado);
        }

        #endregion

        #region Métodos de Ordenación

        public string OrdenarLista()
        {
            if (Cantidad <= 1) return "Lista vacía o ya ordenada";

            bool ordenado;
            do
            {
                ordenado = true;
                Nodo actual = PrimerNodo;
                Nodo anterior = null;

                while (actual?.Referencia != null)
                {
                    if (int.Parse(actual.Informacion) > int.Parse(actual.Referencia.Informacion))
                    {
                        IntercambiarNodos(actual, actual.Referencia);
                        ordenado = false;
                    }
                    anterior = actual;
                    actual = actual.Referencia;
                }
            } while (!ordenado);

            return "Lista ordenada correctamente";
        }

        #endregion

        #region Métodos Auxiliares

        private Nodo BuscarNodoPorDato(int dato)
        {
            Nodo actual = PrimerNodo;
            while (actual != null && actual.Informacion != dato.ToString())
                actual = actual.Referencia;
            return actual;
        }

        private Nodo ObtenerNodoEnPosicion(int posicion)
        {
            Nodo actual = PrimerNodo;
            for (int i = 0; i < posicion; i++)
                actual = actual.Referencia;
            return actual;
        }

        private void IntercambiarNodos(Nodo a, Nodo b)
        {
            // Caso especial: nodos adyacentes con a antes que b
            if (a.Referencia == b)
            {
                a.Referencia = b.Referencia;
                if (b.Referencia != null)
                    b.Referencia.Anterior = a;
                b.Referencia = a;
                b.Anterior = a.Anterior;
                if (a.Anterior != null)
                    a.Anterior.Referencia = b;
                a.Anterior = b;
            }
            else
            {
                // Para nodos no adyacentes
                Nodo tempRef = a.Referencia;
                Nodo tempAnt = a.Anterior;

                a.Referencia = b.Referencia;
                a.Anterior = b.Anterior;
                b.Referencia = tempRef;
                b.Anterior = tempAnt;

                if (a.Referencia != null) a.Referencia.Anterior = a;
                if (a.Anterior != null) a.Anterior.Referencia = a;
                if (b.Referencia != null) b.Referencia.Anterior = b;
                if (b.Anterior != null) b.Anterior.Referencia = b;
            }

            // Actualizar primer y último nodo si es necesario
            if (PrimerNodo == a) PrimerNodo = b;
            else if (PrimerNodo == b) PrimerNodo = a;

            if (UltimoNodo == a) UltimoNodo = b;
            else if (UltimoNodo == b) UltimoNodo = a;
        }

        #endregion
    }
}