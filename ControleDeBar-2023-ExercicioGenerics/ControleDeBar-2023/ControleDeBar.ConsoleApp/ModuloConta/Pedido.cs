using ControleDeBar.ConsoleApp.ModuloProduto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeBar.ConsoleApp.ModuloConta
{
    public class Pedido
    {
        public static int contadorId;
        public int id;
        public Produto produto;
        public int quantidadeSolicitada;

        public Pedido(Produto produto, int quantidadeSolicitada)
        {
            contadorId++;
            id = contadorId;
            this.produto = produto;
            this.quantidadeSolicitada = quantidadeSolicitada;
        }
        public decimal CalcularValorParcial()
        {
            return produto.valor * quantidadeSolicitada;
        }


    }
}
