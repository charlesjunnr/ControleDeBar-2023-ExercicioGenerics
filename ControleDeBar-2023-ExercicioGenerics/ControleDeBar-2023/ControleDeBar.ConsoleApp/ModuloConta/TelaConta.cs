using ControleDeBar.ConsoleApp.Compartilhado;
using ControleDeBar.ConsoleApp.ModuloFuncionario;
using ControleDeBar.ConsoleApp.ModuloMesa;
using ControleDeBar.ConsoleApp.ModuloProduto;
using System.Collections;

namespace ControleDeBar.ConsoleApp.ModuloConta
{
    internal class TelaConta : Tela<RepositorioConta, Conta>
    {
        protected override string nomeEntidade { get; set; }

        private RepositorioFuncionario repositorioFuncionario;
        private RepositorioMesa repositorioMesa;
        private RepositorioProduto repositorioProduto;
        private RepositorioConta repositorioConta;
        Pedido pedido;

        private TelaFuncionario telaFuncionario;
        private TelaMesa telaMesa;
        private TelaProduto telaProduto;

        public TelaConta(
            RepositorioConta repositorioConta,
            RepositorioFuncionario repositorioFuncionario,
            RepositorioMesa repositorioMesa,
            RepositorioProduto repositorioProduto, 
            TelaFuncionario telaFuncionario, 
            TelaMesa telaMesa, 
            TelaProduto telaProduto)
        {
            repositorioBase = repositorioConta;
            this.repositorioConta = repositorioConta;
            this.repositorioFuncionario = repositorioFuncionario;
            this.repositorioProduto = repositorioProduto;
            this.repositorioMesa = repositorioMesa;
            this.telaFuncionario = telaFuncionario;
            this.telaMesa = telaMesa;
            this.telaProduto = telaProduto;

            nomeEntidade = "Conta";
        }

