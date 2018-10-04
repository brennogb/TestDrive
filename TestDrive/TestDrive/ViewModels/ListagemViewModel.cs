using Newtonsoft;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestDrive.Models;
using Xamarin.Forms;

namespace TestDrive.ViewModels
{
    public class ListagemViewModel : BaseViewModel
    {

        const string URL_VEICULOS = "http://aluracar.herokuapp.com";

        private bool aguarde;

        public bool Aguarde
        { get { return aguarde; }
          set
            {
                aguarde = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Veiculo> Veiculos { get; set; }

        private Veiculo veiculoSelecionado;
        public Veiculo VeiculoSelecionado
        { get
            {
                return veiculoSelecionado;
            }
            set
            {
                veiculoSelecionado = value;
                if (value != null)
                {
                    MessagingCenter.Send(veiculoSelecionado, "VeiculoSelecionado");
                }
            }
        }

        public ListagemViewModel()
        {
            this.Veiculos = new ObservableCollection<Veiculo>();
        }

        public async Task GetVeiculos()
        {
            Aguarde = true;
            HttpClient client = new HttpClient();
            var json = await client.GetStringAsync(URL_VEICULOS);

            VeiculoJson[] veiculos = JsonConvert.DeserializeObject<VeiculoJson[]>(json);

            foreach (var item in veiculos)
            {
                this.Veiculos.Add(new Veiculo { Nome = item.nome, Preco = item.preco });
            }

            Aguarde = false;
        }
    }

    class VeiculoJson
    {
        public string nome { get; set; }
        public int preco { get; set; }

    }
}
