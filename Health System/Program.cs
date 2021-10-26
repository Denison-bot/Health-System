using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics; //Debug.Assert()

namespace Health_System
{
    class Program
    {
        static int shots;
        static int ammoPack;
        static int damage;
        static int hp;
        static int health = 100;
        static int shield = 100;
        static int lives = 3;
        static string condition;
        static int weapon = 0; //current weapon
        static string[] weaponName = new string[3];
        static int[] ammo = new int[3];
        static int[] ammoReserves = new int[3];
        static int[] ammoCapacity = new int[3];
        static bool gameOver = false;

        static void ShowHUD()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("");
            Console.WriteLine("---------------------");
            Console.WriteLine("Shield: " + shield + " / 100");
            Console.WriteLine("Health: " + health + " / 100");
            Console.WriteLine("Lives: " + lives);
            HealthCheck();
            Console.WriteLine("Condition: " + condition);
            Console.WriteLine("---------------------");
            Console.WriteLine(weaponName[weapon] + " Ammo: " + ammo[weapon]);
            Console.WriteLine(weaponName[weapon] + " Ammo Reloads: " + ammoReserves[weapon]);
            Console.WriteLine("---------------------");
            Console.WriteLine("");
            Console.ReadKey(true);
        }

        static void InitWeapons()
        {
            // weapon capacity
            ammo[0] = 6; //revolver
            ammo[1] = 8; //repeater
            ammo[2] = 4; //shotty

            // weapon capacity
            weaponName[0] = "Revolver";
            weaponName[1] = "Repeater";
            weaponName[2] = "Shotty";
            // weapon capacity           
            ammoCapacity[0] = 6;
            ammoCapacity[1] = 8;
            ammoCapacity[2] = 4;
            // ammo reserves
            // ammo reserves are mesured in clips            
            ammoReserves[0] = 4;
            ammoReserves[1] = 3;
            ammoReserves[2] = 2;
        }

        static void Reset()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;

            ShowHUD();
            Console.WriteLine("GAME OVER");
            Console.ReadKey(true);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Reseting game");
            health = 100;
            shield = 100;
            lives = 3;
            ammo[weapon] = ammo[weapon];
            ammoReserves[weapon] = ammoReserves[weapon];
            InitWeapons();
            
            gameOver = false;
        }

        static void Respawn()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("You Died");           
            Console.WriteLine("respawn");
            health = 100;
            shield = 100;
            InitWeapons();   
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
                    if (lives == 0)
                    {
                        Reset();
                    }
                    else
                    {
                        Respawn();
                    }
                    
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
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("Health cannot be over 100hp");
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
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("Shield cannot be over 100");
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

        static int rndDmg()
        {
            Random rndDamage = new Random();
            int damage = rndDamage.Next(-10, 100);
            return damage;
        }

        static int rndHeal()
        {
            Random rndHeal = new Random();
            int hp = rndHeal.Next(-5, 15);
            return hp;
        }

        static int rndShieldRgn()
        {
            Random rndShieldRGN = new Random();
            int hp = rndShieldRGN.Next(-5, 25);
            return hp;
        }



        static void Main(string[] args)
        {
            InitWeapons();

            void Fire(int shots)
            {               
                Console.ForegroundColor = ConsoleColor.DarkMagenta;                
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
                        Reload();
                        ShowHUD();
                    }
                }    
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("Invalid: shots must be a positive int");
                }
                if (shots == 1)
                {
                    Console.WriteLine("Fired " + shots + " time");
                }
                else
                {
                    Console.WriteLine("Fired " + shots + " times");
                }
            }

            int rndFire()
            {
                Random rnd = new Random();
                int shots = rnd.Next(-2, ammo[weapon]);
                return shots;
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

           

            void PickUpAmmoPack(int ammoPack)
            {
                
                ammoReserves[weapon] = ammoReserves[weapon] + ammoPack;
                Console.ForegroundColor = ConsoleColor.DarkMagenta;

                if (ammoPack == 1)
                {
                    Console.WriteLine("Pick up " + ammoPack + " Ammo pack");
                }
                else
                {
                    Console.WriteLine("Pick up " + ammoPack + " ammo packs");
                }
            }

            int rndAmmoPack()
            {
                Random rnd = new Random();
                int ammoPack = rnd.Next(1, 4);
                return ammoPack;
            }


            void UnitTest()
            {
                Console.WriteLine("Testing max HP clamp");
                health = 100;
                Heal(20);
                Debug.Assert(health <= 100);
                
                Console.ResetColor();
                Console.WriteLine("Testing max Shield clamp");
                shield = 100;
                RegenerateShield(20);
                Debug.Assert(shield <= 100);

                Console.ResetColor();
                Console.WriteLine("Testing invalid Heal inputs");
                Heal(-200);
                Debug.Assert(health > 0);

                Console.ResetColor();
                Console.WriteLine("Testing invalid RegenerateShield inputs");
                RegenerateShield(-200);
                Debug.Assert(shield > 0);

                Console.ResetColor();
                Console.WriteLine("Testing");
            }

            UnitTest();

            Console.ReadKey(true);

            // test gameplay starts
            Console.ResetColor();
            Console.WriteLine("");
            Console.WriteLine("Game Starts");
            
            while(gameOver == false)
            {

                ShowHUD();

                shots = rndFire();
                Fire(shots);
                ShowHUD();

                damage = rndDmg();
                TakeDamage(damage);
                ShowHUD();

                ammoPack = rndAmmoPack();
                PickUpAmmoPack(ammoPack);
                ShowHUD();

                damage = rndDmg();
                TakeDamage(damage);
                ShowHUD();

                hp = rndShieldRgn();
                RegenerateShield(hp);
                ShowHUD();

                hp = rndHeal();
                Heal(hp);               

            }     
        }
    }
}