        public int ApresentarMenu()
        {
            Console.Clear();

            MostrarCabecalho($"Bar do Jão - Conta ");

            Console.WriteLine(" [1] - Abrir conta nova");
            Console.WriteLine(" [2] - Visualizar Contas");
            Console.WriteLine(" [3] - Fechar Conta");
            Console.WriteLine(" [4] - Visualizar Faturamento\n");


            Console.WriteLine(" [5] - para Sair");

            int opcao = Convert.ToInt32(Console.ReadLine());

            return opcao;
        }
        public int ApresentarMenuVisualizacaoContas()
        {
            Console.Clear();

            MostrarCabecalho($"Bar do Jão - Visualização de Contas ");

            Console.WriteLine(" [1] - Visualizar Contas ");
            Console.WriteLine(" [2] - Visualizar Contas em Aberto ");
            Console.WriteLine(" [3] - Visualizar Contas do Dia \n");

            Console.WriteLine(" [4] - para Sair");

            int opcao = Convert.ToInt32(Console.ReadLine());

            return opcao;
        }
        protected override void MostrarTabela(List<Conta> registros)
        {
            MostrarCabecalho("Bar do Jão - Visualizando Contas: ");

            Console.WriteLine("| {0,-5} | {1, -10} | {2, -15} | {3, -20} | {4, -20} |", "Id", "Mesa", "Funcionario", "Data", "Status");
            Console.WriteLine(" -------------------------------------------------------------------------------------------------- ");

            string mensagem;

            foreach (Conta conta in registros)
            {
                mensagem = VerificarDisponibilidade(conta);

                Console.WriteLine("| {0,-5} | {1, -10} | {2, -15} | {3, -20} | {4, -20} |",
                    conta.id,
                    conta.mesa.numero,
                    conta.funcionario.nome,
                    conta.data.ToShortDateString(),
                    mensagem);

                Console.ResetColor();
                Console.WriteLine();
                foreach (Pedido pedido in conta.pedido)
                {
                    Console.WriteLine($"Produto: {pedido.produto.nome} \nQuantidade: {pedido.quantidadeSolicitada}");
                }
            }
        }
        private static string VerificarDisponibilidade(Conta conta)
        {
            string mensagem;
            if (conta.contaAberta == true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                mensagem = "Conta aberta";
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                mensagem = "Conta fechada";

            }

            return mensagem;
        }
        protected override Conta ObterRegistro()
        {
            MostrarCabecalho("Bar do Jão - Conta Nova");

            Funcionario funcionario = ObterFuncionario();

            Mesa mesa = ObterMesa();

            DateTime dataAtual = DateTime.Today;

            bool statusConta = true;

            return new Conta(mesa, funcionario, dataAtual, statusConta);
        }
        public void RegistrarPedidos()
        {
            MostrarCabecalho("Bar do Jão - Cadastro de Pedidos");

            bool temContasEmAberto = VisualizarContasEmAberto();

            if(temContasEmAberto == false)
                return;

            Console.Clear();

            Conta contaSelecionada = EncontrarRegistro("Digite o Id da Conta: ");

            Console.WriteLine(" [1] - Adicionar pedidos ");
            Console.WriteLine(" [2] - Remover pedidos ");

            string opcao = Console.ReadLine();

            if(opcao == "1")
            {
                AdicionarPedidos(contaSelecionada);
            }
            else if(opcao == "2")
            {
                RemoverPedidos(contaSelecionada);
            }
        }
        private void AdicionarPedidos(Conta contaSelecionada)
        {
            Console.WriteLine("Deseja selecionar algum produto? " +
                "\n [1] - sim " +
                "\n [2] - não");

            string opcao = Console.ReadLine();

            while(opcao == "1")
            {
                Produto produto = ObterProduto();

                Console.WriteLine("Quantidade: ");
                int quantidade = Convert.ToInt32(Console.ReadLine());

                contaSelecionada.RegistrarPedido(produto, quantidade);

                Console.WriteLine("Deseja adicionar mais algum item? " +
                    "\n [1] - sim " +
                    "\n [2] - não");

                opcao = Console.ReadLine();
            }
        }
        private Produto ObterProduto()
        {
            telaProduto.Visualizar(false);

            Console.Write("Id do produto: ");
            int idProduto = Convert.ToInt32(Console.ReadLine());

            Produto produto = repositorioProduto.SelecionarPorId(idProduto);
            return produto;
        }
        private void RemoverPedidos(Conta contaSelecionada)
        {
            int id = 0;

            if (contaSelecionada.pedido.Count == 0)
            {
                MostrarMensagem("Nenhum pedido cadastrado para esta conta", ConsoleColor.DarkYellow);
                return;
            }
        }
        private Mesa ObterMesa()
        {
            telaMesa.Visualizar(false);

            Console.WriteLine("Id da Mesa: ");
            int idMesa = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Mesa mesa = repositorioMesa.SelecionarPorId(idMesa);

            return mesa;
        }
        private Funcionario ObterFuncionario()
        {
            telaFuncionario.Visualizar(false);

            Console.WriteLine();

            Console.WriteLine("Id do Funcionário: ");
            int idFuncionario = Convert.ToInt32(Console.ReadLine());

            Funcionario funcionario = repositorioFuncionario.SelecionarPorId(idFuncionario);

            return funcionario;
        }
        public void FecharConta()
        {
            MostrarCabecalho("Bar do Jão - Fechamento de conta");

            VisualizarContasEmAberto();

            Console.WriteLine("Digite o Id da conta:");
            int id = Convert.ToInt32(Console.ReadLine());

            Conta conta = (Conta)repositorioBase.SelecionarPorId(id);

            conta.contaAberta = false;

            MostrarMensagem("Conta fechada com sucesso!", ConsoleColor.Green);
        }
        public void VisualizarFaturamento()
        {
            MostrarCabecalho("Bar do Jão - Faturamento do dia");

            Visualizar(false);

            Console.WriteLine("Digite a data: ");
            DateTime data = Convert.ToDateTime(Console.ReadLine());

            List<Conta> contasFechadasNoDia = repositorioConta.SelecionarContasFechadas(data);

            FaturamentoDiario faturamentoDiario = new FaturamentoDiario(contasFechadasNoDia);

            decimal totalFaturado = faturamentoDiario.CalcularTotal();

            Console.WriteLine("Contas fechadas na data: " + data.ToShortDateString());

            MostrarTabela(contasFechadasNoDia);

            Console.WriteLine();

            MostrarMensagem("Total faturado: " + totalFaturado, ConsoleColor.Green);
        }
        public bool VisualizarContasEmAberto()
        {
            MostrarCabecalho("Bar do Jão - Visualizando Contas em Aberto: ");

            List<Conta> contasEmAberto = repositorioConta.SelecionarContasEmAberto(); //repositorio base tem essa informações mas estamos dizendo pra ele que a instância tem e executa o método

            if (contasEmAberto.Count == 0)
            {
                MostrarMensagem("Nenhuma conta em aberto", ConsoleColor.DarkYellow);
                return false;
            }

            MostrarTabela(contasEmAberto);

            return contasEmAberto.Count > 0;
        }
        public void VisualizarContasDia()
        {
            MostrarCabecalho("Bar do Jão - Visualizando Contas do Dia: ");

            List<Conta> registros = repositorioBase.SelecionarTodos();

            Console.WriteLine("| {0,-5} | {1, -10} | {2, -15} | {3, -20} |  {4, -10} |", "Id", "Mesa", "Funcionario", "Data", "Status");
            Console.WriteLine(" -------------------------------------------------------------------------------------------------- ");

            foreach (Conta conta in registros)
            {
                string mensagem = VerificarDisponibilidade(conta);

                if (conta.data == DateTime.Today)
                {
                    Console.WriteLine("| {0,-5} | {1, -10} | {2, -15} | {3, -20} | {4, -10} |",
                     conta.id,
                     conta.mesa.numero,
                     conta.funcionario.nome,
                     conta.data.ToShortDateString(),
                     mensagem);
                }
                else
                {
                    continue;
                }
                Console.ResetColor();
            }
        }
        public void AbrirNovaConta()
        {
            MostrarCabecalho("Bar do Jão - Abertura de Contas");

            Conta conta = (Conta)ObterRegistro();

            if (TemErrosDeValidacao(conta))
            {
                AbrirNovaConta(); //chamada recursiva

                return;
            }

            repositorioBase.Inserir(conta);

            AdicionarPedidos(conta);

            MostrarMensagem("Registro inserido com sucesso!", ConsoleColor.Green);
        }

    }
}
