using ClassLibrary;
using DatabasePostgreSQL;
using DatabasePostgreSQL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;
using TheArtOfDevHtmlRenderer.Core;
using View;
using Newtonsoft.Json;
using static ClassLibrary.DistributionAnalyzer;

namespace SoftwareReliStat
{
	public partial class MainForm : Form
	{
		private HandlerSCADA currentToken;

		private readonly DatabaseDbContext _context;

		public MainForm()
		{
			InitializeComponent();

			// ���������� ������� ��� �������� ���������
			var dbContextFactory = new DesignTimeDbContextFactory();
			_context = dbContextFactory.CreateDbContext(null);

			// ��������� ������
			LoadUnifiedPowerSystems();

			// ��������� ��������� ������ ���������
			guna2ComboBox4.Items.AddRange(new[] { "�������", "������", "���", "�����" });
			//guna2ComboBox4.SelectedIndex = 1; // ������������� �������� �� ���������

			// ������������� �� �������
			guna2TextBox1.TextChanged += (s, e) => ProcessDiscreteStep();
			guna2ComboBox4.SelectedIndexChanged += (s, e) => ProcessDiscreteStep();
		}

		/// <summary>
		/// �������� �������� ��� �� ��
		/// </summary>
		private void LoadUnifiedPowerSystems()
		{
			var unifiedPowerSystems = _context.UnifiedPowerSystem.ToList();
			guna2ComboBox1.DataSource = unifiedPowerSystems;
			guna2ComboBox1.DisplayMember = "Title";
			guna2ComboBox1.ValueMember = "Id";
			guna2ComboBox1.SelectedIndex = -1; // ���������� ����� �� ���������
		}

		/// <summary>
		/// ����� ������ �������� ��� �� ���������������� �����.
		/// </summary>
		/// <param name="sender">������.</param>
		/// <param name="e">������ � �������.</param>
		private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (guna2ComboBox1.SelectedValue is int unifiedPowerSystemId)
			{
				var powerSystems = _context.PowerSystem
					.Where(ps => ps.UnifiedPowerSystemId == unifiedPowerSystemId)
					.ToList();

				guna2ComboBox2.DataSource = powerSystems;
				guna2ComboBox2.DisplayMember = "Title";
				guna2ComboBox2.ValueMember = "Id";
				guna2ComboBox2.Enabled = true;
				guna2ComboBox2.SelectedIndex = -1; // ���������� �����

				// ������� ���������� ������
				guna2ComboBox3.DataSource = null;
				guna2ComboBox3.Enabled = false;
			}
			else
			{
				// ���� ������ �� �������, ���������� ������
				guna2ComboBox2.DataSource = null;
				guna2ComboBox2.Enabled = false;
				guna2ComboBox3.DataSource = null;
				guna2ComboBox3.Enabled = false;
			}
		}

