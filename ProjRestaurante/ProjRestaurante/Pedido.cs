using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjRestaurante
{
    public class Pedido
    {
        public const int MaxItens = 10;

        private int id;
        private string cliente;
        private Item?[] itens;

        public Pedido(string cliente) 
        {
            this.cliente = cliente;
            itens = new Item?[MaxItens];
        }

        public Pedido(int id) : this("")
        {
            this.id = id;
        }

        public int Id
        {
            get => id;
            set => id = value;
        }

        public string Cliente => cliente;
        public bool AdicionarItem(Item item)
        {
            for (int i = 0; i < itens.Length; i++)
            {
                if (itens[i] == null)
                {
                    itens[i] = item;
                    return true;
                }
            }
            return false;
        }

        public bool RemoverItem(Item item)
        {
            for (int i = 0; i < itens.Length; i++)
            {
                if (itens[i] != null && itens[i]!.Id == item.Id)
                {
                    itens[i] = null;
                    return true;
                }
            }
            return false;
        }

        public double CalcularTotal()
        {
            double total = 0;
            foreach (Item? item in itens)
            {
                if (item != null)
                    total += item.Preco;
            }
            return total;
        }
        public string DadosDoPedido()
        {
            var pt = CultureInfo.GetCultureInfo("pt-BR");
            var sb = new StringBuilder();

            sb.AppendLine($"Pedido numero: {id}");
            sb.AppendLine($"Cliente: {cliente}");
            sb.AppendLine("Itens:");

            int qtde = 0;
            foreach (Item? item in itens)
            {
                if (item == null) continue;
                qtde++;
                sb.AppendLine($"  {item.Id,2} - {item.Descricao,-20} {item.Preco.ToString("C2", pt),12}");
            }

            if (qtde == 0)
                sb.AppendLine(" (nenhum item)");

            sb.Append($"Total: {CalcularTotal().ToString("C2", pt)}");
            return sb.ToString();
        }

    }
}
