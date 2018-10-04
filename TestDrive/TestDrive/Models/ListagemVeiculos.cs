using System;
using System.Collections.Generic;
using System.Text;

namespace TestDrive.Models
{
    public class ListagemVeiculos
    {
        public List<Veiculo> Veiculos { get; set; }

        public ListagemVeiculos()
        {
            this.Veiculos = new List<Veiculo>();
            this.Veiculos.Add(new Veiculo("Azera V6", 60000));
            this.Veiculos.Add(new Veiculo("Fiesta 2.0", 50000));
            this.Veiculos.Add(new Veiculo("HB20 S", 40000));
        }
    }
}
