using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int me = 1; // starting number
            int moveCount = 0;
            int numOfTargets = 0;
            int newTargetAt = 0;
            bool gameover = false;
            int[] target = new int[1000000];
            int[] targetX = new int[1000000];
            int[] targetY = new int[1000000];
            int[] newTargetX = { 38, 44, 50, 56, 62, 68, 74 }; // possible target x coordinates
            int[] newTargetY = { 3, 7, 11, 15, 19 }; // possible target y coordinates
            int[] targetOptions = { 1, 2, 4, 8, 16, 32 }; // all possible target options
            int score = 0;
            int runs = 0;
            int level = 1;
            Console.WindowHeight = 25;
            Console.WindowWidth = 87;
            Console.CursorVisible = false;
            Random r = new Random();
            int randomX = r.Next(7);
            int randomY = r.Next(5);
            int meX = newTargetX[randomX];
            int meY = newTargetY[randomY];
            while (!gameover)
            {
                ConsoleKey k = GetKeyStroke();
                Move(k, ref meX, ref meY, ref moveCount);
                if (runs == 0) // display menu and instructions at beginning of game
                {
                    Console.Clear();
                    bool a = false;
                    while (!a)
                    {
                        k = GetKeyStroke();
                        if (k == ConsoleKey.LeftArrow || k == ConsoleKey.NoName)
                        {
                            do
                            {
                                k = GetKeyStroke();
                                MenuPlay();
                                if (k == ConsoleKey.Enter)
                                {
                                    Console.Clear();
                                    me = 1;
                                    a = true;
                                    break;
                                }
                            } while (k != ConsoleKey.RightArrow);
                        }
                        if (k == ConsoleKey.RightArrow)
                        {
                            do
                            {
                                k = GetKeyStroke();
                                MenuExit();
                                if (k == ConsoleKey.Enter)
                                {
                                    Environment.Exit(0);
                                }
                            } while (k != ConsoleKey.LeftArrow);
                        }
                    }
                    Instructions();
                    Console.Clear();
                    runs++;
                }
                Console.SetCursorPosition(meX, meY); // begin at random location of board
                Console.ForegroundColor = ConsoleColor.Red; // character colour 
                Console.Write(me);
                Console.ResetColor();
                MyDraw(score, level);
                DrawTarget(targetX, targetY, target, numOfTargets);
                if (moveCount == newTargetAt) // create target everytime character moves in a valid direction
                {
                    MakeTarget(targetX, targetY, target, newTargetX, newTargetY, targetOptions, ref numOfTargets, ref newTargetAt, meX, meY);
                }
                int i = 0;
                do // check to see if character moves into a cell with a target
                {
                    if ((meX == targetX[i] && meY == targetY[i]) && (me == target[i])) // if character moves into a cell with target that is equal to the character
                    {
                        targetX[i] = 0;
                        targetY[i] = 0;
                        score += target[i];
                        target[i] = 0;
                        me *= 2; // multiply character value
                    }
                    if ((meX == targetX[i] && meY == targetY[i]) && (me > target[i])) // if character moves into a cell with target that is less that the character 
                    {
                        targetX[i] = 0;
                        targetY[i] = 0;
                        score += target[i];
                        me = target[i]; // demote down to target that character moved into
                        target[i] = 0;
                    }
                    if ((meX == targetX[i] && meY == targetY[i]) && (me < target[i])) // if character moves into a cell with target that is greater than the character
                    {
                        Console.Clear();
                        bool a = false;
                        while (!a)
                        {
                            k = GetKeyStroke();
                            if (k == ConsoleKey.LeftArrow || k == ConsoleKey.NoName) // highlight option to play again
                            {
                                do
                                {
                                    k = GetKeyStroke();
                                    GameoverScreenYes();
                                    if (k == ConsoleKey.Enter)
                                    {
                                        Console.Clear();
                                        Main(null);
                                        break;
                                    }
                                } while (k != ConsoleKey.RightArrow);
                            }
                            if (k == ConsoleKey.RightArrow) // highlight option to exit
                            {
                                do
                                {
                                    k = GetKeyStroke();
                                    GameoverScreenNo();
                                    if (k == ConsoleKey.Enter)
                                    {
                                        Environment.Exit(0);
                                    }
                                } while (k != ConsoleKey.LeftArrow);
                            }
                        }
                    }
                    i++;
                } while (i < numOfTargets);
                if (k == ConsoleKey.Escape) // exit game if user presses escape
                {
                    gameover = true;
                }
                if (me == 64) // win if character reaches 64
                {
                    Console.Clear();
                    bool a = false;
                    while (!a)
                    {
                        k = GetKeyStroke();
                        if (k == ConsoleKey.LeftArrow || k == ConsoleKey.NoName) // highlight continue playing
                        {
                            do
                            {
                                k = GetKeyStroke();
                                WinScreenYes();
                                if (k == ConsoleKey.Enter)
                                {
                                    Console.Clear();
                                    me = 1;
                                    a = true;
                                    level++; // increase level
                                    break;
                                }
                            } while (k != ConsoleKey.RightArrow);
                        }
                        if (k == ConsoleKey.RightArrow) // highlight exit
                        {
                            do
                            {
                                k = GetKeyStroke();
                                WinScreenNo();
                                if (k == ConsoleKey.Enter)
                                {
                                    Console.Clear();
                                    Main(null);
                                }
                            } while (k != ConsoleKey.LeftArrow);
                        }
                    }
                }
            }
        }
        static ConsoleKey GetKeyStroke()
        {
            ConsoleKey userInput = ConsoleKey.NoName; // reset key
            if (Console.KeyAvailable)
            {
                userInput = Console.ReadKey(true).Key; // read key that is being pressed
            }
            return userInput;
        }
        static void Move(ConsoleKey dk, ref int dmeX, ref int dmeY, ref int dmoveCount)
        {
            if (dk == ConsoleKey.UpArrow && dmeY != 3) // move character up if up arrow is pressed
            {
                Console.Clear();
                dmeY -= 4;
                dmoveCount++;
            }
            if (dk == ConsoleKey.DownArrow && dmeY != 19) // move character down if down arrow is pressed
            {
                Console.Clear();
                dmeY += 4;
                dmoveCount++;
            }
            if (dk == ConsoleKey.RightArrow && dmeX != 74) // move character to the right if right arrow is pressed
            {
                Console.Clear();
                dmeX += 6;
                dmoveCount++;
            }
            if (dk == ConsoleKey.LeftArrow && dmeX != 38) // move character to the left if the left arrow is pressed
            {
                Console.Clear();
                dmeX -= 6;
                dmoveCount++;
            }
        }
        static void MyDraw(int dscore, int dlevel) // draw game screen 
        {
            int i = 0;
            Console.SetCursorPosition(1, 2);
            Console.WriteLine("    ██████╗ ██╗  ██╗");
            Console.WriteLine("    ██╔════╝ ██║  ██║");
            Console.WriteLine("    ███████╗ ███████║");
            Console.WriteLine("    ██╔═══██╗╚════██║");
            Console.WriteLine("    ╚██████╔╝     ██║");
            Console.WriteLine("     ╚═════╝      ╚═╝");
            Console.SetCursorPosition(3, 18);
            Console.WriteLine("Score: {0}", dscore); // display score
            Console.SetCursorPosition(3, 20);
            Console.WriteLine("Level: {0}", dlevel); // display level 
            int topX = 35; // Start of top of box
            int topY = 1;
            Console.SetCursorPosition(topX, topY);
            do
            {
                Console.Write("▄");
                i++;
            } while (i < 43); // End of box top
            i = 0;
            int leftX = 35; // Start of left of Box
            int leftY = 2;
            Console.SetCursorPosition(leftX, leftY);
            do
            {
                Console.Write("▌");
                leftY++;
                Console.SetCursorPosition(leftX, leftY);
                i++;
            } while (i < 19); // End of left of box
            i = 0;
            int bottomX = 35; // Start of bottom of Box
            int bottomY = 21;
            Console.SetCursorPosition(bottomX, bottomY);
            do
            {
                Console.Write("▀");
                i++;
            } while (i < 43); // End of bottom of box
            i = 0;
            int rightX = 77; // Start of right of Box
            int rightY = 2;
            Console.SetCursorPosition(rightX, rightY);
            do
            {
                Console.Write("▐");
                rightY++;
                Console.SetCursorPosition(rightX, rightY);
                i++;
            } while (i < 19); // End of right of box
            i = 0;
            int v1X = 41; // Start of vertical line 1
            int v1Y = 2;
            Console.SetCursorPosition(v1X, v1Y);
            do
            {
                Console.Write("║");
                v1Y++;
                Console.SetCursorPosition(v1X, v1Y);
                i++;
            } while (i < 19); // End of vertical line 1
            i = 0;
            int v2X = 47; // Start of vertical line 2
            int v2Y = 2;
            Console.SetCursorPosition(v2X, v2Y);
            do
            {
                Console.Write("║");
                v2Y++;
                Console.SetCursorPosition(v2X, v2Y);
                i++;
            } while (i < 19); // End of vertical line 2
            i = 0;
            int v3X = 53; // Start of vertical line 3
            int v3Y = 2;
            Console.SetCursorPosition(v3X, v3Y);
            do
            {
                Console.Write("║");
                v3Y++;
                Console.SetCursorPosition(v3X, v3Y);
                i++;
            } while (i < 19); // End of vertical line 3
            i = 0;
            int v4X = 59; // Start of vertical line 4
            int v4Y = 2;
            Console.SetCursorPosition(v4X, v4Y);
            do
            {
                Console.Write("║");
                v4Y++;
                Console.SetCursorPosition(v4X, v4Y);
                i++;
            } while (i < 19); // End of vertical line 4
            i = 0;
            int v5X = 65; // Start of vertical line 5
            int v5Y = 2;
            Console.SetCursorPosition(v5X, v5Y);
            do
            {
                Console.Write("║");
                v5Y++;
                Console.SetCursorPosition(v5X, v5Y);
                i++;
            } while (i < 19); // End of vertical line 5
            i = 0;
            int v6X = 71; // Start of vertical line 6
            int v6Y = 2;
            Console.SetCursorPosition(v6X, v6Y);
            do
            {
                Console.Write("║");
                v6Y++;
                Console.SetCursorPosition(v6X, v6Y);
                i++;
            } while (i < 19); // End of vertical line 6
            int j = 0;
            int h1X = 36; // Start of horizontal line 1
            int h1Y = 5;
            do
            {
                i = 0;
                Console.SetCursorPosition(h1X, h1Y);
                do
                {
                    Console.Write("═");
                    i++;
                } while (i < 5);
                h1X += 6;
                j++;
            } while (j < 7); // End of horizontal line 1
            j = 0;
            int h2X = 36; // Start of horizontal line 2
            int h2Y = 9;
            do
            {
                i = 0;
                Console.SetCursorPosition(h2X, h2Y);
                do
                {
                    Console.Write("═");
                    i++;
                } while (i < 5);
                h2X += 6;
                j++;
            } while (j < 7); // End of horizontal line 2
            j = 0;
            int h3X = 36; // Start of horizontal line 3
            int h3Y = 13;
            do
            {
                i = 0;
                Console.SetCursorPosition(h3X, h3Y);
                do
                {
                    Console.Write("═");
                    i++;
                } while (i < 5);
                h3X += 6;
                j++;
            } while (j < 7); // End of horizontal line 3
            j = 0;
            int h4X = 36; // Start of horizontal line 4
            int h4Y = 17;
            do
            {
                i = 0;
                Console.SetCursorPosition(h4X, h4Y);
                do
                {
                    Console.Write("═");
                    i++;
                } while (i < 5);
                h4X += 6;
                j++;
            } while (j < 7); // End of horizontal line 4
        }
        static void MakeTarget(int[] dtargetX, int[] dtargetY, int[] dtarget, int[] dnewTargetX, int[] dnewTargetY, int[] dtargetOptions, ref int dnumOfTargets, ref int dnewTargetAt, int dmeX, int dmeY)
        {
            int randomX;
            int randomY;
            int randomTarget;
            bool goodCoords;
            if (dnumOfTargets == 0)
            {
                Random r = new Random();
                randomX = r.Next(7);
                randomY = r.Next(5);
                randomTarget = r.Next(6);
                dtargetX[dnumOfTargets] = dnewTargetX[randomX];
                dtargetY[dnumOfTargets] = dnewTargetY[randomY];
                dtarget[dnumOfTargets] = dtargetOptions[randomTarget];
                dnumOfTargets++;
                dnewTargetAt++;
            }
            else
            {
                do
                {
                    Random r = new Random();
                    randomX = r.Next(7);
                    randomY = r.Next(5);
                    randomTarget = r.Next(6);
                    dtargetX[dnumOfTargets] = dnewTargetX[randomX];
                    dtargetY[dnumOfTargets] = dnewTargetY[randomY];
                    dtarget[dnumOfTargets] = dtargetOptions[randomTarget];
                    int i = 0;
                    do // make sure that new target is being made in an open cell 
                    {
                        if ((dtargetX[dnumOfTargets] == dtargetX[i] && dtargetY[dnumOfTargets] == dtargetY[i]) || (dtargetX[dnumOfTargets] == dmeX && dtargetY[dnumOfTargets] == dmeY))
                        {
                            goodCoords = false;
                            break;
                        }
                        else
                        {
                            goodCoords = true;
                        }
                        i++;
                    } while (i < dnumOfTargets);
                } while (!goodCoords);
                dnumOfTargets++;
                dnewTargetAt++;
            }
        }
        static void DrawTarget(int[] dtargetX, int[] dtargetY, int[] dtarget, int dnumOfTargets)
        {
            int i = 0;
            do
            {
                if (dtarget[i] == 0) // hide all targets equal to 0
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.SetCursorPosition(dtargetX[i], dtargetY[i]);
                    Console.Write(dtarget[i]);
                    Console.ResetColor();
                }
                if (dtarget[i] == 1)
                {
                    Console.SetCursorPosition(dtargetX[i], dtargetY[i]);
                    Console.ForegroundColor = ConsoleColor.White; // make all 1 targets white
                    Console.Write(dtarget[i]);
                    Console.ResetColor();
                }
                if (dtarget[i] == 2)
                {
                    Console.SetCursorPosition(dtargetX[i], dtargetY[i]);
                    Console.ForegroundColor = ConsoleColor.Yellow; // make all 2 targets yellow
                    Console.Write(dtarget[i]);
                    Console.ResetColor();
                }
                if (dtarget[i] == 4)
                {
                    Console.SetCursorPosition(dtargetX[i], dtargetY[i]);
                    Console.ForegroundColor = ConsoleColor.DarkYellow; // make all 4 targets dark yellow
                    Console.Write(dtarget[i]);
                    Console.ResetColor();
                }
                if (dtarget[i] == 8)
                {
                    Console.SetCursorPosition(dtargetX[i], dtargetY[i]);
                    Console.ForegroundColor = ConsoleColor.Green; // make all 8 targets green
                    Console.Write(dtarget[i]);
                    Console.ResetColor();
                }
                if (dtarget[i] == 16)
                {
                    Console.SetCursorPosition(dtargetX[i], dtargetY[i]);
                    Console.ForegroundColor = ConsoleColor.DarkGreen; // make all 16 targets dark green
                    Console.Write(dtarget[i]);
                    Console.ResetColor();
                }
                if (dtarget[i] == 32)
                {
                    Console.SetCursorPosition(dtargetX[i], dtargetY[i]);
                    Console.ForegroundColor = ConsoleColor.Cyan; // make all 32 targets cyan
                    Console.Write(dtarget[i]);
                    Console.ResetColor();
                }
                if (dtarget[i] == 64)
                {
                    Console.SetCursorPosition(dtargetX[i], dtargetY[i]);
                    Console.ForegroundColor = ConsoleColor.DarkCyan; // make all 64 targets dark cyan
                    Console.Write(dtarget[i]);
                    Console.ResetColor();
                }
                i++;
            } while (i < dnumOfTargets); // ensures all created targets are drawn
        }
        static void MenuPlay()
        {
            Console.SetCursorPosition(36, 8);
            Console.Write("██████╗ ██╗  ██╗");
            Console.SetCursorPosition(35, 9);
            Console.Write("██╔════╝ ██║  ██║");
            Console.SetCursorPosition(35, 10);
            Console.Write("███████╗ ███████║");
            Console.SetCursorPosition(35, 11);
            Console.Write("██╔═══██╗╚════██║");
            Console.SetCursorPosition(35, 12);
            Console.Write("╚██████╔╝     ██║");
            Console.SetCursorPosition(36, 13);
            Console.Write("╚═════╝      ╚═╝");
            Console.ResetColor();
            Console.SetCursorPosition(35, 17);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Play");
            Console.ResetColor();
            Console.Write("         Exit");
        }
        static void MenuExit()
        {
            Console.SetCursorPosition(36, 8);
            Console.Write("██████╗ ██╗  ██╗");
            Console.SetCursorPosition(35, 9);
            Console.Write("██╔════╝ ██║  ██║");
            Console.SetCursorPosition(35, 10);
            Console.Write("███████╗ ███████║");
            Console.SetCursorPosition(35, 11);
            Console.Write("██╔═══██╗╚════██║");
            Console.SetCursorPosition(35, 12);
            Console.Write("╚██████╔╝     ██║");
            Console.SetCursorPosition(36, 13);
            Console.Write("╚═════╝      ╚═╝");
            Console.SetCursorPosition(35, 17);
            Console.Write("Play");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("         Exit");
            Console.ResetColor();
        }
        static void WinScreenYes()
        {
            Console.SetCursorPosition(33, 8);
            Console.Write("██████╗ ██╗  ██╗ ██╗");
            Console.SetCursorPosition(32, 9);
            Console.Write("██╔════╝ ██║  ██║║██║");
            Console.SetCursorPosition(32, 10);
            Console.Write("███████╗ ███████║║██║");
            Console.SetCursorPosition(32, 11);
            Console.Write("██╔═══██╗╚════██║ ╚═╝");
            Console.SetCursorPosition(32, 12);
            Console.Write("╚██████╔╝     ██║ ██╗");
            Console.SetCursorPosition(33, 13);
            Console.Write("╚═════╝      ╚═╝ ╚═╝ ");
            Console.SetCursorPosition(30, 15);
            Console.Write("Congradulations, you won!");
            Console.SetCursorPosition(25, 17);
            Console.Write("Would you like to continue playing?");
            Console.SetCursorPosition(35, 19);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("YES");
            Console.ResetColor();
            Console.Write("          NO");
        }
        static void WinScreenNo()
        {
            Console.SetCursorPosition(33, 8);
            Console.Write("██████╗ ██╗  ██╗ ██╗");
            Console.SetCursorPosition(32, 9);
            Console.Write("██╔════╝ ██║  ██║║██║");
            Console.SetCursorPosition(32, 10);
            Console.Write("███████╗ ███████║║██║");
            Console.SetCursorPosition(32, 11);
            Console.Write("██╔═══██╗╚════██║ ╚═╝");
            Console.SetCursorPosition(32, 12);
            Console.Write("╚██████╔╝     ██║ ██╗");
            Console.SetCursorPosition(33, 13);
            Console.Write("╚═════╝      ╚═╝ ╚═╝ ");
            Console.SetCursorPosition(30, 15);
            Console.Write("Congradulations, you won!");
            Console.SetCursorPosition(25, 17);
            Console.Write("Would you like to continue playing?");
            Console.SetCursorPosition(35, 19);
            Console.Write("YES");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("          NO");
            Console.ResetColor();
        }
        static void GameoverScreenYes()
        {
            Console.SetCursorPosition(8, 8);
            Console.Write(" ██████╗  █████╗ ███╗   ███╗███████╗ ██████╗ ██╗   ██╗███████╗██████╗ ");
            Console.SetCursorPosition(8, 9);
            Console.Write("██╔════╝ ██╔══██╗████╗ ████║██╔════╝██╔═══██╗██║   ██║██╔════╝██╔══██╗");
            Console.SetCursorPosition(8, 10);
            Console.Write("██║  ███╗███████║██╔████╔██║█████╗  ██║   ██║██║   ██║█████╗  ██████╔╝");
            Console.SetCursorPosition(8, 11);
            Console.Write("██║   ██║██╔══██║██║╚██╔╝██║██╔══╝  ██║   ██║╚██╗ ██╔╝██╔══╝  ██╔══██╗");
            Console.SetCursorPosition(8, 12);
            Console.Write("╚██████╔╝██║  ██║██║ ╚═╝ ██║███████╗╚██████╔╝ ╚████╔╝ ███████╗██║  ██║");
            Console.SetCursorPosition(8, 13);
            Console.Write(" ╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝ ╚═════╝   ╚═══╝  ╚══════╝╚═╝  ╚═╝");
            Console.SetCursorPosition(28, 16);
            Console.Write("Would you like to play again?");
            Console.SetCursorPosition(35, 19);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("YES");
            Console.ResetColor();
            Console.Write("          NO");
        }
        static void GameoverScreenNo()
        {
            Console.SetCursorPosition(8, 8);
            Console.Write(" ██████╗  █████╗ ███╗   ███╗███████╗ ██████╗ ██╗   ██╗███████╗██████╗ ");
            Console.SetCursorPosition(8, 9);
            Console.Write("██╔════╝ ██╔══██╗████╗ ████║██╔════╝██╔═══██╗██║   ██║██╔════╝██╔══██╗");
            Console.SetCursorPosition(8, 10);
            Console.Write("██║  ███╗███████║██╔████╔██║█████╗  ██║   ██║██║   ██║█████╗  ██████╔╝");
            Console.SetCursorPosition(8, 11);
            Console.Write("██║   ██║██╔══██║██║╚██╔╝██║██╔══╝  ██║   ██║╚██╗ ██╔╝██╔══╝  ██╔══██╗");
            Console.SetCursorPosition(8, 12);
            Console.Write("╚██████╔╝██║  ██║██║ ╚═╝ ██║███████╗╚██████╔╝ ╚████╔╝ ███████╗██║  ██║");
            Console.SetCursorPosition(8, 13);
            Console.Write(" ╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝ ╚═════╝   ╚═══╝  ╚══════╝╚═╝  ╚═╝");
            Console.SetCursorPosition(28, 16);
            Console.Write("Would you like to play again?");
            Console.SetCursorPosition(35, 19);
            Console.Write("YES");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("          NO");
            Console.ResetColor();
        }
        static void Instructions()
        {
            Console.SetCursorPosition(36, 3);
            Console.Write("██████╗ ██╗  ██╗");
            Console.SetCursorPosition(35, 4);
            Console.Write("██╔════╝ ██║  ██║");
            Console.SetCursorPosition(35, 5);
            Console.Write("███████╗ ███████║");
            Console.SetCursorPosition(35, 6);
            Console.Write("██╔═══██╗╚════██║");
            Console.SetCursorPosition(35, 7);
            Console.Write("╚██████╔╝     ██║");
            Console.SetCursorPosition(36, 8);
            Console.Write("╚═════╝      ╚═╝");
            Console.SetCursorPosition(20, 10);
            Console.Write("Move your character into a number that is equal");
            Console.SetCursorPosition(22, 11);
            Console.Write("to you in order to multiply and reach 64!");
            Console.SetCursorPosition(15, 13);
            Console.Write("If you are stuck you can move into a number that is less");
            Console.SetCursorPosition(19, 14);
            Console.Write("than you but you will demote down to that number.");
            Console.SetCursorPosition(13, 16);
            Console.Write("If you move into a number that is bigger than you, you lose.");
            Console.SetCursorPosition(11, 16);
            Console.Write("If you do get to 64 you can continue playing to attempt to get a");
            Console.SetCursorPosition(7, 17);
            Console.Write("higher score. Your number will reset but the board will remain the same.");
            Console.SetCursorPosition(31, 20);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Press any key to play...");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}