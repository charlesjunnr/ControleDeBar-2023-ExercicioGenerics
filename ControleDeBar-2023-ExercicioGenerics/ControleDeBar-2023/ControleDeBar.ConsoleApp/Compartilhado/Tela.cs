using System.Collections;

namespace ControleDeBar.ConsoleApp.Compartilhado
{
    public abstract class Tela<TRepositorio, TEntidade> : ITela where TRepositorio : Repositorio<TEntidade> where TEntidade : Entidade<TEntidade> 
    {
        protected Repositorio<TEntidade> repositorioBase = null!;

        protected abstract string nomeEntidade { get; set; }

        public void MostrarCabecalho(string titulo)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            
            Console.WriteLine(" ----- " + titulo +  " ----- \n");

            Console.ResetColor();

        }

        public void MostrarMensagem(string mensagem, ConsoleColor cor)
        {
            Console.WriteLine();

            Console.ForegroundColor = cor;

            Console.WriteLine(mensagem);

            Console.ResetColor();

            Console.ReadLine();
        }

        public int ApresentarMenu()
        {
            Console.Clear();

            MostrarCabecalho($"Cadastro de {nomeEntidade}");

            Console.WriteLine($" [1] -  Inserir {nomeEntidade}");
            Console.WriteLine($" [2] -  Visualizar {nomeEntidade}");
            Console.WriteLine($" [3] -  Editar {nomeEntidade}");
            Console.WriteLine($" [4] -  Excluir {nomeEntidade}\n");

            Console.WriteLine(" [5] - Sair");

            int opcao = Convert.ToInt32(Console.ReadLine());

            return opcao;
        }

        public virtual void Cadastrar()
        {
            try
            {
                MostrarCabecalho($"Cadastro de {nomeEntidade}");

                TEntidade registro = ObterRegistro();

                if (TemErrosDeValidacao(registro))
                {
                    Cadastrar();

                    return;
                }

                repositorioBase.Inserir(registro);

                MostrarMensagem("Registro inserido com sucesso!", ConsoleColor.Green);
            }
            catch (NullReferenceException)
            {
                Console.WriteLine($"Erro ao tentar cadastrar o {nomeEntidade}.");
            }
        }
        
        public virtual bool Visualizar(bool mostrarCabecalho)
        {
            if (mostrarCabecalho)
                MostrarCabecalho($"Visualização de {nomeEntidade}");

            List<TEntidade> registros = repositorioBase.SelecionarTodos();

            if (registros.Count == 0)
            {
                MostrarMensagem("Nenhum registro cadastrado", ConsoleColor.DarkYellow);
                return false;
            }

            MostrarTabela(registros);
            Console.WriteLine();
            return true;
        }

        public virtual void Editar()
        {
            MostrarCabecalho($"Edição de {nomeEntidade}");

            if (!Visualizar(false)) return;

            Console.WriteLine();

            TEntidade registro = EncontrarRegistro("Digite o id do registro: ");

            TEntidade registroAtualizado = ObterRegistro();

            if (TemErrosDeValidacao(registroAtualizado))
            {
                Editar();

                return;
            }

            repositorioBase.Editar(registro, registroAtualizado);

            MostrarMensagem("Registro editado com sucesso!", ConsoleColor.Green);
        }

        public virtual void Excluir()
        {
            MostrarCabecalho($"Exclusão de {nomeEntidade}");

            if (!Visualizar(false)) return;

            Console.WriteLine();

            TEntidade registro = EncontrarRegistro("Digite o id do registro: ");

            repositorioBase.Excluir(registro);

            MostrarMensagem("Registro excluído com sucesso!", ConsoleColor.Green);
        }      

        public virtual TEntidade EncontrarRegistro(string textoCampo)
        {            
            bool idInvalido;
            TEntidade registroSelecionado = null!;

            do
            {
                idInvalido = false;
                Console.Write("\n" + textoCampo);
                try
                {
                    int id = Convert.ToInt32(Console.ReadLine());

                    registroSelecionado = repositorioBase.SelecionarPorId(id);

                    if (registroSelecionado == null)
                        idInvalido = true;
                }
                catch (FormatException)
                {
                    idInvalido = true;
                }

                if (idInvalido)
                    MostrarMensagem("Id inválido, tente novamente", ConsoleColor.Red);

            } while (idInvalido);

            return registroSelecionado;
        }

        protected bool TemErrosDeValidacao(TEntidade registro)
        {
            bool temErros = false;

            List<string> erros = registro.Validar();

            if (erros.Count > 0)
            {
                temErros = true;
                Console.ForegroundColor = ConsoleColor.Red;

                foreach (string erro in erros)
                {
                    Console.WriteLine(erro);
                }

                Console.ResetColor();

                Console.ReadLine();
            }

            return temErros;
        }

        protected abstract TEntidade ObterRegistro();

        protected abstract void MostrarTabela(List<TEntidade> registros);
    }
}
