using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeBar.ConsoleApp.Compartilhado
{
    public interface ITela
    {
        int ApresentarMenu();
        void Cadastrar();
        bool Visualizar(bool mostrarCabecalho);
        void Editar();
        void Excluir();
    }
}
