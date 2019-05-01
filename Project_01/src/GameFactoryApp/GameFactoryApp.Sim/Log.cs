using System;
using System.IO;
using System.Text;

namespace GameFactoryApp.Sim
{
    public static class Log
    {
        private static StreamWriter _output;

        public static void Initialize()
        {
            _output = new StreamWriter("log.txt", false);
            Console.SetOut(new MyConsoleOutput(Console.Out));
            Console.SetIn(new MyConsoleInput(Console.In));
        }

        public static void Write(char c)
        {
            lock (_output)
            {
                _output.Write(c);
            }
        }

        public static void Close()
        {
            _output.Close();
        }
    }

    public class MyConsoleOutput : TextWriter
    {
        private readonly TextWriter _standard;

        public MyConsoleOutput(TextWriter standard)
        {
            this._standard = standard;
        }

        public override void Write(char value)
        {
            _standard.Write(value);
            Log.Write(value);
        }

        public override Encoding Encoding
        {
            get { return Encoding.Default; }
        }

        protected override void Dispose(bool disposing)
        {
            _standard.Dispose();
        }
    }

    public class MyConsoleInput : TextReader
    {
        private readonly TextReader _standard;

        public MyConsoleInput(TextReader standard)
        {
            this._standard = standard;
        }

        public override int Peek()
        {
            return _standard.Peek();
        }

        public override int Read()
        {
            int result = _standard.Read();
            Log.Write((char)result);
            return result;
        }

        protected override void Dispose(bool disposing)
        {
            _standard.Dispose();
        }
    }
}
