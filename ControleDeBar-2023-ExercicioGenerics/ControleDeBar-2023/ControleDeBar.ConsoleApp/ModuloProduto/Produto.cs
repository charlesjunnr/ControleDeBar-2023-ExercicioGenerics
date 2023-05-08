using ControleDeBar.ConsoleApp.Compartilhado;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeBar.ConsoleApp.ModuloProduto
{
    public class Produto : Entidade<Produto>
    {
        public string nome;
        public decimal valor;

        public Produto(string nome, decimal valor)
        {
            this.nome = nome;
            this.valor = valor;
        }

        public override void AtualizarInformacoes(Produto registroAtualizado)
        {
            Produto produtoAtualizado = registroAtualizado;
            
            nome = produtoAtualizado.nome;
            valor = produtoAtualizado.valor;
        }

        public override List<string> Validar()
        {
            List<string> erros = new List<string>();

            if (string.IsNullOrEmpty(nome) && string.IsNullOrWhiteSpace(nome))
            {
                erros.Add("É obrigatório preencher o nome!");
            }
            if (nome.Length < 3)
            {
                erros.Add("Escreva um nome maior que três caracteres!");
            }
            if (valor <= 0)
            {
                erros.Add("Tá distribuindo? Coloque um valor acima de 0!");
            }
            return erros;
        }
    }
}
