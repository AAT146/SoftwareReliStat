using ClassLibrary;
using DatabasePostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;
using System.Windows.Forms;
using TheArtOfDevHtmlRenderer.Core;

namespace SoftwareReliStat
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void ButtonConnectionNpgsql(object sender, EventArgs e)
		{
			// ������ ����������� � PostgreSQL
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

			var connectionString = configuration.GetConnectionString("DefaultConnection");

			try
			{
				using (var connection = new NpgsqlConnection(connectionString))
				{
					connection.Open(); // ������� ������� ����������

					// ����������� � �������� ����������
					MessageBox.Show("���������� � ����� ������ �����������!", "�����������",
						MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			catch (Exception ex)
			{
				// ����������� � ��������� ���������� � ��������� ������
				MessageBox.Show($"�� ������� ������������ � ���� ������:\n{ex.Message}", "������",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void ButtonConnectionSCADA(object sender, EventArgs e)
		{

		}

		private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			//// ��������� ������ �� ������� ���� ������
			//using (DatabaseDbContext context = new DatabaseDbContext())
			//{
			//	var items = context.YourTable.ToList();

			//	// �������� ������ � ComboBox
			//	guna2ComboBox1.Items.AddRange(items);
			//	guna2ComboBox1.SelectedIndex = 0; // ��������� ���������� �������
			//}
		}

		private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e)
		{
			DateTime selectedDateTime = guna2DateTimePicker1.Value;
			//MessageBox.Show($"�� �������: {selectedDateTime}");
		}

		private void CSVToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Filter = "CSV files (*.csv)|*.csv",
				Title = "�������� CSV ����"
			};

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					HandlerCSV handler = new HandlerCSV();
					string[] data = handler.ReadCSVData(openFileDialog.FileName);

					// ����������� � ���������� ������
					MessageBox.Show($"������ ������� ���������! " +
						$"������� �����: {data.Length}.",
						"�����", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				catch (ArgumentException ex)
				{
					MessageBox.Show($"������: {ex.Message}", "������",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				catch (Exception ex)
				{
					MessageBox.Show($"����������� ������: {ex.Message}", "������",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
	}
}
