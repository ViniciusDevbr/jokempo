using System;

namespace Jokempo
{
    public class Jogador
    {
        public string Nome { get; set; }
        public int Vitorias { get; set; }
        public int Derrotas { get; set; }
        public int Empates { get; set; }
        public int Partidas { get; set; }

        public Jogador(string nome)
        {
            Nome = nome;
            Vitorias = 0;
            Derrotas = 0;
            Empates = 0;
            Partidas = 0;
        }

        public void RegistrarVitoria()
        {
            Vitorias++;
            Partidas++;
        }

        public void RegistrarDerrota()
        {
            Derrotas++;
            Partidas++;
        }

        public void RegistrarEmpate()
        {
            Empates++;
            Partidas++;
        }

        public double CalcularPorcentagemVitorias()
        {
            if (Partidas == 0) return 0;
            return (double)Vitorias / Partidas * 100;
        }

        public void ExibirEstatisticas()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n╔══════════════════════════════════════════╗");
            Console.WriteLine($"║     ESTATÍSTICAS DE {Nome.ToUpper().PadRight(20)}║");
            Console.WriteLine($"╠══════════════════════════════════════════╣");
            Console.WriteLine($"║  Partidas Jogadas: {Partidas.ToString().PadRight(22)}║");
            Console.WriteLine($"║  Vitórias:         {Vitorias.ToString().PadRight(22)}║");
            Console.WriteLine($"║  Derrotas:         {Derrotas.ToString().PadRight(22)}║");
            Console.WriteLine($"║  Empates:          {Empates.ToString().PadRight(22)}║");
            Console.WriteLine($"║  % de Vitórias:    {CalcularPorcentagemVitorias():F1}%".PadRight(24) + "║");
            Console.WriteLine($"╚══════════════════════════════════════════╝");
            Console.ResetColor();
        }
    }
}
