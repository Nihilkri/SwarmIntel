using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SwarmIntel {
	public partial class Form1 : Form {
		#region Variables
		Graphics gb, gf; Bitmap gi; int fx, fy, fx2, fy2;
		List<Robot> bots = new List<Robot>();
		Random rnd = new Random();

		#endregion Variables
		#region Events
		public Form1() {InitializeComponent();}
		private void Form1_Load(object sender, EventArgs e) {
			fx = Width; fy = Height; fx2 = fx / 2; fy2 = fy / 2;
			gi = new Bitmap(fx, fy); gb = Graphics.FromImage(gi);
			gf = CreateGraphics();

			bots.Add(new Robot(Color.Red, fx2, fy2));
			bots.Add(new Robot(Color.Orange, rnd.NextDouble() * fx, rnd.NextDouble() * fy));
			bots.Add(new Robot(Color.Yellow, rnd.NextDouble() * fx, rnd.NextDouble() * fy));
			bots.Add(new Robot(Color.Green , rnd.NextDouble() * fx, rnd.NextDouble() * fy));
			bots.Add(new Robot(Color.Blue  , rnd.NextDouble() * fx, rnd.NextDouble() * fy));
			bots.Add(new Robot(Color.Violet, rnd.NextDouble() * fx, rnd.NextDouble() * fy));

			Calc(); Draw();
		}

		private void Form1_Paint(object sender, PaintEventArgs e) { gf.DrawImage(gi, 0, 0); }

		private void Form1_MouseClick(object sender, MouseEventArgs e) {
			bots[0].dx = e.X; bots[0].dy = e.Y; Draw();

		}

		private void Form1_KeyDown(object sender, KeyEventArgs e) {
			switch(e.KeyCode) {
				case Keys.Escape: Close(); return;

				default: break;
			}

		}
		#endregion Events
		#region Calc
		void Calc() {


		}
		#endregion Calc
		#region Draw
		void Draw() {
			gb.Clear(Color.Black);
			foreach(Robot q in bots) {
				q.Draw(gb);
				
			}

			gf.DrawImage(gi, 0, 0);
		}
		#endregion Draw
	}
}
