using ControleDeBar.ConsoleApp.Compartilhado;
using ControleDeBar.ConsoleApp.ModuloMesa;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeBar.ConsoleApp.ModuloProduto
{
    public class RepositorioProduto : Repositorio<Produto>
    {
        public RepositorioProduto(List<Produto> listaRegistros) : base(listaRegistros)
        {
        }

        public override Produto SelecionarPorId(int id)
        {
            return base.SelecionarPorId(id);
        }
    }
}