		/// <summary>
		/// ����� ������ �������� �� �� ���������������� �����.
		/// </summary>
		/// <param name="sender">������.</param>
		/// <param name="e">������ � �������.</param>
		private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (guna2ComboBox2.SelectedValue is int powerSystemId)
			{
				var reliabilityZones = _context.ReliabilityZone
					.Where(rz => rz.PowerSystemId == powerSystemId)
					.ToList();

				guna2ComboBox3.DataSource = reliabilityZones;
				guna2ComboBox3.DisplayMember = "Title";
				guna2ComboBox3.ValueMember = "Id";
				guna2ComboBox3.Enabled = true;
				guna2ComboBox3.SelectedIndex = -1; // ���������� �����
			}
			else
			{
				// ���� ������ �� �������, ���������� ������
				guna2ComboBox3.DataSource = null;
				guna2ComboBox3.Enabled = false;
			}
		}

		/// <summary>
		/// ����� �������� ���������� � �� (PostgeSQL).
		/// </summary>
		/// <param name="sender">������.</param>
		/// <param name="e">������ � �������.</param>
		private void ConnectPostgreSQL(object sender, EventArgs e)
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

					// �������� ���� guna2ComboBox1
					guna2ComboBox1.Enabled = true;
				}
			}
			catch (Exception ex)
			{
				// ����������� � ��������� ���������� � ��������� ������
				MessageBox.Show($"�� ������� ������������ � ���� ������:\n{ex.Message}", "������",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// ����� �������� ���������� � ��� ��-11.
		/// </summary>
		/// <param name="sender">������.</param>
		/// <param name="e">������ � �������.</param>
		private void ConnectSCADA(object sender, EventArgs e)
		{
			try
			{
				// ��������� ���������� � ��� ��-11
				HandlerSCADA.Token currentToken = HandlerSCADA.GetToken();
				MessageBox.Show("���������� � ��� ��-11 �����������.", "����������", MessageBoxButtons.OK, MessageBoxIcon.Information);

				// �������� ������ ������
				guna2Button1.Enabled = true;
			}
			catch (Exception ex)
			{
				MessageBox.Show($"������ �����������: {ex.Message}", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// ����: �������� ������ ��������.
		/// </summary>
		private string[] data;

		/// <summary>
		/// ����: ������������ ������ ��������.
		/// </summary>
		private int[] processedData;



		private List<ClusterAnalysisResult> _calculationResults; // ���� ��� �������� �����������

		private double _discreteStepInSeconds; // ���� ��� �������� �������� � ��������

		/// <summary>
		/// ����� �������� ������ �� CSV �����.
		/// </summary>
		/// <param name="sender">������.</param>
		/// <param name="e">������ � �������.</param>
		private void UploadFileCSV(object sender, EventArgs e)
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
					data = handler.ReadCSVData(openFileDialog.FileName);

					// ������ ������
					var result = handler.AnalyzeData(data);
					processedData = result.processedNumbers; // ��������� ��������� � ����
					int replacedCount = result.replacedCount;

					// ����������� � ���������� ������
					MessageBox.Show($"������ ������� ����������! \n" +
						$"�������� �����: {data.Length}, \n" +
						$"������������ �����: {replacedCount}.",
						"�����������", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

		/// <summary>
		/// ����� ������ � ������ ������� �� UID.
		/// </summary>
		/// <param name="sender">������.</param>
		/// <param name="e">������ � �������.</param>
		private void ReadWriteDataUID(object sender, EventArgs e)
		{
			if (processedData == null)
			{
				MessageBox.Show("��������� ���� ���������� �\n" +
					"���������� ��������� �� ����������.", "�����������",
					MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			try
			{
				// ������ ������ �� ��� ��-11
				HandlerSCADA.ReadResponse response = HandlerSCADA.GetDataFromCK11("2023-11-13T09:00:00Z", "2023-11-13T21:00:00Z", HandlerSCADA.ck11Uids);

				// ��������� ������
				//SaveDataToExcel(response);
				MessageBox.Show("������ ������� ��������� � ���������.", "�����", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"������ ������ ������: {ex.Message}", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// ����� ����������� ������ �������������.
		/// </summary>
		/// <param name="sender">�������� �������, ������.</param>
		/// <param name="e">��������� ������� �����.</param>
		private async void DetermineDistributionLaw(object sender, EventArgs e)
		{
			// ��������, ��� ������ ����������
			if (processedData == null || processedData.Length == 0)
			{
				MessageBox.Show("����������� ������. " +
					"��������� ���� CSV ��� �������\n" +
					"��������� ���� ���������� � ���������� ���������.", "�����������",
					MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			// �������� � ����������� ���� � ����������
			ProgressDialog progressDialog = new ProgressDialog();
			progressDialog.Show();

			// ����������� ���������� ����������
			double maxDeviation = 0.1;

			try
			{
				// ������������� ������� ��� ������������ ���������
				var progress = new Progress<int>(percent =>
				{
					progressDialog.UpdateProgress(percent);
				});

				// ����� ������� � ����������� ������
				_calculationResults = await Task.Run(() =>
					DistributionAnalyzer.AnalyzeDistribution(
						processedData.Select(x => (double)x).ToArray(),
						maxDeviation,
						progress
					)
				);

				// �������� ���� ���������
				progressDialog.Close();

				// ����� ����������� ������� (������)
				MessageBox.Show("������ ������������� �������� �������!", "�����������",
					MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				// �������� ���� ��������� ��� ������
				progressDialog.Close();

				// ��������� ������
				MessageBox.Show($"������ ��� ���������� �������: {ex.Message}", "������",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// ����� ���������� ������ � CSV ����.
		/// </summary>
		/// <param name="sender">�������� �������, ������.</param>
		/// <param name="e">��������� ������� �����.</param>
		private void SaveFileCSV(object sender, EventArgs e)
		{
			if (_calculationResults == null || _calculationResults.Count == 0)
			{
				MessageBox.Show("���������� ������� �����������.\n" +
					"��������� ������ �� �����������\n" +
					"������� �������������.", "�����������",
					MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			// ����� ���� ��� ���������� �����
			using (var saveFileDialog = new SaveFileDialog())
			{
				saveFileDialog.Filter = "CSV ����� (*.csv)|*.csv";
				saveFileDialog.DefaultExt = "csv";
				saveFileDialog.FileName = "ClusterAnalysisReport.csv";

				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					try
					{
						// ���������� � CSV
						HandlerCSV.WriteCSVData(_calculationResults, saveFileDialog.FileName);
						MessageBox.Show("����� ������� ��������.");
					}
					catch (Exception ex)
					{
						MessageBox.Show($"������ ����������: {ex.Message}");
					}
				}
			}
		}


		/// <summary>
		/// ����� ��������� ���� ������������� (���������� � ���.).
		/// </summary>
		private void ProcessDiscreteStep()
		{
			// ���������, ��� ���� ��� ���� ������������� �� ������
			if (double.TryParse(guna2TextBox1.Text, out double stepValue))
			{
				// �������� ��������� ������� ���������
				string selectedUnit = guna2ComboBox4.SelectedItem?.ToString();

				if (!string.IsNullOrEmpty(selectedUnit))
				{
					double resultInSeconds = stepValue; // �������� � ��������

					// �������������� � ������� � ����������� �� ��������� �������
					switch (selectedUnit)
					{
						case "�������":
							resultInSeconds = stepValue;
							break;
						case "������":
							resultInSeconds = stepValue * 60;
							break;
						case "���":
							resultInSeconds = stepValue * 3600;
							break;
						case "�����": // �������: ���� ������� ����� = 30 ����
							resultInSeconds = stepValue * 30 * 24 * 3600;
							break;
						default:
							return;
					}

					// ��������� �������� � ��������� ����
					_discreteStepInSeconds = resultInSeconds;

					// ������� ��������� � MessageBox ��� ������������ (�����������)
					//MessageBox.Show($"�������� � ��������: {_discreteStepInSeconds}", "���������",
					//	MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
		}


		///// <summary>
		///// ���������� �������� ������ "����������� ������� �������������".
		///// </summary>
		///// <param name="sender">�������� �������, ������.</param>
		///// <param name="e">��������� ������� �����.</param>
		//private void guna2Button2_Click(object sender, EventArgs e)
		//{
		//	// ��������, ��� ������ ����������
		//	if (processedData == null || processedData.Length == 0)
		//	{
		//		MessageBox.Show("������ ����������� ��� �������.", "������",
		//			MessageBoxButtons.OK, MessageBoxIcon.Error);
		//		return;
		//	}

		//	// ����������� ���������� ����������
		//	double maxDeviation = 0.1;

		//	try
		//	{
		//		var progress = new Progress<int>(percent => Console.WriteLine($"��������: {percent}%"));

		//		// ����� �������
		//		var results = DistributionAnalyzer.AnalyzeDistribution(
		//			processedData.Select(x => (double)x).ToArray(),
		//			maxDeviation,
		//			progress
		//		);

		//		// ����� ����������� ������� (������)
		//		MessageBox.Show("������ ������������� �������� �������!", "�����������",
		//			MessageBoxButtons.OK, MessageBoxIcon.Information);
		//	}
		//	catch (Exception ex)
		//	{
		//		// ��������� ������
		//		MessageBox.Show($"������ ��� ���������� �������: {ex.Message}", "������",
		//			MessageBoxButtons.OK, MessageBoxIcon.Error);
		//	}
		//}
	}
}
