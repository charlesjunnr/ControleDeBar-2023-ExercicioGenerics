using ControleDeBar.ConsoleApp.Compartilhado;
using ControleDeBar.ConsoleApp.ModuloFuncionario;
using ControleDeBar.ConsoleApp.ModuloMesa;
using ControleDeBar.ConsoleApp.ModuloProduto;


using System.Collections;

namespace ControleDeBar.ConsoleApp.ModuloConta
{
    public class Conta : Entidade<Conta>
    {
        public Mesa mesa;
        public Funcionario funcionario;
        public ArrayList pedido;
        public DateTime data;
        public bool contaAberta;
        public decimal valorTotal;

        public Conta(Mesa mesa, Funcionario funcionario, DateTime data, bool contaAberta)
        {
            this.mesa = mesa;
            this.funcionario = funcionario;
            this.data = data;
            this.contaAberta = contaAberta;
            pedido = new ArrayList();

        }

        public override void AtualizarInformacoes(Conta registroAtualizado)
        {
            Conta contaAtualizada =  registroAtualizado;

            this.mesa = contaAtualizada.mesa;
            this.funcionario = contaAtualizada.funcionario;
            this.data = contaAtualizada.data;
            this.contaAberta = contaAtualizada.contaAberta;
            
        }

        public override List<string> Validar()
        {
            List<string> erros = new List<string>();

            if (contaAberta == false)
            {
                erros.Add("Essa conta já está fechou!");
            }
            if (mesa.estaDisponivel == false)
            {
                erros.Add("Mesa indisponível!");
            }
            return erros;
        }
        public void RegistrarPedido(Produto produto, int quantidadeEscolhida)
        {
            Pedido novoPedido = new Pedido(produto, quantidadeEscolhida);

            pedido.Add(novoPedido);
        }
        public decimal CalcularValorTotal()
        {
            decimal total = 0;

            foreach (Pedido pedido in pedido)
            {
                decimal totalPedido = pedido.CalcularValorParcial();

                total += totalPedido;
            }

            return total;
        }

    }
}
