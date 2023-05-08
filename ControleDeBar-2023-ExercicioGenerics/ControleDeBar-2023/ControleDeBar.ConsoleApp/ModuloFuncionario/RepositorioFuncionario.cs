using ControleDeBar.ConsoleApp.Compartilhado;
using System.Collections;

namespace ControleDeBar.ConsoleApp.ModuloFuncionario
{
    public class RepositorioFuncionario : Repositorio<Funcionario>
    {
        public RepositorioFuncionario(List<Funcionario> listaRegistros) : base(listaRegistros)
        {
        }

        public override Funcionario SelecionarPorId(int id)
        {
            return base.SelecionarPorId(id);
        }
    }
}
