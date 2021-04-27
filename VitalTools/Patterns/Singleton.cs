using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitalTools.Patterns
{
	public class Singleton<T> where T : new()
	{
		private static readonly T instance;
		public static T Instance
		{
			get { return instance; }
		}

		#region Constructor
		
		static Singleton()
		{
			instance = new T();
		} 

		#endregion
	}
}
