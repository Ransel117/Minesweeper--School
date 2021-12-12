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


			MineField mineField = new(9, 9);
			// Size of the minefield

			mineField.GenerateField(this);
			// Generation of it

			
		}

		private void button1_Click(object sender, EventArgs e)
		{
			
			// Reset
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
					field.Text = id.ToString();

					this.Fields.Add(field);
					form.Controls.Add(field);

					id++;
					field.MouseDown += Mouse_Down;
				}
			}

			this.PlaceMines(10);
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
					
					this.Fields[i].Image = Image.FromFile("majn.png"); // Delete code when number of mines next to empty field fixed
					
					count--;
				}

				i++;
			}
			// The random placement of the mines in the grid
		}



		void Mouse_Down(object sender, MouseEventArgs e)
		{
			Field field = (Field)sender;

			if (e.Button == MouseButtons.Right)
			{
				field.SetFlagged(!field.IsFlagged);

			}
			else if (e.Button == MouseButtons.Left && !field.IsFlagged)
				// If field leftclicked and not flagged do stuff
            {
				if (field.IsMine)
                {
					foreach (Field field2 in this.Fields)
                    {
						if (field2.IsMine)
						{
							field2.Image = Image.FromFile("majn.png");
						}
						field2.Enabled = false;
                    }
					// If press on mine u ded
                }
				else
                {
					field.Text = "";
					// set field text to number of adjacent mines or smth liknande

					// If not u live
                }
				field.Enabled = false;
				// Deactivate button so u not be able to press anymore
            }
		}

	}
	
	class Field : Button
	{
		public bool IsWin {get; private set; }
		public bool IsFlagged { get; private set; }
		public bool IsMine { get; set; }
		public int Id { get; }
		
		public int flaggedMines { get; set; }
		public int flagNum { get; set; }

		public Field(int id) : base()
		{
			this.IsFlagged = false;
	
			this.IsMine = false;
			this.Id = id;
			this.flaggedMines = 0;
			this.flagNum = 0;
		}

		public void SetFlagged(bool value)
        {
			this.IsFlagged = value;

			this.SetText();
        }
		void SetText()
        {
			// IF flagged == true
			if (this.IsFlagged)
			{
				if (this.IsMine)
                {
					this.flaggedMines++;
                }
				this.flagNum++;
				
				this.Image = Image.FromFile("flagg.png");
			}
			// If flagged == false
			else
			{
				if(this.IsMine)
                {
					this.flaggedMines--;
                }
				this.flagNum--;

				this.Image = null;
			}
			this.Text = this.flaggedMines + "\n" + this.flagNum;
		}
		public void SetWin()
        {

			// If all mines flagged and flagNum = number of mines, u win
        }

	}


}
