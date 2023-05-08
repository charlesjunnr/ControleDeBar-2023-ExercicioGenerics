using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeBar.ConsoleApp.ModuloConta
{
    public class FaturamentoDiario
    {
        private List<Conta> contasFechadas;

        public FaturamentoDiario(List<Conta> contas)
        {
            this.contasFechadas = contas;
        }

        public decimal CalcularTotal()
        {
            decimal total = 0;

            foreach (Conta conta in contasFechadas)
            {
                total += conta.CalcularValorTotal();
            }

            return total;
        }
    }
}
