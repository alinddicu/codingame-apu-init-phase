namespace codingame.apu.init.phase.test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NFluent;
    
    [TestClass]
    public class GridTest
    {
        private Writter _writter;

        [TestInitialize]
        public void Initialize()
        {
            _writter = new Writter();
        }

        [TestMethod]
        public void BaseTest()
        {
            var grid = new Grid(2, 2);
            grid.AddCells(0, "00");
            grid.AddCells(1, "0.");

            grid.WriteLines(_writter.Write);

            var expected = new[] 
            { 
                "0 0 1 0 0 1",
                "1 0 -1 -1 -1 -1",
                "0 1 -1 -1 -1 -1"
            };

            Check.That(_writter.Lines).Contains(expected);
        }

        [TestMethod]
        public void Horizontal()
        {
            var grid = new Grid(5, 1);
            grid.AddCells(0, "0.0.0");

            grid.WriteLines(_writter.Write);

            var expected = new[] 
            { 
                "0 0 1 0 0 1",
                "1 0 -1 -1 -1 -1",
                "0 1 -1 -1 -1 -1"
            };

            Check.That(_writter.Lines).Contains(expected);
        }

        private class Writter
        {
            private readonly List<string> _lines = new List<string>();

            public void Write(string line)
            {
                _lines.Add(line);
            }

            public IEnumerable<string> Lines { get { return _lines; } }
        }
    }
}
