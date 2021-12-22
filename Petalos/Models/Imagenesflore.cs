using System;
using System.Collections.Generic;

#nullable disable

namespace Petalos.Models
{
    public partial class Imagenesflore
    {
        public uint Idimagen { get; set; }
        public string Nombreimagen { get; set; }
        public uint Idflor { get; set; }

        public virtual Datosflore IdflorNavigation { get; set; }
    }
}
