﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace camelfuck
{
    class camelfuck
    {
        // Instance info.
        // Instance of camelfuck interpreter, actually handles interpreting.

        /** The array that acts as the interpreter's virtual memory */
        private char[] mem;
        /** The memory pointer */
        private int mp;

        /** The string containing the comands to be executed */
        private char[] com;

        /** The instruction pointer */
        private int ip = 0;

        private int EOF; //End Of File

        /**
        * Create the camelfuck VM and give it the string to be interpreted.
        * @param s The string to be interpreted.
        */
        public camelfuck(string s)
        {

            mem = new char[30000];
            mp = 0;
            com = s.ToCharArray();
            EOF = com.Length;

        }

        /**
        * Run the interpreter with its given string
        */
        public void run()
        {
            while (ip < EOF)
            {

                // Get the current command
                char c = com[ip];

                // Act based on the current command and the camelfuck spec
                switch (c)
                {
                    case '>': mp++; break;
                    case '<': mp--; break;
                    case '+': mem[mp]++; break;
                    case '-': mem[mp]--; break;
                    case '.': Console.Write((mem[mp])); break;
                    case ',':
                        try
                        {
                            mem[mp] = (char)Console.Read();
                        }
                        catch (Exception e)
                        {
                            Debug.Write(e.StackTrace);
                        }
                        break;
                    case '[':
                        if (mem[mp] == 0)
                        {
                            while (com[ip] != ']') ip++;
                        }
                        break;

                    case ']':
                        if (mem[mp] != 0)
                        {
                            while (com[ip] != '[') ip--;
                        }
                        break;
                }

                // increment instruction mp
                ip++;

            }
        }

        // Static stuff - boilerplate code and file reading

        public static int EXIT_SUCCESS = 1;
        public static int EXIT_FAILURE = -1;

        /**
        * Set up a single instance of the camelfuck interpreter, and run it, with the given string or file.
        * @param args Command line arguments
        */
        static void Main(String[] args)
        {

            string s = "";
            // Test that there are exactly two arguments
            if (args.Length != 2)
            {
                Usage();
            }

            // Assign s
            if (args[0] == ("-f"))
            {
                s = System.IO.File.ReadAllText(@"C:\Users\Arthur\Source\Repos\Camelfuck\Camelfuck\Camelfuck\bin\Debug\" + args[1]);
            }
            else if (args[0] == ("-i"))
            {
                s = args[1];
            }
            else
            {
                Usage();
            }

            // Start the interpreter
            (new camelfuck(s)).run();

            Console.Read(); // Pause before closing console window

            // Exit
            System.Environment.Exit(EXIT_SUCCESS);
        }

        /**
        * Called when incorrect parameters are used.
        */
        static void Usage()
        {
            Console.Write("Usage:\n\tcamelfuck -f <filename>\n\tcamelfuck -i <string>\n");
            Console.Write("For help:\n\tcamelfuck -h\n\tcamelfuck --help\n");
            Console.Read(); // Pause before closing console window
            System.Environment.Exit(EXIT_FAILURE);
        }
    }
}