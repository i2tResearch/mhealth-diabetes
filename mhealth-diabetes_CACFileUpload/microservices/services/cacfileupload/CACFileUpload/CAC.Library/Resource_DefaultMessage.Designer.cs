﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CAC.Library {
    using System;
    
    
    /// <summary>
    ///   Clase de recurso fuertemente tipado, para buscar cadenas traducidas, etc.
    /// </summary>
    // StronglyTypedResourceBuilder generó automáticamente esta clase
    // a través de una herramienta como ResGen o Visual Studio.
    // Para agregar o quitar un miembro, edite el archivo .ResX y, a continuación, vuelva a ejecutar ResGen
    // con la opción /str o recompile su proyecto de VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resource_DefaultMessage {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource_DefaultMessage() {
        }
        
        /// <summary>
        ///   Devuelve la instancia de ResourceManager almacenada en caché utilizada por esta clase.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CAC.Library.Resource_DefaultMessage", typeof(Resource_DefaultMessage).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Reemplaza la propiedad CurrentUICulture del subproceso actual para todas las
        ///   búsquedas de recursos mediante esta clase de recurso fuertemente tipado.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a @{0}@{1}@{2}@{3}@{4}@{5}.
        /// </summary>
        internal static string CONTROL_VALUE {
            get {
                return ResourceManager.GetString("CONTROL_VALUE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a La celda {0}{1} no tiene un valor valido o admitido por la plantilla..
        /// </summary>
        internal static string ERROR_CELL_DO_NOT_LIST_VALUE {
            get {
                return ResourceManager.GetString("ERROR_CELL_DO_NOT_LIST_VALUE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a La celda {0}{1} está vacía..
        /// </summary>
        internal static string ERROR_CELL_EMPTY_OR_NULL {
            get {
                return ResourceManager.GetString("ERROR_CELL_EMPTY_OR_NULL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a La celda {0}{1} contiene una formula o similar. La plantilla sólo permite números, fechas según el formato, o textos alfa-numericos..
        /// </summary>
        internal static string ERROR_CELL_HAVE_FORMULA {
            get {
                return ResourceManager.GetString("ERROR_CELL_HAVE_FORMULA", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a La celda {0}{1} no contiene un valor valido al formato especificado en la plantilla. Tipo de formato admitido {2}..
        /// </summary>
        internal static string ERROR_CELL_NOT_FORMAT {
            get {
                return ResourceManager.GetString("ERROR_CELL_NOT_FORMAT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a El tipo de dato de la celda {0}{1} no concuerda con el de la plantilla. Debe ser del tipo &apos;{2}&apos; y es de tipo &apos;{3}&apos;..
        /// </summary>
        internal static string ERROR_CELL_NOT_VALID_TYPE {
            get {
                return ResourceManager.GetString("ERROR_CELL_NOT_VALID_TYPE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a La columna {0} no existe en el archivo y es requerido en la plantilla..
        /// </summary>
        internal static string ERROR_COLUMN_MISSING_NAME {
            get {
                return ResourceManager.GetString("ERROR_COLUMN_MISSING_NAME", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a El nombre de la columna {0} no es valido..
        /// </summary>
        internal static string ERROR_COLUMN_NAME_NOT_VALID {
            get {
                return ResourceManager.GetString("ERROR_COLUMN_NAME_NOT_VALID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Al menos {0} columna está repetida. No se permiten columnas repetidas: {1}..
        /// </summary>
        internal static string ERROR_COLUMN_NAME_REPEATED {
            get {
                return ResourceManager.GetString("ERROR_COLUMN_NAME_REPEATED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a La columna {0} no esta permitida en la plantilla..
        /// </summary>
        internal static string ERROR_COLUMN_NOT_ALLOWED {
            get {
                return ResourceManager.GetString("ERROR_COLUMN_NOT_ALLOWED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a La columna {0} no contiene filas para ser analizadas y es requerido en la plantilla..
        /// </summary>
        internal static string ERROR_COLUMN_NOT_DATA {
            get {
                return ResourceManager.GetString("ERROR_COLUMN_NOT_DATA", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a La columna {0} no permite valores duplicados..
        /// </summary>
        internal static string ERROR_COLUMN_NOT_DUPLICATE_DATA {
            get {
                return ResourceManager.GetString("ERROR_COLUMN_NOT_DUPLICATE_DATA", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a El número de columnas no coinciden con la plantilla. Existen {0} columnas y deben ser {1}..
        /// </summary>
        internal static string ERROR_COLUMNS_NOT_EQUALS {
            get {
                return ResourceManager.GetString("ERROR_COLUMNS_NOT_EQUALS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a El archivo enviado o enviados no tiene un extensión valida para ser procesado. Sólo se permiten extensiones CSV, XLS, o XLSX. Nombre del archivo: {0}..
        /// </summary>
        internal static string ERROR_FILE_NOT_VALID_EXT {
            get {
                return ResourceManager.GetString("ERROR_FILE_NOT_VALID_EXT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Existe un error no clasificado. Por favor comunicarse con servicio técnico enviando el siguiente resultado: {0}..
        /// </summary>
        internal static string ERROR_LOG {
            get {
                return ResourceManager.GetString("ERROR_LOG", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a .
        /// </summary>
        internal static string ERROR_ROW_ {
            get {
                return ResourceManager.GetString("ERROR_ROW_", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a La plantilla de comparación no pudo ser cargada o no se encuentra disponible en el servidor. Si este error persiste, comuniquese con servicio técnico..
        /// </summary>
        internal static string ERROR_TEMPLATE_NOT_FOUND {
            get {
                return ResourceManager.GetString("ERROR_TEMPLATE_NOT_FOUND", resourceCulture);
            }
        }
    }
}
