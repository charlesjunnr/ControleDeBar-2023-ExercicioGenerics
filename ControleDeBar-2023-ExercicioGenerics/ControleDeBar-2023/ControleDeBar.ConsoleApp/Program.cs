using ControleDeBar.ConsoleApp.Compartilhado;
using ControleDeBar.ConsoleApp.ModuloConta;
using ControleDeBar.ConsoleApp.ModuloFuncionario;
using ControleDeBar.ConsoleApp.ModuloMesa;
using ControleDeBar.ConsoleApp.ModuloProduto;
using System.Collections;

namespace ControleDeBar.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            RepositorioProduto repositorioProduto = new RepositorioProduto(new List<Produto>());
            TelaProduto telaProduto = new TelaProduto(repositorioProduto);

            RepositorioFuncionario repositorioFuncionario = new RepositorioFuncionario(new List<Funcionario>());
            TelaFuncionario telaFuncionario = new TelaFuncionario(repositorioFuncionario);

            RepositorioMesa repositorioMesa = new RepositorioMesa(new List<Mesa>());
            TelaMesa telaMesa = new TelaMesa(repositorioMesa);

            RepositorioConta repositorioConta = new RepositorioConta(new List<Conta>());
            TelaConta telaConta = new TelaConta(repositorioConta, repositorioFuncionario, repositorioMesa, repositorioProduto, telaFuncionario, telaMesa, telaProduto);

            GerarRegistros(repositorioProduto, repositorioMesa, repositorioFuncionario, repositorioConta);

            ITela tela;

            bool ehContinuar = true;

            while (ehContinuar == true)
            {
                try
                {
                    int opcao = ApresentarMenuPrincipal();
                    switch (opcao)
                    {
                        case 1: tela = telaProduto; break;
                        case 2: tela = telaFuncionario; break;
                        case 3: tela = telaMesa; break;
                        case 4: tela = telaConta; break;
                        case 5: ehContinuar = false; continue;
                        default: continue;
                    }
                    int subMenu = 0;

                    if (tela is TelaConta)
                    {
                        do
                        {
                            subMenu = tela.ApresentarMenu();
                            switch (subMenu)
                            {
                                case 1: telaConta.Cadastrar(); break;
                                case 2:
                                    int subMenuConta = telaConta.ApresentarMenuVisualizacaoContas();
                                    if (subMenuConta == 1)
                                    {
                                        telaConta.Visualizar(true);
                                        Console.ReadLine();
                                    }
                                    else if (subMenuConta == 2)
                                    {
                                        telaConta.VisualizarContasEmAberto();
                                    }
                                    else if (subMenuConta == 3)
                                    {
                                        telaConta.VisualizarContasDia();
                                    }
                                    else
                                    {
                                        return;
                                    }
                                    break;
                                case 3: telaConta.FecharConta(); break;
                                case 4: telaConta.VisualizarFaturamento(); break;
                            }
                        } while (subMenu != 5);


                    }
                    do
                    {
                        subMenu = tela.ApresentarMenu();
                        switch (subMenu)
                        {
                            case 1: tela.Cadastrar(); break;
                            case 2: tela.Visualizar(true); Console.ReadLine(); break;
                            case 3: tela.Editar(); break;
                            case 4: tela.Excluir(); break;

                        }
                    } while (subMenu != 5);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Opção inválida!");
                    Console.ReadLine();
                    
                }

            }

        }
        public static int ApresentarMenuPrincipal()
        {
            Console.Clear();
            Console.WriteLine(" ------ Bar do Jão ------");
            Console.WriteLine(" [1] - Produtos");
            Console.WriteLine(" [2] - Funcionários");
            Console.WriteLine(" [3] - Mesas");
            Console.WriteLine(" [4] - Contas\n");

            Console.WriteLine(" [5] - Sair");

            int opcao = Convert.ToInt32(Console.ReadLine());
            return opcao;
        }
        private static void GerarRegistros(RepositorioProduto repositorioProduto,
        RepositorioMesa repositorioMesa,
        RepositorioFuncionario repositorioFuncionario,
        RepositorioConta repositorioConta)
        {
            Produto produto1 = new Produto("Coca-cola lata", 6);
            Produto produto2 = new Produto("Batata-frita porção", 12);
            Produto produto3 = new Produto("Cachorro-quente", 15);

            repositorioProduto.Inserir(produto1);
            repositorioProduto.Inserir(produto2);
            repositorioProduto.Inserir(produto3);

            Mesa mesa1 = new Mesa(22, "INTERIOR", 4, true);
            Mesa mesa2 = new Mesa(2, "BALCÃO", 1, false);
            Mesa mesa3 = new Mesa(4, "EXTERIOR", 3, false);

            repositorioMesa.Inserir(mesa1);
            repositorioMesa.Inserir(mesa2);
            repositorioMesa.Inserir(mesa3);

            Funcionario funcionario1 = new Funcionario("Jão", "(49) 9922-2337", "Rua das Hortaliças", "MANHÃ");
            Funcionario funcionario2 = new Funcionario("Fernanda", "(49) 9122-4447", "Rua das Flores", "TARDE");
            Funcionario funcionario3 = new Funcionario("Kaio", "(49) 9911-4437", "Rua das Frutas", "NOITE");

            repositorioFuncionario.Inserir(funcionario1);
            repositorioFuncionario.Inserir(funcionario2);
            repositorioFuncionario.Inserir(funcionario3);
        }
    }
}