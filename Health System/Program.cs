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
        static int weapon = 1; //current weapon

        
        static void Respawn()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("respawn");
            health = 100;
            shield = 100;
        }

        static void TakeDamage(int damage)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("Take " + damage + " damage");

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
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("Heal " + hp + " points of health");
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
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("Regen " + hp + " shield points");
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
            // weapon capacity
            int[] ammo = new int[3];
            ammo[0] = 6; //revolver
            ammo[1] = 8; //repeater
            ammo[2] = 4; //shotty
            // weapon capacity
            string[] weaponName = new string[3];
            weaponName[0] = "Revolver";
            weaponName[1] = "Repeater";
            weaponName[2] = "Shotty";
            // weapon capacity
            int[] ammoCapacity = new int[3];
            ammoCapacity[0] = 6;
            ammoCapacity[1] = 8;
            ammoCapacity[2] = 4;
            // ammo reserves
            // ammo reserves are mesured in clips
            int[] ammoReserves = new int[3];
            ammoReserves[0] = 4;
            ammoReserves[1] = 3;
            ammoReserves[2] = 2;

            void Reset()
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("Reseting game");
                health = 100;
                shield = 100;
                lives = 3;
                ammo[weapon] = ammo[weapon];
                ammoReserves[weapon] = ammoReserves[weapon];
                ShowHUD();
            }

            void Fire(int shots)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("Fire " + shots + " times");
                if (shots >= 0)
                {
                   

                    if (shots <= ammo[weapon])
                    {
                        ammo[weapon] = ammo[weapon] - shots;
                    }
                    else if (shots > ammo[weapon])
                    {
                        shots = ammo[weapon];
                        ammo[weapon] = ammo[weapon] - shots;
                    }
                    else if (ammo[weapon] <= 0)
                    {
                        ammo[weapon] = 0;
                    }
                }    
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("Invalid: shots must be a positive int");
                }
            }

            void Reload()
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("Reload weapon");
                ammoReserves[weapon] = ammoReserves[weapon] - 1;
                if (ammoReserves[weapon] <= 0)
                {
                    ammoReserves[weapon] = 0;
                }
                if (ammoReserves[weapon] <= 0)
                {
                    ammo[weapon] = 0;
                }
                else if (ammoReserves[weapon] > 0)
                {
                    ammo[weapon] = ammoCapacity[weapon];
                }
            }

            Random rnd = new Random();
            int ammoPack = rnd.Next(1, 4);

            void PickUpAmmoPack()
            {
                ammoPack = rnd.Next(1, 4);
                ammoReserves[weapon] = ammoReserves[weapon] + ammoPack;
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("Pick up " + ammoPack + " ammoReserves");
            }

            void ShowHUD()
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
                Console.WriteLine(weaponName[weapon] + " Ammo: " + ammo[weapon]);
                Console.WriteLine(weaponName[weapon] + " Ammo Reserves: " + ammoReserves[weapon]);
                Console.WriteLine("---------------------");
                Console.WriteLine("");
                Console.ReadKey(true);
            }


            // test gameplay starts 
            Console.WriteLine("Game Starts");
            ShowHUD();

            Fire(2);
            ShowHUD();

            Fire(1);
            ShowHUD();
            
            TakeDamage(60);
            ShowHUD();

            TakeDamage(80);
            ShowHUD();

            Fire(1);
            ShowHUD();

            RegenerateShield(50);
            ShowHUD();

            Fire(1);
            ShowHUD();

            TakeDamage(60);
            ShowHUD();

            Reload();
            ShowHUD();

            TakeDamage(100);
            ShowHUD();

            Respawn();
            ShowHUD();

            Fire(6);            
            ShowHUD();

            Reload();
            ShowHUD();

            PickUpAmmoPack();
            ShowHUD();

            TakeDamage(150);
            ShowHUD();

            Heal(25);
            ShowHUD();

            RegenerateShield(75);
            ShowHUD();

            Reset();

            // start de-bugging

            Fire(-2);
            ShowHUD();

            Heal(-50);
            ShowHUD();

            RegenerateShield(-50);
            ShowHUD();

            TakeDamage(-20);
            ShowHUD();

            Heal(200);
            ShowHUD();

            RegenerateShield(200);
            ShowHUD();

        }
    }
}
