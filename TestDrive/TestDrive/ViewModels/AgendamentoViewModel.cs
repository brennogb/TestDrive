using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using TestDrive.Models;
using Xamarin.Forms;

namespace TestDrive.ViewModels
{
    public class AgendamentoViewModel : BaseViewModel
    {

        const string URL_SALVAR_AGENDAMENTO = "https://aluracar.herokuapp.com/salvaragendamento";

        public Agendamento Agendamento { get; set; }

        public AgendamentoViewModel(Veiculo veiculo)
        {
            this.Agendamento = new Agendamento();
            this.Agendamento.Veiculo = veiculo;
            AgendarCommand = new Command(() =>
            {
                //MessagingCenter.Send<Agendamento>(this.Agendamento, "Agendamento");
                this.SalvarAgendamento();
            }, () => 
            { return !string.IsNullOrEmpty(Nome) && !string.IsNullOrEmpty(Fone) && !string.IsNullOrEmpty(Email); });
        }

        public async void SalvarAgendamento()
        {
            HttpClient client = new HttpClient();

            var dataHoraAgendamento = new DateTime(Agendamento.DataAgendamento.Year, Agendamento.DataAgendamento.Month,
                Agendamento.DataAgendamento.Day, Agendamento.HoraAgendamento.Hours, Agendamento.HoraAgendamento.Minutes,
                Agendamento.HoraAgendamento.Seconds);

            string json = JsonConvert.SerializeObject(new
            { nome = Nome,
              fone = Fone,
              email = Email,
              carro = Agendamento.Veiculo.Nome,
              preco = Agendamento.Veiculo.Preco,
              dataAgendamento = dataHoraAgendamento
            });

            var conteudo = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(URL_SALVAR_AGENDAMENTO, conteudo);

            if (response.IsSuccessStatusCode)
            {
                MessagingCenter.Send<Agendamento>(this.Agendamento, "SucessoAgendamento");
            }
            else
            {
                MessagingCenter.Send<ArgumentException>(new ArgumentException(), "FalhaAgendamento");
            }
        }

        public ICommand AgendarCommand { get; set; }

        public string Nome { get { return Agendamento.Nome; }
            set
            {
                Agendamento.Nome = value;
                OnPropertyChanged();
                ((Command)AgendarCommand).ChangeCanExecute();
            }
        }
        public string Fone { get { return Agendamento.Fone; }
            set
            {
                Agendamento.Fone = value;
                OnPropertyChanged();
                ((Command)AgendarCommand).ChangeCanExecute();
            }
        }
        public string Email { get { return Agendamento.Email; }
            set
            {
                Agendamento.Email = value;
                OnPropertyChanged();
                ((Command)AgendarCommand).ChangeCanExecute();
            }
        }
    }
}
