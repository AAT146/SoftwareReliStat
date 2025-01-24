using Accord.Statistics.Distributions.Univariate;
using Accord.Math;
using Accord.MachineLearning;
using MathNet.Numerics;
using Accord.Math.Optimization;
using MathNet.Numerics.Optimization;
using MathNet.Numerics.LinearAlgebra;
using System.ComponentModel;

namespace ClassLibrary
{
	/// <summary>
	/// Класс для определения закона распределения.
	/// </summary>
	public class DistributionAnalyzer
	{
		/// <summary>
		/// Метод локтя.
		/// </summary>
		/// <param name="data">Массив значений.</param>
		/// <param name="maxK">Максимальное число кластеров.</param>
		/// <returns>Оптимальное число кластеров.</returns>
		public static int FindOptimalNumberCluster(double[] data, int maxK = 10)
		{
			// Начальное число кластеров
			int k = 2;
			bool improvement = true;

			var kmeans = new KMeans(k);
			var clusters = kmeans.Learn(data.ToJagged());
			int[] labels = clusters.Decide(data.ToJagged());
			double[][] centroids = clusters.Centroids;

			while (improvement && k < maxK)
			{
				improvement = false;
				var newCentroids = new List<double[]>();
				var newLabels = new List<int>();
				int currentCluster = 0;

				for (int i = 0; i < centroids.Length; i++)
				{
					var clusterData = data.Where((_, index) => labels[index] == i).ToArray();

					if (clusterData.Length < 2)
					{
						newCentroids.Add(centroids[i]);
						newLabels.AddRange(Enumerable.Repeat(currentCluster++, clusterData.Length));
						continue;
					}

					var subKMeans = new KMeans(2);
					var subClusters = subKMeans.Learn(clusterData.ToJagged());
					var subLabels = subClusters.Decide(clusterData.ToJagged());
					double[][] subCentroids = subClusters.Centroids;

					double bicCurrent = CalculateBIC(clusterData, new[] { centroids[i] }, new int[clusterData.Length]);
					double bicSplit = CalculateBIC(clusterData, subCentroids, subLabels);

					if (bicSplit > bicCurrent)
					{
						newCentroids.AddRange(subCentroids);
						newLabels.AddRange(subLabels.Select(l => l + currentCluster));
						currentCluster += 2;
						improvement = true;
					}
					else
					{
						newCentroids.Add(centroids[i]);
						newLabels.AddRange(Enumerable.Repeat(currentCluster++, clusterData.Length));
					}
				}

				centroids = newCentroids.ToArray();
				labels = newLabels.ToArray();
				k = centroids.Length;
			}

			return k;
		}

		/// <summary>
		/// Вычисляет значение критерия Байеса (BIC).
		/// </summary>
		/// <param name="data">Массив данных.</param>
		/// <param name="centroids">Центроиды кластеров.</param>
		/// <param name="labels">Метки кластеров.</param>
		/// <returns>Значение BIC.</returns>
		private static double CalculateBIC(double[] data, double[][] centroids, int[] labels)
		{
			int nClusters = centroids.Length;
			int nData = data.Length;

			double logLikelihood = 0.0;
			int totalParameters = 0;

			for (int i = 0; i < nClusters; i++)
			{
				var clusterData = data.Where((_, index) => labels[index] == i).ToArray();

				if (clusterData.Length == 0)
					continue;

				double variance = clusterData.Average(x => Math.Pow(x - centroids[i][0], 2));
				variance = Math.Max(variance, 1e-6); // Избегаем деления на 0

				logLikelihood += clusterData.Length * Math.Log(1.0 / Math.Sqrt(2 * Math.PI * variance))
								 - clusterData.Sum(x => Math.Pow(x - centroids[i][0], 2)) / (2 * variance);

				totalParameters += 1 + 1;
			}

			return logLikelihood - 0.5 * totalParameters * Math.Log(nData);
		}

		/// <summary>
		/// Метод кластеризации данных с использованием X-Means.
		/// </summary>
		/// <param name="data">Массив значений.</param>
		/// <param name="maxK">Максимальное число кластеров.</param>
		/// <returns>Массивы кластеров; центроиды.</returns>
		public static (List<double[]> Clusters, double[][] Centroids) ClusterData(double[] data, int maxK = 20)
		{
			// Определяем оптимальное число кластеров с помощью X-Means
			int optimalK = FindOptimalNumberCluster(data, maxK);

			// Кластеризация с использованием найденного числа кластеров
			var kmeans = new KMeans(optimalK);
			var clusters = kmeans.Learn(data.ToJagged());
			var labels = clusters.Decide(data.ToJagged());

			var resultClusters = new List<double[]>();
			for (int i = 0; i < optimalK; i++)
			{
				var clusterData = data.Where((_, index) => labels[index] == i).ToArray();
				resultClusters.Add(clusterData);
			}

			return (resultClusters, clusters.Centroids);
		}

