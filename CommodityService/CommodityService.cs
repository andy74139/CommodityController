using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CommodityService
{
    public class CommodityService
    {
        private readonly List<Commodity> _Commodities;

        public CommodityService(IEnumerable<Commodity> commodities)
        {
            _Commodities = commodities.ToList();
        }

        public IEnumerable<int> GroupValueSums(int amountPerGroup, string dataName)
        {
            ThrowIfArgumentInvaild(amountPerGroup, dataName);

            return GroupValueSumsWithValidArguments(amountPerGroup, dataName);
        }

        private IEnumerable<int> GroupValueSumsWithValidArguments(int amountPerGroup, string dataName)
        {
            var fieldInfo = typeof(Commodity).GetField(dataName);
            var result = new List<int>();
            for (var startIndex = 0; startIndex < _Commodities.Count; startIndex += amountPerGroup)
                result.Add(GetGroupValueSum(amountPerGroup, fieldInfo, startIndex));

            return result;
        }

        private int GetGroupValueSum(int amountPerGroup, FieldInfo fieldInfo, int startIndex)
        {
            var sum = 0;
            var commodityAmount = Math.Min(amountPerGroup, _Commodities.Count - startIndex);
            for (var i = 0; i < commodityAmount; i++)
                sum += (int) fieldInfo.GetValue(_Commodities[startIndex + i]);
            return sum;
        }

        private void ThrowIfArgumentInvaild(int amountPerGroup, string dataName)
        {
            if (dataName == null)
                throw new ArgumentException("Invalid dataName, dataName cannot be null.");

            var fieldInfo = typeof(Commodity).GetField(dataName);
            if (fieldInfo == null)
                throw new ArgumentException(string.Format("Invalid dataName, cannot find specified dataName '{0}'.", dataName));
            if (amountPerGroup <= 0)
                throw new ArgumentException("Invalid amountPerGroup, it must be a positive integer.");
        }
    }
}
