using ControleDeBar.ConsoleApp.Compartilhado;
using ControleDeBar.ConsoleApp.ModuloFuncionario;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeBar.ConsoleApp.ModuloMesa
{
    internal class RepositorioMesa : Repositorio<Mesa>
    {
        public RepositorioMesa(List<Mesa> listaRegistros) : base(listaRegistros)
        {
        }

        public override Mesa SelecionarPorId(int id)
        {
            return base.SelecionarPorId(id);
        }
    }
}
