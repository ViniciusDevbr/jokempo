using System;
using System.Collections.Generic;

namespace Jokempo
{
    public enum Opcao
    {
        Pedra = 1,
        Papel = 2,
        Tesoura = 3,
    }

    public class Jogo
    {
        private List<Jogador> jogadores = new List<Jogador>();
        private Jogador? _jogadorAtual;
        private Jogador? _jogador2;

        private Jogador jogadorAtual => _jogadorAtual!;
        private Jogador jogador2 => _jogador2!;

        public Jogo()
        {
            jogadores = new List<Jogador>();
        }

        public void Iniciar()
        {
            MostrarBoasVindas();
            
            // Criar primeiro jogador
            string nome1 = LerNomeJogador(1);
            _jogadorAtual = new Jogador(nome1);
            jogadores.Add(_jogadorAtual);

            // Perguntar se quer jogar contra CPU ou outro jogador
            bool jogandoContraCpu = PerguntarContraCpu();

            if (jogandoContraCpu)
            {
                _jogador2 = new Jogador("CPU");
            }
            else
            {
                string nome2 = LerNomeJogador(2);
                _jogador2 = new Jogador(nome2);
                jogadores.Add(_jogador2);
            }

            // Loop principal do jogo
            bool continuando = true;
            while (continuando)
            {
                MostrarMenuPrincipal();
                string opcao = Console.ReadLine() ?? "";

                switch (opcao)
                {
                    case "1":
                        JogarRodada();
                        break;
                    case "2":
                        MostrarEstatisticas();
                        break;
                    case "3":
                        AdicionarSegundoJogador();
                        break;
                    case "4":
                        TrocarJogador();
                        break;
                    case "5":
                        MostrarEstatisticasGlobais();
                        break;
                    case "0":
                        continuando = false;
                        MostrarDespedida();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nOpção inválida! Tente novamente.");
                        Console.ResetColor();
                        break;
                }
            }
        }

        private void MostrarBoasVindas()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(@"
    ╔══════════════════════════════════════════════════════════════╗
    ║                                                              ║
    ║     █████╗  ██████╗ ██████╗███████╗███████╗███████╗         ║
    ║    ██╔══██╗██╔════╝██╔════╝██╔════╝██╔════╝██╔════╝         ║
    ║    ███████║██║     ██║     █████╗  ███████╗███████╗         ║
    ║    ██╔══██║██║     ██║     ██╔══╝  ╚════██║╚════██║         ║
    ║    ██║  ██║╚██████╗╚██████╗███████╗███████║███████║         ║
    ║    ╚═╝  ╚═╝ ╚═════╝ ╚═════╝╚══════╝╚══════╝╚══════╝         ║
    ║                                                              ║
    ║                    BEM-VINDO AO JOKEMPÔ!                     ║
    ║                                                              ║
    ╚══════════════════════════════════════════════════════════════╝
            ");
            Console.ResetColor();
            Console.WriteLine("\nPressione ENTER para continuar...");
            Console.ReadLine();
        }

        private string LerNomeJogador(int numero)
        {
            string nome;
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"\nDigite o nome do Jogador {numero}: ");
                nome = Console.ReadLine()?.Trim() ?? "";

                if (string.IsNullOrEmpty(nome))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Nome não pode ser vazio! Tente novamente.");
                    Console.ResetColor();
                    continue;
                }

