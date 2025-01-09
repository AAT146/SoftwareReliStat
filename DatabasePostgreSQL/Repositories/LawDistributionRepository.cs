using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabasePostgreSQL.Repositories
{
	public class LawDistributionRepository
	{
		private DatabaseDbContext _databaseDbContext;

		public LawDistributionRepository(DatabaseDbContext databaseDbContext)
		{
			_databaseDbContext = databaseDbContext;
		}


	}
}
