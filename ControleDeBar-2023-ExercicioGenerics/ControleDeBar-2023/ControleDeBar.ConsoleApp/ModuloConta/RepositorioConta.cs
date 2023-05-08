using ControleDeBar.ConsoleApp.Compartilhado;
using ControleDeBar.ConsoleApp.ModuloFuncionario;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeBar.ConsoleApp.ModuloConta
{
    public class RepositorioConta : Repositorio<Conta>
    {
        public RepositorioConta(List<Conta> listaRegistros) : base(listaRegistros)
        {
        }

        public override Conta SelecionarPorId(int id)
        {
            return base.SelecionarPorId(id);
        }

        public List<Conta> SelecionarContasEmAberto()
        {
            List<Conta> contasEmAberto = new List<Conta>();

            foreach (Conta conta in listaRegistros)
            {
                if (conta.contaAberta == true)
                {
                    contasEmAberto.Add(conta);
                }

            }
            return contasEmAberto;
        }

        public List<Conta> SelecionarContasFechadas(DateTime data)
        {
            List<Conta> contasEmAberto = new List<Conta>();

            foreach (Conta conta in listaRegistros)
            {
                if (conta.contaAberta == false && conta.data.Date == data.Date)
                    contasEmAberto.Add(conta);
            }

            return contasEmAberto;
        }
    }
}
