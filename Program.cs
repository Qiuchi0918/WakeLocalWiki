using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace WakeLocalWiki
{
    class Program
    {
        public static void ExtractResFile(string resFileName, string outputFile)
        {
            BufferedStream inStream = null;
            FileStream outStream = null;
            try
            {
                Assembly asm = Assembly.GetExecutingAssembly();
                inStream = new BufferedStream(asm.GetManifestResourceStream(resFileName));
                outStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write);

                byte[] buffer = new byte[1024];
                int length;

                while ((length = inStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    outStream.Write(buffer, 0, length);
                }
                outStream.Flush();
            }
            finally
            {
                if (outStream != null)
                {
                    outStream.Close();
                }
                if (inStream != null)
                {
                    inStream.Close();
                }
            }
        }

        static void Main(string[] args)
        {
            Assembly ass = Assembly.GetExecutingAssembly();
            string curDir = Environment.CurrentDirectory;
            File.WriteAllText(curDir + "\\wake.bat", (new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("WakeLocalWiki.wake.bat"))).ReadToEnd());
            Process proc = new Process();
            proc.StartInfo.WorkingDirectory = curDir;
            proc.StartInfo.FileName = "wake.bat";
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            ExtractResFile("WakeLocalWiki.kiwix-serve.exe", curDir + "\\kiwix-serve.exe");
            proc.Start();
        }
    }
}
