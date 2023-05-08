using ControleDeBar.ConsoleApp.Compartilhado;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeBar.ConsoleApp.ModuloFuncionario
{
    public class Funcionario : Entidade<Funcionario>
    {
        public string nome;
        public string telefone;
        public string endereco;
        public string turno;

        public Funcionario(string nome, string telefone, string endereco, string turno)
        {
            this.nome = nome;
            this.telefone = telefone;
            this.endereco = endereco;
            this.turno = turno;
        }

        public override void AtualizarInformacoes(Funcionario registroAtualizado)
        {
            Funcionario FuncionarioAtualizado = registroAtualizado;
            
            nome = FuncionarioAtualizado.nome;
            telefone = FuncionarioAtualizado.telefone;
            endereco = FuncionarioAtualizado.endereco;
            turno = FuncionarioAtualizado.turno;

        }

        public override List<string> Validar()
        {
            List<string> erros = new List<string>();

            if (string.IsNullOrEmpty(nome) && string.IsNullOrWhiteSpace(nome))
            {
                erros.Add("É obrigatório preencher o nome!");
            }
            if (string.IsNullOrEmpty(telefone) && string.IsNullOrWhiteSpace(telefone))
            {
                erros.Add("É obrigatório preencher o telefone!");
            }
            if (string.IsNullOrEmpty(endereco) && string.IsNullOrWhiteSpace(endereco))
            {
                erros.Add("É obrigatório preencher o endereço!");
            }

            if (nome.Length < 3)
            {
                erros.Add("Nome deve ser maior que 3 caracteres!");
            }

            if (telefone.Length < 10)
            {
                erros.Add("Número de telefone inválido! " +
                    "\nCogite o formato (XX) XXXXX-XXXX" +
                    "\nPreencha somente com números.");
            }

            if (turno != "MANHÃ" && turno != "TARDE" && turno != "NOITE")
            {
                erros.Add("Turno inexistente! " +
                    "\nEscolha uma entre: " +
                    "\n - Manhã " +
                    "\n - Tarde " +
                    "\n - Noite");
            }
            return erros;
        }
    }
}
