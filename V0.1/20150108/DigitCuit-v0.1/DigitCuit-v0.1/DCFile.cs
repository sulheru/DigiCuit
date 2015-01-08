using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitCuit_v0._1
{
    class DCFile
    {
        /// <summary>
        /// Carpeta del archivo
        /// </summary>
        public string FilePath;

        /// <summary>
        /// Titulo del archivo
        /// </summary>
        public string FileTitle;

        /// <summary>
        /// Extension del archivo
        /// </summary>
        public string Extension;

        /// <summary>
        /// Dirección completa del archivo
        /// </summary>
        public string FullPath
        {
            get
            { return this.FilePath + '\\' + FileName; }
            set
            {
                ArrayList file = new ArrayList(value.Split('\\'));
                int c = file.Count - 1;
                this.FileName = file[c].ToString();
                
                file.RemoveAt(c);
                this.FilePath = String.Join("\\", file.ToArray());
            }
        }

        /// <summary>
        /// Nombre del archivo
        /// </summary>
        public string FileName
        {
            get
            {
                return this.FileTitle + '.' + this.Extension;
            }
            set
            {
                ArrayList file = new ArrayList(value.Split('.'));
                int c = file.Count - 1;
                this.Extension = file[c].ToString();
                
                file.RemoveAt(c);
                this.FileTitle = String.Join(".", file.ToArray());
            }
        }

        public DCFile(string FullName)
        { this.FullPath = FullName; }

        public override string ToString()
        { return this.FullPath; }
    }
}
