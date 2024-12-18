﻿namespace Labs.API.Models
{
    public class Produto
    {
        public Produto() { }

        public Produto(int id, string nome, string marca, int quantidade) 
        {
            this.Id = id;
            this.Nome = nome;
            this.Marca = marca;
            this.Quantidade = quantidade;
        }

        public virtual int Id { get; private set; }
        public virtual string Nome { get; private set; }
        public virtual string Marca { get; private set; }
        public virtual int Quantidade { get; private set; }

        public virtual void SetId(int id) 
        { 
            this.Id = id;
        }

        public virtual void SetNome(string nome)
        {
            this.Nome = nome;
        }

        public virtual void SetMarca(string marca)
        {
            this.Marca = marca;
        }

        public virtual void SetQuantidade(int quantidade)
        {
            this.Quantidade = quantidade;
        }
    }
}
