using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjRestaurante
{
    public class Item
    {
        private int id;
        private string descricao;
        private double preco;

        public Item(int id, string descricao, double preco) 
        {
            this.id = id;
            this.Descricao = descricao;
            this.preco = preco;
        }

        public Item(int id) : this(id, "", 0) { }


        public int Id => id;
        public string Descricao => descricao;
        public double Preco => preco;

    }

}
