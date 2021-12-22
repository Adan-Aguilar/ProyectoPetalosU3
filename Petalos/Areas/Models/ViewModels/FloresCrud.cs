using Microsoft.AspNetCore.Http;
using Petalos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petalos.Areas.Models.ViewModels
{
    public class FloresCrud
    {
        public Datosflore Flores { get; set; }
        public IEnumerable<Datosflore> ListaFlores { get; set; }
        public Imagenesflore ImagenesMedianteEl_ID { get; set; }
        //public IEnumerable<Datosflore> ListaFlores { get; set; }
        //public IEnumerable<Imagenesflore> ListaImagenes { get; set; }

    }
}
