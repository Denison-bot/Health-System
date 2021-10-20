using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health_System
{
    class Program
    {
        static int health = 100;
        static int shield = 100;
        static int lives = 3;
        static string condition;

        static void ShowHUD()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("");
            Console.WriteLine("---------------------");
            Console.WriteLine("Health: " + health + " / 100");
            Console.WriteLine("Shield: " + shield + " / 100");
            Console.WriteLine("Lives: " + lives);
            HealthCheck();
            Console.WriteLine("Condition: " + condition);
            Console.WriteLine("---------------------");
            Console.WriteLine("");
        }

        static void TakeDamage(int damage)
        {
            if (damage >= 0)
            {
                shield = shield - damage;

                if (shield <= 0)
                {
                    health = health + shield;
                }
                if (shield <= 0)
                {
                    shield = 0;
                }
                if (health <= 0)
                {
                    lives = lives - 1;
                    health = 0;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Invalid: damage must be a positive int");
            }
        }
        
        static void Heal(int hp)
        {
            if (hp >= 0)
            {

                health = health + hp;
                if (health >= 100)
                {
                    health = 100;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Invalid: hp must be a positive int");
            }
        }

        static void RegenerateShield(int hp)
        {
            if (hp >= 0)
            {
                shield = shield + hp;
                if (shield >= 100)
                {
                    shield = 100;
                }
               
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Invalid: hp must be a positive int");
            }



        }

        static void HealthCheck()
        {
            if (health == 100)
            {
                condition = "Unscathed";
            }
            else if (health <= 99 && health >= 75)
            {
                condition = "Scathed";
            }
            else if (health <= 74 && health >= 50)
            {
                condition = "Battered";
            }
            else if (health <= 49 && health >= 25)
            {
                condition = "Bleeding out";
            }
            else if (health <= 24 && health >= 1)
            {
                condition = "Blacking Out";
            }
            else if (health == 0)
            {
                condition = "DEAD";
            }
        }

        static void Main(string[] args)
        {
            ShowHUD();
            
            TakeDamage(75);
            ShowHUD();
            
            TakeDamage(100);
            ShowHUD();
           
            Heal(200);
            ShowHUD();

            RegenerateShield(200);
            ShowHUD();

            TakeDamage(100);
            ShowHUD();

            Console.ReadKey(true);
        }
    }
}
