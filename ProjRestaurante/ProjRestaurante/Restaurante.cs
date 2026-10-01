using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjRestaurante
{
    public class Restaurante
    {
        public const int MaxPedidos = 50;

        private int proxPedido;
        private Pedido?[] pedidos;

        public Restaurante()
        {
            proxPedido = 1;
            pedidos = new Pedido?[MaxPedidos];
        }

        public bool NovoPedido(Pedido pedido)
        {
            for (int i = 0; i < pedidos.Length; i++)
            {
                if (pedidos[i] == null)
                {
                    pedido.Id = proxPedido++;
                    pedidos[i] = pedido;
                    return true;
                }
            }
            return false;
        }

        public Pedido? BuscarPedido(Pedido pedido)
        {
            foreach (Pedido? p in pedidos)
            {
                if (p != null && p.Id == pedido.Id)
                    return p;
            }
            return null;
        }

        public bool CancelarPedido(Pedido pedido)
        {
            for (int i = 0; i < pedidos.Length; i++)
            {
                if (pedidos[i] != null && pedidos[i]!.Id == pedido.Id)
                {
                    pedidos[i] = null;
                    return true;
                }
            }
            return false;
        }

        public double TotalGeral()
        {
            double soma = 0;
            foreach (Pedido? p in pedidos)
            {
                if (p != null)
                    soma += p.CalcularTotal();
            }
            return soma;
        }

        public string ListarPedidos()
        {
            var pt = CultureInfo.GetCultureInfo("pt-BR");
            var sb = new StringBuilder();
            int qtde = 0;

            foreach (Pedido? p in pedidos)
            {
                if (p == null) continue;
                qtde++;
                sb.AppendLine($"Pedido {p.Id,3} | {p.Cliente,-20} | {p.CalcularTotal().ToString("C2", pt),12}");
            }

            if (qtde == 0)
                sb.AppendLine("(nenhum pedido registrado)");

            sb.AppendLine(new string('-', 45));
            sb.Append($"Pedidos: {qtde}  |  Soma geral do dia: {TotalGeral().ToString("C2", pt)}");
            return sb.ToString();
        }
    }
}
