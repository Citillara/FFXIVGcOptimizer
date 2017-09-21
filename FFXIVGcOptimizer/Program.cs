using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVGcOptimizer
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Character> chars = new List<Character>();
            List<Offset> offsets = new List<Offset>();
            File.ReadAllLines("characters.txt").ToList().ForEach(l => chars.Add(new Character(l)));
            File.ReadAllLines("offsets.txt").ToList().ForEach(l => offsets.Add(new Offset(l)));
            int tank, heal, damage;

            while (true)
            {
                Console.WriteLine("Tank ?");
                tank = int.Parse(Console.ReadLine());

                Console.WriteLine("Heal ?");
                heal = int.Parse(Console.ReadLine());

                Console.WriteLine("Damage ?");
                damage = int.Parse(Console.ReadLine());

                var m = new Mission(tank, heal, damage, chars.ToArray(), offsets.ToArray());
                m.PrintSolution();

                Console.ReadKey();
            }
        }
    }



    class Mission
    {
        public Offset[] Offsets;
        public Character[] Characters;
        public int Tank;
        public int Heal;
        public int Damage;

        public Mission(int tank, int heal, int damage, Character[] characters, Offset[] offsets)
        {
            Tank = tank;
            Heal = heal;
            Damage = damage;
            Characters = characters;
            Offsets = offsets;
        }

        public void PrintSolution()
        {
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            Character[] solution = null;
            Offset osolution = null;
            int diff = int.MaxValue;

            int tank = 0, heal = 0, damage = 0, complete = 0, tempdiff = 0;

            int length = Characters.Length;

            for (int o = 0; o < Offsets.Length; o++)
            {
                for (int a = 0; a < length; a++)
                {
                    Characters[a].Selected = true;
                    for (int b = 0; b < length; b++)
                    {
                        if (Characters[b].Selected)
                            continue;
                        Characters[b].Selected = true;

                        for (int c = 0; c < length; c++)
                        {
                            if (Characters[c].Selected)
                                continue;
                            Characters[c].Selected = true;

                            for (int d = 0; d < length; d++)
                            {

                                if (Characters[d].Selected)
                                    continue;
                                Characters[d].Selected = true;

                                tank = Characters[a].Tank + Characters[b].Tank + Characters[c].Tank + Characters[d].Tank + Offsets[o].Tank;
                                heal = Characters[a].Heal + Characters[b].Heal + Characters[c].Heal + Characters[d].Heal + Offsets[o].Heal;
                                damage = Characters[a].Damage + Characters[b].Damage + Characters[c].Damage + Characters[d].Damage + Offsets[o].Damage;

                                complete = 0;
                                tempdiff = 0;

                                if (tank >= Tank)
                                    complete++;
                                else
                                    tempdiff += (Tank - tank);

                                if (heal >= Heal)
                                    complete++;
                                else
                                    tempdiff += (Heal - heal);

                                if (damage >= Damage)
                                    complete++;
                                else
                                    tempdiff += (Damage - damage);

                                if (complete == 3)
                                {
                                    watch.Stop();
                                    Console.WriteLine("Solution found ! " + watch.Elapsed.TotalMilliseconds + "ms");

                                    Console.WriteLine(Characters[a].Name);
                                    Console.WriteLine("{0}, {1}, {2}", Characters[a].Tank, Characters[a].Heal, Characters[a].Damage);
                                    Console.WriteLine(Characters[b].Name);
                                    Console.WriteLine("{0}, {1}, {2}", Characters[b].Tank, Characters[b].Heal, Characters[b].Damage);
                                    Console.WriteLine(Characters[c].Name);
                                    Console.WriteLine("{0}, {1}, {2}", Characters[c].Tank, Characters[c].Heal, Characters[c].Damage);
                                    Console.WriteLine(Characters[d].Name);
                                    Console.WriteLine("{0}, {1}, {2}", Characters[d].Tank, Characters[d].Heal, Characters[d].Damage);
                                    Console.WriteLine(Offsets[o].Name);
                                    Console.WriteLine("{0}, {1}, {2}", Offsets[o].Tank, Offsets[o].Heal, Offsets[o].Damage);

                                    Console.WriteLine("");
                                    Console.WriteLine("Total : {0}, {1}, {2}", tank, heal, damage);

                                    return;
                                }

                                if (diff > tempdiff)
                                {
                                    solution = new Character[] { Characters[a], Characters[b], Characters[c], Characters[d] };
                                    diff = tempdiff;
                                    osolution = Offsets[o];
                                }

                                Characters[d].Selected = false;
                            }
                            Characters[c].Selected = false;
                        }
                        Characters[b].Selected = false;
                    }
                    Characters[a].Selected = false;
                }
            }
            watch.Stop();
            Console.WriteLine("Partial solution found ! " + watch.Elapsed.TotalMilliseconds + "ms");


            Console.WriteLine(solution[0].Name);
            Console.WriteLine("{0}, {1}, {2}", solution[0].Tank, solution[0].Heal, solution[0].Damage);
            Console.WriteLine(solution[1].Name);
            Console.WriteLine("{0}, {1}, {2}", solution[1].Tank, solution[1].Heal, solution[1].Damage);
            Console.WriteLine(solution[2].Name);
            Console.WriteLine("{0}, {1}, {2}", solution[2].Tank, solution[2].Heal, solution[2].Damage);
            Console.WriteLine(solution[3].Name);
            Console.WriteLine("{0}, {1}, {2}", solution[3].Tank, solution[3].Heal, solution[3].Damage);
            Console.WriteLine(osolution.Name);
            Console.WriteLine("{0}, {1}, {2}", osolution.Tank, osolution.Heal, osolution.Damage);

            Console.WriteLine("");
            Console.WriteLine("Total : {0}, {1}, {2}", tank, heal, damage);

            Console.WriteLine();
        }
        
    }



    class Character
    {
        public string Name;
        public int Tank;
        public int Heal;
        public int Damage;
        public bool Selected;

        public Character(string input)
        {
            var split = input.Split(new char[] { ',' });

            Name = split[0];
            Tank = int.Parse(split[1]);
            Heal = int.Parse(split[2]);
            Damage = int.Parse(split[3]);
        }
    }

    class Offset
    {
        public string Name;
        public int Tank;
        public int Heal;
        public int Damage;

        public Offset(string input)
        {
            var split = input.Split(new char[] { ',' });

            Name = split[0];
            Tank = int.Parse(split[1]);
            Heal = int.Parse(split[2]);
            Damage = int.Parse(split[3]);
        }
    }
}
