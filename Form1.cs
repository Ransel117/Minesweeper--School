using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cool_gam
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MineField mineField = new(8, 8);

            mineField.GenerateField(this);
        }
    }
    class MineField
    {
        public MineField(int colCount, int rowCount) : base()
        {
            ColCount = colCount;
            RowCount = rowCount;
            this.Fields = new();
        }

        public int ColCount { get; }
        public int RowCount { get; }
        public List<Field> Fields { get; }

        public void GenerateField(Form1 form)
        {
            int id = 0;
            for (int i = 0; i < this.RowCount; i++)
            {
                for (int j = 0; j < this.ColCount; j++)
                {
                    const int BTN_SIZE = 40;
                    Field field = new(id);

                    field.Size = new(BTN_SIZE, BTN_SIZE);
                    field.Location = new((BTN_SIZE + 5) * j, (BTN_SIZE + 5) * i);

                    field.Name = string.Format($"button_{id}");
                    field.Text = id.ToString() + "\n" + field.IsFlagged.ToString();

                    this.Fields.Add(field);
                    form.Controls.Add(field);

                    id++;
                }
            }

            this.PlaceMines(5);
        }

        void PlaceMines(int count)
        {
            int numOfMines = this.Fields.Count;
            Random rand = new();

            int i = 0;
            while (count != 0)
            {
                if (i == numOfMines - 1)
                    i = 0;

                if (rand.Next(0, numOfMines) == i)
                {
                    this.Fields[i].IsMine = true;
                    this.Fields[i].Text = "MINE";
                    count--;
                }

                i++;
            }
        }
    }

    class Field : Button
    {
        public bool IsFlagged { get; }
        public bool IsMine { get; set; }
        public int Id { get; }

        public Field(int id) : base()
        {
            this.IsFlagged = false;
            this.IsMine = false;
            this.Id = id;
        }
    }
  
}
