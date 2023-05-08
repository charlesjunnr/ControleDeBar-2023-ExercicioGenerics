using System.Collections;

namespace ControleDeBar.ConsoleApp.Compartilhado
{
    public class Repositorio<TEntidade> where TEntidade : Entidade<TEntidade>
    {
        
        protected List<TEntidade> listaRegistros;

        protected int contadorRegistros = 0;

        public Repositorio(List<TEntidade> listaRegistros)
        {
            this.listaRegistros = listaRegistros;
        }

        public virtual void Inserir(TEntidade registro) 
        {
            contadorRegistros++;

            registro.id = contadorRegistros;

            listaRegistros.Add(registro);
        }

        public virtual void Editar(int id, TEntidade registroAtualizado)
        {
            TEntidade registroSelecionado = SelecionarPorId(id);

            registroSelecionado.AtualizarInformacoes(registroAtualizado);
        }

        public virtual void Editar(TEntidade registroSelecionado, TEntidade registroAtualizado)
        {
            registroSelecionado.AtualizarInformacoes(registroAtualizado);
        }

        public virtual void Excluir(int id)
        {
            TEntidade registroSelecionado = (TEntidade)SelecionarPorId(id);

            listaRegistros.Remove(registroSelecionado);
        }

        public virtual void Excluir(TEntidade registroSelecionado)
        {
            listaRegistros.Remove(registroSelecionado);
        }

        public virtual TEntidade SelecionarPorId(int id)
        {
            return listaRegistros.Find(e => id.Equals(id))!;
        }

        public virtual List<TEntidade> SelecionarTodos()
        {
            return listaRegistros;
        }

        public bool TemRegistros()
        {
            return listaRegistros.Count > 0;
        }
    }
}