		/// <summary>
		/// Метод определения лучшего распределения для кластера.
		/// </summary>
		/// <param name="cluster">Массив кластеров.</param>
		/// <returns>Лучшие параметры.</returns>
		public static (string BestDistribution, object BestParams, double BestStat) FitDistribution(double[] cluster)
		{
			var distributions = new Dictionary<string, Func<double[], object>>()
			{
				{ "Normal", data => NormalDistribution.Estimate(data) },
				{ "Exponential", data => new ExponentialDistribution(data.Average()) },
				{ "Uniform", data => new UniformContinuousDistribution(data.Min(), data.Max()) }
			};

			string bestDist = null;
			object bestParams = null;
			double bestStat = double.MaxValue;

			foreach (var dist in distributions)
			{
				var distName = dist.Key;
				var distObj = dist.Value(cluster);
				var ksStat = CalculateKSTest(cluster, distObj);

				if (ksStat < bestStat)
				{
					bestStat = ksStat;
					bestDist = distName;
					bestParams = distObj;
				}
			}

			return (bestDist, bestParams, bestStat);
		}

		/// <summary>
		/// Тест Колмогорова-Смирнова для оценки соответствия распределения.
		/// </summary>
		/// <param name="data">Массив значений.</param>
		/// <param name="distribution">Закон распределения.</param>
		/// <returns>Величина отклонения.</returns>
		private static double CalculateKSTest(double[] data, object distribution)
		{
			if (distribution is NormalDistribution normal)
			{
				return data.Select(x => Math.Abs(normal.DistributionFunction(x) - EmpiricalCDF(data, x)))
						   .Max();
			}
			else if (distribution is ExponentialDistribution exponential)
			{
				return data.Select(x => Math.Abs(exponential.DistributionFunction(x) - EmpiricalCDF(data, x)))
						   .Max();
			}
			else if (distribution is UniformContinuousDistribution uniform)
			{
				return data.Select(x => Math.Abs(uniform.DistributionFunction(x) - EmpiricalCDF(data, x)))
						   .Max();
			}

			return double.MaxValue;
		}

		/// <summary>
		/// Эмпирическая функция распределения (CDF).
		/// </summary>
		/// <param name="data">Массив данных.</param>
		/// <param name="x">Значение для расчета CDF.</param>
		/// <returns>Значение CDF.</returns>
		private static double EmpiricalCDF(double[] data, double x)
		{
			return data.Count(v => v <= x) / (double)data.Length;
		}

		

		/// <summary>
		/// Метод оптимизации весов распределений.
		/// </summary>
		/// <param name="data">Массив данных для анализа.</param>
		/// <param name="clusters">Список кластеров.</param>
		/// <param name="distributions">Список распределений (параметры и тип).</param>
		/// <returns>Оптимальные веса и минимальное отклонение (KS-статистика).</returns>
		/// <exception cref="InvalidOperationException">Если оптимизация не удалась.</exception>
		public static (double[] Weights, double Deviation) OptimizeWeights(
			double[] data,
			List<double[]> clusters,
			List<object> distributions)
		{
			int nClusters = clusters.Count;

			// Определение функции ошибки
			Func<double[], double> errorFunction = weights =>
			{
				Func<double, double> combinedCDF = x =>
				{
					double sum = 0.0;
					for (int i = 0; i < nClusters; i++)
					{
						if (distributions[i] is NormalDistribution normal)
						{
							sum += weights[i] * normal.DistributionFunction(x);
						}
						else if (distributions[i] is ExponentialDistribution exponential)
						{
							sum += weights[i] * exponential.DistributionFunction(x);
						}
						else if (distributions[i] is UniformContinuousDistribution uniform)
						{
							sum += weights[i] * uniform.DistributionFunction(x);
						}
					}
					return sum;
				};

				// Максимальная ошибка между эмпирическим CDF и комбинированным CDF
				return data.Select(x => Math.Abs(combinedCDF(x) - EmpiricalCDF(data, x))).Max();
			};

			// Начальные веса (равномерное распределение)
			double[] initialWeights = Enumerable.Repeat(1.0 / nClusters, nClusters).ToArray();

			Func<double[], double> errorWithPenalty = weights =>
			{
				double error = errorFunction(weights);

				// Штраф за веса вне диапазона [0, 1]
				double penalty = weights.Where(w => w < 0 || w > 1).Sum(w => Math.Abs(w) * 100);

				// Штраф за сумму весов, не равную 1
				penalty += Math.Abs(weights.Sum() - 1) * 100;

				return error + penalty;
			};

			// Оптимизация с использованием Accord.NET (Cobyla)
			var optimizer = new Cobyla(new NonlinearObjectiveFunction(nClusters, errorWithPenalty));

			bool success = optimizer.Minimize(initialWeights);

			if (success)
			{
				return (optimizer.Solution, optimizer.Value);
			}
			else
			{
				throw new InvalidOperationException("Оптимизация не удалась.");
			}
		}

