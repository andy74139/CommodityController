using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GroupSums
{
    public class GroupSums<T>
    {
        private readonly IEnumerable<T> _Products;

        public GroupSums(IEnumerable<T> products)
        {
            _Products = products;
        }

        public IEnumerable<int> Get(int groupSize, string dataName)
        {
            ThrowIfArgumentInvaild(groupSize, dataName);

            return GroupSumsWithValidArguments(groupSize, dataName);
        }

        private IEnumerable<int> GroupSumsWithValidArguments(int groupSize, string columnName)
        {
            var propertyInfo = typeof(T).GetProperty(columnName);
            for (var startIndex = 0; startIndex < _Products.Count(); startIndex += groupSize)
                yield return GroupSum(groupSize, propertyInfo, startIndex);
        }

        private int GroupSum(int groupSize, PropertyInfo propertyInfo, int startIndex)
        {
            var thisGroupSize = Math.Min(groupSize, _Products.Count() - startIndex);
            var thisGroup = _Products.Skip(startIndex).Take(thisGroupSize);
            return thisGroup.Select(p => (int) propertyInfo.GetValue(p)).Sum();
        }

        private void ThrowIfArgumentInvaild(int groupSize, string dataName)
        {
            if (dataName == null)
                throw new ArgumentException("Invalid dataName, dataName cannot be null.");

            var propertyInfo = typeof(T).GetProperty(dataName);
            if (propertyInfo == null)
                throw new ArgumentException(string.Format("Invalid dataName, cannot find specified dataName '{0}'.", dataName));
            if (groupSize <= 0)
                throw new ArgumentException("Invalid amountPerGroup, it must be a positive integer.");
        }
    }
}
