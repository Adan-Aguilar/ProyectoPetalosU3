using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petalos.Models.ViewModels
{
    public class FloresViewModel
    {
        public IEnumerable<Datosflore> CuatroFlores { get; set; }
        public Datosflore FlorUnica { get; set; }
    }

}
