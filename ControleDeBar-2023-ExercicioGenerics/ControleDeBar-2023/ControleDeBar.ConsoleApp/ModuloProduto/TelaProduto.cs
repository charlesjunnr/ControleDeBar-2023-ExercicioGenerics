using ControleDeBar.ConsoleApp.Compartilhado;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeBar.ConsoleApp.ModuloProduto
{
    internal class TelaProduto : Tela<RepositorioProduto, Produto>
    {
        protected override string nomeEntidade { get; set; }
        public TelaProduto(RepositorioProduto repositorioProduto)
        {
            repositorioBase = repositorioProduto;
            nomeEntidade = "Produto";
            
        }

        protected override void MostrarTabela(List<Produto> registros)
        {
            MostrarCabecalho("Bar do Jão - Cardápio");

            Console.WriteLine("{0, -5} | {1, -10} | {2, -10} ", "Id", "Nome", "Valor");
            Console.WriteLine(" ---------------------------------------- ");
            foreach (Produto produto in registros)
            {
                Console.WriteLine("{0, -5} | {1, -10} | {2, -10} ", produto.id, produto.nome, produto.valor);
            }
            
        }

        protected override Produto ObterRegistro()
        {
            MostrarCabecalho("Bar do Jão - Registrar novos produtos");
            

            Console.Write("Digite o nome do produto: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o valor do produto: ");
            decimal valor = Convert.ToDecimal(Console.ReadLine());

            return new Produto(nome, valor);
        }

    }
}
