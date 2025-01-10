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
			// Строка подключения к PostgreSQL
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

			var connectionString = configuration.GetConnectionString("DefaultConnection");

			try
			{
				using (var connection = new NpgsqlConnection(connectionString))
				{
					connection.Open(); // Попытка открыть соединение

					// Уведомление о успешном соединении
					MessageBox.Show("Соединение с базой данных установлено!", "Уведомление",
						MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			catch (Exception ex)
			{
				// Уведомление о неудачном соединении с указанием ошибки
				MessageBox.Show($"Не удалось подключиться к базе данных:\n{ex.Message}", "Ошибка",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void ButtonConnectionSCADA(object sender, EventArgs e)
		{

		}

		private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			//// Получение данных из таблицы базы данных
			//using (DatabaseDbContext context = new DatabaseDbContext())
			//{
			//	var items = context.YourTable.ToList();

			//	// Привязка данных к ComboBox
			//	guna2ComboBox1.Items.AddRange(items);
			//	guna2ComboBox1.SelectedIndex = 0; // Установка выбранного индекса
			//}
		}

		private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e)
		{
			DateTime selectedDateTime = guna2DateTimePicker1.Value;
			//MessageBox.Show($"Вы выбрали: {selectedDateTime}");
		}

		private void CSVToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Filter = "CSV files (*.csv)|*.csv",
				Title = "Выберите CSV файл"
			};

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					HandlerCSV handler = new HandlerCSV();
					string[] data = handler.ReadCSVData(openFileDialog.FileName);

					// Уведомление о сохранении данных
					MessageBox.Show($"Данные успешно загружены! " +
						$"Найдено строк: {data.Length}.",
						"Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				catch (ArgumentException ex)
				{
					MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Неизвестная ошибка: {ex.Message}", "Ошибка",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
	}
}
