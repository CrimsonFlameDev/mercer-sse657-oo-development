using System;
using System.IO;
using System.Text;

namespace TicTacToe
{
    public static class Log
    {
        private static StreamWriter _output;

        public static void Initialize(/*string path*/)
        {
            //_output = new StreamWriter(path, false);
            _output = new StreamWriter("log.txt", false);
            Console.SetOut(new MyConsoleOutput(Console.Out/*, log*/));
            Console.SetIn(new MyConsoleInput(Console.In/*, log*/));
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
        //private readonly Log _log;

        public MyConsoleOutput(TextWriter standard/*, Log log*/)
        {
            this._standard = standard;
            //this._log = log;
        }

        public override void Write(char value)
        {
            _standard.Write(value);
            //_log.Write(value);
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
        //private readonly Log _log;

        public MyConsoleInput(TextReader standard/*, Log log*/)
        {
            this._standard = standard;
            //this._log = log;
        }

        public override int Peek()
        {
            return _standard.Peek();
        }

        public override int Read()
        {
            int result = _standard.Read();
            //_log.Write((char)result);
            Log.Write((char)result);
            return result;
        }

        protected override void Dispose(bool disposing)
        {
            _standard.Dispose();
        }
    }
}
