using ControleDeBar.ConsoleApp.Compartilhado;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeBar.ConsoleApp.ModuloMesa
{
    public class Mesa : Entidade<Mesa>
    {
        public int numero;
        public string localizacao;
        public int quantidadeLugares;
        public bool estaDisponivel;

        //implementar disponibilidade com uma booleana

        public Mesa(int numero, string localizacao, int quantidadeLugares, bool estaDisponivel)
        {
            this.numero = numero;
            this.localizacao = localizacao;
            this.quantidadeLugares = quantidadeLugares;
            this.estaDisponivel = estaDisponivel;
        }

        public override void AtualizarInformacoes(Mesa registroAtualizado)
        {
            Mesa mesaAtualizada = registroAtualizado;

            numero = mesaAtualizada.numero;
            localizacao = mesaAtualizada.localizacao;
            quantidadeLugares = mesaAtualizada.quantidadeLugares;
            estaDisponivel = mesaAtualizada.estaDisponivel;
        }

        public override List<string> Validar()
        {
            List<string> erros = new List<string>();

            if (string.IsNullOrEmpty(localizacao) && string.IsNullOrWhiteSpace(localizacao))
            {
                erros.Add("É obrigatório preencher o nome!");
            }
            if (numero <= 0)
            {
                erros.Add("Número acima de 0!");
            }
            if(quantidadeLugares <= 0 || quantidadeLugares > 6)
            {
                erros.Add("A quantidade máxima de lugares é 6!");
            }
            if (localizacao != "INTERIOR" && localizacao != "EXTERIOR" && localizacao != "BALCÃO")
            {
                erros.Add("Localização inexistente! " +
                    "\nEscolha uma entre: " +
                    "\n - Interior " +
                    "\n - Exterior " +
                    "\n - Balcão");
            }
            if (localizacao == "Balcão" && quantidadeLugares > 1)
            {
                erros.Add("O balcão comporta somente um (1) lugar!");
            }

            return erros;
        }
    }
}
