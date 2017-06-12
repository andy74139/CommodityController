using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GroupSums
{
    public class GroupSums<P>
    {
        private readonly List<P> _Products;

        public GroupSums(IEnumerable<P> products)
        {
            _Products = products.ToList();
        }

        public IEnumerable<int> Get(int amountPerGroup, string dataName)
        {
            ThrowIfArgumentInvaild(amountPerGroup, dataName);

            return GroupSumsWithValidArguments(amountPerGroup, dataName);
        }

        private IEnumerable<int> GroupSumsWithValidArguments(int amountPerGroup, string dataName)
        {
            var propertyInfo = typeof(Product).GetProperty(dataName);
            var result = new List<int>();
            for (var startIndex = 0; startIndex < _Products.Count; startIndex += amountPerGroup)
                result.Add(GroupSum(amountPerGroup, propertyInfo, startIndex));

            return result;
        }

        private int GroupSum(int amountPerGroup, PropertyInfo propertyInfo, int startIndex)
        {
            var sum = 0;
            for (var i = 0; i < Math.Min(amountPerGroup, _Products.Count - startIndex); i++)
                sum += (int) propertyInfo.GetValue(_Products[startIndex + i]);
            return sum;
        }

        private void ThrowIfArgumentInvaild(int amountPerGroup, string dataName)
        {
            if (dataName == null)
                throw new ArgumentException("Invalid dataName, dataName cannot be null.");

            var propertyInfo = typeof(Product).GetProperty(dataName);
            if (propertyInfo == null)
                throw new ArgumentException(string.Format("Invalid dataName, cannot find specified dataName '{0}'.", dataName));
            if (amountPerGroup <= 0)
                throw new ArgumentException("Invalid amountPerGroup, it must be a positive integer.");
        }
    }
}
