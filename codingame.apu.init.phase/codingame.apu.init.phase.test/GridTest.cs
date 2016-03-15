namespace codingame.apu.init.phase.test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NFluent;
    using System.Collections.Generic;

    [TestClass]
    public class GridTest
    {
        private Grid _grid;
        private Writter _writter;

        [TestInitialize]
        public void Initialize()
        {
            _grid = new Grid();
            _writter = new Writter();
        }

        [TestMethod]
        public void BaseTest()
        {
            _grid = new Grid();
            _grid.AddCells(0, "00");
            _grid.AddCells(1, "0.");

            _grid.WriteLines(_writter.Write);

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
            _grid = new Grid();
            _grid.AddCells(0, "0.0.0");

            _grid.WriteLines(_writter.Write);

            var expected = new[] 
            { 
                "0 0 2 0 -1 -1",
                "2 0 4 0 -1 -1",
                "4 0 -1 -1 -1 -1"
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
