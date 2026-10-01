using CozinhaIndustrial;
using ProjRestaurante;
using System.Globalization;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
var pt = CultureInfo.GetCultureInfo("pt-BR");

var restaurante = new Restaurante();

//pedi pra IA popular o array
Item[] cardapio =
{
    new Item(1,  "Prato Executivo",   28.90),
    new Item(2,  "Feijoada",          39.90),
    new Item(3,  "Salada Caesar",     24.50),
    new Item(4,  "Lasanha",           32.00),
    new Item(5,  "Porção de Batata",  22.00),
    new Item(6,  "Suco Natural",       9.00),
    new Item(7,  "Refrigerante",       6.50),
    new Item(8,  "Água",               4.00),
    new Item(9,  "Pudim",             12.00),
    new Item(10, "Café",               5.00),
};

int opcao;

do
{
    ExibirMenu();
    opcao = LerInt("Opção: ");
    Console.WriteLine();

    switch (opcao)
    {
        case 0: Console.WriteLine("Encerrando..."); break;
        case 1: CriarPedido(); break;
        case 2: AdicionarItem(); break;
        case 3: RemoverItem(); break;
        case 4: ConsultarPedido(); break;
        case 5: CancelarPedido(); break;
        case 6: ListarPedidos(); break;
        default: Console.WriteLine("Opção inválida."); break;
    }

    if (opcao != 0)
    {
        Console.WriteLine("\nPressione ´enter´ para continuar...");
        Console.ReadLine();
    }
} while (opcao != 0);


void ExibirMenu()
{
    Console.Clear();
    Console.WriteLine("0. Sair");
    Console.WriteLine("1. Criar novo pedido");
    Console.WriteLine("2. Adicionar item ao pedido");
    Console.WriteLine("3. Remover item do pedido");
    Console.WriteLine("4. Consultar pedido");
    Console.WriteLine("5. Cancelar pedido");
    Console.WriteLine("6. Listar todos os pedidos");
    Console.WriteLine();
}

void CriarPedido()
{
    string cliente = LerTexto("Nome do cliente: ");
    var pedido = new Pedido(cliente);

    if (restaurante.NovoPedido(pedido))
        Console.WriteLine($"Pedido criado com sucesso. Número do pedido: {pedido.Id}");
    else
        Console.WriteLine($"Limite de {Restaurante.MaxPedidos} pedidos atingido. Não é possível fazer novos pedidos.");
}

void AdicionarItem()
{
    Pedido? pedido = BuscarPedido();
    if (pedido == null) return;

    ExibirCardapio();
    Item? item = BuscarItemNoCardapio();
    if (item == null) return;

    if (pedido.AdicionarItem(item))
        Console.WriteLine($"'{item.Descricao}' adicionado ao pedido {pedido.Id}.");
    else
        Console.WriteLine($"O pedido já tem o máximo de {Pedido.MaxItens} itens.");
}

void RemoverItem()
{
    Pedido? pedido = BuscarPedido();
    if (pedido == null) return;

    Console.WriteLine();
    Console.WriteLine(pedido.DadosDoPedido());
    Console.WriteLine();

    Item? item = BuscarItemNoCardapio();
    if (item == null) return;

    if (pedido.RemoverItem(item))
        Console.WriteLine($"'{item.Descricao}' removido do pedido {pedido.Id}.");
    else
        Console.WriteLine("Esse item não está no pedido.");
}

void ConsultarPedido()
{
    Pedido? pedido = BuscarPedido();
    if (pedido == null) return;

    Console.WriteLine();
    Console.WriteLine(pedido.DadosDoPedido());
}

void CancelarPedido()
{
    Pedido? pedido = BuscarPedido();
    if (pedido == null) return;

    if (restaurante.CancelarPedido(pedido))
        Console.WriteLine($"Pedido {pedido.Id} cancelado.");
    else
        Console.WriteLine("Nao foi possível cancelar o pedido.");
}

void ListarPedidos()
{
    Console.WriteLine(restaurante.ListarPedidos());
}

void ExibirCardapio()
{
    Console.WriteLine("Cardapio: ");
    foreach (Item i in cardapio)
        Console.WriteLine($"{i.Id,2} - {i.Descricao,-20} {i.Preco.ToString("C2", pt),12}");
    Console.WriteLine();
}

Pedido? BuscarPedido()
{
    int id = LerInt("Número do pedido: ");
    Pedido? pedido = restaurante.BuscarPedido(new Pedido(id));

    if (pedido == null)
        Console.WriteLine("Pedido não encontrado.");

    return pedido;
}

Item? BuscarItemNoCardapio()
{
    int id = LerInt("Código do item: ");
    Item? item = cardapio.FirstOrDefault(i => i.Id == id);

    if (item == null)
        Console.WriteLine("Item nao encontrado no cardápio.");

    return item;
}

int LerInt(string mensagem)
{
    while (true)
    {
        Console.Write(mensagem);
        if (int.TryParse(Console.ReadLine(), out int valor))
            return valor;

        Console.WriteLine("Valor inválido. Digite um número inteiro.");
    }
}

string LerTexto(string mensagem)
{
    while (true)
    {
        Console.Write(mensagem);
        string? texto = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(texto))
            return texto.Trim();

        Console.WriteLine("Campo obrigatório.");
    }
}