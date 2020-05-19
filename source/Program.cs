 using System;
 using System.IO;

namespace yeetPadlock
{
    class Program
    {
        public static void prompt()
        {
            Console.Write("\nyeet> ");
            interpret(Console.ReadLine().ToString());
        }
        public static string reader(char param,string text)
        {
            try 
            {
                int startPos = text.LastIndexOf("-" + param + "(") + ("-" + param + "(").Length;
                int length = text.IndexOf(");") - startPos;
                string sub = text.Substring(startPos, length);
                //Console.WriteLine("number chosen: "+sub+"\n");
                return sub;
            }
            catch { Console.WriteLine("exeption: could not read parameter: "+param+", resetting...\n");return "-1"; }            
        }
        public static void interpret(string text)
        {
            if (text==("exit"))
            {
                Environment.Exit(0);
            }
            else if (text.Contains("-f"))
            {
                if (text.Contains("-r"))
                {
                    string sub = reader('f', text);
                    try { Console.WriteLine("reading file: " + sub + "\nContent:\n" + System.IO.File.ReadAllText(sub)); }
                    catch { Console.WriteLine("exeption: could not open file\n"); }
                }
                else if (text.Contains("-y"))
                {
                    string sub = reader('f', text);
                    string myString = text.Remove(text.IndexOf(("-f(" + sub + ");")), ("-f(" + sub + ");").Length);
                    //Console.WriteLine(myString);
                    int number = int.Parse(reader('s', myString));
                    if(number < 0 || number > 10){ Console.WriteLine("exeption: invalid number, accepting only 1-10, resetting...\n");prompt(); }
                    try
                    { 
                        Console.WriteLine("yeeting file: " + sub+" please wait...\n");
                        text=System.IO.File.ReadAllText(sub);
                        char[] array = text.ToCharArray();
                        for (int i = 0; i < array.Length; i++)
                        {
                            array[i] = (char)(array[i] + number);
                        }
                        File.Delete(sub);
                        string s = new string(array);
                        string[] contents = { s };
                        File.WriteAllLines(sub, contents);
                        Console.WriteLine("Complete!\n");
                    }
                    catch(Exception e) {Console.WriteLine("exeption: could process file"+e+", resetting...\n");}
                }
                else {Console.WriteLine("exeption: no action parameters, resetting...\n");}
                prompt();
            }
            else {Console.WriteLine("exeption: could not proceed, resetting...\n");prompt();}
        }
        static void Main(string[] args)
        {
            prompt();
        }
    }
}