                if (nome.Length < 2)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Nome deve ter pelo menos 2 caracteres!");
                    Console.ResetColor();
                    continue;
                }

                if (nome.Length > 20)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Nome deve ter no máximo 20 caracteres!");
                    Console.ResetColor();
                    continue;
                }

                break;
            }
            return nome;
        }

        private bool PerguntarContraCpu()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nDeseja jogar contra CPU? (S/N): ");
                string resposta = Console.ReadLine()?.Trim().ToUpper() ?? "";
                
                if (resposta == "S" || resposta == "SIM")
                    return true;
                else if (resposta == "N" || resposta == "NÃO" || resposta == "NAO")
                    return false;
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Resposta inválida! Digite S para Sim ou N para Não.");
                    Console.ResetColor();
                }
            }
        }

        private void MostrarMenuPrincipal()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\n┌────────────────────────────────────────┐");
            Console.WriteLine($"│           MENU PRINCIPAL               │");
            Console.WriteLine($"├────────────────────────────────────────┤");
            Console.WriteLine($"│  Jogador atual: {jogadorAtual.Nome.PadRight(22)}│");
            Console.WriteLine($"│  Adversário:     {jogador2.Nome.PadRight(22)}│");
            Console.WriteLine($"├────────────────────────────────────────┤");
            Console.WriteLine($"│  1 - Jogar uma rodada                  │");
            Console.WriteLine($"│  2 - Minhas estatísticas               │");
            Console.WriteLine($"│  3 - Adicionar segundo jogador         │");
            Console.WriteLine($"│  4 - Trocar de jogador                 │");
            Console.WriteLine($"│  5 - Estatísticas globais              │");
            Console.WriteLine($"│  0 - Sair do jogo                      │");
            Console.WriteLine($"└────────────────────────────────────────┘");
            Console.ResetColor();
            Console.Write("Escolha uma opção: ");
        }

        private void AdicionarSegundoJogador()
        {
            if (_jogador2 != null && _jogador2.Nome != "CPU")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nJá existe um segundo jogador cadastrado!");
                Console.ResetColor();
                return;
            }

            string nome2 = LerNomeJogador(2);
            _jogador2 = new Jogador(nome2);
            
            // Se estava jogando contra CPU, substituir
            bool cpuJaExistia = jogadores.Count > 1 && jogadores[1].Nome == "CPU";
            if (cpuJaExistia)
            {
                jogadores[1] = _jogador2;
            }
            else
            {
                jogadores.Add(_jogador2);
            }
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nJogador {nome2} adicionado com sucesso!");
            Console.ResetColor();
        }

        private void JogarRodada()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n═══════════════════════════════════════════════════");
            Console.WriteLine("              NOVA RODADA - JOKEMPÔ");
            Console.WriteLine("═══════════════════════════════════════════════════\n");
            Console.ResetColor();

            // Mostrar opções de jogada
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Escolha sua jogada:");
            Console.WriteLine("  1 - ✊ PEDRA");
            Console.WriteLine("  2 - ✋ PAPEL");
            Console.WriteLine("  3 - ✌ TESOURA");
            Console.ResetColor();

            // Ler jogada do jogador
            Opcao jogadaJogador = LerJogadaValida(jogadorAtual.Nome);
            Console.WriteLine($"\n{jogadorAtual.Nome} escolheu: {ObterEmoji(jogadaJogador)} {jogadaJogador}");

            // Jogada da CPU ou jogador 2
            Opcao jogadaAdversario;
            if (jogador2.Nome == "CPU")
            {
                Random random = new Random();
                jogadaAdversario = (Opcao)random.Next(1, 4);
                Console.WriteLine($"CPU escolheu: {ObterEmoji(jogadaAdversario)} {jogadaAdversario}");
            }
            else
            {
                Console.WriteLine($"\nAgora é a vez do {jogador2.Nome}!");
                jogadaAdversario = LerJogadaValida(jogador2.Nome);
                Console.WriteLine($"\n{jogador2.Nome} escolheu: {ObterEmoji(jogadaAdversario)} {jogadaAdversario}");
            }

            // Determinar resultado
            Console.WriteLine("\n───────────────────────────────────────────");
            DeterminarResultado(jogadaJogador, jogadaAdversario);
            Console.WriteLine("───────────────────────────────────────────\n");

            Console.WriteLine("Pressione ENTER para continuar...");
            Console.ReadLine();
        }

        private Opcao LerJogadaValida(string nomeJogador)
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"\n{nomeJogador}, digite sua escolha (1-3): ");
                string entrada = Console.ReadLine() ?? "";

                if (int.TryParse(entrada, out int escolha))
                {
                    if (escolha >= 1 && escolha <= 3)
                    {
                        Console.ResetColor();
                        return (Opcao)escolha;
                    }
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Entrada inválida! Digite 1, 2 ou 3.");
                Console.ResetColor();
            }
        }

        private string ObterEmoji(Opcao opcao)
        {
            return opcao switch
            {
                Opcao.Pedra => "✊",
                Opcao.Papel => "✋",
                Opcao.Tesoura => "✌",
                _ => "❓"
            };
        }

        private void DeterminarResultado(Opcao jogada1, Opcao jogada2)
        {
            bool jogador1Venceu = false;
            bool jogador2Venceu = false;
            bool empate = false;

            if (jogada1 == jogada2)
            {
                empate = true;
            }
            else if ((jogada1 == Opcao.Pedra && jogada2 == Opcao.Tesoura) ||
                     (jogada1 == Opcao.Papel && jogada2 == Opcao.Pedra) ||
                     (jogada1 == Opcao.Tesoura && jogada2 == Opcao.Papel))
            {
                jogador1Venceu = true;
            }
            else
            {
                jogador2Venceu = true;
            }

            if (empate)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("              🤝 EMPATE! 🤝");
                Console.WriteLine("         As duas jogadas são iguais!");
                Console.ResetColor();
                jogadorAtual.RegistrarEmpate();
                jogador2.RegistrarEmpate();
            }
            else if (jogador1Venceu)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"        🎉 {jogadorAtual.Nome} VENCEU! 🎉");
                Console.WriteLine($"    {ObterEmoji(jogada1)} {jogada1} vence {ObterEmoji(jogada2)} {jogada2}");
                Console.ResetColor();
                jogadorAtual.RegistrarVitoria();
                jogador2.RegistrarDerrota();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"        💀 {jogador2.Nome} VENCEU! 💀");
                Console.WriteLine($"    {ObterEmoji(jogada2)} {jogada2} vence {ObterEmoji(jogada1)} {jogada1}");
                Console.ResetColor();
                jogadorAtual.RegistrarDerrota();
                jogador2.RegistrarVitoria();
            }
            
            // Usar a variável para evitar warning
            _ = jogador2Venceu;
        }

        private void MostrarEstatisticas()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║              📊 ESTATÍSTICAS DO JOGADOR 📊                 ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
            Console.ResetColor();

            Console.WriteLine($"\nEstatísticas de: {jogadorAtual.Nome}");
            jogadorAtual.ExibirEstatisticas();
            
            Console.WriteLine("\nPressione ENTER para continuar...");
            Console.ReadLine();
        }

        private void TrocarJogador()
        {
            if (_jogador2 == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNão existe segundo jogador!");
                Console.ResetColor();
                return;
            }

            if (jogador2.Nome == "CPU")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNão é possível trocar com a CPU!");
                Console.WriteLine("Adicione um segundo jogador primeiro.");
                Console.ResetColor();
                return;
            }

            Console.WriteLine($"\nTrocando de {jogadorAtual.Nome} para {jogador2.Nome}...");
            
            Jogador temp = _jogadorAtual;
            _jogadorAtual = _jogador2;
            _jogador2 = temp;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Agora você é {jogadorAtual.Nome}!");
            Console.ResetColor();
        }

        private void MostrarEstatisticasGlobais()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║           📈 ESTATÍSTICAS GLOBAIS DO JOGO 📈              ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
            Console.ResetColor();

            foreach (var jogador in jogadores)
            {
                jogador.ExibirEstatisticas();
            }

            // Estatísticas combinadas
            int totalVitorias = 0, totalDerrotas = 0, totalEmpates = 0, totalPartidas = 0;
            foreach (var jogador in jogadores)
            {
                totalVitorias += jogador.Vitorias;
                totalDerrotas += jogador.Derrotas;
                totalEmpates += jogador.Empates;
                totalPartidas += jogador.Partidas;
            }

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\n╔══════════════════════════════════════════╗");
            Console.WriteLine($"║           RESUMO DO JOGO                 ║");
            Console.WriteLine($"╠══════════════════════════════════════════╣");
            Console.WriteLine($"║  Total de Partidas: {totalPartidas.ToString().PadRight(22)}║");
            Console.WriteLine($"║  Total de Vitórias: {totalVitorias.ToString().PadRight(22)}║");
            Console.WriteLine($"║  Total de Derrotas: {totalDerrotas.ToString().PadRight(22)}║");
            Console.WriteLine($"║  Total de Empates:  {totalEmpates.ToString().PadRight(22)}║");
            Console.WriteLine($"╚══════════════════════════════════════════╝");
            Console.ResetColor();
            
            Console.WriteLine("\nPressione ENTER para continuar...");
            Console.ReadLine();
        }

        private void MostrarDespedida()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(@"
    ╔══════════════════════════════════════════════════════════════╗
    ║                                                              ║
    ║           OBRIGADO POR JOGAR NOSSO JOKEMPÔ!                ║
    ║                                                              ║
    ║                       ATÉ LOGO! 👋                           ║
    ║                                                              ║
    ╚══════════════════════════════════════════════════════════════╝
            ");
            Console.ResetColor();
        }
    }
}
