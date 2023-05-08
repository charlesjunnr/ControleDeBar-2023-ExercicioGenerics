using ControleDeBar.ConsoleApp.Compartilhado;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeBar.ConsoleApp.ModuloMesa
{
    internal class TelaMesa : Tela<RepositorioMesa, Mesa>
    {
        protected override string nomeEntidade { get; set; }

        public TelaMesa(RepositorioMesa repositorioMesa)
        {
            repositorioBase = repositorioMesa;
            nomeEntidade = "Mesa";
            
        }

        protected override void MostrarTabela(List<Mesa> registros)
        {
            MostrarCabecalho("Bar do Jão - Visualizando Mesas: ");
            Console.WriteLine("{0,-5} | {1, -10} | {2, -15} | {3, -20} |", "Id", "Mesa", "Localização", "Status");

            string mensagem;

            foreach (Mesa mesa in registros)
            {
                if (mesa.estaDisponivel == true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    mensagem = "Mesa livre";
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    mensagem = "Mesa ocupada";
                }

                Console.WriteLine("{0,-5} | {1, -10} | {2, -15} | {3, -20} |", mesa.id, mesa.numero, mesa.localizacao, mensagem);

                Console.ResetColor();
            }
            
        }

        protected override Mesa ObterRegistro()
        {
            MostrarCabecalho("Bar do Jão - Inserir mesa");

            Console.Write("Número da mesa: ");
            int numero = Convert.ToInt32(Console.ReadLine());

            Console.Write("Localização: ");
            string localizacao = Console.ReadLine().ToUpper(); ;
            
            Console.Write("Quantidade de Lugares: ");
            int quantidadeLugares = Convert.ToInt32(Console.ReadLine());

            bool estaDisponivel = true;

            return new Mesa(numero, localizacao, quantidadeLugares, estaDisponivel);
        }

        private bool ReservarMesaNoCadastro(string opcao, bool estaDisponivel)
        {
            while (opcao != "1" || opcao != "2")
            {
                if (opcao == "1")
                {
                    estaDisponivel = false;
                }
                else if (opcao == "2")
                {
                    estaDisponivel = true;
                }
            }
            return estaDisponivel;
        }
    }
}
