using Accord.Statistics.Distributions.Univariate;
using Accord.Math;
using Accord.MachineLearning;
using Accord.Math.Optimization;

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
			var distortions = new List<double>();

			for (int k = 1; k <= maxK; k++)
			{
				var kmeans = new KMeans(k);
				var clusters = kmeans.Learn(data.ToJagged());
				var distortion = clusters.Decide(data.ToJagged())
					.Select((cluster, i) => Math.Pow(data[i] - clusters.Centroids[cluster][0], 2))
					.Sum();

				distortions.Add(distortion);
			}

			int optimalK = 1;
			double maxDiff = 0;
			for (int i = 1; i < distortions.Count - 1; i++)
			{
				double diff = distortions[i - 1] - distortions[i];
				if (diff > maxDiff)
				{
					maxDiff = diff;
					optimalK = i + 1;
				}
			}

			return optimalK;
		}

		/// <summary>
		/// Метод кластеризации данных методом K-Means.
		/// </summary>
		/// <param name="data">Массив значений.</param>
		/// <param name="nClusters">Число кластеров.</param>
		/// <returns>Массивы кластеров; центроиды.</returns>
		public static (List<double[]> Clusters, double[][] Centroids) ClusterData(double[] data, int nClusters)
		{
			var kmeans = new KMeans(nClusters);
			var clusters = kmeans.Learn(data.ToJagged());
			var labels = clusters.Decide(data.ToJagged());

			var resultClusters = new List<double[]>();
			for (int i = 0; i < nClusters; i++)
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
		/// Метод для определения эмпирической CDF.
		/// </summary>
		/// <param name="data"></param>
		/// <param name="x"></param>
		/// <returns></returns>
		private static double EmpiricalCDF(double[] data, double x)
		{
			return data.Count(v => v <= x) / (double)data.Length;
		}

		/// <summary>
		/// Метод оптимизации весов распределений.
		/// </summary>
		/// <param name="data">Массив значений.</param>
		/// <param name="clusters">Листы кластеров.</param>
		/// <param name="distributions">Лист распределений.</param>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException"></exception>
		public static (double[] Weights, double Deviation) OptimizeWeights(double[] data, List<double[]> clusters, List<object> distributions)
		{
			int nClusters = clusters.Count;

			// Функция ошибки
			Func<double[], double> errorFunction = weights =>
			{
				Func<double, double> combinedCDF = x =>
					weights.Zip(distributions, (w, dist) =>
					{
						if (dist is NormalDistribution normal)
							return w * normal.DistributionFunction(x);
						else if (dist is ExponentialDistribution exponential)
							return w * exponential.DistributionFunction(x);
						else if (dist is UniformContinuousDistribution uniform)
							return w * uniform.DistributionFunction(x);
						return 0.0;
					}).Sum();

				return data.Select(x => Math.Abs(combinedCDF(x) - EmpiricalCDF(data, x))).Max();
			};

			// Начальные веса (равномерные)
			var weights = Enumerable.Repeat(1.0 / nClusters, nClusters).ToArray();

			// Оптимизация с использованием ConjugateGradient
			var optimizer = new ConjugateGradient(nClusters)
			{
				Function = errorFunction,
				Tolerance = 1e-6,
				MaxIterations = 1000
			};

			bool success = optimizer.Minimize(weights);

			if (!success)
				throw new InvalidOperationException("Оптимизация не удалась.");

			return (weights, optimizer.Value);
		}

		/// <summary>
		/// Метод определения закона распределения.
		/// </summary>
		/// <param name="data">Массив значений.</param>
		/// <param name="maxDeviation">Величина максимального отклонения.</param>
		/// <returns>Лист результатов.</returns>
		/// <exception cref="Exception">Исключение, возникающее при не достижении 
		/// величины maxDeviation.</exception>
		public static List<ClusterAnalysisResult> AnalyzeDistribution(double[] data, double maxDeviation, IProgress<int> progress)
		{
			// Шаг прогресса
			int totalSteps = 6; // Этапы анализа
			int currentStep = 0;

			// Определение оптимального числа кластеров
			progress?.Report((++currentStep * 100) / totalSteps);
			int optimalK = FindOptimalNumberCluster(data);

			// Кластеризация данных
			progress?.Report((++currentStep * 100) / totalSteps);
			var (clusters, centroids) = ClusterData(data, optimalK);

			var results = new List<ClusterAnalysisResult>();
			var distributions = new List<object>();
			var stats = new List<double>();

			// Анализ кластеров
			foreach (var cluster in clusters)
			{
				progress?.Report((++currentStep * 100) / (totalSteps + clusters.Count)); // Прогресс внутри цикла
				var (bestDist, bestParams, bestStat) = FitDistribution(cluster);
				distributions.Add(bestParams);
				stats.Add(bestStat);
			}

			// Оптимизация весов
			progress?.Report((++currentStep * 100) / totalSteps);
			var (weights, deviation) = OptimizeWeights(data, clusters, distributions);

			if (deviation > maxDeviation)
			{
				throw new Exception($"Не удалось достичь отклонения < {maxDeviation}. " +
					$"Текущее отклонение: {deviation}");
			}

			// Формирование результатов
			progress?.Report((++currentStep * 100) / totalSteps);
			for (int i = 0; i < clusters.Count; i++)
			{
				string paramsDescription = distributions[i] switch
				{
					NormalDistribution normal => $"µ={normal.Mean}, σ={normal.StandardDeviation}",
					ExponentialDistribution exponential => $"λ={exponential.Rate}",
					UniformContinuousDistribution uniform => $"Min={uniform.Minimum}, Max={uniform.Maximum}",
					_ => "Неизвестно"
				};

				results.Add(new ClusterAnalysisResult
				{
					ClusterNumber = i + 1,
					Interval = $"[{clusters[i].Min()}, {clusters[i].Max()}]",
					Weight = weights[i],
					Distribution = distributions[i].GetType().Name,
					Parameters = paramsDescription,
					Deviation = stats[i]
				});
			}

			// Финальное уведомление о завершении
			progress?.Report(100);

			return results;
		}

		// DTO для представления результатов анализа
		public class ClusterAnalysisResult
		{
			public int ClusterNumber { get; set; }
			public string Interval { get; set; }
			public double Weight { get; set; }
			public string Distribution { get; set; }
			public string Parameters { get; set; }
			public double Deviation { get; set; }
		}





		///// <summary>
		///// Метод определения закона распределения.
		///// </summary>
		///// <param name="data">Массив значений.</param>
		///// <param name="maxDeviation">Величина максимального отклонения.</param>
		///// <returns>Лист результатов.</returns>
		///// <exception cref="Exception">Исключение, возникающее при не достижении 
		///// величины maxDeviation.</exception>
		//public static List<ClusterAnalysisResult> AnalyzeDistribution(double[] data, double maxDeviation = 0.1)
		//{
		//	// Определение оптимального числа кластеров
		//	int optimalK = FindOptimalNumberCluster(data);

		//	var (clusters, centroids) = ClusterData(data, optimalK);

		//	var results = new List<ClusterAnalysisResult>();
		//	var distributions = new List<object>();
		//	var stats = new List<double>();

		//	// Анализ кластеров
		//	foreach (var cluster in clusters)
		//	{
		//		var (bestDist, bestParams, bestStat) = FitDistribution(cluster);
		//		distributions.Add(bestParams);
		//		stats.Add(bestStat);
		//	}

		//	// Оптимизация весов
		//	var (weights, deviation) = OptimizeWeights(data, clusters, distributions);

		//	if (deviation > maxDeviation)
		//	{
		//		throw new Exception($"Не удалось достичь отклонения < {maxDeviation}. " +
		//			$"Текущее отклонение: {deviation}");
		//	}

		//	// Формирование результатов
		//	for (int i = 0; i < clusters.Count; i++)
		//	{
		//		string paramsDescription = distributions[i] switch
		//		{
		//			NormalDistribution normal => $"µ={normal.Mean}, σ={normal.StandardDeviation}",
		//			ExponentialDistribution exponential => $"λ={exponential.Rate}",
		//			UniformContinuousDistribution uniform => $"Min={uniform.Minimum}, Max={uniform.Maximum}",
		//			_ => "Неизвестно"
		//		};

		//		results.Add(new ClusterAnalysisResult
		//		{
		//			ClusterNumber = i + 1,
		//			Interval = $"[{clusters[i].Min()}, {clusters[i].Max()}]",
		//			Weight = weights[i],
		//			Distribution = distributions[i].GetType().Name,
		//			Parameters = paramsDescription,
		//			Deviation = stats[i]
		//		});
		//	}

		//	return results;
		//}
	}
}
