using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiCuitBeta.Electronics
{
    /// <summary>
    /// Componente Electrónico
    /// </summary>
    public class Component
    {
        private Jint.Engine engine;

        /// <summary>
        /// Optiene la información del archivo de componente en uso
        /// </summary>
        public FileInfo ComponentFile { get; private set; }
        /// <summary>
        /// Optiene la información del archivo de librerias globales
        /// </summary>
        public FileInfo GlobalLibraryFile { get; private set; }

        /// <summary>
        /// Optiene el nombre del componente
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Optiene la descripción del componente
        /// </summary>
        public string Description { get; private set; }
        /// <summary>
        /// Optiene la collección a la que pertenece
        /// </summary>
        public string Collection { get; private set; }

        /// <summary>
        /// Optiene la versión del componente
        /// </summary>
        public Version ComponentVersion { get; private set; }
        /// <summary>
        /// Optiene la versión de la librería global
        /// </summary>
        public Version GlobalLibraryVersion { get; private set; }

        /// <summary>
        /// Contenido de la librería global
        /// </summary>
        public string GlobalLibraryContent { get; private set; }
        /// <summary>
        /// Contenido del archivo de componente
        /// </summary>
        public string ComponentContent { get; private set; }

        /// <summary>
        /// Coordenadas de la esquina superior izquierda del componente
        /// </summary>
        public System.Drawing.Point Position { set; get; }

        /**
         * Variables sin establecer:
         * -----------------------------
         * PropertyCollection Properties
         * DirectCurrentCollection InOut
         * List-Marker- Plugs
         * List-Marker- Sockets
         **/
    }
}
