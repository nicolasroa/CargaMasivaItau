using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Documental.Entidades;
using Documental.Global;

namespace Documental.Data
{
    public static class AccesoDatos
    {
        /// <summary>
        /// Metodo que Ejecuta un Procedimiento almacenado de Búsqueda
        /// </summary>
        /// <typeparam name="T">Tipo de Dato de la Lista Resultante</typeparam>
        /// <typeparam name="X">Tipo de Dato del Filtro</typeparam>
        /// <param name="Filtro">Objeto Fitro</param>
        /// <param name="Procedimiento">Nombre del Procedimiento Almacenado</param>
        /// <returns></returns>
        public static Resultado<T> Buscar<T, X>(X Filtro, Documental.Data.Global.SP Procedimiento)
        {
            var NombreClase = "";
            T obj = default(T);
            obj = Activator.CreateInstance<T>();
            NombreClase = obj.GetType().Name.Replace("Info", "");
            var ObjResultado = new Resultado<T>();
            try
            {

                ObjResultado = Global.EjecutarProcedimientoBusqueda<T, X>(Filtro, Procedimiento.ToString());

                if (!ObjResultado.ResultadoGeneral)
                {
                    if (!Constantes.ModoDebug)
                    {
                        ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + NombreClase;
                    }
                }
            }
            catch (Exception Ex)
            {

                if (!Constantes.ModoDebug)
                {
                    ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + NombreClase;
                }
                else
                {
                    ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + NombreClase + " Exception: " + Ex.Message;
                }
            }
                       
            return ObjResultado;
        }
        /// <summary>
        /// Metodo que Ejecuta un Procedimiento almacenado de Búsqueda con Coenxion variable
        /// </summary>
        /// <typeparam name="T">Tipo de Dato de la Lista Resultante</typeparam>
        /// <typeparam name="X">Tipo de Dato del Filtro</typeparam>
        /// <param name="Filtro">Objeto Fitro</param>
        /// <param name="Conexion">Nombre de la Conexion</param>
        /// <param name="Procedimiento">Nombre del Procedimiento Almacenado</param>
        /// <returns></returns>
        public static Resultado<T> Buscar<T, X>(X Filtro, Documental.Data.Global.SP Procedimiento, string Conexion)
        {
            var NombreClase = "";
            T obj = default(T);
            obj = Activator.CreateInstance<T>();
            NombreClase = obj.GetType().Name.Replace("Info", "");
            var ObjResultado = new Resultado<T>();
            try
            {

                ObjResultado = Global.EjecutarProcedimientoBusqueda<T, X>(Filtro, Procedimiento.ToString(), Conexion);

                if (!ObjResultado.ResultadoGeneral)
                {
                    if (!Constantes.ModoDebug)
                    {
                        ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + NombreClase;
                    }
                }
            }
            catch (Exception Ex)
            {

                if (!Constantes.ModoDebug)
                {
                    ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + NombreClase;
                }
                else
                {
                    ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + NombreClase + " Exception: " + Ex.Message;
                }
            }
            return ObjResultado;
        }

        public static Resultado<T> Operacion<T>(T Entidad, Global.SP Procedimiento, Global.Accion Accion, string Conexion)
        {

            var NombreClase = "";
            T obj = default(T);
            obj = Activator.CreateInstance<T>();
            NombreClase = obj.GetType().Name.Replace("Info", "");
            var ObjResultado = new Resultado<T>();
            try
            {

                ObjResultado = Global.EjecutarProcedimientoOperacional<T>(Entidad, Procedimiento.ToString(), Conexion);
                if (!ObjResultado.ResultadoGeneral)
                {
                    if (!Constantes.ModoDebug)
                    {
                        switch (Accion)
                        {
                            case Global.Accion.Eliminar:
                                ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorEliminar.ToString()) + NombreClase;
                                break;
                            case Global.Accion.Guardar:
                                ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase;
                                break;
                            default:
                                ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase;
                                break;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                if (!Constantes.ModoDebug)
                {
                    switch (Accion)
                    {
                        case Global.Accion.Eliminar:
                            ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorEliminar.ToString()) + NombreClase;
                            break;
                        case Global.Accion.Guardar:
                            ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase;
                            break;
                        default:
                            ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase;
                            break;
                    }
                }
                else
                {
                    switch (Accion)
                    {
                        case Global.Accion.Eliminar:
                            ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorEliminar.ToString()) + NombreClase + " Exception: " + Ex.Message;
                            break;
                        case Global.Accion.Guardar:
                            ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase + " Exception: " + Ex.Message;
                            break;
                        default:
                            ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase + " Exception: " + Ex.Message;
                            break;
                    }
                }
            }
            return ObjResultado;
        }

        public static Resultado<T> Operacion<T>(ref T Entidad, Global.SP Procedimiento, Global.Accion Accion, string Conexion)
        {

            var NombreClase = "";
            T obj = default(T);
            obj = Activator.CreateInstance<T>();
            NombreClase = obj.GetType().Name.Replace("Info", "");
            var ObjResultado = new Resultado<T>();
            try
            {

                ObjResultado = Global.EjecutarProcedimientoOperacional<T>(ref Entidad, Procedimiento.ToString(), Conexion);
                if (!ObjResultado.ResultadoGeneral)
                {
                    if (!Constantes.ModoDebug)
                    {
                        switch (Accion)
                        {
                            case Global.Accion.Eliminar:
                                ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorEliminar.ToString()) + NombreClase;
                                break;
                            case Global.Accion.Guardar:
                                ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase;
                                break;
                            default:
                                ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase;
                                break;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                if (!Constantes.ModoDebug)
                {
                    switch (Accion)
                    {
                        case Global.Accion.Eliminar:
                            ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorEliminar.ToString()) + NombreClase;
                            break;
                        case Global.Accion.Guardar:
                            ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase;
                            break;
                        default:
                            ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase;
                            break;
                    }
                }
                else
                {
                    switch (Accion)
                    {
                        case Global.Accion.Eliminar:
                            ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorEliminar.ToString()) + NombreClase + " Exception: " + Ex.Message;
                            break;
                        case Global.Accion.Guardar:
                            ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase + " Exception: " + Ex.Message;
                            break;
                        default:
                            ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase + " Exception: " + Ex.Message;
                            break;
                    }
                }
            }
            return ObjResultado;
        }

