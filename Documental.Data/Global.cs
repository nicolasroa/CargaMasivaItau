using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Documental.Entidades;
using System.ComponentModel;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.IO;
using System.Web.UI;

namespace Documental.Data
{
    public static class Global
    {


        /// <summary>
        /// Lista de Procedimientos Almacenados
        /// </summary>
        public enum SP
        {

            #region Procedimientos de Usuarios

            Usuario_INS,
            Usuario_QRY,
            UsuarioRol_QRY,
            UsuarioRol_INS,
            UsuarioRol_DEL,

            #endregion

            #region Procedimientos de Menu

            Menu_INS,
            Menu_QRY,
            RolMenu_INS,
            RolMenu_DEL,
            RolMenu_QRY,
            MenuUsuario_QRY,
            ValidarAccesoMenu_QRY,



            #endregion

            #region Procedimientos de Controles

            Controles_INS,
            Controles_QRY,
            RolMenuControles_INS,
            RolMenuControles_QRY,
            MenuControlesUsuario_QRY,

            #endregion

            #region Procedimientos de Tablas

            Tablas_INS,
            Tablas_QRY,
            IdentificadorMaestro_QRY,
            #endregion

            #region Procedimientos de ConfiguracionGeneral

            ConfiguracionGeneral_INS,
            ConfiguracionGeneral_QRY,

            #endregion

            #region Procedimientos de Log

            Log_INS,
            Auditoria_QRY,
            ResponsablesAuditoria_QRY,

            #endregion

            //PROCEDIMIENTOS DE NEGOCIO
            #region Procedimientos Clientes
            Clientes_INS,
            Clientes_QRY,

            #endregion

            #region Procedimientos Localizacion
            Comuna_QRY,
            Ciudad_QRY,
            Region_QRY,

            #endregion

            #region Procedimientos Proyectos
            Inmobiliaria_QRY,
            Inmobiliaria_INS,
            Proyecto_QRY,
            Proyecto_INS,
            #endregion

            #region Procedimientos GruposDocumentos

            GruposDocumentos_QRY,
            GruposDocumentos_INS,
            GruposDocumentosPortal_QRY,

            #endregion

            #region Procedimientos TiposDocumentos

            TiposDocumentos_QRY,
            TiposDocumentos_INS,
            TiposDocumentosPortal_QRY,

            #endregion

            #region Procedimientos RolesTiposDocumentos

            RolesTiposDocumentos_QRY,
            RolesTiposDocumentos_INS,
            V_Roles_QRY,

            #endregion

            #region Procedimientos Archivos Repositorio

            BandejaGrupoDocumentoSolicitud_QRY,
            BandejaGrupoDocumentoRepositorio_QRY,
            ArchivosRepositorios_QRY,
            ArchivosRepositorios_INS,
            ArchivosRepositorios_DEL,
            AsignarDocumentoSolicitud,

            #endregion

            #region Carga Masiva
            prc_ProcesarCargaMasiva,
            LogCargaMasiva_QRY,
            #endregion
            #region Procedimientos Archivos Repositorio
            MailWf_INS,
            MailWf_QRY,
            VariablesWf_QRY,
            #endregion

            #region Portal

            DatosWorkflow_QRY,
            DOC_DatosWorkflow_QRY,

            #endregion

            #region Bitacora

            Bitacora_QRY,

            #endregion

        }

        /// <summary>
        /// Lista de Esquemas de la Base de Datos
        /// </summary>
        public enum Esquemas
        {
            ADMIN,
            NEG
        }

        /// <summary>
        /// Lista de Acciones de la capa de Acceso a Datos
        /// </summary>
        public enum Accion
        {
            Buscar,
            Guardar,
            Eliminar,
            Validar
        }
        /// <summary>
        /// Método que Ejecuta un Procedimiento Almacenado de Inserción, Eliminación o Modificación
        /// </summary>
        /// <param name="Entidad">Entidad que se interpretará como los parámetros de entrada del Procedimiento</param>
        /// <param name="Procedimiento">Nombre del Procedimiento Almacenado</param>
        /// <returns>Retorna un Bool que indica si el procedimieno se ejecutó correctamente</returns>
        public static Resultado<T> EjecutarProcedimientoOperacional<T>(T Entidad, string Procedimiento)
        {
            var ObjResultado = new Resultado<T>();

            try
            {
                Database db = DatabaseFactory.CreateDatabase(ConfigBase.ConexionSQL);
                DbCommand dbCommand = db.GetStoredProcCommand(Procedimiento);
                db.SetParametros(dbCommand, Entidad);
                db.ExecuteNonQuery(dbCommand);

                return ObjResultado;
            }
            catch (Exception Ex)
            {

                DacLog.Registrar(Ex, Procedimiento);
                ObjResultado.ResultadoGeneral = false;
                ObjResultado.Mensaje = Ex.Message;
                return ObjResultado;
            }
        }