		/// <summary>
		/// Метод определения закона распределения.
		/// </summary>
		/// <param name="data">Массив значений.</param>
		/// <param name="maxDeviation">Величина максимального отклонения.</param>
		/// <returns>Лист результатов.</returns>
		/// <exception cref="Exception">Исключение, возникающее при не достижении 
		/// величины maxDeviation.</exception>
		public static List<ClusterAnalysisResult> AnalyzeDistribution(
			double[] data, 
			double maxDeviation, 
			IProgress<int> progress)
		{
			// Шаг прогресса
			int totalSteps = 6; // Этапы анализа
			int currentStep = 0;

			// Определение оптимального числа кластеров
			//int optimalK = FindOptimalNumberCluster(data);
			progress?.Report((++currentStep * 100) / totalSteps);

			// Кластеризация данных
			var (clusters, centroids) = ClusterData(data, 8);
			progress?.Report((++currentStep * 100) / totalSteps);

			var results = new List<ClusterAnalysisResult>();
			var distributions = new List<object>();
			var stats = new List<double>();

			// Анализ кластеров
			foreach (var cluster in clusters)
			{
				var (bestDist, bestParams, bestStat) = FitDistribution(cluster);
				distributions.Add(bestParams);
				stats.Add(bestStat);
				progress?.Report((++currentStep * 100) / (totalSteps + clusters.Count));
			}

			var (weights, deviation) = OptimizeWeights(data, clusters, distributions);
			progress?.Report((++currentStep * 100) / totalSteps);

			if (deviation > maxDeviation)
			{
				throw new Exception($"Не удалось достичь отклонения < {maxDeviation}. " +
					$"Текущее отклонение: {deviation}");
			}

			// Формирование результатов
			for (int i = 0; i < clusters.Count; i++)
			{
				string paramsDescription = distributions[i] switch
				{
					NormalDistribution normal => $"µ={Math.Round(normal.Mean, 2)}, " +
					$"σ={Math.Round(normal.StandardDeviation, 2)}",
					ExponentialDistribution exponential => $"λ={Math.Round(exponential.Rate, 2)}",
					UniformContinuousDistribution uniform => $"Min={Math.Round(uniform.Minimum, 2)}, " +
					$"Max={Math.Round(uniform.Maximum, 2)}",
					_ => "Неизвестно"
				};

				results.Add(new ClusterAnalysisResult
				{
					ClusterNumber = i + 1,
					//Interval = $"[{clusters[i].Min()}, {clusters[i].Max()}]",
					Weight = Math.Round(weights[i], 2),
					Distribution = DistributionNames.TryGetValue(distributions[i].
					GetType().Name, out var name) ? name : "Неизвестно",
					Parameters = paramsDescription,
					Deviation = Math.Round(stats[i], 2)
				});

				progress?.Report((++currentStep * 100) / totalSteps);
			}

			// Финальное уведомление о завершении
			progress?.Report(100);

			return results;
		}

		private static Dictionary<string, string> DistributionNames = new Dictionary<string, string>
		{
			{ nameof(NormalDistribution), "Нормальный закон" },
			{ nameof(ExponentialDistribution), "Экспоненциальный закон" },
			{ nameof(UniformContinuousDistribution), "Равномерное распределение" }
		};

		// DTO для представления результатов анализа
		public class ClusterAnalysisResult
		{
			public int ClusterNumber { get; set; }

			//public string Interval { get; set; }

			public double Weight { get; set; }

			public string Distribution { get; set; }

			public string Parameters { get; set; }

			public double Deviation { get; set; }
		}
	}
}