        public static Resultado<T> Operacion<T>(T Entidad, Global.SP Procedimiento, Global.Accion Accion)
        {

            var NombreClase = "";
            T obj = default(T);
            obj = Activator.CreateInstance<T>();
            NombreClase = obj.GetType().Name.Replace("Info", "");
            var ObjResultado = new Resultado<T>();
            try
            {

                ObjResultado = Global.EjecutarProcedimientoOperacional<T>(Entidad, Procedimiento.ToString());
                if (!ObjResultado.ResultadoGeneral)
                {
                    if (!Constantes.ModoDebug)
                    {
                        switch (Accion)
                        {
                            case Global.Accion.Eliminar:
                                ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorEliminar.ToString()) + NombreClase;
                                break;
                            case Global.Accion.Guardar:
                                ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase;
                                break;
                            default:
                                ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase;
                                break;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                if (!Constantes.ModoDebug)
                {
                    switch (Accion)
                    {
                        case Global.Accion.Eliminar:
                            ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorEliminar.ToString()) + NombreClase;
                            break;
                        case Global.Accion.Guardar:
                            ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase;
                            break;
                        default:
                            ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase;
                            break;
                    }
                }
                else
                {
                    switch (Accion)
                    {
                        case Global.Accion.Eliminar:
                            ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorEliminar.ToString()) + NombreClase + " Exception: " + Ex.Message;
                            break;
                        case Global.Accion.Guardar:
                            ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase + " Exception: " + Ex.Message;
                            break;
                        default:
                            ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase + " Exception: " + Ex.Message;
                            break;
                    }
                }
            }
            return ObjResultado;
        }

        public static Resultado<T> Operacion<T>(List<T> lstEntidad, string NombreTabla, Global.SP Procedimiento, Global.Accion Accion)
        {

            var NombreClase = "";
            T obj = default(T);
            obj = Activator.CreateInstance<T>();
            NombreClase = obj.GetType().Name.Replace("Info", "");
            var ObjResultado = new Resultado<T>();
            try
            {
                ObjResultado = Global.EjecutarProcedimientoOperacional<T>(lstEntidad, NombreTabla, Procedimiento.ToString());
                if (!ObjResultado.ResultadoGeneral)
                {
                    if (!Constantes.ModoDebug)
                    {
                        switch (Accion)
                        {
                            case Global.Accion.Eliminar:
                                ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorEliminar.ToString()) + NombreClase;
                                break;
                            case Global.Accion.Guardar:
                                ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase;
                                break;
                            default:
                                ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase;
                                break;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                if (!Constantes.ModoDebug)
                {
                    switch (Accion)
                    {
                        case Global.Accion.Eliminar:
                            ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorEliminar.ToString()) + NombreClase;
                            break;
                        case Global.Accion.Guardar:
                            ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase;
                            break;
                        default:
                            ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase;
                            break;
                    }
                }
                else
                {
                    switch (Accion)
                    {
                        case Global.Accion.Eliminar:
                            ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorEliminar.ToString()) + NombreClase + " Exception: " + Ex.Message;
                            break;
                        case Global.Accion.Guardar:
                            ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase + " Exception: " + Ex.Message;
                            break;
                        default:
                            ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase + " Exception: " + Ex.Message;
                            break;
                    }
                }
            }
            return ObjResultado;
        }


        public static Resultado<T> Operacion<T>(ref T Entidad, Global.SP Procedimiento, Global.Accion Accion)
        {

            var NombreClase = "";
            T obj = default(T);
            obj = Activator.CreateInstance<T>();
            NombreClase = obj.GetType().Name.Replace("Info", "");
            var ObjResultado = new Resultado<T>();
            try
            {

                ObjResultado = Global.EjecutarProcedimientoOperacional<T>(ref Entidad, Procedimiento.ToString());
                if (!ObjResultado.ResultadoGeneral)
                {
                    if (!Constantes.ModoDebug)
                    {
                        switch (Accion)
                        {
                            case Global.Accion.Eliminar:
                                ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorEliminar.ToString()) + NombreClase;
                                break;
                            case Global.Accion.Guardar:
                                ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase;
                                break;
                            default:
                                ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase;
                                break;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                if (!Constantes.ModoDebug)
                {
                    switch (Accion)
                    {
                        case Global.Accion.Eliminar:
                            ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorEliminar.ToString()) + NombreClase;
                            break;
                        case Global.Accion.Guardar:
                            ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase;
                            break;
                        default:
                            ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase;
                            break;
                    }
                }
                else
                {
                    switch (Accion)
                    {
                        case Global.Accion.Eliminar:
                            ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorEliminar.ToString()) + NombreClase + " Exception: " + Ex.Message;
                            break;
                        case Global.Accion.Guardar:
                            ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase + " Exception: " + Ex.Message;
                            break;
                        default:
                            ObjResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + NombreClase + " Exception: " + Ex.Message;
                            break;
                    }
                }
            }
            return ObjResultado;
        }

        public static Resultado<T> OperacionLog<T>(T Entidad, Global.SP Procedimiento)
        {
            return Global.EjecutarProcedimientoLog<T>(Entidad, Procedimiento.ToString());
        }

    }
}
