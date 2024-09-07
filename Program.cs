using System;
using System.Collections.Generic;


interface IHabilidades
{
    int Atacar();
    int Defender();
}


abstract class Pokemon : IHabilidades
{
    private string nombre;
    private string tipo;
    private List<int> ataques;
    private int defensa;
    private Random random;

    public Pokemon(string nombre, string tipo, List<int> ataques, int defensa)
    {
        this.nombre = nombre;
        this.tipo = tipo;
        this.ataques = ataques;
        this.defensa = defensa;
        this.random = new Random();
    }

    public int Atacar()
    {
        int ataqueSeleccionado = ataques[random.Next(ataques.Count)];
        return ataqueSeleccionado * random.Next(2); // Multiplica por 0 o 1
    }

    public int Defender()
    {
        return (int)(defensa * (random.Next(2) == 0 ? 0.5 : 1)); // Multiplica por 0.5 o 1
    }

    public abstract void MostrarInfo();

    public string Nombre { get { return nombre; } }
}

// Clase que hereda de Pokemon
class PokemonEspecifico : Pokemon
{
    public PokemonEspecifico(string nombre, string tipo, List<int> ataques, int defensa) 
        : base(nombre, tipo, ataques, defensa) { }

    public override void MostrarInfo()
    {
        Console.WriteLine($"Nombre: {Nombre}");
    }
}

class Programa
{
    static Pokemon CrearPokemon()
    {
        Console.Write("Ingrese el nombre del Pokémon: ");
        string nombre = Console.ReadLine();

        Console.Write("Ingrese el tipo del Pokémon: ");
        string tipo = Console.ReadLine();

        List<int> ataques = new List<int>();
        for (int i = 0; i < 3; i++)
        {
            Console.Write($"Ingrese el puntaje del ataque {i + 1} (0-40): ");
            ataques.Add(int.Parse(Console.ReadLine()));
        }

        Console.Write("Ingrese la defensa del Pokémon (10-35): ");
        int defensa = int.Parse(Console.ReadLine());

        return new PokemonEspecifico(nombre, tipo, ataques, defensa);
    }

    static void Main()
    {
        Console.WriteLine("Creando Pokémon 1:");
        Pokemon pokemon1 = CrearPokemon();

        Console.WriteLine("\nCreando Pokémon 2:");
        Pokemon pokemon2 = CrearPokemon();

        int puntajePokemon1 = 0;
        int puntajePokemon2 = 0;

        for (int turno = 1; turno <= 3; turno++)
        {
            Console.WriteLine($"\nTurno {turno}:");

            int ataquePokemon1 = pokemon1.Atacar();
            int defensaPokemon2 = pokemon2.Defender();
            int resultadoTurno1 = Math.Max(0, ataquePokemon1 - defensaPokemon2);
            puntajePokemon1 += resultadoTurno1;

            int ataquePokemon2 = pokemon2.Atacar();
            int defensaPokemon1 = pokemon1.Defender();
            int resultadoTurno2 = Math.Max(0, ataquePokemon2 - defensaPokemon1);
            puntajePokemon2 += resultadoTurno2;

            Console.WriteLine($"{pokemon1.Nombre} ataca con {ataquePokemon1} y {pokemon2.Nombre} defiende con {defensaPokemon2}");
            Console.WriteLine($"{pokemon2.Nombre} ataca con {ataquePokemon2} y {pokemon1.Nombre} defiende con {defensaPokemon1}");
        }

        Console.WriteLine("\nResultado final:");
        Console.WriteLine($"{pokemon1.Nombre}: {puntajePokemon1} puntos");
        Console.WriteLine($"{pokemon2.Nombre}: {puntajePokemon2} puntos");

        if (puntajePokemon1 > puntajePokemon2)
            Console.WriteLine($"¡{pokemon1.Nombre} gana!");
        else if (puntajePokemon2 > puntajePokemon1)
            Console.WriteLine($"¡{pokemon2.Nombre} gana!");
        else
            Console.WriteLine("¡Es un empate!");
    }
}
