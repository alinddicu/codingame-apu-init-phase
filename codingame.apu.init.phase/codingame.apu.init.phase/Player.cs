namespace codingame.apu.init.phase
{
    using System;
    using System.Linq;
    using System.IO;
    using System.Text;
    using System.Collections;
    using System.Collections.Generic;

    /**
     * Don't let the machines win. You are humanity's last hope...
     **/
    class Player
    {
        static void Main(string[] args)
        {
            int width = int.Parse(Console.ReadLine()); // the number of cells on the X axis
            int height = int.Parse(Console.ReadLine()); // the number of cells on the Y axis
            var grid = new Grid(width, height);
            for (int i = 0; i < height; i++)
            {
                grid.AddCells(i, Console.ReadLine()); // width characters, each either 0 or .
            }

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");

            grid.WriteLines(Console.WriteLine);

            //Console.WriteLine("0 0 1 0 0 1"); // Three coordinates: a node, its right neighbor, its bottom neighbor
        }
    }

    public class Grid
    {
        private readonly List<Cell> _cells = new List<Cell>();

        public Grid(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Height { get; private set; }

        public int Width { get; private set; }

        public void AddCells(int y, string linePopulation)
        {
            var cellOrNodeDiscriminants = linePopulation.Select(s => s.ToString()).ToArray();
            for (var x = 0; x < cellOrNodeDiscriminants.Length; x++)
            {
                var cellOrNodeDiscriminant = cellOrNodeDiscriminants[x];
                _cells.Add(Cell.Create(x, y, cellOrNodeDiscriminant));
            }
        }

        public Cell GetAt(int x, int y)
        {
            return _cells.SingleOrDefault(c => c.X == x && c.Y == y);
        }

        public void WriteLines(Action<string> writter)
        {
            var nodes = _cells.OfType<Node>().Cast<Node>().ToArray();
            foreach (var node in nodes)
            {
                writter(node + " " + string.Join(" ", node.GetNeighbours(this)));
            }
        }
    }

    public class Cell
    {
        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; private set; }

        public int Y { get; private set; }

        public static Cell Create(int x, int y, string cellOrNodeDiscriminant)
        {
            if (cellOrNodeDiscriminant == "0")
            {
                return new Node(x, y);
            }

            if (cellOrNodeDiscriminant == ".")
            {
                return new Cell(x, y);
            }

            throw new ArgumentException("Discriminant invalide : " + cellOrNodeDiscriminant);
        }

        public override string ToString()
        {
            return X + " " + Y;
        }
    }

    public class Node : Cell
    {
        private static readonly Cell DefaultNeighbour = new Cell(-1, -1);

        public Node(int x, int y)
            : base(x, y)
        {
        }

        public IEnumerable<Cell> GetNeighbours(Grid grid)
        {
            yield return GetRight(grid);
            yield return GetBelow(grid);
        }

        private Cell GetBelow(Grid grid)
        {
            Cell cell = null;
            var y = Y + 1;

            do
            {
                cell = Get(X, y, grid);
                //if (y > grid.Height - 1)
                //{
                //    break;
                //}

                y++;
            }
            while (cell != null && !(cell is Node));

            return GetDefault(cell);
        }

        private Cell GetRight(Grid grid)
        {
            Cell cell = null;
            var x = X + 1;

            do
            {
                cell = Get(x, Y, grid);
                //if (x > grid.Width - 1)
                //{
                //    break;
                //}

                x++;
            }
            while (cell != null && !(cell is Node));

            return GetDefault(cell);
        }

        private Cell Get(int x, int y, Grid grid)
        {
            return grid.GetAt(x, y);
        }

        private static Cell GetDefault(Cell cell)
        {
            if (cell != null && cell is Node)
            {
                return cell;
            }

            return DefaultNeighbour;
        }
    }
}
