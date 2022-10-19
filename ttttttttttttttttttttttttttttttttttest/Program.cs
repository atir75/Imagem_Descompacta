using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace CabecalhoBMP
{
    class Program
    {
        static int converteBinDec(String _s)
        {
            BitArray original = new BitArray(Encoding.Default.GetBytes(_s));
            String binario = "";

            for (int i = original.Count - 1; i >= 0; i--) binario += Convert.ToInt32(original[i]);
            return Convert.ToInt32(binario, 2);
        }
        static void Main(string[] args)
        {
            FileStream infile, outfile;
            String buffer = "";
            int tamcabec;
            char contador, pixel;

            infile = new System.IO.FileStream("imagem.compac", FileMode.Open, FileAccess.Read);
            outfile = new System.IO.FileStream("imagem.bmp", FileMode.Create, FileAccess.Write);

            infile.Position = 10;
            for (int i = 0; i < 4; ++i) buffer += (char)infile.ReadByte();
            tamcabec = converteBinDec(buffer);

            infile.Position = 0;
            for (int i = 0; i < tamcabec; ++i)
            {
                outfile.WriteByte((byte)infile.ReadByte());
            }

            for (int i = 0; i < (infile.Length - tamcabec); i += 2)
            {
                contador = (char)infile.ReadByte();
                pixel = (char)infile.ReadByte();
                for (int j = 0; j < (byte)contador; ++j)
                {
                    outfile.WriteByte((byte)pixel);
                }
            }

            infile.Close();
            outfile.Close();
            Console.ReadKey();
        }
    }
}