        /// <summary>
        /// Método que Ejecuta un Procedimiento Almacenado de Inserción, Eliminación o Modificación
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="LstEntidad">Entidad de Tipo Lista que se interpretará como parametro de Entrada del SP</param>
        /// <param name="NombreTabla">Nombre de la Tabla a Insertar</param>
        /// <param name="Procedimiento">Nombre del Procedimiento</param>
        /// <returns></returns>
        public static Resultado<T> EjecutarProcedimientoOperacional<T>(List<T> LstEntidad, string NombreTabla, string Procedimiento)
        {
            var ObjResultado = new Resultado<T>();

            try
            {
                SqlDatabase db = new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConfigBase.ConexionSQL);
                DbCommand dbCommand = db.GetStoredProcCommand(Procedimiento);
                db.AddInParameter(dbCommand, "@" + NombreTabla, SqlDbType.Structured, ListToDataTable<T>(LstEntidad));
                //db.SetParametros(dbCommand, Entidad);
                db.ExecuteNonQuery(dbCommand);

                return ObjResultado;
            }
            catch (Exception Ex)
            {
                DacLog.Registrar(Ex, Procedimiento);
                ObjResultado.ResultadoGeneral = false;
                ObjResultado.Mensaje = Ex.Message;
                return ObjResultado;
            }
        }








