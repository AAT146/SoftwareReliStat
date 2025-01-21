namespace View
{
	partial class ProgressDialog
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgressDialog));
			guna2ProgressBar1 = new Guna.UI2.WinForms.Guna2ProgressBar();
			label1 = new Label();
			SuspendLayout();
			// 
			// guna2ProgressBar1
			// 
			guna2ProgressBar1.CustomizableEdges = customizableEdges1;
			guna2ProgressBar1.Location = new Point(8, 35);
			guna2ProgressBar1.Name = "guna2ProgressBar1";
			guna2ProgressBar1.ProgressColor = Color.LightSteelBlue;
			guna2ProgressBar1.ProgressColor2 = Color.LightSteelBlue;
			guna2ProgressBar1.ShadowDecoration.CustomizableEdges = customizableEdges2;
			guna2ProgressBar1.Size = new Size(330, 30);
			guna2ProgressBar1.TabIndex = 0;
			guna2ProgressBar1.Text = "guna2ProgressBar1";
			guna2ProgressBar1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 9F);
			label1.Location = new Point(8, 9);
			label1.Name = "label1";
			label1.Size = new Size(72, 20);
			label1.TabIndex = 1;
			label1.Text = "Процесс:";
			// 
			// ProgressDialog
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.AliceBlue;
			ClientSize = new Size(347, 74);
			Controls.Add(label1);
			Controls.Add(guna2ProgressBar1);
			Icon = (Icon)resources.GetObject("$this.Icon");
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "ProgressDialog";
			Text = "Процесс выполнения";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Guna.UI2.WinForms.Guna2ProgressBar guna2ProgressBar1;
		private Label label1;
	}
}