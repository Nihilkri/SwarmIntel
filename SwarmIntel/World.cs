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
	public partial class World : Form {
		#region Variables
		const double pi = Math.PI, tau = 2 * Math.PI;
		public static Random rnd = new Random(); 
		public static int fx, fy, fx2, fy2;
		private static List<Robot> bots = new List<Robot>();
		private static Bitmap gi; private static Graphics gb, gf;
		Timer tim = new Timer(); DateTime st; TimeSpan ft;
		bool active = true; Color clr = Color.Black;//FromArgb(8, 0, 0, 0);

		#endregion Variables
		#region Events
		public World() {InitializeComponent();}
		private void Form1_Load(object sender, EventArgs e) {
			fx = Width; fy = Height; fx2 = fx / 2; fy2 = fy / 2;
			gi = new Bitmap(fx, fy); gb = Graphics.FromImage(gi);
			gf = CreateGraphics();

			bots.Add(new Robot(bots.Count(), Color.Red, fx2, fy2));
			bots.Add(new Robot(bots.Count(), Color.Orange , rnd.NextDouble() * fx, rnd.NextDouble() * fy));
			bots.Add(new Robot(bots.Count(), Color.Yellow , rnd.NextDouble() * fx, rnd.NextDouble() * fy));
			bots.Add(new Robot(bots.Count(), Color.Green  , rnd.NextDouble() * fx, rnd.NextDouble() * fy));
			bots.Add(new Robot(bots.Count(), Color.SkyBlue, rnd.NextDouble() * fx, rnd.NextDouble() * fy));
			bots.Add(new Robot(bots.Count(), Color.Purple , rnd.NextDouble() * fx, rnd.NextDouble() * fy));

			tim.Interval = 1000 / 60; tim.Tick += tim_Tick; tim.Start();

			Calc(); Draw();
		}

		private void Form1_Paint(object sender, PaintEventArgs e) { gf.DrawImage(gi, 0, 0); }

		private void Form1_MouseClick(object sender, MouseEventArgs e) {
			if(bots[0].ox == e.X && bots[0].oy == e.Y) { bots[0].ox = -1; bots[0].oy = -1; } else { bots[0].ox = e.X; bots[0].oy = e.Y; } Draw(); if(bots[0].Active()) active = true;

		}

		private void Form1_KeyDown(object sender, KeyEventArgs e) {
			switch(e.KeyCode) {
				case Keys.Escape: Close(); return;
				case Keys.Tab: if(e.Shift) { MessageBox.Show("Shift Tab!"); } break;

				default: break;
			}
			int n = -1;
			if(e.KeyCode >= Keys.D1 && e.KeyCode <= Keys.D9) n = e.KeyCode - Keys.D1;
			if(e.KeyCode >= Keys.NumPad1 && e.KeyCode <= Keys.NumPad9) n = e.KeyCode - Keys.NumPad1;
			if(e.KeyCode == Keys.D0 || e.KeyCode == Keys.NumPad0) n = 10;
			if(n > -1) Robot.scanpower = n * 30;


		}

		private void tim_Tick(object sender, EventArgs e) {
			Calc(); Draw(); 
		}

		#endregion Events
		#region Calc
		void Calc() {
			st = DateTime.Now;
			gb.Clear(clr);

			active = false;
			foreach(Robot b in bots) {
				b.Calc(); active = active || b.Active();

			}

		}
		#endregion Calc
		#region Draw
		void Draw() {
			foreach(Robot b in bots) {
				b.Draw(gb);
				
			}
			if(!active) for(int q = 0 ; q < 5 ; q++) gb.DrawRectangle(Pens.Red, q, q, fx - 2 * q, fy - 2 * q);

			//DateTime sst = DateTime.Now; TimeSpan sft;
			//for(int b = 0 ; b < bots.Count ; b++)
			//	//for(double q = -pi2 / 8 ; q < pi2 / 8 ; q += 5.0 * pi2 / 360.0)
			//	for(double q = 0 ; q < pi2 ; q += 5.0 * pi2 / 360.0)
			//		Scan(b, q, 250, 16);
			//sft = DateTime.Now - sst;
			//gb.DrawString(sft.TotalMilliseconds.ToString() + "ms", Font, Brushes.White, fx2, 0);
			//gb.DrawString((1000 / sft.TotalMilliseconds).ToString() + " FPS", Font, Brushes.White, fx2, 16);

			//for(int q = 2 ; q <= 50 ; q++) gb.DrawString(rnd.NextDouble().ToString(), Font, Brushes.White, 0, 16*q);


			ft = DateTime.Now - st;
			gb.DrawString(ft.TotalMilliseconds.ToString() + "ms", Font, Brushes.White, 0, 0);
			gb.DrawString((1000 / ft.TotalMilliseconds).ToString() + " FPS", Font, Brushes.White, 0, 16);
			gf.DrawImage(gi, 0, 0);
		}
		#endregion Draw
		#region Robot Interactions
		/// <summary>
		/// Scans the world around a robot in a straight line
		/// </summary>
		/// <param name="a">Angle relative to robot</param>
		/// <param name="r">Radius of scan</param>
		/// <param name="i">Intensity or resolution</param>
		/// <returns></returns>
		public static double Scan(int n, double a, double r, double i) {
			a = (a + bots[n].T + tau) % tau + tau;
			int x, y; int l=5;
			for(double q = bots[n].width ; q < r ; q += q - bots[n].width+1) {
				x = (int)(bots[n].X + Math.Cos(a) * q); y = (int)(bots[n].Y + Math.Sin(a) * q);
				if((0 <= x && x < fx) && (0 <= y && y < fy)) gi.SetPixel(x, y, bots[n].c);
				foreach(Robot b in bots) if(b.rect.Contains(x, y)) return q;
				if(n == 0 && a == 0) gb.DrawString(q.ToString(), DefaultFont, Brushes.White, 0, l++ * 16);
			}
			return -1.0;
		}
		#endregion Robot Interactions

	}
}