        /// <summary>
        /// Método que Ejecuta un Procedimiento Almacenado de Inserción, Eliminación o Modificación
        /// </summary>
        /// <param name="Entidad">Entidad que se interpretará como los parámetros de entrada del Procedimiento</param>
        /// <param name="Procedimiento">Nombre del Procedimiento Almacenado</param>
        /// <returns>Retorna un Bool que indica si el procedimieno se ejecutó correctamente</returns>
        public static Resultado<T> EjecutarProcedimientoOperacional<T>(ref T Entidad, string Procedimiento)
        {
            var ObjResultado = new Resultado<T>();
            try
            {
                List<T> Lista = new List<T>();
                IDataReader dr;
                Database db = DatabaseFactory.CreateDatabase(ConfigBase.ConexionSQL);
                DbCommand dbCommand = db.GetStoredProcCommand(Procedimiento);
                db.SetParametros(dbCommand, Entidad);
                dr = db.ExecuteReader(dbCommand);
                Entidad = GetResultado<T>(dr).FirstOrDefault();
                return ObjResultado;
            }
            catch (Exception Ex)
            {

                DacLog.Registrar(Ex, Procedimiento);

                ObjResultado.ResultadoGeneral = false;
                ObjResultado.Mensaje = Ex.Message;
                return ObjResultado;
            }
        }
        /// <summary>
        /// Método que Ejecuta un Procedimiento Almacenado de Inserción, Eliminación o Modificación
        /// </summary>
        /// <param name="Entidad">Entidad que se interpretará como los parámetros de entrada del Procedimiento</param>
        /// <param name="Procedimiento">Nombre del Procedimiento Almacenado</param>
        /// <returns>Retorna un Bool que indica si el procedimieno se ejecutó correctamente</returns>
        public static Resultado<T> EjecutarProcedimientoLog<T>(T Entidad, string Procedimiento)
        {
            var ObjResultado = new Resultado<T>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase(ConfigBase.ConexionSQL);
                DbCommand dbCommand = db.GetStoredProcCommand(Procedimiento);
                db.SetParametros(dbCommand, Entidad);
                db.ExecuteNonQuery(dbCommand);

                return ObjResultado;
            }
            catch (Exception Ex)
            {
                ObjResultado.ResultadoGeneral = false;
                ObjResultado.Mensaje = Ex.Message;
                return ObjResultado;
            }
        }


        public static Resultado<T> EjecutarProcedimientoBusqueda<T, X>(X Entidad, string Procedimiento)
        {
            var ObjResultado = new Resultado<T>();
            try
            {
                List<T> Lista = new List<T>();
                IDataReader dr;
                Database db = DatabaseFactory.CreateDatabase(ConfigBase.ConexionSQL);

                using (var store = db.GetStoredProcCommand(Procedimiento))
                using (store.Connection = db.CreateConnection())
                {
                    store.Connection.Open();
                    store.CommandTimeout = 10;
                    db.SetParametros(store, Entidad);
                    dr = db.ExecuteReader(store);
                    ObjResultado.Lista = GetResultado<T>(dr);
                    store.Connection.Close();
                    store.Connection.Dispose();
                                      
                }
                return ObjResultado;
            }
            catch (Exception Ex)
            {
                DacLog.Registrar(Ex, Procedimiento);
                ObjResultado.ResultadoGeneral = false;
                ObjResultado.Mensaje = Ex.Message;
                ObjResultado.Lista = new List<T>();
                return ObjResultado;
            }
        }
        public static Resultado<T> EjecutarProcedimientoBusqueda<T, X>(X Entidad, string Procedimiento, string Conexion)
        {
            var ObjResultado = new Resultado<T>();
            try
            {
                List<T> Lista = new List<T>();
                IDataReader dr;
                Database db = DatabaseFactory.CreateDatabase(Conexion);

                using (var store = db.GetStoredProcCommand(Procedimiento))
                using (store.Connection = db.CreateConnection())
                {
                    store.Connection.Open();
                    store.CommandTimeout = 10;
                    db.SetParametros(store, Entidad);
                    dr = db.ExecuteReader(store);
                    ObjResultado.Lista = GetResultado<T>(dr);
                    store.Connection.Close();
                    store.Connection.Dispose();

                }
                return ObjResultado;
            }
            catch (Exception Ex)
            {
                DacLog.Registrar(Ex, Procedimiento);
                ObjResultado.ResultadoGeneral = false;
                ObjResultado.Mensaje = Ex.Message;
                ObjResultado.Lista = new List<T>();
                return ObjResultado;
            }
        }
        public static Resultado<T> EjecutarProcedimientoOperacional<T>(T Entidad, string Procedimiento, string Conexion)
        {
            var ObjResultado = new Resultado<T>();
            try
            {
                List<T> Lista = new List<T>();
                IDataReader dr;
                Database db = DatabaseFactory.CreateDatabase(Conexion);
                DbCommand dbCommand = db.GetStoredProcCommand(Procedimiento);
                db.SetParametros(dbCommand, Entidad);
                dr = db.ExecuteReader(dbCommand);
                Entidad = GetResultado<T>(dr).FirstOrDefault();
                return ObjResultado;
            }
            catch (Exception Ex)
            {

                DacLog.Registrar(Ex, Procedimiento);

                ObjResultado.ResultadoGeneral = false;
                ObjResultado.Mensaje = Ex.Message;
                return ObjResultado;
            }
        }

        public static Resultado<T> EjecutarProcedimientoOperacional<T>(ref T Entidad, string Procedimiento, string Conexion)
        {
            var ObjResultado = new Resultado<T>();
            try
            {
                List<T> Lista = new List<T>();
                IDataReader dr;
                DataSet ds = new DataSet();

                Database db = DatabaseFactory.CreateDatabase(Conexion);
                DbCommand dbCommand = db.GetStoredProcCommand(Procedimiento);
                db.SetParametros(dbCommand, Entidad);
                ds = db.ExecuteDataSet(dbCommand);
                
                Entidad = GetResultado<T>(ds.Tables[ds.Tables.Count -1]).FirstOrDefault();
                return ObjResultado;
            }
            catch (Exception Ex)
            {

                DacLog.Registrar(Ex, Procedimiento);

                ObjResultado.ResultadoGeneral = false;
                ObjResultado.Mensaje = Ex.Message;
                return ObjResultado;
            }
        }


        #region Métodos Generales
        /// <summary>
        /// Asocia los Atributos de una Entidad con los parametros de entrada de un Procedimiento Almacenado
        /// </summary>
        /// <param name="db">Base de Datos</param>
        /// <param name="dbCommand">Comando con el Procedimiento Almacenado a Asociar</param>
        /// <param name="input">Entidad a Asociar</param>
        public static void SetParametros(this Database db, DbCommand dbCommand, object input)
        {
            try
            {
                db.DiscoverParameters(dbCommand);

                foreach (DbParameter param in dbCommand.Parameters)
                {
                    if (param.Direction == ParameterDirection.Input || param.Direction == ParameterDirection.InputOutput)
                    {
                        string parameterName = GetName(param.ParameterName);

                        if (!parameterName.Equals("RETURN_VALUE") && !parameterName.Equals("PromotePrivate"))
                        {
                            object data = GetPropertyValue(input, parameterName);
                            param.Value = ValidateData(data);
                        }
                    }
                }
            }
            catch
            {


            }
        }



        public static void GetResultado(this Database db, DbCommand dbCommand, object input)
        {
            db.DiscoverParameters(dbCommand);

            foreach (DbParameter param in dbCommand.Parameters)
            {
                if (param.Direction == ParameterDirection.InputOutput || param.Direction == ParameterDirection.Output)
                {
                    string parameterName = GetName(param.ParameterName);

                    if (!parameterName.Equals("RETURN_VALUE"))
                    {
                        try
                        {
                            object value = db.GetParameterValue(dbCommand, parameterName);
                            SetPropertyValue(input, parameterName, value);
                        }
                        catch (Exception)
                        {
                            object value = db.GetParameterValue(dbCommand, parameterName);
                        }
                    }
                }
            }
        }



        public static List<T> GetResultado<T>(IDataReader table)
        {
            if (table == null)
            {
                return null;
            }
            
            List<T> Lista = new List<T>();

            while (table.Read())
            {
                T obj = default(T);
                obj = Activator.CreateInstance<T>();
                for (int x = 0; x <= table.FieldCount - 1; x++)
                {
                    PropertyInfo prop = obj.GetType().GetProperty(table.GetName(x));
                    try
                    {
                        object value = table[table.GetName(x)];
                        if (value == System.DBNull.Value)
                        {
                            value = null;
                        }
                        prop.SetValue(obj, value, null);

                    }
                    catch
                    {
                        //You can log something here throw;
                    }

                }
                Lista.Add(obj);
            }
            return Lista;
        }



        public static List<T> GetResultado<T>(DataTable table)
        {

            List<T> data = new List<T>();
            foreach (DataRow row in table.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;   
          
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }  
        #endregion


        #region "Helper methods"
        private static object ValidateData(object value)
        {
            if (value == null)
                return null;
            else
            {
                Type tipo = value.GetType();

                if (tipo == typeof(int))
                    return (int)value == 0 ? null : value;

                if (tipo == typeof(DateTime))
                    return ((DateTime)value).Equals(DateTime.MinValue) ? null : value;

                return value;
            }
        }

        private static string GetName(string parameterName)
        {
            if (parameterName.StartsWith("@"))
                parameterName = parameterName.Replace("@", "").Trim();

            return parameterName;
        }

        private static object GetPropertyValue(object input, string parameterName)
        {
            Type type = input.GetType();
            PropertyInfo propInfo = type.GetProperty(parameterName);

            object value = propInfo.GetValue(input, null);

            if (value == null && propInfo.PropertyType == typeof(Byte[]))
                return new byte[] { };

            return value;
        }

        private static void SetPropertyValue(object input, string parameterName, object value)
        {
            Type type = input.GetType();
            PropertyInfo propInfo = type.GetProperty(parameterName);

            object data = null;

            if (string.IsNullOrEmpty(value.ToString()) == false)
                data = Convert.ChangeType(value, propInfo.PropertyType);

            propInfo.SetValue(input, data, null);
        }





        public static DataTable ListToDataTable<T>(List<T> Lista)
        {
            try
            {

                DataTable table = new DataTable();

                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                    {

                        T obj = Lista.FirstOrDefault();
                        foreach (PropertyInfo proinfo in obj.GetType().GetProperties())
                        {
                            table.Columns.Add(proinfo.Name);
                        }
                        foreach (T Objeto in Lista)
                        {
                            DataRow fila = table.NewRow();

                            foreach (PropertyInfo proinfo in Objeto.GetType().GetProperties())
                            {
                                var Tipo = proinfo.PropertyType.Name;


                                var x = proinfo.GetValue(Objeto, null);
                                fila[proinfo.Name] = proinfo.GetValue(Objeto, null);

                            }
                            table.Rows.Add(fila);
                        }
                    }
                }
                return table;
            }
            catch
            {
                return null;
                throw;
            }
        }

        #endregion

    }
}
