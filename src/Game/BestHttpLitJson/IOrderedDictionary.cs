using System.Collections;

namespace BestHttpLitJson
{
	public interface IOrderedDictionary : IDictionary, ICollection, IEnumerable
	{
		object this[int index] { get; set; }

		new IDictionaryEnumerator GetEnumerator();

		void Insert(int index, object key, object value);

		void RemoveAt(int index);
	}
}
